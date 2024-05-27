global using StaffInputData = (
    string FName,
    string LName,
    string Salary,
    string DateOfBirth);

global using AddStaffEventData = (
    StaffRegistry.model.PersonalData PersonalData,
    StaffRegistry.model.EmploymentContract EmploymentContract);

global using GetStaffEventData = (
    int StaffId,
    StaffRegistry.model.StaffEntity? Staff);
