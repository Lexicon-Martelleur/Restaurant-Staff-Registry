
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retaurant_Staff_Registry.model;

public class StaffRegistryService(IStaffRepository repository)
{
    public void AddStaff(Staff staff)
    {
        repository.AddStaff(staff);
    }

    public List<Staff> GetAllStaffEntries()
    {
        return repository.GetAllStaffEntries();
    }
}
