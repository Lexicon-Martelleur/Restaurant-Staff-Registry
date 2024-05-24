using Retaurant_Staff_Registry.factory;

StaffRegistryFactory factory = new();
var sqlRegistry = factory.CreateSqliteRegistry();
var inMeoryRegistry = factory.CreateInMemoryRegistry();

sqlRegistry.StartMenu();
