
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
    public EventHandler<StaffRegistryEvent>? StaffRegistryEvent;
    public void AddStaff(Staff staff)
    {
        if (
            staff.Fname.Length < Staff.MIN_NAME_SIZE || staff.Fname.Length > Staff.MAX_NAME_SIZE ||
            staff.Fname.Length < Staff.MIN_NAME_SIZE || staff.Fname.Length > Staff.MAX_NAME_SIZE ||
            staff.Salary < Staff.MIN_SALARY || staff.Salary > Staff.MAX_SALARY)
        {
            OnAddStaffFailure(staff, "Invalid staff value");
        } else
        {
            repository.AddStaff(staff);
            OnAddStaffOk(staff);
        }
    }

    private void OnAddStaffOk(Staff staff)
    {
        StaffRegistryEvent?.Invoke(this, new StaffRegistryEvent(
            RepositoryResult.ADD_STAFF_OK,
            "Staff registred ok",
            staff
        ));
    }

    private void OnAddStaffFailure(Staff staff, string msg)
    {
        StaffRegistryEvent?.Invoke(this, new StaffRegistryEvent(
            RepositoryResult.ADD_STAFF_FAILURE,
            msg,
            staff
        ));
    }

    public List<Staff> GetAllStaffEntries()
    {
        return repository.GetAllStaffEntries(); 
    }
}
