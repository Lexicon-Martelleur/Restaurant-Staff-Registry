namespace Retaurant_Staff_Registry.model;

public class Staff
{
    public static readonly int MIN_SALARY = 1;
    public static readonly int MAX_SALARY = 9999999;
    public static readonly int MIN_NAME_SIZE = 1;
    public static readonly int MAX_NAME_SIZE = 100;
    private string _fName = "Anonymous";
    private string _lName = "Anonymous";
    private double _salary = MIN_SALARY;

    public Staff(string fName, string lName, double salary, int staffID)
    {
        FName = fName;
        LName = lName;
        Salary = salary;
        StaffID = staffID;
    }

    public string FName {
        get => _fName;
        init
        {
            if (value.Length >= MIN_NAME_SIZE &&
                value.Length <= Staff.MAX_NAME_SIZE)
            {
                _fName = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(FName), "Invalid name range");
            }
        }
    }

    public string LName {
        get => _lName;
        init
        {
            if (value.Length >= MIN_NAME_SIZE &&
                value.Length <= Staff.MAX_NAME_SIZE)
            {
                _lName = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(LName), "Invalid name range");
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
                throw new ArgumentOutOfRangeException(nameof(Salary), $"Invalid salary range {value}");
            }
        }
    }

    public int StaffID { get; init; }
}
