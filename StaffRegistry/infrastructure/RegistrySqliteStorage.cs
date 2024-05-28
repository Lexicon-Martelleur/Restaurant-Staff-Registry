using Microsoft.EntityFrameworkCore.ChangeTracking;
using StaffRegistry.Sqlite3;
using DB = StaffRegistry.EntityModels;
using StaffRegistry.model;
using StaffRegistry.factory;

namespace StaffRegistry.infrastructure;

internal class RegistrySqliteStorage(StaffFactory staffFactory) : IStaffRepository
{
    public void AddStaff(StaffEntity staff)
    {
        using StaffRegistryDB db = new();

        DB.Staff staffEnityModel = new()
        {
            FirstName = staff.FName,
            LastName = staff.LName,
            Position = staff.Position,
            Department = staff.Department,
            DateOfBirth = staff.DateOfBirth,
            Salary = staff.Salary,
        };

        AddStaffToDBChangeTracking(db, staffEnityModel);
        SaveTrackedChangesToDB(db);
    }

    private void AddStaffToDBChangeTracking (StaffRegistryDB db, DB.Staff staffEntityModel)
    {
        if (db.Staff is null)
        {
            return;
        }
        EntityEntry<DB.Staff> entity = db.Staff.Add(staffEntityModel);
    }

    private void SaveTrackedChangesToDB(StaffRegistryDB db)
    {
        int affected = db.SaveChanges();
    }

    public IReadOnlyList<StaffEntity> GetAllStaffEntries()
    {
        using StaffRegistryDB db = new();

        IQueryable<DB.Staff>? staff = db.Staff?
            .OrderByDescending(staff => staff.FirstName);

        return MapDBStaffEntriesToStaffEntityList(staff);
    }

    private IReadOnlyList<StaffEntity> MapDBStaffEntriesToStaffEntityList(
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
        using StaffRegistryDB db = new();
        DB.Staff? staff = db.Staff?.Find(id);
        Console.WriteLine($"staff {staff}");
        if (staff == null)
        {
            throw new Exception("Could not get staff from DB");
        }
        return MapDBStaffEntryToStaffEntity(staff);
    }

    private StaffEntity MapDBStaffEntryToStaffEntity(DB.Staff staff)
    {
        return staffFactory.GetStaffEntity(
            staff.FirstName,
            staff.LastName,
            staff.Salary,
            staff.DateOfBirth,
            staff.Id);
    }

    public int DeleteStaff(int staffId)
    {
        using StaffRegistryDB db = new();
        DB.Staff staffEntry = new() { Id = staffId };
        db.Attach(staffEntry);
        db.Staff?.Remove(staffEntry);

        #if DEBUG
        db.ChangeTracker.DetectChanges();
        Console.WriteLine("db.ChangeTracker.DebugView.LongView");
        Console.WriteLine(db.ChangeTracker.DebugView.LongView);
        #endif

        db.SaveChanges();
        return staffId;
    }

    public StaffEntity UpdateStaff(StaffEntity staffEntity)
    {
        using StaffRegistryDB db = new();
        DB.Staff staffEntry = MapStaffEntityToDBStaffEntry(staffEntity);
        db.Staff?.Update(staffEntry);

        #if DEBUG
        db.ChangeTracker.DetectChanges();
        Console.WriteLine("db.ChangeTracker.DebugView.LongView");
        Console.WriteLine(db.ChangeTracker.DebugView.LongView);
        #endif

        db.SaveChanges();
        return staffEntity;
    }

    private DB.Staff MapStaffEntityToDBStaffEntry(StaffEntity staffEntity)
    {
        return new DB.Staff()
        {
            Id = staffEntity.StaffID,
            FirstName = staffEntity.FName,
            LastName = staffEntity.LName,
            Salary = staffEntity.Salary,
            Position = staffEntity.Position,
            Department = staffEntity.Department,
            DateOfBirth = staffEntity.DateOfBirth,
        };
    }
}
