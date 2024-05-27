using StaffRegistry.model;

namespace StaffRegistry.infrastructure;

public class RegistryMemoryStorage : IStaffRepository
{
    private readonly List<StaffEntity> _staffEntries = [];

    public void AddStaff(StaffEntity staff)
    {
        _staffEntries.Add(staff);
    }

    public IReadOnlyList<StaffEntity> GetAllStaffEntries()
    {
        return _staffEntries.AsReadOnly();
    }

    public StaffEntity GetStaff(int id)
    {
        throw new NotImplementedException();
    }

    public int DeleteStaff(int staffId)
    {
        throw new NotImplementedException();
    }

    public int UpdateStaff(int staffId)
    {
        throw new NotImplementedException();
    }
}

