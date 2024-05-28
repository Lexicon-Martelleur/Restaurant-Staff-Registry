global using StaffInputData = (
    string FName,
    string LName,
    string Salary,
    string DateOfBirth);

global using AddStaffEventData = (
    StaffRegistry.model.PersonalData PersonalData,
    StaffRegistry.model.SoftwareITContract EmploymentContract);

global using GetStaffEventData = (
    int StaffId,
    StaffRegistry.model.StaffEntity? Staff);

global using UpdateStaffEventData = (
    int StaffId,
    StaffRegistry.model.StaffEntity? Staff);
