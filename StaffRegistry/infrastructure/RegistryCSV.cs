using StaffRegistry.factory;
using StaffRegistry.model;
using StaffRegistry.utility;

namespace StaffRegistry.infrastructure;

public class RegistryCSV(StaffFactory staffFactory) : IStaffRepository
{
    private readonly string csvFile = "restaurant.csv";

    private readonly string csvDir = "resources";
    public void AddStaff(StaffEntity staff)
    {
        bool append = true;
        using StreamWriter writer = new(
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
        using StreamReader reader = new(
            FileUtility.CreateFileIfNotExit(csvFile, csvDir)
        );
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] values = line.Split(',');
            StaffEntity staff = staffFactory.GetStaffEntity(
                values[0],
                values[1],
                double.Parse(values[2]),
                long.Parse(values[3]),
                int.Parse(values[4])
            );
            staffEntries.Add(staff);
        }
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
