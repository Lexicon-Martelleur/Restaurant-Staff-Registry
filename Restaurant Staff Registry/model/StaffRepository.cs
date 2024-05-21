using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retaurant_Staff_Registry.model;

internal class StaffRepository : IStaffRepository
{
    private readonly List<Staff> _staffEntries = [];
    
    public void AddStaff(Staff staff)
    {
        _staffEntries.Add(staff);
    }

    public IReadOnlyList<Staff> GetAllStaffEntries()
    {
        return _staffEntries.AsReadOnly();
    }
}
