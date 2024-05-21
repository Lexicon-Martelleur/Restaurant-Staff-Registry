using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retaurant_Staff_Registry.model;

public class Staff(string fname, string lname, double salary, int staffID)
{
    public static int MIN_SALARY = 1;
    public static int MAX_SALARY = 99999;
    public static int MIN_NAME_SIZE = 1;
    public static int MAX_NAME_SIZE = 100;
    public string Fname { get; } = fname;
    public string Lname { get; } = lname;
    public double Salary { get; } = salary;
    public int StaffID { get; } = staffID;
}
