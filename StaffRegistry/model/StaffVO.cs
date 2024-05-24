namespace StaffRegistry.model;

public record class StaffVO(
    string FName,
    string LName,
    double Salary,
    string DateOfBirth
);
