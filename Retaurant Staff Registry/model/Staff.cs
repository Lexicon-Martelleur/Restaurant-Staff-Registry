using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retaurant_Staff_Registry.model;

public class Staff(string fname, string lname, double salary, int staffID)
{
    public string Fname { get; } = fname;
    public string Lname { get; } = lname;
    public double Salary { get; } = salary;
    public int StaffID { get; } = staffID;
}
