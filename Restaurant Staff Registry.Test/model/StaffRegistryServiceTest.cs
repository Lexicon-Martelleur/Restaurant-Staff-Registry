using Retaurant_Staff_Registry.constant;
using Retaurant_Staff_Registry.events;
using Retaurant_Staff_Registry.model;

namespace Restaurant_Staff_Registry.model;

public class StaffTest
{
    public class Fixture
    {
        public StaffRegistryService StaffRegistryService { get; private init; }
        public Mock<IStaffRepository> MockRepository { get; private init; }

        public Fixture()
        {
            MockRepository = new();
            StaffRegistryService = new(MockRepository.Object);
        }
    }

    public class AddStaffSuccessCase(Fixture fixture) : IClassFixture<Fixture>, IDisposable
    {
        private readonly Fixture _f = fixture;
        private readonly List<EventHandler<StaffRegistryEventArgs>> eventHandlers = [];
        public static IEnumerable<object[]> TestData = [
            [
                new List<(string fname, string lname, double salary)> {
                    ("Eric", "Larson", 2366),
                    ("Lisa", "Erikson", 336),
                    ("Anna", "Jonson", 4366)
                }
            ],
        ];

        [Theory(DisplayName = "Call add staff to repository if valid staff data")]
        [MemberData(nameof(TestData))]
        public void T1(List<(string fname, string lname, double salary)> staffDataItems)
        {
            foreach ((string fname, string lname, double salary) in staffDataItems)
            {
                _f.StaffRegistryService.AddStaff((fname, lname, salary));
            }

            _f.MockRepository.Verify(repository =>
                repository.AddStaff(It.IsAny<Staff>()),
                Times.Exactly(staffDataItems.Count)
            );
        }

        [Theory(DisplayName = "Raise OK event if valid staff data")]
        [MemberData(nameof(TestData))]
        public void T2(List<(string fname, string lname, double salary)> staffDataItems)
        {
            bool isRaised = false;
            RepositoryResult result = RepositoryResult.ADD_STAFF_FAILURE;

            foreach ((string fname, string lname, double salary) in staffDataItems)
            {
                EventHandler<StaffRegistryEventArgs> handler = (sender, e) =>
                {
                    isRaised = true;
                    result = e.Status;
                };

                eventHandlers.Add(handler);
                _f.StaffRegistryService.StaffRegistryEventHandler += handler;
                _f.StaffRegistryService.AddStaff((fname, lname, salary));
                Assert.True(isRaised);
                Assert.Equal(RepositoryResult.ADD_STAFF_OK, result);
            }
        }

        public void Dispose()
        {
            foreach (EventHandler<StaffRegistryEventArgs> handler in eventHandlers)
            {
                _f.StaffRegistryService.StaffRegistryEventHandler -= handler;
            }
            _f.MockRepository.Reset();
        }
    }

    public class AddStaffFailureCase(Fixture f) : IClassFixture<Fixture>, IDisposable
    {
        private readonly Fixture _f = f;
        private readonly List<EventHandler<StaffRegistryEventArgs>> eventHandlers = [];

        public static IEnumerable<object[]> TestData = [
            [
                new List<(string fname, string lname, double salary)> {
                    ("Eric", "", 2366),
                    ("", "Erikson", 336),
                    ("Anna", "Jonson", -4366)
                }
            ],
        ];

        [Theory(DisplayName = "Not call add staff to repository if invalid staff data")]
        [MemberData(nameof(TestData))]
        public void T1(List<(string fname, string lname, double salary)> staffDataItems)
        {
            foreach ((string fname, string lname, double salary) in staffDataItems)
            {
                _f.StaffRegistryService.AddStaff((fname, lname, salary));
            }

            _f.MockRepository.Verify(repository =>
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
                EventHandler<StaffRegistryEventArgs> handler = (sender, e) =>
                {
                    isRaised = true;
                    result = e.Status;
                };

                eventHandlers.Add(handler);
                _f.StaffRegistryService.StaffRegistryEventHandler += handler;
                _f.StaffRegistryService.AddStaff((fname, lname, salary));
                Assert.True(isRaised);
                Assert.Equal(RepositoryResult.ADD_STAFF_FAILURE, result);
            }
        }

        public void Dispose()
        {
            foreach (EventHandler<StaffRegistryEventArgs> handler in eventHandlers)
            {
                _f.StaffRegistryService.StaffRegistryEventHandler -= handler;
            }
            _f.MockRepository.Reset();
        }
    }
}
