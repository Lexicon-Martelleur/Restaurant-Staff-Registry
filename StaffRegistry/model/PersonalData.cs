namespace StaffRegistry.model;

public record struct PersonalData(
    string FName,
    string LName,
    long DateOfBirth
);
