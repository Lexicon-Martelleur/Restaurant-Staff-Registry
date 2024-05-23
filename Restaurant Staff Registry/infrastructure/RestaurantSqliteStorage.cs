using Microsoft.EntityFrameworkCore.ChangeTracking;
using Model = Retaurant_Staff_Registry.model;
using Restaurant.Sqlite3;
using DB = Restaurant.EntityModels;
using Restaurant.EntityModels;

namespace Retaurant_Staff_Registry.Restaurant_Staff_Registry;

internal class RestaurantSqliteStorage : Model.IStaffRepository
{
    public void AddStaff(Model.Staff staff)
    {
        using RestaurantDB db = new();

        DB.Staff staffEnityModel = new()
        {
            FirstName = staff.FName,
            LastName = staff.LName,
            Position = "System Developer",
            Department = "IT",
            DateOfBirth = "?????"
        };

        AddStaffToDBChangeTracking(db, staffEnityModel);
        SaveTrackedChangesToDB(db);
    }

    private void AddStaffToDBChangeTracking (RestaurantDB db, DB.Staff staffEntityModel)
    {
        if (db.Staff is null)
        {
            return;
        }
        EntityEntry<DB.Staff> entity = db.Staff.Add(staffEntityModel);
        Console.WriteLine($"State: {entity.State}, StaffId: {staffEntityModel.Id}");
    }

    private void SaveTrackedChangesToDB(RestaurantDB db)
    {
        int affected = db.SaveChanges();
        Console.WriteLine($"affected {affected}");
    }

    public IReadOnlyList<Model.Staff> GetAllStaffEntries()
    {
        using RestaurantDB db = new();

        IQueryable<DB.Staff>? staff = db.Staff?
            .OrderByDescending(staff => staff.FirstName);

        return ConvertDBStaffResultToImmutableList(staff);
    }

    private IReadOnlyList<Model.Staff> ConvertDBStaffResultToImmutableList(
        IQueryable<DB.Staff>? staff)
    {
        if (staff is null || !staff.Any())
        {
            return [];
        }

        int mockSalary = 111;

        return staff
            .OrderByDescending(staff => staff.FirstName)
            .Select(staff => new Model.Staff(
                staff.FirstName,
                staff.LastName,
                mockSalary,
                staff.Id
            ))
            .ToList()
            .AsReadOnly();
    }
}
