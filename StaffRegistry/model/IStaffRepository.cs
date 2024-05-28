namespace StaffRegistry.model;

internal interface IStaffRepository
{
    void AddStaff(StaffEntity staff);

    IReadOnlyList<StaffEntity> GetAllStaffEntries();

    StaffEntity GetStaff(int id);

    StaffEntity UpdateStaff(StaffEntity staffEntity);

    int DeleteStaff(int staffId);
}
