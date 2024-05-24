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
                new List<StaffVO> {
                    new("Eric", "Larson", 2366, "1999-01-01"),
                    new("Lisa", "Erikson", 336, "1999-01-01"),
                    new("Anna", "Jonson", 4366, "1999-01-01")
                }
            ],
        ];

        [Theory(DisplayName = "Call add staff to repository if valid staff data")]
        [MemberData(nameof(TestData))]
        public void T1(List<StaffVO> staffDataItems)
        {
            foreach (StaffVO staffData in staffDataItems)
            {
                _f.StaffRegistryService.AddStaff(staffData);
            }

            _f.MockRepository.Verify(repository =>
                repository.AddStaff(It.IsAny<StaffEntity>()),
                Times.Exactly(staffDataItems.Count)
            );
        }

        [Theory(DisplayName = "Raise OK event if valid staff data")]
        [MemberData(nameof(TestData))]
        public void T2(List<StaffVO> staffDataItems)
        {
            bool isRaised = false;
            RepositoryResult result = RepositoryResult.ADD_STAFF_FAILURE;

            foreach (StaffVO staffData in staffDataItems)
            {
                EventHandler<StaffRegistryEventArgs> handler = (sender, e) =>
                {
                    isRaised = true;
                    result = e.Status;
                };

                eventHandlers.Add(handler);
                _f.StaffRegistryService.StaffRegistryEventHandler += handler;
                _f.StaffRegistryService.AddStaff(staffData);
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
                new List<StaffVO> {
                    new("Eric", "", 2366, "1999-01-01"),
                    new("", "Erikson", 336, "1999-01-01"),
                    new("Anna", "Jonson", -4366, "1999-01-01")
                }
            ],
        ];

        [Theory(DisplayName = "Not call add staff to repository if invalid staff data")]
        [MemberData(nameof(TestData))]
        public void T1(List<StaffVO> staffDataItems)
        {
            foreach (StaffVO staffData in staffDataItems)
            {
                _f.StaffRegistryService.AddStaff(staffData);
            }

            _f.MockRepository.Verify(repository =>
                repository.AddStaff(It.IsAny<StaffEntity>()),
                Times.Never()
            );
        }

        [Theory(DisplayName = "Raise failure event if invalid staff data")]
        [MemberData(nameof(TestData))]
        public void T2(List<StaffVO> staffDataItems)
        {
            bool isRaised = false;
            RepositoryResult result = RepositoryResult.ADD_STAFF_OK;

            foreach (StaffVO staffData in staffDataItems)
            {
                EventHandler<StaffRegistryEventArgs> handler = (sender, e) =>
                {
                    isRaised = true;
                    result = e.Status;
                };

                eventHandlers.Add(handler);
                _f.StaffRegistryService.StaffRegistryEventHandler += handler;
                _f.StaffRegistryService.AddStaff(staffData);
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
