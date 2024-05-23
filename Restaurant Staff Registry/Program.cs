using Restaurant.Sqlite3;
using Retaurant_Staff_Registry.controller;
using Retaurant_Staff_Registry.model;
using Retaurant_Staff_Registry.Restaurant_Staff_Registry;
using Retaurant_Staff_Registry.view;

// RestaurantMemoryStorage storage = new();
RestaurantSqliteStorage storage = new();
StaffRegistryService service = new(storage);
StaffRegistryView view = new();
StaffRegistryController menuController = new(service, view);
menuController.StartMenu();
