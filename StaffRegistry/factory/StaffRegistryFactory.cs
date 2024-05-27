using StaffRegistry.controller;
using StaffRegistry.model;
using StaffRegistry.infrastructure;
using StaffRegistry.view;

namespace StaffRegistry.factory;

internal class StaffRegistryFactory
{
    internal StaffRegistryController CreateInMemoryRegistry()
    {
        StaffFactory staffFactory = new();
        RegistryMemoryStorage storage = new();
        StaffRegistryService service = new(storage, staffFactory);
        StaffRegistryView view = new();
        return new(service, view);
    }

    internal StaffRegistryController CreateSqliteRegistry()
    {
        StaffFactory staffFactory = new();
        RegistrySqliteStorage storage = new(staffFactory);
        StaffRegistryService service = new(storage, staffFactory);
        StaffRegistryView view = new();
        return new(service, view);
    }

    internal StaffRegistryController CreateCSVRegistry()
    {
        StaffFactory staffFactory = new();
        RegistryCSV storage = new(staffFactory);
        StaffRegistryService service = new(storage, staffFactory);
        StaffRegistryView view = new();
        return new(service, view);
    }

    internal StaffRegistryController CreateJSONRegistry()
    {
        StaffFactory staffFactory = new();
        RegistryJSON storage = new();
        StaffRegistryService service = new(storage, staffFactory);
        StaffRegistryView view = new();
        return new(service, view);
    }
}
