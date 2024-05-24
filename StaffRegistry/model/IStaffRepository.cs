namespace StaffRegistry.model;

public interface IStaffRepository
{
    public void AddStaff(StaffEntity staff);

    public IReadOnlyList<StaffEntity> GetAllStaffEntries();
}
