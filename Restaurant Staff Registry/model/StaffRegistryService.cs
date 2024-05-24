
using Retaurant_Staff_Registry.constant;
using Retaurant_Staff_Registry.events;
using Retaurant_Staff_Registry.utility;

namespace Retaurant_Staff_Registry.model;

public class StaffRegistryService(IStaffRepository repository)
{
    private readonly HashSet<int> _staffIDs = [];

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
                GetStaffID());
            repository.AddStaff(staff);
            Console.WriteLine($"staffItems {staffData}");
            OnAddStaffOk(staffData);
            Console.WriteLine($"staffItems {staffData}");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            OnAddStaffFailure(staffData, ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ex {ex.StackTrace}");
            OnAddStaffFailure(staffData, "Invalid property value");
        }
    }

    private int GetStaffID()
    {
        Random random = new();
        int id;
        do
        {
            id = random.Next(1, int.MaxValue);
        } while (!_staffIDs.Add(id));
        return id;
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
