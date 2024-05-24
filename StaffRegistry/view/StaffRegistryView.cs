using StaffRegistry.constant;
using StaffRegistry.model;
using StaffRegistry.utility;

namespace StaffRegistry.view;

//TODO! Update view class to be more UI friendly!
//  1) Add animations
//  2) Add colors.
public class StaffRegistryView
{
    public void PrintWelcome()
    {
        Console.Clear();
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
        (2) List all staff entries
        (Q) Exit staff registry
    """);
        Console.Write("Select menu item: ");
        var selectedMenu = Console.ReadLine();
        return GetSelectedMenuItem(selectedMenu ?? "");
    }

    private MenuItem GetSelectedMenuItem(string selectedMenu) => selectedMenu switch
    {
        "1" => MenuItem.ADD_STAFF,
        "2" => MenuItem.LIST_ALL_STAFF,
        "q" or "Q" => MenuItem.EXIT,
        _ => MenuItem.DEFAULT
    };

    public StaffVO ReadNewStaffInput ()
    {
        Console.Write("\nEnter new staff (FirstName LastName Salary$ DateOfBirth(yyyy-mm-dd)): ");
        string newStaffInput = Console.ReadLine() ?? "";
        string[] newStaff = newStaffInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        return new(
            newStaff[0],
            newStaff[1],
            double.Parse(newStaff[2]),
            newStaff[3]
        );
    }

    public void PrintExit()
    {
        Console.WriteLine("\n🍕🍕🍕 Goodbye 🍕🍕🍕");
    }

    public void PrintInvalidMenuChoice()
    {
        Console.WriteLine("\n⚠️ Not valid selection");
    }

    public void PrintStaffAddedSuccessfully (StaffVO staffData)
    {
        Console.WriteLine($"""
        ✅ Staff {staffData.FName} {staffData.LName} with salary {staffData.Salary}$
        have been added to the registry.
        """);
    }

    public void PrintStaffAddedUnsuccessfully()
    {
        Console.WriteLine("⚠️ Failure! Staff could not be added to the registry");
    }

    public void PrintStaffAddedUnsuccessfully(StaffVO staffData)
    {
        Console.WriteLine($"""
        ⚠️ Failure! Staff {staffData.FName} {staffData.LName} with salary {staffData.Salary}$
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
                🚀 {staff.FName} {staff.LName}, salary {staff.Salary}, date of birth {dateString}
            """);
        }
        Console.WriteLine("");
    }
}
