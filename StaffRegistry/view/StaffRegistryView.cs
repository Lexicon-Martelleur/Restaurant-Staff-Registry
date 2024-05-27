﻿using StaffRegistry.constant;
using StaffRegistry.model;
using StaffRegistry.utility;
using System.Diagnostics.Contracts;

namespace StaffRegistry.view;

//TODO! Update view class to be more UI friendly!
//  1) Add animations
//  2) Add colors.
//TODO! Update view with custom console!
//TODO! Move parsing responsibility to the controller.
public class StaffRegistryView
{
    public void PrintWelcome()
    {
        try { Console.Clear(); } catch { };
        Console.WriteLine($"""

        ==============================================
         🍕🍕🍕 Welcome to my staff registry 🍕🍕🍕
        ==============================================

        """);
    }
    public MenuItem ReadMenuSelection()
    {
        Console.WriteLine("""

    Staff registry menu
    ===================
        (1) Add staff entry
        (2) Get staff entry
        (3) Update staff entry
        (4) Delete staff entry
        (5) List all staff entries
        (Q) Exit staff registry
    """);
        Console.Write("Select menu item: ");
        var selectedMenu = Console.ReadLine();
        return GetSelectedMenuItem(selectedMenu ?? "");
    }

    private MenuItem GetSelectedMenuItem(string selectedMenu) => selectedMenu switch
    {
        "1" => MenuItem.ADD_STAFF,
        "2" => MenuItem.GET_STAFF,
        "3" => MenuItem.UPDATE_STAFF,
        "4" => MenuItem.DELETE_STAFF,
        "5" => MenuItem.LIST_ALL_STAFF,
        "q" or "Q" => MenuItem.EXIT,
        _ => MenuItem.DEFAULT
    };

    public StaffInputData ReadNewStaffInput ()
    {
        Console.Write("\nEnter new staff (FirstName LastName Salary$ DateOfBirth(yyyy-mm-dd)): ");
        string newStaffInput = Console.ReadLine() ?? "";
        string[] newStaff = newStaffInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        return (
            FName: newStaff[0],
            LName: newStaff[1],
            Salary: newStaff[2],
            DateOfBirth: newStaff[3]
        );
    }

    public string ReadStaffID()
    {
        Console.Write("\nEnter staff ID: ");
        return Console.ReadLine() ?? "";
    }

    public void PrintStaffAddedSuccessfully(
        PersonalData personalData,
        EmploymentContract contract)
    {
        string dateOfBirth = DateUtility.ConvertTimeStampToDateString(personalData.DateOfBirth);
        Console.WriteLine($"""
        ✅ Staff {personalData.FName} {personalData.LName} {dateOfBirth} with salary {contract.Salary}$
        have been added to the registry.
        """);
    }

    public void PrintStaffAddedUnsuccessfully()
    {
        Console.WriteLine("⚠️ Failure! Staff could not be added to the registry");
    }

    public void PrintStaffAddedUnsuccessfully(
        PersonalData personalData,
        EmploymentContract contract)
    {
        string dateOfBirth = DateUtility.ConvertTimeStampToDateString(personalData.DateOfBirth);
        Console.WriteLine($"""
        ⚠️ Failure! Staff {personalData.FName} {personalData.LName} {dateOfBirth} with salary {contract.Salary}$
        could not be added to the registry.
        """
        );
    }

    public void PrintAllStaffEntries(IReadOnlyList<StaffEntity> staffEntries)
    {
        Console.WriteLine("\nList of registered staff:");

        if (staffEntries.Count == 0)
        {
            Console.WriteLine("🚀 Empty");
        }
        foreach (var staff in staffEntries)
        {
            string dateString = DateUtility.ConvertTimeStampToDateString(staff.DateOfBirth);
            Console.WriteLine($"""
                🚀 {staff.StaffID}: {staff.FName} {staff.LName}, salary {staff.Salary}, date of birth {dateString}
            """);
        }
        Console.WriteLine("");
    }

    public void PrintInvalidMenuChoice()
    {
        Console.WriteLine("\n⚠️ Not valid selection");
    }

    public void PrintGetStaffSuccessfully(StaffEntity staff)
    {
        string dateOfBirth = DateUtility.ConvertTimeStampToDateString(staff.DateOfBirth);
        Console.WriteLine($"""
        ✅ StaffID {staff.StaffID}: {staff.FName} {staff.LName} {dateOfBirth} with salary {staff.Salary}$.
        """);
    }

    public void PrintGetStaffUnsuccessfully(string staffId)
    {
        Console.WriteLine($"\n⚠️ Could not get staff with id: {staffId}");
    }

    public void PrintExit()
    {
        Console.WriteLine("\n🍕🍕🍕 Goodbye 🍕🍕🍕");
    }
}
