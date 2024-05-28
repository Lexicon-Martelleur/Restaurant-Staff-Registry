﻿using StaffRegistry.constant;
using StaffRegistry.EntityModels;
using StaffRegistry.events;
using StaffRegistry.factory;

namespace StaffRegistry.model;

internal class StaffRegistryService(IStaffRepository repository, StaffFactory staffFactory)
{
    internal EventHandler<
        StaffRegistryEventArgs<AddStaffEventData>
    >? AddStaffEventHandler;

    internal EventHandler<
        StaffRegistryEventArgs<GetStaffEventData>
    >? GetStaffEventHandler;

    internal EventHandler<StaffRegistryEventArgs<int>>? DeleteStaffEventHandler;

    internal EventHandler<
        StaffRegistryEventArgs<UpdateStaffEventData>
    >? UpdateStaffEventHandler;

    internal void AddStaff(PersonalData personalData, SoftwareITContract contract)
    {
        try {
            StaffEntity staff = staffFactory.CreateStaffEntity(
                personalData.FName,
                personalData.LName,
                contract.Salary,
                personalData.DateOfBirth);
            repository.AddStaff(staff);
            OnAddStaffOk(personalData, contract);
        }
        catch (StaffEntityException ex)
        {
            OnAddStaffFailure(personalData, contract, ex.Message);
            
        }
        catch
        {
            OnAddStaffFailure(personalData, contract, "Invalid property value");
        }
    }

    private void OnAddStaffOk(
        PersonalData data,
        SoftwareITContract contract)
    {
        StaffRegistryEventArgs<AddStaffEventData> eventArgs = new(
            RepositoryResult.OK,
            "Staff registered OK",
            (PersonalData: data, EmploymentContract: contract));
        AddStaffEventHandler?.Invoke(this, eventArgs);
    }

    private void OnAddStaffFailure(
        PersonalData data,
        SoftwareITContract contract,
        string msg)
    {
        StaffRegistryEventArgs<AddStaffEventData> eventArgs = new(
            RepositoryResult.FAILURE,
            msg,
            (PersonalData: data, EmploymentContract: contract));
        AddStaffEventHandler?.Invoke(this, eventArgs);
    }

    internal void GetStaff(int staffId)
    {
        try
        {
            StaffEntity staff = repository.GetStaff(staffId);
            OnGetStaffOk(staff);
        } catch
        {
            OnGetStaffFailure(staffId);
        }
    }

    private void OnGetStaffOk(StaffEntity staff)
    {
        StaffRegistryEventArgs<GetStaffEventData> eventArgs = new(
            RepositoryResult.OK,
            "Get staff OK",
            (StaffId: staff.StaffID, Staff: staff));
        GetStaffEventHandler?.Invoke(this, eventArgs);
    }

    private void OnGetStaffFailure(int staffId)
    {
        StaffRegistryEventArgs<GetStaffEventData> eventArgs = new(
            RepositoryResult.FAILURE,
            "Get staff failure",
            (StaffId: staffId, Staff: null));
        GetStaffEventHandler?.Invoke(this, eventArgs);
    }

    internal IReadOnlyList<StaffEntity> GetAllStaffEntries()
    {
        return repository.GetAllStaffEntries(); 
    }

    internal void DeleteStaff(int staffId)
    {
        try
        {
            repository.DeleteStaff(staffId); 
            OnDeleteStaffOk(staffId);
        }
        catch
        {
            OnDeleteStaffFailure(staffId);
        }
    }

    private void OnDeleteStaffOk(int staffId)
    {
        StaffRegistryEventArgs<int> eventArgs = new(
            RepositoryResult.OK,
            "Delete staff OK",
            staffId);
        DeleteStaffEventHandler?.Invoke(this, eventArgs);
    }

    private void OnDeleteStaffFailure(int staffId)
    {
        StaffRegistryEventArgs<int> eventArgs = new(
            RepositoryResult.FAILURE,
            "Delete staff failure",
            staffId);
        DeleteStaffEventHandler?.Invoke(this, eventArgs);
    }

    internal void UpdateStaff(
        int staffId,
        PersonalData personalData,
        SoftwareITContract contract)
    {
        try
        {
            StaffEntity staff = staffFactory.GetStaffEntity(
                personalData.FName,
                personalData.LName,
                contract.Salary,
                personalData.DateOfBirth,
                staffId);
            repository.UpdateStaff(staff);
            OnUpdateStaffOk(staff);
        }
        catch
        {
            OnUpdateStaffFailure(staffId);
        }
    }

    private void OnUpdateStaffOk(StaffEntity staff)
    {
        StaffRegistryEventArgs<UpdateStaffEventData> eventArgs = new(
            RepositoryResult.OK,
            "Update staff OK",
            (StaffId: staff.StaffID, Staff: staff));
        UpdateStaffEventHandler?.Invoke(this, eventArgs);
    }

    private void OnUpdateStaffFailure(int staffId)
    {
        StaffRegistryEventArgs<UpdateStaffEventData> eventArgs = new(
            RepositoryResult.FAILURE,
            "Update staff failure",
            (StaffId: staffId, Staff: null));
        UpdateStaffEventHandler?.Invoke(this, eventArgs);
    }
}
