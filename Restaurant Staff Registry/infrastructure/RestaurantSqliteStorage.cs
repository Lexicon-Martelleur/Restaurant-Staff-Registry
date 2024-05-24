using Microsoft.EntityFrameworkCore.ChangeTracking;
using Model = Retaurant_Staff_Registry.model;
using Restaurant.Sqlite3;
using DB = Restaurant.EntityModels;
using Restaurant.EntityModels;

namespace Retaurant_Staff_Registry.Restaurant_Staff_Registry;

internal class RestaurantSqliteStorage : Model.IStaffRepository
{
    public void AddStaff(Model.StaffEntity staff)
    {
        using RestaurantDB db = new();

        DB.Staff staffEnityModel = new()
        {
            FirstName = staff.FName,
            LastName = staff.LName,
            Position = Model.StaffEntity.Position,
            Department = Model.StaffEntity.Department,
            DateOfBirth = staff.DateOfBirth,
            Salary = staff.Salary,
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

    public IReadOnlyList<Model.StaffEntity> GetAllStaffEntries()
    {
        using RestaurantDB db = new();

        IQueryable<DB.Staff>? staff = db.Staff?
            .OrderByDescending(staff => staff.FirstName);

        return ConvertDBStaffResultToImmutableList(staff);
    }

    private IReadOnlyList<Model.StaffEntity> ConvertDBStaffResultToImmutableList(
        IQueryable<DB.Staff>? staff)
    {
        if (staff is null || !staff.Any())
        {
            return [];
        }

        return staff
            .OrderByDescending(staff => staff.FirstName)
            .Select(staff => new Model.StaffEntity(
                staff.FirstName,
                staff.LastName,
                staff.Salary,
                staff.DateOfBirth,
                staff.Id
            ))
            .ToList()
            .AsReadOnly();
    }
}
