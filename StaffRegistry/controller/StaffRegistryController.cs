using StaffRegistry.constant;
using StaffRegistry.events;
using StaffRegistry.model;
using StaffRegistry.utility;
using StaffRegistry.view;

namespace StaffRegistry.controller;

internal class StaffRegistryController(
    StaffRegistryService service,
    StaffRegistryView view)
{
    internal void StartMenu () {
        view.PrintWelcome();
        AddStaffRegistryEventListners();
        bool useMenu = true;
        do
        {
            try
            {
                MenuItem menuItem = view.ReadMenuSelection();
                useMenu = HandleMenuSelection(menuItem);
            }
            catch
            {
                useMenu = ContinueMenu(HandleInvalidChoice, true);
            }
        } while (useMenu);
    }

    private void AddStaffRegistryEventListners()
    {
        service.AddStaffEventHandler += HandleAddStaffResult;
        service.GetStaffEventHandler += HandleGetStaffResult;
        service.DeleteStaffEventHandler += HandleDeleteStaffResult;
    }

    private bool HandleMenuSelection(MenuItem menuItem) => menuItem switch
    {
        MenuItem.ADD_STAFF => ContinueMenu(HandleAddStaffSelection, true),
        MenuItem.GET_STAFF => ContinueMenu(HandleGetStaffSelection, true),
        MenuItem.UPDATE_STAFF => ContinueMenu(HandleUpdateStaffSelection, true),
        MenuItem.DELETE_STAFF => ContinueMenu(HandleDeleteStaffSelection, true),
        MenuItem.LIST_ALL_STAFF => ContinueMenu(HandleGetStaffEntriesSelection, true),
        MenuItem.DEFAULT => ContinueMenu(HandleInvalidChoice, true),
        MenuItem.EXIT => ContinueMenu(HandleExit, false),
        _ => ContinueMenu(HandleInvalidChoice, true)
    };

    private bool ContinueMenu(Action action, bool exitMenu)
    {
        action();
        return exitMenu;
    }

    private void HandleInvalidChoice()
    {
        view.PrintInvalidMenuChoice();
    }

    private void HandleAddStaffSelection ()
    {
        try
        {
            var staffInputData = view.ReadNewStaffInput();
            PersonalData personalData = new(
                staffInputData.FName,
                staffInputData.LName,
                DateUtility.ConvertDateStringToTimeStamp(staffInputData.DateOfBirth));
            EmploymentContract contract = new(
                double.Parse(staffInputData.Salary));
            service.AddStaff(personalData, contract);
        }
        catch
        {
            view.PrintStaffAddedUnsuccessfully();
        }
    }

    private void HandleAddStaffResult (
        object? sender,
        StaffRegistryEventArgs<AddStaffEventData> e)
    {
        if (e.Status == RepositoryResult.OK)
        {
            view.PrintStaffAddedSuccessfully(e.Data.PersonalData, e.Data.EmploymentContract);
        }
        if (e.Status == RepositoryResult.FAILURE)
        {
            view.PrintStaffAddedUnsuccessfully(e.Data.PersonalData, e.Data.EmploymentContract);
        }
    }

    private void HandleGetStaffEntriesSelection()
    {
        IReadOnlyList<StaffEntity> staffEntries = service.GetAllStaffEntries();
        view.PrintAllStaffEntries(staffEntries);
    }

    private void HandleGetStaffSelection()
    {
        string staffId = view.ReadStaffIDToView();
        try
        {
            service.GetStaff(int.Parse(staffId));
        }
        catch
        {
            view.PrintGetStaffUnsuccessfully(staffId);
        }
    }

    private void HandleGetStaffResult(
        object? sender,
        StaffRegistryEventArgs<GetStaffEventData> e)
    {
        StaffEntity? staff = e.Data.Staff;
        if (e.Status == RepositoryResult.OK && staff != null)
        {
            view.PrintGetStaffSuccessfully(staff);
        }
        if (e.Status == RepositoryResult.FAILURE)
        {
            view.PrintGetStaffUnsuccessfully($"{e.Data.StaffId}");
        }
    }

    private void HandleUpdateStaffSelection()
    {
        throw new NotImplementedException();
    }

    private void HandleDeleteStaffSelection()
    {
        string staffId = view.ReadStaffIDToDelete();
        try
        {
            service.DeleteStaff(int.Parse(staffId));
        }
        catch
        {
            view.PrintDeleteStaffUnsuccessfully(staffId);
        }
    }

    private void HandleDeleteStaffResult(
        object? sender,
        StaffRegistryEventArgs<int> e)
    {
        if (e.Status == RepositoryResult.OK)
        {
            view.PrintDeleteStaffSuccessfully(e.Data);
        }
        if (e.Status == RepositoryResult.FAILURE)
        {
            view.PrintDeleteStaffUnsuccessfully(e.Data);
        }
    }

    private void HandleExit()
    {
        service.AddStaffEventHandler -= this.HandleAddStaffResult;
        service.GetStaffEventHandler -= this.HandleGetStaffResult;
        service.DeleteStaffEventHandler -= this.HandleDeleteStaffResult;
        view.PrintExit();
    }
}
