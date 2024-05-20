# Restaurant-Staff-Registry

## 1. Klasser som bör ingå

- En **Staff** klass som beskriver en anställd.

- En **StaffRegistryService** som ansvarar för registrering av anställda.

- En **IStaffRepository** för abstraktion av datalagring, detta antas kommer att behövas.

- En **StaffRepository** som implementerar **IStaffRepository**, i denna implementation så lagras data endast i programmets minne.

- En **MenuView** klass för console I/O.

- En **MenuController** class för hantering av hur data tolkas och hanteras av programmet.

## 2. Klasser detaljer

- **Staff:** Bör vara en klass med egenskaper; namn, salary, och id.

-  **StaffRegistryService:** Bör ansvara för operationer som att lägga till personal samt lista all personal.
Kan sedan implementera en mer event driven programming för att kommunicera tillbaka events (e.g., resultat) till controller klass.

- **IStaffRepository samt StaffRepository:** Operationer för att spara och läsa data till programmet, abstraktion av dessa operationer gör det lättare att uppdatera hur data ska sparas.

- **MenuView** Bör ansvara för ett tydligt UI som uppskattas av användarna av programmet.

- **MenuController** Bör hantera och tolka indata och utdata från användaren för att sedan skicka denna data till Staff Registry Service.
Denna klass kan också vid utökning av programmet lyssna på event från **StaffRegistryService** för att kommunicera tillbaka mer tydligt till användaren.

## 3. Todo

- Se över testerna, för närvarande är testerna implementerade som *dummy tester* (tester utan värde).
