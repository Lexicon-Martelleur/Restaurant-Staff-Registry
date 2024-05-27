using System.Xml;

namespace StaffRegistry.utility;

internal static class FileUtility
{
    internal static string CreateFileIfNotExit(string file, string dir)
    {
        try
        {
            CreateDirIfNotExit(dir);
            string absolutePath = Path.Combine(Environment.CurrentDirectory, dir, file);

            if (!File.Exists(absolutePath))
            {
                using (File.Create(absolutePath)) { }
            }
            return absolutePath;
        }
        catch
        {
            throw new IOException("Could not create a valid json file");
        }
    }

    internal static string CreateJSONFileIfNotExit(string file, string dir)
    {
        try
        {
            CreateDirIfNotExit(dir);
            string absolutePath = Path.Combine(Environment.CurrentDirectory, dir, file);

            if (!File.Exists(absolutePath))
            {
                File.WriteAllText(absolutePath, "[]");
            }
            return absolutePath;
        }
        catch
        {
            throw new IOException("Could not create a valid json file");
        }
    }

    private static void CreateDirIfNotExit(string dir)
    {
        string absolutePath = Path.Combine(Environment.CurrentDirectory, dir);

        if (!Directory.Exists(absolutePath))
        {
            Directory.CreateDirectory(absolutePath);
        }
    }
}
