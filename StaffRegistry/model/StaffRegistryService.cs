using StaffRegistry.constant;
using StaffRegistry.events;
using StaffRegistry.utility;

namespace StaffRegistry.model;

public class StaffRegistryService(IStaffRepository repository)
{
    

    public EventHandler<StaffRegistryEventArgs>? StaffRegistryEventHandler;
    public void AddStaff(StaffVO staffData)
    {
        try {
            Console.WriteLine($"staffItems {staffData}");
            StaffEntity staff = new(
                staffData.FName,
                staffData.LName,
                staffData.Salary,
                DateUtility.ConvertDateStringToTimeStamp(staffData.DateOfBirth),
                IDUtility.GetInMemoryUniqueID()
            );
            repository.AddStaff(staff);
            OnAddStaffOk(staffData);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            OnAddStaffFailure(staffData, ex.Message);
        }
        catch
        {
            OnAddStaffFailure(staffData, "Invalid property value");
        }
    }

    private void OnAddStaffOk(StaffVO staffData)
    {
        StaffRegistryEventHandler?.Invoke(this, new StaffRegistryEventArgs(
            RepositoryResult.ADD_STAFF_OK,
            "Staff registered OK",
            staffData
        ));
    }

    private void OnAddStaffFailure(
        StaffVO staffData,
        string msg)
    {
        StaffRegistryEventHandler?.Invoke(this, new StaffRegistryEventArgs(
            RepositoryResult.ADD_STAFF_FAILURE,
            msg,
            staffData
        ));
    }

    public IReadOnlyList<StaffEntity> GetAllStaffEntries()
    {
        return repository.GetAllStaffEntries(); 
    }
}
