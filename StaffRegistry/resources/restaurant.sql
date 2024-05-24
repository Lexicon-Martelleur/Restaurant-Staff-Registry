-- create_staff_database.sql
CREATE TABLE Staff (

    Id INTEGER PRIMARY KEY,
    FirstName TEXT NOT NULL,
    LastName TEXT NOT NULL,
    Position TEXT NOT NULL,
    Department TEXT NOT NULL,
    Salary REAL NOT NULL,
    DateOfBirth INTEGER NOT NULL
);
