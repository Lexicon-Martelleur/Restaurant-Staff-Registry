namespace StaffRegistry.model;

public interface IStaffRepository
{
    public void AddStaff(StaffEntity staff);

    public IReadOnlyList<StaffEntity> GetAllStaffEntries();

    public StaffEntity GetStaff(int id);

    public int UpdateStaff(int staffId);

    public int DeleteStaff(int staffId);
}
