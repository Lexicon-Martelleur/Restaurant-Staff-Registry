using StaffRegistry.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

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

    internal static string CreateXMLFileIfNotExit(
        string xmlFile,
        string xmlDir,
        string rootElement)
    {
        try
        {
            CreateDirIfNotExit(xmlDir);
            string absolutePath = Path.Combine(Environment.CurrentDirectory, xmlDir, xmlFile);

            if (!File.Exists(absolutePath))
            {
                XmlDocument doc = new XmlDocument();
                XmlElement root = doc.CreateElement(rootElement);
                doc.AppendChild(root);
                doc.Save(absolutePath);
            }

            return absolutePath;
        }
        catch
        {
            throw new IOException("Could not create a valid xml file");
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
