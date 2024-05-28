using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffRegistry.model;

internal interface EmploymentContract
{
    double Salary { get; }
    string Position { get; }
    string Department { get; }
}
