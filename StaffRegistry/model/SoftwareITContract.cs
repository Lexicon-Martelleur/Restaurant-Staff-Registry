namespace StaffRegistry.model;

internal record struct SoftwareITContract(
    double Salary
) : EmploymentContract
{
    private readonly string _position = "System Development";

    private readonly string _department = "IT";

    public string Position { get => _position; }
    public string Department { get => _department; }
}
