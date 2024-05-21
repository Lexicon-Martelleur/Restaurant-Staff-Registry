﻿using Retaurant_Staff_Registry.controller;
using Retaurant_Staff_Registry.model;
using Retaurant_Staff_Registry.view;

StaffRepository repository = new();
StaffRegistryService service = new(repository);
StaffRegistryView view = new();
StaffRegistryController menuController = new(service, view);
menuController.StartMenu();
