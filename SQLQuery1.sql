﻿CREATE TABLE Products
(
    [Id] UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
    [Name] NVARCHAR (100) NOT NULL,
    [Price]  DECIMAL (9,2) NOT NULL
);
