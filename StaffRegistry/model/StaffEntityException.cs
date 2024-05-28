namespace StaffRegistry.model;

internal class StaffEntityException : ArgumentOutOfRangeException
{
    internal StaffEntityException(
        string paraName, 
        string msg = "Invalid staff entity property"
    ) : base(paraName, msg) { }
}
