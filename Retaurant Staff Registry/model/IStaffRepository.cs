﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retaurant_Staff_Registry.model;

public interface IStaffRepository
{
    public void AddStaff(Staff staff);

    public List<Staff> GetAllStaffEntries();
}