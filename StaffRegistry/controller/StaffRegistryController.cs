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
                string menuSelection = view.ReadMenuSelection();
                Console.WriteLine(menuSelection);
                useMenu = HandleMenuSelection(menuSelection);
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
        service.UpdateStaffEventHandler += HandleUpdateStaffResult;
        service.DeleteStaffEventHandler += HandleDeleteStaffResult;
    }

    private bool HandleMenuSelection(string menuSelection) => menuSelection switch
    {
        StaffMenu.ADD_STAFF => ContinueMenu(HandleAddStaffSelection, true),
        StaffMenu.GET_STAFF => ContinueMenu(HandleGetStaffSelection, true),
        StaffMenu.UPDATE_STAFF => ContinueMenu(HandleUpdateStaffSelection, true),
        StaffMenu.DELETE_STAFF => ContinueMenu(HandleDeleteStaffSelection, true),
        StaffMenu.LIST_ALL_STAFF => ContinueMenu(HandleGetStaffEntriesSelection, true),
        StaffMenu.DEFAULT => ContinueMenu(HandleInvalidChoice, true),
        StaffMenu.EXIT => ContinueMenu(HandleExit, false),
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
            SoftwareITContract contract = new(
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
        var updateStaffData = view.UpdateNewStaffInput();
        try
        {
            PersonalData personalData = new(
                updateStaffData.Data.FName,
                updateStaffData.Data.LName,
                DateUtility.ConvertDateStringToTimeStamp(updateStaffData.Data.DateOfBirth));
            SoftwareITContract contract = new(
                double.Parse(updateStaffData.Data.Salary));
            int staffId = int.Parse(updateStaffData.Id);
            service.UpdateStaff(staffId, personalData, contract);
        }
        catch
        {
            view.PrintUpdatedStaffUnsuccessfully(updateStaffData.Id);
        }
    }

    private void HandleUpdateStaffResult(
        object? sender,
        StaffRegistryEventArgs<UpdateStaffEventData> e)
    {
        StaffEntity? staff = e.Data.Staff;
        if (e.Status == RepositoryResult.OK && staff != null)
        {
            view.PrintUpdatedStaffSuccessfully(staff);
        }
        if (e.Status == RepositoryResult.FAILURE)
        {
            view.PrintUpdatedStaffUnsuccessfully(e.Data.StaffId);
        }
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
            view.PrintDeletedStaffSuccessfully(e.Data);
        }
        if (e.Status == RepositoryResult.FAILURE)
        {
            view.PrintDeletedStaffUnsuccessfully(e.Data);
        }
    }

    private void HandleExit()
    {
        service.AddStaffEventHandler -= this.HandleAddStaffResult;
        service.GetStaffEventHandler -= this.HandleGetStaffResult;
        service.UpdateStaffEventHandler -= HandleUpdateStaffResult;
        service.DeleteStaffEventHandler -= this.HandleDeleteStaffResult;
        view.PrintExit();
    }
}
