using StaffRegistry.model;

namespace StaffRegistryTest.Test.model;

public class StaffEntityTest
{
    public class StaffEntityConstructor()
    {
        public static IEnumerable<object[]> ValidTestData = [
            [
                new PersonalData("Eric", "Larson", 123),
                new EmploymentContract(1234),
                1
            ],
            [
                new PersonalData("Lisa", "Larson", 123),
                new EmploymentContract(1234),
                2
            ],
            [
                new PersonalData("Anna", "Larson", 123),
                new EmploymentContract(1234),
                3
            ],
        ];

        [Theory(DisplayName = "Create a valid staff entry")]
        [MemberData(nameof(ValidTestData))]
        internal void T1(
            PersonalData personalData,
            EmploymentContract contract,
            int staffId)
        {
            StaffEntity staff = new(personalData, contract, staffId);
            Assert.Equal(staff.FName, personalData.FName);
            Assert.Equal(staff.LName, personalData.LName);
            Assert.Equal(staff.Salary, contract.Salary);
            Assert.Equal(staff.StaffID, staffId);
        }

        public static IEnumerable<object[]> InvalidTestData = [
            [
                new PersonalData("Eric", "", 123),
                new EmploymentContract(1234),
                1
            ],
            [
                new PersonalData("Lisa", "Scott", 123),
                new EmploymentContract(0),
                2
            ],
            [
                new PersonalData("", "", 123),
                new EmploymentContract(1234),
                3
            ],
        ];

        [Theory(DisplayName = "Do not create a staff entry")]
        [MemberData(nameof(InvalidTestData))]
        internal void T2(
            PersonalData personalData,
            EmploymentContract contract,
            int staffId)
        {
            Assert.Throws<StaffEntityException>(() =>
            {
                StaffEntity staff = new(personalData, contract, staffId);
            }
            );
        }
    }
}
