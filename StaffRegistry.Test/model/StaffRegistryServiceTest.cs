using StaffRegistry.constant;
using StaffRegistry.events;
using StaffRegistry.factory;
using StaffRegistry.model;

namespace StaffRegistryTest.model;

public class StaffTest
{
    public class Fixture
    {
        internal StaffRegistryService StaffRegistryService { get; private init; }
        internal Mock<IStaffRepository> MockRepository { get; private init; }
        internal Mock<StaffFactory> MockStaffFactory { get; private init; }

        public Fixture()
        {
            MockRepository = new();
            MockStaffFactory = new();
            StaffRegistryService = new(MockRepository.Object, MockStaffFactory.Object);
        }
    }

    public class AddStaffSuccessCase(Fixture fixture) : IClassFixture<Fixture>, IDisposable
    {
        private readonly Fixture _f = fixture;
        private readonly List<EventHandler<StaffRegistryEventArgs<(
            PersonalData PersonalData,
            SoftwareITContract EmploymentContract
        )>>> eventHandlers = [];
        public static IEnumerable<object[]> TestData = [
            [
                new PersonalData("Eric", "Larson", 123456),
                new SoftwareITContract(123)
            ],
            [
                new PersonalData("Mia", "Larson", 123456),
                new SoftwareITContract(123)
            ],
            [
                new PersonalData("Anna", "Nilsson", 123456),
                new SoftwareITContract(123)
            ],
            [
                new PersonalData("Carl", "Scott", 123456),
                new SoftwareITContract(123)
            ]
        ];

        [Theory(DisplayName = "Call add staff to repository if valid staff data")]
        [MemberData(nameof(TestData))]
        internal void T1(PersonalData personalData, SoftwareITContract contract)
        {
            
            _f.StaffRegistryService.AddStaff(personalData, contract);

            int expectedCallTimes = 1;

            _f.MockRepository.Verify(repository =>
                repository.AddStaff(It.IsAny<StaffEntity>()),
                Times.Exactly(expectedCallTimes)
            );
        }

        [Theory(DisplayName = "Raise OK event if valid staff data")]
        [MemberData(nameof(TestData))]
        internal void T2(PersonalData personalData, SoftwareITContract contract)
        {
            bool isRaised = false;
            RepositoryResult result = RepositoryResult.FAILURE;

            EventHandler<StaffRegistryEventArgs<(
                PersonalData PersonalData,
                SoftwareITContract EmploymentContract
            )>> handler = (sender, e) =>
            {
                isRaised = true;
                result = e.Status;
            };

            eventHandlers.Add(handler);
            _f.StaffRegistryService.AddStaffEventHandler += handler;
            _f.StaffRegistryService.AddStaff(personalData, contract);
            Assert.True(isRaised);
            Assert.Equal(RepositoryResult.OK, result);
        }

        public void Dispose()
        {
            foreach (var handler in eventHandlers)
            {
                _f.StaffRegistryService.AddStaffEventHandler -= handler;
            }
            _f.MockRepository.Reset();
        }
    }

    public class AddStaffFailureCase(Fixture fixture) : IClassFixture<Fixture>, IDisposable
    {
        private readonly Fixture _f = fixture;
        private readonly List<EventHandler<StaffRegistryEventArgs<(
            PersonalData PersonalData,
            SoftwareITContract EmploymentContract
        )>>> eventHandlers = [];
        public static IEnumerable<object[]> TestData = [
            [
                new PersonalData("", "Larson", 123456),
                new SoftwareITContract(123)
            ],
            [
                new PersonalData("Mia", "Larson", 123456),
                new SoftwareITContract(0)
            ],
            [
                new PersonalData("Anna", "", 123456),
                new SoftwareITContract(123)
            ],
            [
                new PersonalData("", "", 123456),
                new SoftwareITContract(123)
            ]
        ];

        [Theory(DisplayName = "Do not call add staff to repository if valid staff data")]
        [MemberData(nameof(TestData))]
        internal void T1(PersonalData personalData, SoftwareITContract contract)
        {

            _f.StaffRegistryService.AddStaff(personalData, contract);

            int expectedCallTimes = 0;

            _f.MockRepository.Verify(repository =>
                repository.AddStaff(It.IsAny<StaffEntity>()),
                Times.Exactly(expectedCallTimes)
            );
        }

        [Theory(DisplayName = "Raise Failure event if valid staff data")]
        [MemberData(nameof(TestData))]
        internal void T2(PersonalData personalData, SoftwareITContract contract)
        {
            bool isRaised = false;
            RepositoryResult result = RepositoryResult.FAILURE;

            EventHandler<StaffRegistryEventArgs<(
                PersonalData PersonalData,
                SoftwareITContract EmploymentContract
            )>> handler = (sender, e) =>
            {
                isRaised = true;
                result = e.Status;
            };

            eventHandlers.Add(handler);
            _f.StaffRegistryService.AddStaffEventHandler += handler;
            _f.StaffRegistryService.AddStaff(personalData, contract);
            Assert.True(isRaised);
            Assert.Equal(RepositoryResult.FAILURE, result);
        }

        public void Dispose()
        {
            foreach (var handler in eventHandlers)
            {
                _f.StaffRegistryService.AddStaffEventHandler -= handler;
            }
            _f.MockRepository.Reset();
        }
    }
}
