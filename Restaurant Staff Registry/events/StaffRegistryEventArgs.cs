using Retaurant_Staff_Registry.constant;
using Retaurant_Staff_Registry.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retaurant_Staff_Registry.events;

public class StaffRegistryEventArgs(
    RepositoryResult status,
    string msg,
    (string fname, string lname, double salary) data
    ) : EventArgs
{
    public RepositoryResult Status { get; } = status;

    public String Msg { get; } = msg;

    public (string fname, string lname, double salary) Data { get; } = data;
}
