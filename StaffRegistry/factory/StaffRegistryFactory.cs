using StaffRegistry.controller;
using StaffRegistry.model;
using StaffRegistry.infrastructure;
using StaffRegistry.view;

namespace StaffRegistry.factory;

internal class StaffRegistryFactory
{
    internal StaffRegistryController CreateInMemoryRegistry()
    {
        RegistryMemoryStorage storage = new();
        StaffRegistryService service = new(storage);
        StaffRegistryView view = new();
        return new(service, view);
    }

    internal StaffRegistryController CreateSqliteRegistry()
    {
        RegistrySqliteStorage storage = new();
        StaffRegistryService service = new(storage);
        StaffRegistryView view = new();
        return new(service, view);
    }
}
