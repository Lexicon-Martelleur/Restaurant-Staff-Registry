using StaffRegistry.constant;
using StaffRegistry.EntityModels;
using StaffRegistry.events;
using StaffRegistry.factory;

namespace StaffRegistry.model;

public class StaffRegistryService(IStaffRepository repository, StaffFactory staffFactory)
{
    public EventHandler<
        StaffRegistryEventArgs<AddStaffEventData>
    >? AddStaffEventHandler;

    public EventHandler<
        StaffRegistryEventArgs<GetStaffEventData>
    >? GetStaffEventHandler;

    public EventHandler<StaffRegistryEventArgs<int>>? DeleteStaffEventHandler;

    public EventHandler<StaffRegistryEventArgs<int>>? UpdateStaffEventHandler;

    public void AddStaff(PersonalData personalData, EmploymentContract contract)
    {
        try {
            Console.WriteLine($"staffItems {personalData}");
            StaffEntity staff = staffFactory.CreateStaffEntity(
                personalData.FName,
                personalData.LName,
                contract.Salary,
                personalData.DateOfBirth);
            repository.AddStaff(staff);
            OnAddStaffOk(personalData, contract);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine(ex.StackTrace);
            OnAddStaffFailure(personalData, contract, ex.Message);
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
            OnAddStaffFailure(personalData, contract, "Invalid property value");
        }
    }

    private void OnAddStaffOk(
        PersonalData data,
        EmploymentContract contract)
    {
        StaffRegistryEventArgs<AddStaffEventData> eventArgs = new(
            RepositoryResult.OK,
            "Staff registered OK",
            (PersonalData: data, EmploymentContract: contract));
        AddStaffEventHandler?.Invoke(this, eventArgs);
    }

    private void OnAddStaffFailure(
        PersonalData data,
        EmploymentContract contract,
        string msg)
    {
        StaffRegistryEventArgs<AddStaffEventData> eventArgs = new(
            RepositoryResult.FAILURE,
            msg,
            (PersonalData: data, EmploymentContract: contract));
        AddStaffEventHandler?.Invoke(this, eventArgs);
    }

    public void GetStaff(int staffId)
    {
        try
        {
            StaffEntity staff = repository.GetStaff(staffId);
            OnGetStaffOk(staff);
        } catch
        {
            OnGetStaffFailure(staffId);
        }
    }

    private void OnGetStaffOk(StaffEntity staff)
    {
        StaffRegistryEventArgs<GetStaffEventData> eventArgs = new(
            RepositoryResult.OK,
            "Get staff OK",
            (StaffId: staff.StaffID, Staff: staff));
        GetStaffEventHandler?.Invoke(this, eventArgs);
    }

    private void OnGetStaffFailure(int staffId)
    {
        StaffRegistryEventArgs<GetStaffEventData> eventArgs = new(
            RepositoryResult.FAILURE,
            "Get staff failure",
            (StaffId: staffId, Staff: null));
        GetStaffEventHandler?.Invoke(this, eventArgs);
    }

    public IReadOnlyList<StaffEntity> GetAllStaffEntries()
    {
        return repository.GetAllStaffEntries(); 
    }

    public void DeleteStaff(int staffId)
    {
        try
        {
            repository.DeleteStaff(staffId); 
            OnDeleteStaffOk(staffId);
        }
        catch
        {
            OnDeleteStaffFailure(staffId);
        }
    }

    private void OnDeleteStaffOk(int staffId)
    {
        StaffRegistryEventArgs<int> eventArgs = new(
            RepositoryResult.OK,
            "Delete staff OK",
            staffId);
        DeleteStaffEventHandler?.Invoke(this, eventArgs);
    }

    private void OnDeleteStaffFailure(int staffId)
    {
        StaffRegistryEventArgs<int> eventArgs = new(
            RepositoryResult.FAILURE,
            "Delete staff failure",
            staffId);
        DeleteStaffEventHandler?.Invoke(this, eventArgs);
    }

    public void UpdateStaff(int staffId)
    {
        try
        {
            repository.UpdateStaff(staffId);
            OnUpdateStaffOk(staffId);
        }
        catch
        {
            OnUpdateStaffFailure(staffId);
        }
    }

    private void OnUpdateStaffOk(int staffId)
    {
        StaffRegistryEventArgs<int> eventArgs = new(
            RepositoryResult.OK,
            "Update staff OK",
            staffId);
        UpdateStaffEventHandler?.Invoke(this, eventArgs);
    }

    private void OnUpdateStaffFailure(int staffId)
    {
        StaffRegistryEventArgs<int> eventArgs = new(
            RepositoryResult.FAILURE,
            "Update staff failure",
            staffId);
        UpdateStaffEventHandler?.Invoke(this, eventArgs);
    }
}
