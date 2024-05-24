using System.Text.Json;
using StaffRegistry.model;
using StaffRegistry.utility;

namespace StaffRegistry.infrastructure;

public class RegistryJSON : IStaffRepository
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
        File.WriteAllText(
            FileUtility.CreateFileIfNotExit(jsonFile, jsonDir),
            jsonString);
    }

    public IReadOnlyList<StaffEntity> GetAllStaffEntries()
    {
        string jsonString = File.ReadAllText(FileUtility.CreateJSONFileIfNotExit(
            jsonFile,
            jsonDir));
        List<StaffEntity> staffList = JsonSerializer.Deserialize<List<StaffEntity>>(jsonString) ?? [];
        return staffList.AsReadOnly();
    }
}
