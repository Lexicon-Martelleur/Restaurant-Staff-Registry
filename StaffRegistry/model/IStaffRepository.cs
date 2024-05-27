namespace StaffRegistry.model;

public interface IStaffRepository
{
    public void AddStaff(StaffEntity staff);

    public IReadOnlyList<StaffEntity> GetAllStaffEntries();

    public StaffEntity GetStaff(int id);

    public StaffEntity UpdateStaff(int id);

    public StaffEntity DeleteStaff(int id);
}
