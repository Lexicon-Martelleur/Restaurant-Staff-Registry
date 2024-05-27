using StaffRegistry.model;
using StaffRegistry.utility;

namespace StaffRegistry.factory;

public class StaffFactory
{
    public StaffEntity CreateStaffEntity(
        string fName,
        string lName,
        double salary,
        long dateOfBirth) 
    {
        PersonalData personalData = new(fName, lName, dateOfBirth);
        EmploymentContract employmentContract = new(salary);
        return new StaffEntity(
            personalData,
            employmentContract,
            IDUtility.GetInMemoryUniqueID());
    }

    public StaffEntity GetStaffEntity(
        string fName,
        string lName,
        double salary,
        long dateOfBirth,
        int staffId
        )
    {
        PersonalData personalData = new(fName, lName, dateOfBirth);
        EmploymentContract employmentContract = new(salary);
        return new StaffEntity(
            personalData,
            employmentContract,
            staffId);
    }
}
