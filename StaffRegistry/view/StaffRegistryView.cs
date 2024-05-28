using StaffRegistry.constant;
using StaffRegistry.model;
using StaffRegistry.utility;
using System.Diagnostics.Contracts;

namespace StaffRegistry.view;

//TODO! Update view class to be more UI friendly!
//  1) Add animations
//  2) Add colors.
//TODO! Update view with custom console!
//TODO! Move parsing responsibility to the controller.
internal class StaffRegistryView
{
    internal void PrintWelcome()
    {
        try { Console.Clear(); } catch { };
        Console.WriteLine($"""

        ==============================================
         🍕🍕🍕 Welcome to my staff registry 🍕🍕🍕
        ==============================================

        """);
    }
    internal string ReadMenuSelection()
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

    internal string GetSelectedMenuItem(string selectedMenu) => selectedMenu switch
    {
        StaffMenu.ADD_STAFF or
        StaffMenu.GET_STAFF or
        StaffMenu.UPDATE_STAFF or
        StaffMenu.DELETE_STAFF or
        StaffMenu.LIST_ALL_STAFF or
        StaffMenu.EXIT => selectedMenu,
        _ => StaffMenu.DEFAULT
    };

    internal StaffInputData ReadNewStaffInput ()
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

    internal string ReadStaffIDToView()
    {
        return ReadStaffID("view");
    }

    internal string ReadStaffIDToDelete()
    {
        return ReadStaffID("delete");
    }

    internal string ReadStaffIDToUpdate()
    {
        return ReadStaffID("update");
    }

    private string ReadStaffID(string ops)
    {
        Console.Write($"\nEnter staff ID to {ops}: ");
        return Console.ReadLine() ?? "";
    }

    internal void PrintStaffAddedSuccessfully(
        PersonalData personalData,
        SoftwareITContract contract)
    {
        string dateOfBirth = DateUtility.ConvertTimeStampToDateString(personalData.DateOfBirth);
        Console.WriteLine($"""
        ✅ Staff {personalData.FName} {personalData.LName} {dateOfBirth} with salary {contract.Salary}$
        have been added to the registry.
        """);
    }

    internal void PrintStaffAddedUnsuccessfully()
    {
        Console.WriteLine("⚠️ Failure! Staff could not be added to the registry");
    }

    internal void PrintStaffAddedUnsuccessfully(
        PersonalData personalData,
        SoftwareITContract contract)
    {
        string dateOfBirth = DateUtility.ConvertTimeStampToDateString(personalData.DateOfBirth);
        Console.WriteLine($"""
        ⚠️ Failure! Staff {personalData.FName} {personalData.LName} {dateOfBirth} with salary {contract.Salary}$
        could not be added to the registry.
        """
        );
    }

    internal void PrintAllStaffEntries(IReadOnlyList<StaffEntity> staffEntries)
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

    internal void PrintInvalidMenuChoice()
    {
        Console.WriteLine("\n⚠️ Not valid selection");
    }

    internal void PrintGetStaffSuccessfully(StaffEntity staff)
    {
        string dateOfBirth = DateUtility.ConvertTimeStampToDateString(staff.DateOfBirth);
        Console.WriteLine($"""
        ✅ StaffID {staff.StaffID}: {staff.FName} {staff.LName} {dateOfBirth} with salary {staff.Salary}$.
        """);
    }

    internal void PrintGetStaffUnsuccessfully(string staffId)
    {
        Console.WriteLine($"\n⚠️ Could not get staff with id: {staffId}");
    }

    internal void PrintDeleteStaffUnsuccessfully(string staffId)
    {
        Console.WriteLine($"\n⚠️ Could not delete staff with id: {staffId}");
    }

    internal void PrintDeletedStaffSuccessfully(int staffId)
    {
        Console.WriteLine($"✅ StaffID {staffId} deleted.");
    }

    internal void PrintDeletedStaffUnsuccessfully(int staffId)
    {
        Console.WriteLine($"\n⚠️ Could not delete staff with id: {staffId}");
    }

    internal (string Id, StaffInputData Data) UpdateNewStaffInput()
    {
        Console.Write("\nUpdate staff (ID FirstName LastName Salary$ DateOfBirth(yyyy-mm-dd)): ");
        string staffStringData = Console.ReadLine() ?? "";
        string[] staffData = staffStringData.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        return (
            Id: staffData[0],
            Data: (
                FName: staffData[1],
                LName: staffData[2],
                Salary: staffData[3],
                DateOfBirth: staffData[4]
            )
        );
    }

    internal void PrintUpdatedStaffSuccessfully(StaffEntity staff)
    {
        Console.WriteLine($"✅ StaffID {staff.StaffID} updated.");
    }

    internal void PrintUpdatedStaffUnsuccessfully(int staffId)
    {
        Console.WriteLine($"\n⚠️ Could not update staff with id: {staffId}");
    }

    internal void PrintUpdatedStaffUnsuccessfully(string staffId)
    {
        Console.WriteLine($"\n⚠️ Could not update staff with id: {staffId}");
    }

    internal void PrintExit()
    {
        Console.WriteLine("\n🍕🍕🍕 Goodbye 🍕🍕🍕");
    }
}
