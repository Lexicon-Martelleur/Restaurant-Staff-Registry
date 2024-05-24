using Retaurant_Staff_Registry.model;

namespace Restaurant_Staff_Registry.Test.model;

public class StaffEntityTest
{
    public class StaffConstructor()
    {
        public static IEnumerable<object[]> ValidTestData = [
            ["Eric", "Larson", 236666.3, 123, 1],
            ["Lisa", "Erikson", 336666.3, 123, 2],
            ["Anna", "Jonson", 436666.3, 123, 3],
        ];

        [Theory(DisplayName = "Create a valid staff entry")]
        [MemberData(nameof(ValidTestData))]
        public void T1(string fName, string lName, double salary, int dateOfBirth, int staffID)
        {
            StaffEntity staff = new(fName, lName, salary, dateOfBirth, staffID);
            Assert.Equal(staff.FName, fName);
            Assert.Equal(staff.LName, lName);
            Assert.Equal(staff.Salary, salary);
            Assert.Equal(staff.StaffID, staffID);
        }

        public static IEnumerable<object[]> InvalidTestData = [
            ["Eric", "Larson", -236666.3, 123, 1],
            ["Lisa", "", -336666.3, 123, 2],
            ["", "Jonson", -436666.3, 123, 3],
        ];

        [Theory(DisplayName = "Do not create a staff entry")]
        [MemberData(nameof(InvalidTestData))]
        public void T2(string fName, string lName, double salary, int dateOfBirth, int staffID)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                StaffEntity staff = new(fName, lName, salary, dateOfBirth, staffID);
            }
            );
        }
    }
}
