namespace StaffRegistry.model;

internal class StaffEntity
{
    internal const int MIN_SALARY = 1;

    internal const int MAX_SALARY = 9999999;

    internal const int MIN_NAME_SIZE = 1;

    internal const int MAX_NAME_SIZE = 100;

    internal const string Position = "System Development";

    internal const string Department = "IT";
    
    private string _fName = "Anonymous";
    
    private string _lName = "Anonymous";
    
    private double _salary = MIN_SALARY;

    internal string FName {
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
                throw new StaffEntityException(nameof(FName), "Invalid name range");
            }
        }
    }

    internal string LName {
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
                throw new StaffEntityException(nameof(LName), "Invalid name range");
            }
        }
    }

    internal double Salary
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
                throw new StaffEntityException(nameof(Salary), $"Invalid salary range {value}");
            }
        }
    }

    internal long DateOfBirth { get; set; }

    internal int StaffID { get; set; }

    // Needed for serialization ???
    internal StaffEntity() { }

    internal StaffEntity(
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
