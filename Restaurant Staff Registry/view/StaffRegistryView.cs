using Retaurant_Staff_Registry.constants;
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
    public MenuItem GetMenuInput()
    {
        Console.WriteLine("""
    Staff registry menu
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
        Console.Write("Enter new staff (Firstname Lastname Salary$): ");
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
        Console.WriteLine("""
            Goodbye
        """);
    }

    public void PrintInvalidMenuChoise()
    {
        Console.WriteLine("""
            Not a valid menu

        """);
    }

    public void PrintStaffAddedSuccessfully (Staff staff)
    {
        Console.WriteLine($"""
            Staff {staff.Fname} {staff.Lname} with salary {staff.Salary}$
            have been added to the registry.

        """);
    }

    public void PrintStaffAddedUnsuccessfully(Staff staff)
    {
        Console.WriteLine($"""
            Failure! Staff {staff.Fname} {staff.Lname} with salary {staff.Salary}$
            could not be added to the registry.
        
        """
        );
    }

    public void PrintAllStaffEntries(List<Staff> staffEntries)
    {
        foreach (var staff in staffEntries)
        {
            Console.WriteLine($"""
                * {staff.Fname} {staff.Lname}, salary {staff.Salary}$ 
            """);
        }
        Console.WriteLine("");
    }
}
