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
    ImagenUrl VARCHAR(MAX) NULL,
    Activo BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (IdMarca) REFERENCES Marcas(IdMarca),
    FOREIGN KEY (IdCategoria) REFERENCES Categorias(IdCategoria)
);
GO

-- Tabla Usuarios
CREATE TABLE Usuarios (
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    Clave VARCHAR(100) NOT NULL,
    Rol VARCHAR(50) NOT NULL, -- "Administrador" o "Cliente"
    Activo BIT NOT NULL DEFAULT 1
);
GO

-- Tabla MetodosPago
CREATE TABLE MetodosPago (
    IdMetodoPago INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1
);
GO

-- Tabla Ventas
CREATE TABLE Ventas (
    IdVenta INT IDENTITY(1,1) PRIMARY KEY,
    NumeroFactura VARCHAR(20) NOT NULL UNIQUE,
    IdUsuario INT NOT NULL,
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    Total DECIMAL(18,2) NOT NULL,
    IdMetodoPago INT NOT NULL,
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario),
    FOREIGN KEY (IdMetodoPago) REFERENCES MetodosPago(IdMetodoPago)
);
GO

-- Tabla DetalleVentas
CREATE TABLE DetalleVentas (
    IdDetalleVenta INT IDENTITY(1,1) PRIMARY KEY,
    IdVenta INT NOT NULL,
    IdProducto INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(18,2) NOT NULL,
    Subtotal DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (IdVenta) REFERENCES Ventas(IdVenta),
    FOREIGN KEY (IdProducto) REFERENCES Productos(IdProducto)
);
GO

-- Tabla Envios
CREATE TABLE Envios (
    IdEnvio INT IDENTITY(1,1) PRIMARY KEY,
    IdVenta INT NOT NULL,
    Direccion VARCHAR(250) NOT NULL,
    Localidad VARCHAR(100) NOT NULL,
    CodigoPostal VARCHAR(20) NOT NULL,
    NumeroSeguimiento VARCHAR(50) NULL UNIQUE,
    Estado VARCHAR(50) NOT NULL DEFAULT 'Pendiente', -- "Pendiente", "Despachado", "Entregado"
    FOREIGN KEY (IdVenta) REFERENCES Ventas(IdVenta)
);
GO

-- Insertar Categorías
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
('Redragon', 1),
('Logitech', 1),
('Kingston', 1),
('Hasbro', 1),
('Ruibal', 1),
('Generico', 1);
GO

-- Insertar Usuarios
INSERT INTO Usuarios (Nombre, Email, Clave, Rol, Activo) VALUES
('Administrador El Rincon', 'admin@elrincon.com', 'admin123', 'Administrador', 1),
('Milton Cliente', 'cliente@elrincon.com', 'cliente123', 'Cliente', 1);
GO

-- Insertar Métodos de Pago
INSERT INTO MetodosPago (Nombre, Activo) VALUES
('Tarjeta de Crédito', 1),
('Tarjeta de Débito', 1),
('Transferencia Bancaria', 1),
('Efectivo / Pago Fácil', 1);
GO

-- Insertar productos de todas las categorías con sus imágenes mock
INSERT INTO Productos (Nombre, IdMarca, IdCategoria, StockActual, ImagenUrl, Activo) VALUES
-- Categoría 1: Librería
('Hojas de carpeta rayadas x96', 1, 1, 80, 'https://images.unsplash.com/photo-1586075010923-2dd4570fb338?w=500&q=80', 1),
('Hojas de carpeta cuadriculadas x96', 1, 1, 70, 'https://images.unsplash.com/photo-1598520106830-8c45c2035160?w=500&q=80', 1),
('Carpeta N3 con anillos', 2, 1, 45, 'https://images.unsplash.com/photo-1600154039889-3568c4e5a72f?w=500&q=80', 1),
('Repuesto escolar N3 x48', 2, 1, 60, 'https://images.unsplash.com/photo-1586075010923-2dd4570fb338?w=500&q=80', 1),
('Lapicera azul punta media', 3, 1, 180, 'https://images.unsplash.com/photo-1583485088034-697b5bc54ccd?w=500&q=80', 1),
('Lapicera negra punta media', 3, 1, 160, 'https://images.unsplash.com/photo-1583485088034-697b5bc54ccd?w=500&q=80', 1),
('Lapiz negro HB', 4, 1, 150, 'https://images.unsplash.com/photo-1513542789411-b6a5d4f31634?w=500&q=80', 1),
('Goma blanca escolar', 4, 1, 100, 'https://images.unsplash.com/photo-1601887389937-0b02c26b6c3c?w=500&q=80', 1),
('Sacapuntas plastico', 4, 1, 90, 'https://images.unsplash.com/photo-1629810842232-a5482329241b?w=500&q=80', 1),
('Cuaderno universitario 80 hojas', 2, 1, 55, 'https://images.unsplash.com/photo-1531346878377-a5be20888e57?w=500&q=80', 1),

-- Categoría 2: Computación
('Mouse óptico USB Logitech M90', 6, 2, 30, 'https://images.unsplash.com/photo-1615663245857-ac93bb7c39e7?w=500&q=80', 1),
('Teclado de oficina Logitech K120', 6, 2, 20, 'https://images.unsplash.com/photo-1587829741301-dc798b83add3?w=500&q=80', 1),
('Pendrive 64GB Kingston DataTraveler', 7, 2, 50, 'https://images.unsplash.com/photo-1618424181497-157f25b6ddd5?w=500&q=80', 1),
('Disco Externo 1TB Kingston', 7, 2, 15, 'https://images.unsplash.com/photo-1544244015-0df4b3ffc6b0?w=500&q=80', 1),

-- Categoría 3: Gaming
('Auriculares Gamer Redragon Pandora', 5, 3, 25, 'https://images.unsplash.com/photo-1618384887929-16ec33fab9ef?w=500&q=80', 1),
('Joystick Inalámbrico Redragon Harrow', 5, 3, 18, 'https://images.unsplash.com/photo-1592840496694-26d035b52b48?w=500&q=80', 1),
('Teclado Mecánico RGB Redragon Kumara', 5, 3, 12, 'https://images.unsplash.com/photo-1601445638532-3c6f6c3aa1d6?w=500&q=80', 1),
('Mouse Gamer Redragon Cobra M711', 5, 3, 40, 'https://images.unsplash.com/photo-1615663245857-ac93bb7c39e7?w=500&q=80', 1),

-- Categoría 4: Juegos de mesa
('Monopoly Clásico Hasbro', 8, 4, 10, 'https://images.unsplash.com/photo-1610890716171-6b1bb98ffd09?w=500&q=80', 1),
('Jenga de madera clásico', 8, 4, 15, 'https://images.unsplash.com/photo-1596466846589-21d683be369a?w=500&q=80', 1),
('Ajedrez profesional Ruibal', 9, 4, 8, 'https://images.unsplash.com/photo-1529699211952-734e80c4d42b?w=500&q=80', 1),
('Carrera de Mente Ruibal', 9, 4, 12, 'https://images.unsplash.com/photo-1585504198199-20277593b94f?w=500&q=80', 1);
GO


