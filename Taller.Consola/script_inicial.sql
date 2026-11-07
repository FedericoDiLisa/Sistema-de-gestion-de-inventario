-- 1. Si no existe la base de datos TallerDB, la creamos
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'TallerDB')
BEGIN
    CREATE DATABASE TallerDB;
END
GO

USE TallerDB;
GO

-- 2. Si no existe la tabla Insumos, la creamos
IF OBJECT_ID('Insumos', 'U') IS NULL
BEGIN
    CREATE TABLE Insumos (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Nombre NVARCHAR(100) NOT NULL,
        Categoria NVARCHAR(50) NOT NULL,
        PrecioUnitario DECIMAL(18,2) NOT NULL,
        StockActual INT NOT NULL,
        StockMinimo INT NOT NULL
    );
END
GO