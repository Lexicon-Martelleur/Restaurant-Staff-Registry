using Retaurant_Staff_Registry.constant;
using Retaurant_Staff_Registry.events;
using Retaurant_Staff_Registry.model;
using Retaurant_Staff_Registry.view;

namespace Retaurant_Staff_Registry.controller;

public class StaffRegistryController(
    StaffRegistryService service,
    StaffRegistryView view)
{
    public void StartMenu () {
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
        service.StaffRegistryEventHandler += HandleAddStaffSucess;
        service.StaffRegistryEventHandler += HandleAddStaffFailure;
    }

    private bool HandleMenuSelection(MenuItem menuItem) => menuItem switch
    {
        MenuItem.DEFAULT => ContinueMenu(HandleInvalidChoice, true),
        MenuItem.EXIT => ContinueMenu(HandleExit, false),
        MenuItem.LIST_ALL_STAFF => ContinueMenu(HandleSelectAllStaffEntries, true),
        MenuItem.ADD_STAFF => ContinueMenu(HandleAddStaffMenu, true),
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

    private void HandleExit()
    {
        service.StaffRegistryEventHandler -= this.HandleAddStaffSucess;
        service.StaffRegistryEventHandler -= this.HandleAddStaffFailure;
        view.PrintExit();
    }

    private void HandleSelectAllStaffEntries()
    {
        IReadOnlyList<StaffEntity> staffEntries = service.GetAllStaffEntries();
        view.PrintAllStaffEntries(staffEntries);
    }

    private void HandleAddStaffMenu ()
    {
        try
        {
            var staffData = view.ReadNewStaffInput();
            service.AddStaff(staffData);
        }
        catch
        {
            view.PrintStaffAddedUnsuccessfully();
        }
    }

    private void HandleAddStaffSucess (object? sender, StaffRegistryEventArgs e)
    {
        if (e.Status == RepositoryResult.ADD_STAFF_OK)
        {
            view.PrintStaffAddedSuccessfully(e.Data);
        }
    }

    private void HandleAddStaffFailure(object? sender, StaffRegistryEventArgs e)
    {
        if (e.Status == RepositoryResult.ADD_STAFF_FAILURE)
        {
            view.PrintStaffAddedUnsuccessfully(e.Data);
        }
    }
}
