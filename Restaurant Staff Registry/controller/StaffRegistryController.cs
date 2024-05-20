using Retaurant_Staff_Registry.constant;
using Retaurant_Staff_Registry.constants;
using Retaurant_Staff_Registry.events;
using Retaurant_Staff_Registry.model;
using Retaurant_Staff_Registry.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retaurant_Staff_Registry.controller;

public class StaffRegistryController(StaffRegistryService service, StaffRegistryView view)
{
    private readonly HashSet<int> _staffIDs = [];
    public void StartStaffRegistryMenu () {
        view.PrintWelcome();
        AddStaffRegistryEventListners();
        bool continueApp = true;
        while (continueApp)
        {
            try {
                var menuItem = view.GetMenuInput();
                continueApp = menuItem switch
                {
                    MenuItem.EXIT => Continue(HandleExit, false),
                    MenuItem.LIST_ALL_STAFF => Continue(HandleSelectAllStaffEntries, true),
                    MenuItem.ADD_STAFF => Continue(HandleAddStaffMenu, true),
                    MenuItem.DEFAULT => Continue(HandleInvalidChoice, true),
                    _ => Continue(HandleInvalidChoice, true)
                };
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

    private bool Continue(Action action, bool exit)
    {
        action();
        return exit;
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
        var staffItems = view.GetStaffInput();
        service.AddStaff(new(
            staffItems.fname,
            staffItems.lname,
            staffItems.salary,
            GetStaffID())
        );
    }

    private int GetStaffID ()
    {
        Random random = new();
        int id;
        do
        {
            id = random.Next(1, int.MaxValue);
        } while (!_staffIDs.Add(id));
        return id;
    }

    private void HandleInvalidChoice()
    {
        view.PrintInvalidMenuChoise();
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
