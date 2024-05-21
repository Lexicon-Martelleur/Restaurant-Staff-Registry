using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Retaurant_Staff_Registry.model;

public class Staff
{
    public static readonly int MIN_SALARY = 1;
    public static readonly int MAX_SALARY = 999999;
    public static readonly int MIN_NAME_SIZE = 1;
    public static readonly int MAX_NAME_SIZE = 100;
    private string _fname = "Anonymous";
    private string _lname = "Anonymous";
    private double _salary = MIN_SALARY;

    public Staff(string fname, string lname, double salary, int staffID)
    {
        Fname = fname;
        Lname = lname;
        Salary = salary;
        StaffID = staffID;
    }

    public string Fname {
        get => _fname;
        init
        {
            if (value.Length >= MIN_NAME_SIZE &&
                value.Length <= Staff.MAX_NAME_SIZE)
            {
                _fname = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(Fname), "Invalid name range");
            }
        }
    }

    public string Lname {
        get => _lname;
        init
        {
            if (value.Length >= MIN_NAME_SIZE &&
                value.Length <= Staff.MAX_NAME_SIZE)
            {
                _lname = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(Lname), "Invalid name range");
            }
        }
    }
    
    public double Salary
    {
        get => _salary;
        init
        {
            if (value >= MIN_SALARY &&
                value <= Staff.MAX_SALARY)
            {
                _salary = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(Salary), "Invalid salary range");
            }
        }
    }

    public int StaffID { get; init; }
}
