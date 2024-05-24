using StaffRegistry.model;

namespace StaffRegistry.infrastructure;

public class RegistryCSV : IStaffRepository
{
    private readonly string csvFile = "restaurant.csv";
    private readonly string csvDir = "resources";
    public void AddStaff(StaffEntity staff)
    {
        bool append = true;
        using StreamWriter writer = new StreamWriter(CreateFileIfNotExit(), append);
        var line = $"{staff.FName}," +
            $"{staff.LName}," +
            $"{staff.Salary}," +
            $"{staff.DateOfBirth}," +
            $"{staff.StaffID}";
        writer.WriteLine(line);
    }

    private string CreateFileIfNotExit()
    {
        try {
            CreateDirIfNotExit();
            string absolutePath = Path.Combine(
                Environment.CurrentDirectory,
                csvDir,
                csvFile);

            if (!File.Exists(absolutePath))
            {
                using (File.Create(absolutePath)) { }
            }
            return absolutePath;
        } catch
        {
            throw new Exception("Could not create a valid csv file");
        }
    }

    private void CreateDirIfNotExit()
    {
        string absolutePath = Path.Combine(
            Environment.CurrentDirectory,
            csvDir);

        if (!Path.Exists(absolutePath))
        {
            Directory.CreateDirectory(absolutePath);
        }
    }

    public IReadOnlyList<StaffEntity> GetAllStaffEntries()
    {
        List<StaffEntity> staffEntries = [];

        using (var reader = new StreamReader(CreateFileIfNotExit()))
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
