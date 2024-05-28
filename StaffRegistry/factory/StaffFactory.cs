using StaffRegistry.model;
using StaffRegistry.utility;

namespace StaffRegistry.factory;

internal class StaffFactory
{
    internal StaffEntity CreateStaffEntity(
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

    internal StaffEntity GetStaffEntity(
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
