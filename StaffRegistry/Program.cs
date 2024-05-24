using StaffRegistry.factory;

StaffRegistryFactory factory = new();
var sqlRegistry = factory.CreateSqliteRegistry();
var inMeoryRegistry = factory.CreateInMemoryRegistry();
var csvStorage = factory.CreateCSVRegistry();

csvStorage.StartMenu();
