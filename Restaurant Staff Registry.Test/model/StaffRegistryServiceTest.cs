using Retaurant_Staff_Registry.constant;
using Retaurant_Staff_Registry.events;
using Retaurant_Staff_Registry.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Restaurant_Staff_Registry.model.StaffTest;

namespace Restaurant_Staff_Registry.model;

public class StaffTest
{
    public class Fixture
    {
        public StaffRegistryService StaffRegistryService { get; private set; }
        public Mock<IStaffRepository> MockRepository { get; private set; }

        public Fixture()
        {
            MockRepository = new();
            StaffRegistryService = new(MockRepository.Object);
        }
    }

    public class AddStaffSuccessCase : IClassFixture<Fixture>, IDisposable
    {
        private readonly Fixture f;
        private readonly List<EventHandler<StaffRegistryEvent>> eventHandlers = [];

        public AddStaffSuccessCase(Fixture fixture)
        {
            f = fixture;
        }

        public static IEnumerable<object[]> TestData()
        {
            yield return new object[]
            {
                new List<(string fname, string lname, double salary)>
                {
                    ("Eric", "Larsson", 2366),
                    ("Lisa", "Ericsson", 336),
                    ("Anna", "Jonsson", 4366)
                }
            };
        }

        [Theory(DisplayName = "Call add staff to repository if valid staff data")]
        [MemberData(nameof(TestData))]
        public void T1(List<(string fname, string lname, double salary)> staffDataItems)
        {
            foreach ((string fname, string lname, double salary) in staffDataItems)
            {
                f.StaffRegistryService.AddStaff((fname, lname, salary));
            }

            f.MockRepository.Verify(repository =>
                repository.AddStaff(It.IsAny<Staff>()),
                Times.Exactly(staffDataItems.Count)
            );
        }

        [Theory(DisplayName = "Raise ok event if valid staff data")]
        [MemberData(nameof(TestData))]
        public void T2(List<(string fname, string lname, double salary)> staffDataItems)
        {
            bool isRaised = false;
            RepositoryResult result = RepositoryResult.ADD_STAFF_FAILURE;

            foreach ((string fname, string lname, double salary) in staffDataItems)
            {
                EventHandler<StaffRegistryEvent> handler = (sender, e) =>
                {
                    isRaised = true;
                    result = e.Status;
                };

                eventHandlers.Add(handler);
                f.StaffRegistryService.StaffRegistryEventHandler += handler;
                f.StaffRegistryService.AddStaff((fname, lname, salary));
                Assert.True(isRaised);
                Assert.Equal(RepositoryResult.ADD_STAFF_OK, result);
            }
        }

        public void Dispose()
        {
            foreach (EventHandler<StaffRegistryEvent> handler in eventHandlers)
            {
                f.StaffRegistryService.StaffRegistryEventHandler -= handler;
            }
            f.MockRepository.Reset();
        }
    }

    public class AddStaffFailureCase(Fixture f) : IClassFixture<Fixture>, IDisposable
    {
        private readonly Fixture f = f;
        private readonly List<EventHandler<StaffRegistryEvent>> eventHandlers = [];

        public static IEnumerable<object[]> TestData = [
            [
                new List<(string fname, string lname, double salary)> {
                    ("Eric", "", 2366),
                    ("", "Ericsson", 336),
                    ("Anna", "Jonsson", -4366)
                }
            ],
        ];

        [Theory(DisplayName = "Not call add staff to repository if invalid staff data")]
        [MemberData(nameof(TestData))]
        public void T1(List<(string fname, string lname, double salary)> staffDataItems)
        {
            foreach ((string fname, string lname, double salary) in staffDataItems)
            {
                f.StaffRegistryService.AddStaff((fname, lname, salary));
            }

            f.MockRepository.Verify(repository =>
                repository.AddStaff(It.IsAny<Staff>()),
                Times.Never()
            );
        }

        [Theory(DisplayName = "Raise failure event if invalid staff data")]
        [MemberData(nameof(TestData))]
        public void T2(List<(string fname, string lname, double salary)> staffDataItems)
        {
            bool isRaised = false;
            RepositoryResult result = RepositoryResult.ADD_STAFF_OK;

            foreach ((string fname, string lname, double salary) in staffDataItems)
            {
                EventHandler<StaffRegistryEvent> handler = (sender, e) =>
                {
                    isRaised = true;
                    result = e.Status;
                };

                eventHandlers.Add(handler);
                f.StaffRegistryService.StaffRegistryEventHandler += handler;
                f.StaffRegistryService.AddStaff((fname, lname, salary));
                Assert.True(isRaised);
                Assert.Equal(RepositoryResult.ADD_STAFF_FAILURE, result);
            }
        }

        public void Dispose()
        {
            foreach (EventHandler<StaffRegistryEvent> handler in eventHandlers)
            {
                f.StaffRegistryService.StaffRegistryEventHandler -= handler;
            }
            f.MockRepository.Reset();
        }
    }
}
