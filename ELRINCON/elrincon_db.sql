-- Crear base de datos
CREATE DATABASE ElRinconDelPapelDB;
GO

USE ElRinconDelPapelDB;
GO

-- Tabla Categorias
CREATE TABLE Categorias (
    IdCategoria INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1
);
GO

-- Tabla Marcas
CREATE TABLE Marcas (
    IdMarca INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1
);
GO

-- Tabla Productos
CREATE TABLE Productos (
    IdProducto INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    IdMarca INT NOT NULL,
    IdCategoria INT NOT NULL,
    StockActual INT NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (IdMarca) REFERENCES Marcas(IdMarca),
    FOREIGN KEY (IdCategoria) REFERENCES Categorias(IdCategoria)
);
GO

-- Insertar Categorías (Librería, Computación, Gaming, Juegos de mesa)
INSERT INTO Categorias (Nombre, Activo) VALUES 
('Librería', 1),
('Computación', 1),
('Gaming', 1),
('Juegos de mesa', 1);
GO

-- Insertar Marcas
INSERT INTO Marcas (Nombre, Activo) VALUES 
('Rivadavia', 1),
('Éxito', 1),
('Bic', 1),
('Faber-Castell', 1),
('Generico', 1);
GO

-- Insertar exactamente 10 productos de la categoría Librería (IdCategoria = 1)
INSERT INTO Productos (Nombre, IdMarca, IdCategoria, StockActual, Activo) VALUES
('Hojas de carpeta rayadas x96', 1, 1, 80, 1),
('Hojas de carpeta cuadriculadas x96', 1, 1, 70, 1),
('Carpeta N3 con anillos', 2, 1, 45, 1),
('Repuesto escolar N3 x48', 2, 1, 60, 1),
('Lapicera azul punta media', 3, 1, 180, 1),
('Lapicera negra punta media', 3, 1, 160, 1),
('Lapiz negro HB', 4, 1, 150, 1),
('Goma blanca escolar', 4, 1, 100, 1),
('Sacapuntas plastico', 4, 1, 90, 1),
('Cuaderno universitario 80 hojas', 2, 1, 55, 1);
GO
