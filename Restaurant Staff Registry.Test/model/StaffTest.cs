using Retaurant_Staff_Registry.model;

namespace Restaurant_Staff_Registry.Test.model;

public class StaffTest
{
    public class StaffConstructor()
    {
        public static IEnumerable<object[]> ValidTestData = [
            ["Eric", "Larson", 236666.3, 1],
            ["Lisa", "Erikson", 336666.3, 2],
            ["Anna", "Jonson", 436666.3, 3],
        ];

        [Theory(DisplayName = "Create a valid staff entry")]
        [MemberData(nameof(ValidTestData))]
        public void T1(string fname, string lname, double salary, int staffID)
        {
            Staff staff = new(fname, lname, salary, staffID);
            Assert.Equal(staff.FName, fname);
            Assert.Equal(staff.LName, lname);
            Assert.Equal(staff.Salary, salary);
            Assert.Equal(staff.StaffID, staffID);
        }

        public static IEnumerable<object[]> InvalidTestData = [
            ["Eric", "Larson", -236666.3, 1],
            ["Lisa", "", -336666.3, 2],
            ["", "Jonson", -436666.3, 3],
        ];

        [Theory(DisplayName = "Do not create a staff entry")]
        [MemberData(nameof(InvalidTestData))]
        public void T2(string fname, string lname, double salary, int staffID)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Staff staff = new(fname, lname, salary, staffID);
            }
            );
        }
    }
}
