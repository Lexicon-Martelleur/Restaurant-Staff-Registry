using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Retaurant_Staff_Registry.model;

namespace Restaurant_Staff_Registry.Test.model;

public class StaffTest
{
    public class StaffConstructor()
    {
        public static IEnumerable<object[]> ValidTestData = [
            ["Eric", "Larsson", 236666.3, 1],
            ["Lisa", "Ericsson", 336666.3, 2],
            ["Anna", "Jonsson", 436666.3, 3],
        ];

        [Theory(DisplayName = "Create a valid staff entry")]
        [MemberData(nameof(ValidTestData))]
        public void T1(string fname, string lname, double salary, int staffID)
        {
            Staff staff = new(fname, lname, salary, staffID);
            Assert.Equal(staff.Fname, fname);
            Assert.Equal(staff.Lname, lname);
            Assert.Equal(staff.Salary, salary);
            Assert.Equal(staff.StaffID, staffID);
        }
    }
}
