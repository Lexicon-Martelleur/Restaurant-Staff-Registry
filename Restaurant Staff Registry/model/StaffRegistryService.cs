
using Retaurant_Staff_Registry.constant;
using Retaurant_Staff_Registry.events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retaurant_Staff_Registry.model;

public class StaffRegistryService(IStaffRepository repository)
{
    private readonly HashSet<int> _staffIDs = [];

    public EventHandler<StaffRegistryEventArgs>? StaffRegistryEventHandler;
    public void AddStaff((string fname, string lname, double salary) staffItems)
    {
        try {
            Console.WriteLine($"staffItems {staffItems}");
            Staff staff = new(
                staffItems.fname,
                staffItems.lname,
                staffItems.salary,
                GetStaffID());
            repository.AddStaff(staff);
            OnAddStaffOk(staffItems);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            OnAddStaffFailure(staffItems, ex.Message);
        }
        catch
        {
            OnAddStaffFailure(staffItems, "Invalid property value");
        }
    }

    private int GetStaffID()
    {
        Random random = new();
        int id;
        do
        {
            id = random.Next(1, int.MaxValue);
        } while (!_staffIDs.Add(id));
        return id;
    }

    private void OnAddStaffOk((string fname, string lname, double salary) staffItems)
    {
        StaffRegistryEventHandler?.Invoke(this, new StaffRegistryEventArgs(
            RepositoryResult.ADD_STAFF_OK,
            "Staff registered OK",
            staffItems
        ));
    }

    private void OnAddStaffFailure(
        (string fname, string lname, double salary) staffItems,
        string msg)
    {
        StaffRegistryEventHandler?.Invoke(this, new StaffRegistryEventArgs(
            RepositoryResult.ADD_STAFF_FAILURE,
            msg,
            staffItems
        ));
    }

    public List<Staff> GetAllStaffEntries()
    {
        return repository.GetAllStaffEntries(); 
    }
}
