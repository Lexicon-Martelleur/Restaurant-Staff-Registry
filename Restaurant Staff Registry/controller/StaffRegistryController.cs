using Retaurant_Staff_Registry.constant;
using Retaurant_Staff_Registry.events;
using Retaurant_Staff_Registry.model;
using Retaurant_Staff_Registry.view;

namespace Retaurant_Staff_Registry.controller;

public class StaffRegistryController(
    StaffRegistryService service,
    StaffRegistryView view)
{
    public void StartStaffRegistryMenu () {
        view.PrintWelcome();
        AddStaffRegistryEventListners();
        bool useMenu = true;
        do
        {
            try
            {
                MenuItem menuItem = view.GetMenuInput();
                useMenu = HandleMenuSelection(menuItem);
            }
            catch
            {
                Continue(HandleInvalidChoice, true);
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
        MenuItem.DEFAULT => Continue(HandleInvalidChoice, true),
        MenuItem.EXIT => Continue(HandleExit, false),
        MenuItem.LIST_ALL_STAFF => Continue(HandleSelectAllStaffEntries, true),
        MenuItem.ADD_STAFF => Continue(HandleAddStaffMenu, true),
        _ => Continue(HandleInvalidChoice, true)
    };

    private bool Continue(Action action, bool exit)
    {
        action();
        return exit;
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
        IReadOnlyList<Staff> staffEntries = service.GetAllStaffEntries();
        view.PrintAllStaffEntries(staffEntries);
    }

    private void HandleAddStaffMenu ()
    {
        try
        {
            var staffItems = view.GetStaffInput();
            service.AddStaff(staffItems);
        }
        catch (Exception)
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
