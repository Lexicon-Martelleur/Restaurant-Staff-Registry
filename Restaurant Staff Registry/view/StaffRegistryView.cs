using Retaurant_Staff_Registry.constant;
using Retaurant_Staff_Registry.model;

namespace Retaurant_Staff_Registry.view;

//TODO! Update view class to be more UI friendly!
//  1) Add animations
//  2) Add colors.
public class StaffRegistryView
{
    public void PrintWelcome()
    {
        Console.Clear();
        Console.WriteLine($"""

        ========================================================
         🍕🍕🍕 Welcome to my restaurant staff registry 🍕🍕🍕
        ========================================================

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

    public (string fname, string lname, double salary) ReadNewStaffInput ()
    {
        Console.Write("\nEnter new staff (FirstName LastName Salary$): ");
        string newStaffInput = Console.ReadLine() ?? "";
        string[] newStaff = newStaffInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        return (
            fname: newStaff[0],
            lname: newStaff[1],
            salary: double.Parse(newStaff[2])
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

    public void PrintStaffAddedSuccessfully ((
        string fname,
        string lname,
        double salary) staffItems)
    {
        Console.WriteLine($"""
        ✅ Staff {staffItems.fname} {staffItems.lname} with salary {staffItems.salary}$
        have been added to the registry.
        """);
    }

    public void PrintStaffAddedUnsuccessfully()
    {
        Console.WriteLine("⚠️ Failure! Staff could not be added to the registry");
    }

    public void PrintStaffAddedUnsuccessfully((
        string fname,
        string lname,
        double salary) staffItems)
    {
        Console.WriteLine($"""
        ⚠️ Failure! Staff {staffItems.fname} {staffItems.lname} with salary {staffItems.salary}$
        could not be added to the registry.
        """
        );
    }

    public void PrintAllStaffEntries(IReadOnlyList<Staff> staffEntries)
    {
        Console.WriteLine("\nList of registered staff:");
        
        if (staffEntries.Count == 0)
        {
            Console.WriteLine("🚀 Empty");
        }
        foreach (var staff in staffEntries)
        {
            Console.WriteLine($"""
                🚀 {staff.FName} {staff.LName}, salary {staff.Salary}$ 
            """);
        }
        Console.WriteLine("");
    }
}
