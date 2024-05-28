using System.Text;
using System.Text.Json;
using StaffRegistry.factory;
using StaffRegistry.model;
using StaffRegistry.utility;

namespace StaffRegistry.infrastructure;

internal class RegistryJSON : IStaffRepository
{
    private readonly string jsonFile = "restaurant.json";
    private readonly string jsonDir = "resources";

    public void AddStaff(StaffEntity staff)
    {
        List<StaffEntity> staffList = GetAllStaffEntries().ToList();
        staffList.Add(staff);

        JsonSerializerOptions option = new() { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(
            staffList,
            option);
        using StreamWriter writer = new(FileUtility.CreateFileIfNotExit(
            jsonFile,
            jsonDir));
        writer.WriteLine(jsonString);
    }

    public IReadOnlyList<StaffEntity> GetAllStaffEntries()
    {
        using StreamReader reader = new(FileUtility.CreateFileIfNotExit(
            jsonFile,
            jsonDir));
        string? line;
        StringBuilder jsonString = new();
        while ((line = reader.ReadLine()) != null)
        {
            jsonString.AppendLine(line);
        }
        List<StaffEntity> staffEntries = JsonSerializer.Deserialize<List<StaffEntity>>(
            jsonString.ToString()
        ) ?? [];
        return staffEntries.AsReadOnly();

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
