using Retaurant_Staff_Registry.constant;
using Retaurant_Staff_Registry.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retaurant_Staff_Registry.events;

public class StaffRegistryEvent(
    RepositoryResult status, string msg, Staff data) : EventArgs
{
    public RepositoryResult Status { get; } = status;

    public String Msg { get; } = msg;

    public Staff Data { get; } = data;
}
