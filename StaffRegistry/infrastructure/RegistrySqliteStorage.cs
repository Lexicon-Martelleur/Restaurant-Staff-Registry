using Microsoft.EntityFrameworkCore.ChangeTracking;
using StaffRegistry.Sqlite3;
using DB = StaffRegistry.EntityModels;
using StaffRegistry.model;
using StaffRegistry.factory;

namespace StaffRegistry.infrastructure;

public class RegistrySqliteStorage(StaffFactory staffFactory) : IStaffRepository
{
    public void AddStaff(StaffEntity staff)
    {
        using RestaurantDB db = new();

        DB.Staff staffEnityModel = new()
        {
            FirstName = staff.FName,
            LastName = staff.LName,
            Position = StaffEntity.Position,
            Department = StaffEntity.Department,
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

    public IReadOnlyList<StaffEntity> GetAllStaffEntries()
    {
        using RestaurantDB db = new();

        IQueryable<DB.Staff>? staff = db.Staff?
            .OrderByDescending(staff => staff.FirstName);

        return MapDBStaffEntriesToStaffList(staff);
    }

    private IReadOnlyList<StaffEntity> MapDBStaffEntriesToStaffList(
        IQueryable<DB.Staff>? staff)
    {
        if (staff is null || !staff.Any())
        {
            return [];
        }

        return staff
            .OrderByDescending(staff => staff.FirstName)
            .Select(staff => staffFactory.GetStaffEntity(
                staff.FirstName,
                staff.LastName,
                staff.Salary,
                staff.DateOfBirth,
                staff.Id
            ))
            .ToList()
            .AsReadOnly();
    }


    public StaffEntity GetStaff(int id)
    {
        using RestaurantDB db = new();
        DB.Staff? staff = db.Staff?.Find(id);
        Console.WriteLine($"staff {staff}");
        if (staff == null)
        {
            throw new Exception("Could not get staff from DB");
        }
        return MapDBStaffEntryToStaff(staff);
    }

    private StaffEntity MapDBStaffEntryToStaff(DB.Staff staff)
    {
        return staffFactory.GetStaffEntity(
            staff.FirstName,
            staff.LastName,
            staff.Salary,
            staff.DateOfBirth,
            staff.Id);
    }

    public StaffEntity UpdateStaff(int id)
    {
        throw new NotImplementedException();
    }

    public StaffEntity DeleteStaff(int id)
    {
        throw new NotImplementedException();
    }
}
