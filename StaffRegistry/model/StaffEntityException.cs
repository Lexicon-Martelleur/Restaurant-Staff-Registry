using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffRegistry.model;

internal class StaffEntityException : ArgumentOutOfRangeException
{
    internal StaffEntityException(
        string paraName, 
        string msg = "Invalid staff entity property"
    ) : base(paraName, msg) { }
}
