-- Primero eliminamos las tablas en orden inverso a su creación (para evitar problemas de dependencias)
IF OBJECT_ID('dbo.Facturas', 'U') IS NOT NULL
    DROP TABLE [dbo].[Facturas];

IF OBJECT_ID('dbo.MetodosPago', 'U') IS NOT NULL
    DROP TABLE [dbo].[MetodosPago];

IF OBJECT_ID('dbo.EstadosFactura', 'U') IS NOT NULL
    DROP TABLE [dbo].[EstadosFactura];

IF OBJECT_ID('dbo.Reservas', 'U') IS NOT NULL
    DROP TABLE [dbo].[Reservas];

IF OBJECT_ID('dbo.EstadosReserva', 'U') IS NOT NULL
    DROP TABLE [dbo].[EstadosReserva];

IF OBJECT_ID('dbo.Tarifas', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tarifas];

IF OBJECT_ID('dbo.Canchas', 'U') IS NOT NULL
    DROP TABLE [dbo].[Canchas];

IF OBJECT_ID('dbo.Usuarios', 'U') IS NOT NULL
    DROP TABLE [dbo].[Usuarios];

IF OBJECT_ID('dbo.Roles', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];

-- Luego recreamos las tablas en el orden correcto
CREATE TABLE [dbo].[Roles]
(
    [IdRol] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Nombre] NVARCHAR(50) NOT NULL UNIQUE,
    [Descripcion] NVARCHAR(MAX) NULL
);

CREATE TABLE [dbo].[Usuarios]
(
    [IdUsuario] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [IdRol] INT NOT NULL,
    [NombreCompleto] NVARCHAR(100) NOT NULL,
    [Correo] NVARCHAR(100) NOT NULL UNIQUE,
    [Contrasena] NVARCHAR(255) NOT NULL,
    [Telefono] NVARCHAR(20) NULL,
    [FechaRegistro] DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT [FK_Usuarios_Roles] FOREIGN KEY ([IdRol]) REFERENCES [dbo].[Roles]([IdRol])
);

CREATE TABLE [dbo].[Canchas]
(
    [IdCancha] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Nombre] NVARCHAR(100) NOT NULL UNIQUE,
    [TipoCancha] NVARCHAR(50) NULL,
    [Ubicacion] NVARCHAR(255) NULL,
    [Descripcion] NVARCHAR(MAX) NULL,
    [Activa] BIT NOT NULL DEFAULT 1,
    [FechaCreacion] DATETIME NOT NULL DEFAULT GETDATE(),
    [FechaActualizacion] DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE [dbo].[Tarifas]
(
    [IdTarifa] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [IdCancha] INT NULL,
    [HoraInicio] TIME NOT NULL,
    [HoraFin] TIME NOT NULL,
    [DiaSemana] NVARCHAR(20) NOT NULL CHECK (DiaSemana IN ('Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado', 'Domingo', 'Todos')),
    [Valor] DECIMAL(10, 2) NOT NULL,
    [Descripcion] NVARCHAR(255) NULL,
    [Activa] BIT NOT NULL DEFAULT 1,
    CONSTRAINT [FK_Tarifas_Canchas] FOREIGN KEY ([IdCancha]) REFERENCES [dbo].[Canchas]([IdCancha])
);

CREATE TABLE [dbo].[EstadosReserva]
(
    [IdEstadoReserva] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Nombre] NVARCHAR(50) NOT NULL UNIQUE,
    [Descripcion] NVARCHAR(MAX) NULL
);

CREATE TABLE [dbo].[Reservas]
(
    [IdReserva] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [IdCliente] INT NOT NULL,
    [IdCancha] INT NOT NULL,
    [FechaReserva] DATE NOT NULL,
    [HoraInicio] TIME NOT NULL,
    [HoraFin] TIME NOT NULL,
    [ValorTotal] DECIMAL(10, 2) NOT NULL,
    [IdEstadoReserva] INT NOT NULL,
    [FechaCreacion] DATETIME NOT NULL DEFAULT GETDATE(),
    [FechaConfirmacion] DATETIME NULL,
    CONSTRAINT [FK_Reservas_Usuarios] FOREIGN KEY ([IdCliente]) REFERENCES [dbo].[Usuarios]([IdUsuario]),
    CONSTRAINT [FK_Reservas_Canchas] FOREIGN KEY ([IdCancha]) REFERENCES [dbo].[Canchas]([IdCancha]),
    CONSTRAINT [FK_Reservas_EstadosReserva] FOREIGN KEY ([IdEstadoReserva]) REFERENCES [dbo].[EstadosReserva]([IdEstadoReserva])
);

CREATE TABLE [dbo].[EstadosFactura]
(
    [IdEstadoFactura] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Nombre] NVARCHAR(50) NOT NULL UNIQUE,
    [Descripcion] NVARCHAR(MAX) NULL
);

CREATE TABLE [dbo].[MetodosPago]
(
    [IdMetodoPago] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Nombre] NVARCHAR(50) NOT NULL UNIQUE,
    [Descripcion] NVARCHAR(MAX) NULL
);

CREATE TABLE [dbo].[Facturas]
(
    [IdFactura] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [IdReserva] INT NOT NULL UNIQUE,
    [IdCliente] INT NOT NULL,
    [IdEstadoFactura] INT NOT NULL,
    [IdMetodoPago] INT NULL,
    [NumeroFactura] NVARCHAR(50) NOT NULL UNIQUE,
    [FechaEmision] DATETIME NOT NULL DEFAULT GETDATE(),
    [ValorTotal] DECIMAL(10, 2) NOT NULL,
    [FechaPago] DATETIME NULL,
    CONSTRAINT [FK_Facturas_Reservas] FOREIGN KEY ([IdReserva]) REFERENCES [dbo].[Reservas]([IdReserva]),
    CONSTRAINT [FK_Facturas_Usuarios] FOREIGN KEY ([IdCliente]) REFERENCES [dbo].[Usuarios]([IdUsuario]),
    CONSTRAINT [FK_Facturas_EstadosFactura] FOREIGN KEY ([IdEstadoFactura]) REFERENCES [dbo].[EstadosFactura]([IdEstadoFactura]),
    CONSTRAINT [FK_Facturas_MetodosPago] FOREIGN KEY ([IdMetodoPago]) REFERENCES [dbo].[MetodosPago]([IdMetodoPago])
);