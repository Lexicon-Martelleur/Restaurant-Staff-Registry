using StaffRegistry.model;
using StaffRegistry.utility;

namespace StaffRegistry.infrastructure;

public class RegistryCSV : IStaffRepository
{
    private readonly string csvFile = "restaurant.csv";
    private readonly string csvDir = "resources";
    public void AddStaff(StaffEntity staff)
    {
        bool append = true;
        using StreamWriter writer = new StreamWriter(
            FileUtility.CreateFileIfNotExit(csvFile, csvDir),
            append);
        string line = $"{staff.FName}," +
            $"{staff.LName}," +
            $"{staff.Salary}," +
            $"{staff.DateOfBirth}," +
            $"{staff.StaffID}";
        writer.WriteLine(line);
    }

    public IReadOnlyList<StaffEntity> GetAllStaffEntries()
    {
        List<StaffEntity> staffEntries = [];

        using (var reader = new StreamReader(
            FileUtility.CreateFileIfNotExit(csvFile, csvDir)))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] values = line.Split(',');
                StaffEntity staff = new(
                    values[0],
                    values[1],
                    double.Parse(values[2]),
                    long.Parse(values[3]),
                    int.Parse(values[4])
                );
                staffEntries.Add(staff);
            }
        }
        return staffEntries.AsReadOnly();
    }
}
