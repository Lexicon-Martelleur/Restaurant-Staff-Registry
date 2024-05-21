using Retaurant_Staff_Registry.constant;
using Retaurant_Staff_Registry.events;
using Retaurant_Staff_Registry.model;
using Retaurant_Staff_Registry.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retaurant_Staff_Registry.controller;

public class StaffRegistryController(
    StaffRegistryService service,
    StaffRegistryView view)
{
    public void StartStaffRegistryMenu () {
        view.PrintWelcome();
        AddStaffRegistryEventListners();
        bool useMenu = true;
        while (useMenu)
        {
            try {
                var menuItem = view.GetMenuInput();
                useMenu = HandleMenuSelection(menuItem);
            }
            catch
            {
                Continue(HandleInvalidChoice, true);
            }
        }
    }

    private void AddStaffRegistryEventListners()
    {
        service.StaffRegistryEvent += HandleAddStaffSucess;
        service.StaffRegistryEvent += HandleAddStaffFailure;
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
        view.PrintInvalidMenuChoise();
    }

    private void HandleExit()
    {
        service.StaffRegistryEvent -= this.HandleAddStaffSucess;
        service.StaffRegistryEvent -= this.HandleAddStaffFailure;
        view.PrintExit();
    }

    private void HandleSelectAllStaffEntries()
    {
        List<Staff> staffEntries = service.GetAllStaffEntries();
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

    private void HandleAddStaffSucess (object? sender, StaffRegistryEvent e)
    {
        if (e.Status == RepositoryResult.ADD_STAFF_OK)
        {
            view.PrintStaffAddedSuccessfully(e.Data);
        }
    }

    private void HandleAddStaffFailure(object? sender, StaffRegistryEvent e)
    {
        if (e.Status == RepositoryResult.ADD_STAFF_FAILURE)
        {
            view.PrintStaffAddedUnsuccessfully(e.Data);
        }
    }
}
