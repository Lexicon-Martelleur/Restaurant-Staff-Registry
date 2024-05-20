// See https://aka.ms/new-console-template for more information
using Retaurant_Staff_Registry.controller;
using Retaurant_Staff_Registry.model;
using Retaurant_Staff_Registry.view;

Console.WriteLine("""

==========================
Welcome to staff registry!
==========================

""");

StaffRepository repository = new();
StaffRegistryService service = new(repository);
MenuView view = new();
MenuController menuController = new(service, view);
menuController.StartStaffRegistryMenu();


