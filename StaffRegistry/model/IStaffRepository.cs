namespace StaffRegistry.model;

internal interface IStaffRepository
{
    void AddStaff(StaffEntity staff);

    IReadOnlyList<StaffEntity> GetAllStaffEntries();

    StaffEntity GetStaff(int id);

    int UpdateStaff(int staffId);

    int DeleteStaff(int staffId);
}
