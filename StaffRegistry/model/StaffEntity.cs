namespace StaffRegistry.model;


//TODO! Refactor Entity and VO for Staff.
// Focus how it is used throughout application, model and db layers.


public class StaffEntity
{
    public const int MIN_SALARY = 1;
    
    public const int MAX_SALARY = 9999999;
    
    public const int MIN_NAME_SIZE = 1;
    
    public const int MAX_NAME_SIZE = 100;

    public const string Position = "System Development";

    public const string Department = "IT";
    
    private string _fName = "Anonymous";
    
    private string _lName = "Anonymous";
    
    private double _salary = MIN_SALARY;

    public string FName {
        get => _fName;
        set
        {
            if (value.Length >= MIN_NAME_SIZE &&
                value.Length <= StaffEntity.MAX_NAME_SIZE)
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
        set
        {
            if (value.Length >= MIN_NAME_SIZE &&
                value.Length <= StaffEntity.MAX_NAME_SIZE)
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
        set
        {
            if (value >= MIN_SALARY &&
                value <= StaffEntity.MAX_SALARY)
            {
                _salary = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(Salary), $"Invalid salary range {value}");
            }
        }
    }

    public long DateOfBirth { get; set; }

    public int StaffID { get; set; }

    // Needed for serialization ???
    public StaffEntity() { }

    public StaffEntity(
        PersonalData personalData,
        EmploymentContract employmentContract,
        int staffID)
    {
        FName = personalData.FName;
        LName = personalData.LName;
        DateOfBirth = personalData.DateOfBirth;
        Salary = employmentContract.Salary;
        StaffID = staffID;
    }
}
