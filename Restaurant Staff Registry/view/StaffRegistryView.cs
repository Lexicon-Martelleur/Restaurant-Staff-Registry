using Retaurant_Staff_Registry.constant;
using Retaurant_Staff_Registry.model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Retaurant_Staff_Registry.view;

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
    public MenuItem GetMenuInput()
    {
        Console.WriteLine("""

    Staff registry menu
    ===================
        1) Add staff entry
        2) List all staff entries
        Q) Exit staff registry
    """);
        Console.Write("Select menu item: ");
        var selectedMenu = Console.ReadLine();
        return GetSelectedMenuItem(selectedMenu ?? "");
    }

    private MenuItem GetSelectedMenuItem(string selectedMenu) => selectedMenu switch
    {
        "1" => MenuItem.ADD_STAFF,
        "2" => MenuItem.LIST_ALL_STAFF,
        "q" => MenuItem.EXIT,
        "Q" => MenuItem.EXIT,
        _ => MenuItem.DEFAULT
    };

    public (string fname, string lname, double salary) GetStaffInput ()
    {
        Console.Write("\nEnter new staff (Firstname Lastname Salary$): ");
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

    public void PrintInvalidMenuChoise()
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

    public void PrintAllStaffEntries(List<Staff> staffEntries)
    {
        Console.WriteLine("\nList of registered staff:");
        
        if (staffEntries.Count == 0)
        {
            Console.WriteLine("🚀 Empty");
        }
        foreach (var staff in staffEntries)
        {
            Console.WriteLine($"""
                🚀 {staff.Fname} {staff.Lname}, salary {staff.Salary}$ 
            """);
        }
        Console.WriteLine("");
    }
}
