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

    public class AddStaff(Fixture f) : IClassFixture<Fixture>
    {
        private readonly Fixture f = f;
        public static IEnumerable<object[]> TestData = [
            [
                new List<Staff> {
                    new("Eric", "Larsson", 236666.3, 1),
                    new("Lisa", "Ericsson", 336666.3, 2),
                    new("Anna", "Jonsson", 436666.3, 3)
                }
            ],
        ];

        [Theory(DisplayName = "Get added staff entries")]
        [MemberData(nameof(TestData))]
        public void T1(List<Staff> staffEntries)
        {
            f.MockRepository.Setup(repository => 
                repository.GetAllStaffEntries()).Returns(staffEntries);

            foreach (var staff in staffEntries)
            {
                f.StaffRegistryService.AddStaff((
                    staff.Fname,
                    staff.Lname,
                    staff.Salary
                    ));
            }

            var result = f.StaffRegistryService.GetAllStaffEntries();

            Assert.Equal(staffEntries, result);
            f.MockRepository.Verify(repository => 
                repository.AddStaff(It.IsAny<Staff>()),
                Times.Exactly(staffEntries.Count)
            );
            f.MockRepository.Verify(repository => 
                repository.GetAllStaffEntries(),
                Times.Exactly(1)
            );
        }
    }
}
