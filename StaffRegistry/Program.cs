using StaffRegistry.factory;

StaffRegistryFactory factory = new();
var sqliteRegistry = factory.CreateSqliteRegistry();
var inMemoryRegistry = factory.CreateInMemoryRegistry();
var csvRegistry = factory.CreateCSVRegistry();
var jsonRegistry = factory.CreateJSONRegistry();

sqliteRegistry.StartMenu();
