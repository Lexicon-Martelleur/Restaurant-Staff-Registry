using Retaurant_Staff_Registry.controller;
using Retaurant_Staff_Registry.model;
using Retaurant_Staff_Registry.Restaurant_Staff_Registry;
using Retaurant_Staff_Registry.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retaurant_Staff_Registry.factory;

internal class StaffRegistryFactory
{
    internal StaffRegistryController CreateInMemoryRegistry()
    {
        RestaurantMemoryStorage storage = new();
        StaffRegistryService service = new(storage);
        StaffRegistryView view = new();
        return new(service, view);
    }

    internal StaffRegistryController CreateSqliteRegistry()
    {
        RestaurantSqliteStorage storage = new();
        StaffRegistryService service = new(storage);
        StaffRegistryView view = new();
        return new(service, view);
    }
}
