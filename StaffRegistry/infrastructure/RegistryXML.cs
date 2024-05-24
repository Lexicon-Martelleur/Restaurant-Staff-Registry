using System.Xml;
using StaffRegistry.model;
using StaffRegistry.utility;

namespace StaffRegistry.infrastructure;

public class RegistryXML : IStaffRepository
{
    private readonly string xmlFile = "restaurant.xml";
    private readonly string xmlDir = "resources";

    public void AddStaff(StaffEntity staff)
    {
        XmlDocument doc = new XmlDocument();
        string filePath = FileUtility.CreateFileIfNotExit(xmlFile, xmlDir);
        doc.Load(filePath);

        XmlElement staffElement = doc.CreateElement("StaffEntity");

        AppendChildElement(doc, staffElement, "FName", staff.FName);
        AppendChildElement(doc, staffElement, "LName", staff.LName);
        AppendChildElement(doc, staffElement, "Salary", staff.Salary.ToString());
        AppendChildElement(doc, staffElement, "DateOfBirth", staff.DateOfBirth.ToString());
        AppendChildElement(doc, staffElement, "StaffID", staff.StaffID.ToString());

        XmlElement? xmlElement = doc.DocumentElement;

        doc.DocumentElement?.AppendChild(staffElement);
        doc.Save(filePath);
    }

    private void AppendChildElement(XmlDocument doc, XmlElement parent, string name, string value)
    {
        XmlElement element = doc.CreateElement(name);
        element.InnerText = value;
        parent.AppendChild(element);
    }

    public IReadOnlyList<StaffEntity> GetAllStaffEntries()
    {
        List<StaffEntity> staffEntries = new();
        string filePath = FileUtility.CreateFileIfNotExit(xmlFile, xmlDir);

        XmlDocument doc = new();
        doc.Load(filePath);

        XmlElement? xmlElement = doc.DocumentElement;

        if (xmlElement == null)
        {
            return [];
        }
        foreach (XmlNode node in xmlElement.ChildNodes)
        {
            StaffEntity? staff = MapXMLNodeToStaffEntity(node);
            if (staff != null) { staffEntries.Add(staff); }
        }
        return staffEntries.AsReadOnly();
    }

    private StaffEntity? MapXMLNodeToStaffEntity(XmlNode node)
    {
        if (node is not XmlElement staffElement)
        {
            return null;
        }

        var FName = staffElement["FName"]?.InnerText ?? "invalid";
        var LName = staffElement["LName"]?.InnerText ?? "invalid";
        var Salary = staffElement["Salary"]?.InnerText ?? "0";
        var DateOfBirth = staffElement["DateOfBirth"]?.InnerText ?? "0";
        var StaffID = staffElement["StaffID"]?.InnerText ?? "0";

        return new StaffEntity(
            FName,
            LName,
            double.Parse(Salary),
            long.Parse(DateOfBirth),
            int.Parse(StaffID)
        );
    }
}
