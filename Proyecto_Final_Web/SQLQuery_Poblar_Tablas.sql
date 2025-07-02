-- 1. Primero activar IDENTITY_INSERT para las tablas con columnas de identidad
SET IDENTITY_INSERT [dbo].[Roles] ON;
INSERT INTO [dbo].[Roles] ([IdRol], [Nombre], [Descripcion]) VALUES
(1, 'gerente', 'Tiene control total sobre el sistema, incluyendo la gestión de canchas y usuarios.'),
(2, 'administrador', 'Gestiona las reservas, tarifas y clientes de su(s) cancha(s) asignada(s).'),
(3, 'empleado', 'Asiste en la gestión de reservas y atención al cliente.'),
(4, 'cliente', 'Puede registrarse, consultar disponibilidad y realizar reservas.');
SET IDENTITY_INSERT [dbo].[Roles] OFF;

-- 2. Insertar usuarios (no necesitamos IDENTITY_INSERT aquí porque no especificamos IdUsuario)
INSERT INTO [dbo].[Usuarios] ([IdRol], [NombreCompleto], [Correo], [Contrasena], [Telefono], [FechaRegistro]) VALUES
-- Gerente
(1, 'William David Diaz Gonzalez', 'estudiante@udistrital.edu.co', 'root', '3101234567', '2025-06-01 10:00:00'),
-- Administradores
(2, 'Laura Gomez', 'laura.admin1@outlook.com', 'admin123', '3001000001', '2025-06-02 09:15:00'),
(2, 'Carlos Ruiz', 'carlos.admin2@outlook.com', 'admin123', '3011000002', '2025-06-02 09:20:00'),
(2, 'Ana Ramirez', 'ana.admin3@outlook.com', 'admin123', '3021000003', '2025-06-02 09:25:00'),
(2, 'Diego Torres', 'diego.admin4@outlook.com', 'admin123', '3031000004', '2025-06-02 09:30:00'),
(2, 'Sofia Vargas', 'sofia.admin5@outlook.com', 'admin123', '3041000005', '2025-06-02 09:35:00'),
(2, 'Felipe Castro', 'felipe.admin6@outlook.com', 'admin123', '3051000006', '2025-06-02 09:40:00'),
(2, 'Isabella Moreno', 'isabella.admin7@outlook.com', 'admin123', '3061000007', '2025-06-02 09:45:00'),
(2, 'Juan Perez', 'juan.admin8@outlook.com', 'admin123', '3071000008', '2025-06-02 09:50:00'),
(2, 'Valeria Sánchez', 'valeria.admin9@outlook.com', 'admin123', '3081000009', '2025-06-02 09:55:00'),
(2, 'Ricardo Martinez', 'ricardo.admin10@outlook.com', 'admin123', '3091000010', '2025-06-02 10:00:00'),
-- Empleados
(3, 'Paula Lopez', 'paula.emp1@gmail.com', 'empleado123', '3112000001', '2025-06-03 08:00:00'),
(3, 'Andres Diaz', 'andres.emp2@gmail.com', 'empleado123', '3122000002', '2025-06-03 08:05:00'),
(3, 'Maria Restrepo', 'maria.emp3@gmail.com', 'empleado123', '3132000003', '2025-06-03 08:10:00'),
(3, 'Jose Ospina', 'jose.emp4@gmail.com', 'empleado123', '3142000004', '2025-06-03 08:15:00'),
(3, 'Natalia Quintero', 'natalia.emp5@gmail.com', 'empleado123', '3102000005', '2025-06-03 08:20:00'),
(3, 'Daniel Parra', 'daniel.emp6@gmail.com', 'empleado123', '3002000006', '2025-06-03 08:25:00'),
(3, 'Valentina Cortes', 'valentina.emp7@gmail.com', 'empleado123', '3012000007', '2025-06-03 08:30:00'),
(3, 'Mario Herrera', 'mario.emp8@gmail.com', 'empleado123', '3022000008', '2025-06-03 08:35:00'),
(3, 'Daniela Rojas', 'daniela.emp9@gmail.com', 'empleado123', '3032000009', '2025-06-03 08:40:00'),
(3, 'Santiago Velez', 'santiago.emp10@gmail.com', 'empleado123', '3042000010', '2025-06-03 08:45:00'),
(3, 'Laura Pineda', 'laura.emp11@gmail.com', 'empleado123', '3052000011', '2025-06-03 08:50:00'),
(3, 'Sebastian Guzmán', 'sebastian.emp12@gmail.com', 'empleado123', '3062000012', '2025-06-03 08:55:00'),
(3, 'Carolina Marin', 'carolina.emp13@gmail.com', 'empleado123', '3072000013', '2025-06-03 09:00:00'),
(3, 'Esteban Giraldo', 'esteban.emp14@gmail.com', 'empleado123', '3082000014', '2025-06-03 09:05:00'),
(3, 'Camila Soto', 'camila.emp15@gmail.com', 'empleado123', '3092000015', '2025-06-03 09:10:00'),
(3, 'Juan David Pardo', 'juand.emp16@gmail.com', 'empleado123', '3102000016', '2025-06-03 09:15:00'),
(3, 'Sara Rodriguez', 'sara.emp17@gmail.com', 'empleado123', '3112000017', '2025-06-03 09:20:00'),
(3, 'Mateo Bernal', 'mateo.emp18@gmail.com', 'empleado123', '3122000018', '2025-06-03 09:25:00'),
(3, 'Mariana Vargas', 'mariana.emp19@gmail.com', 'empleado123', '3132000019', '2025-06-03 09:30:00'),
(3, 'Alejandro Cruz', 'alejo.emp20@gmail.com', 'empleado123', '3142000020', '2025-06-03 09:35:00'),
-- Clientes 
(4, 'Miguel Ángel Ramírez', 'miguel.ramirez@gmail.com', 'cliente123', '3103456789', '2025-05-01 10:00:00'),
(4, 'Luisa Fernanda Mendoza', 'luisa.mendoza@gmail.com', 'cliente123', '3114567890', '2025-05-02 11:00:00'),
(4, 'Carlos Andrés Herrera', 'carlos.herrera@gmail.com', 'cliente123', '3125678901', '2025-05-03 12:00:00'),
(4, 'Ana María Torres', 'ana.torres@gmail.com', 'cliente123', '3136789012', '2025-05-04 13:00:00'),
(4, 'Jorge Luis Silva', 'jorge.silva@gmail.com', 'cliente123', '3147890123', '2025-05-05 14:00:00'),
(4, 'Diana Carolina Rojas', 'diana.rojas@gmail.com', 'cliente123', '3158901234', '2025-05-06 15:00:00'),
(4, 'Oscar Javier Gutiérrez', 'oscar.gutierrez@gmail.com', 'cliente123', '3169012345', '2025-05-07 16:00:00'),
(4, 'Patricia Elena Castro', 'patricia.castro@gmail.com', 'cliente123', '3170123456', '2025-05-08 17:00:00'),
(4, 'Fernando José Peña', 'fernando.pena@gmail.com', 'cliente123', '3181234567', '2025-05-09 18:00:00'),
(4, 'Gabriela Alejandra Mora', 'gabriela.mora@gmail.com', 'cliente123', '3192345678', '2025-05-10 19:00:00'),
(4, 'Ricardo Antonio Jiménez', 'ricardo.jimenez@gmail.com', 'cliente123', '3203456789', '2025-05-11 10:30:00'),
(4, 'Sandra Milena Ruiz', 'sandra.ruiz@gmail.com', 'cliente123', '3214567890', '2025-05-12 11:30:00'),
(4, 'Hernán Darío Vargas', 'hernan.vargas@gmail.com', 'cliente123', '3225678901', '2025-05-13 12:30:00'),
(4, 'Claudia Marcela Pardo', 'claudia.pardo@gmail.com', 'cliente123', '3236789012', '2025-05-14 13:30:00'),
(4, 'Alberto José Ríos', 'alberto.rios@gmail.com', 'cliente123', '3247890123', '2025-05-15 14:30:00'),
(4, 'Mónica Patricia Acosta', 'monica.acosta@gmail.com', 'cliente123', '3258901234', '2025-05-16 15:30:00'),
(4, 'Gustavo Adolfo Medina', 'gustavo.medina@gmail.com', 'cliente123', '3269012345', '2025-05-17 16:30:00'),
(4, 'Liliana María Franco', 'liliana.franco@gmail.com', 'cliente123', '3270123456', '2025-05-18 17:30:00'),
(4, 'Jairo Alonso Suárez', 'jairo.suarez@gmail.com', 'cliente123', '3281234567', '2025-05-19 18:30:00'),
(4, 'Adriana Lucía Guzmán', 'adriana.guzman@gmail.com', 'cliente123', '3292345678', '2025-05-20 19:30:00'),
(4, 'Mauricio Alejandro López', 'mauricio.lopez@gmail.com', 'cliente123', '3303456789', '2025-05-21 10:45:00'),
(4, 'Tatiana Andrea Méndez', 'tatiana.mendez@gmail.com', 'cliente123', '3314567890', '2025-05-22 11:45:00'),
(4, 'Fabián Orlando Gómez', 'fabian.gomez@gmail.com', 'cliente123', '3325678901', '2025-05-23 12:45:00'),
(4, 'Natalia Andrea Castillo', 'natalia.castillo@gmail.com', 'cliente123', '3336789012', '2025-05-24 13:45:00'),
(4, 'Héctor Manuel Díaz', 'hector.diaz@gmail.com', 'cliente123', '3347890123', '2025-05-25 14:45:00'),
(4, 'María Camila Restrepo', 'maria.restrepo@gmail.com', 'cliente123', '3358901234', '2025-05-26 15:45:00'),
(4, 'Diego Armando Cárdenas', 'diego.cardenas@gmail.com', 'cliente123', '3369012345', '2025-05-27 16:45:00'),
(4, 'Laura Vanessa Arango', 'laura.arango@gmail.com', 'cliente123', '3370123456', '2025-05-28 17:45:00'),
(4, 'Juan Esteban Orozco', 'juan.orozco@gmail.com', 'cliente123', '3381234567', '2025-05-29 18:45:00'),
(4, 'Paola Andrea Zapata', 'paola.zapata@gmail.com', 'cliente123', '3392345678', '2025-05-30 19:45:00'),
(4, 'Andrés Felipe Quintero', 'andres.quintero@gmail.com', 'cliente123', '3403456789', '2025-05-31 10:15:00'),
(4, 'Carolina Andrea Osorio', 'carolina.osorio@gmail.com', 'cliente123', '3414567890', '2025-06-01 11:15:00'),
(4, 'Julián David Mesa', 'julian.mesa@gmail.com', 'cliente123', '3425678901', '2025-06-02 12:15:00'),
(4, 'Sara Marcela Valencia', 'sara.valencia@gmail.com', 'cliente123', '3436789012', '2025-06-03 13:15:00'),
(4, 'Camilo Andrés Agudelo', 'camilo.agudelo@gmail.com', 'cliente123', '3447890123', '2025-06-04 14:15:00'),
(4, 'Daniela Alejandra Londoño', 'daniela.londono@gmail.com', 'cliente123', '3458901234', '2025-06-05 15:15:00'),
(4, 'Sebastián Felipe Muñoz', 'sebastian.munoz@gmail.com', 'cliente123', '3469012345', '2025-06-06 16:15:00'),
(4, 'Valentina Sánchez', 'valentina.sanchez@gmail.com', 'cliente123', '3470123456', '2025-06-07 17:15:00'),
(4, 'Juan Pablo Vélez', 'juan.velez@gmail.com', 'cliente123', '3481234567', '2025-06-08 18:15:00'),
(4, 'María José Giraldo', 'maria.giraldo@gmail.com', 'cliente123', '3492345678', '2025-06-09 19:15:00'),
(4, 'David Santiago Cardona', 'david.cardona@gmail.com', 'cliente123', '3503456789', '2025-06-10 10:20:00'),
(4, 'Lina María Arias', 'lina.arias@gmail.com', 'cliente123', '3514567890', '2025-06-11 11:20:00'),
(4, 'Cristian Camilo Parra', 'cristian.parra@gmail.com', 'cliente123', '3525678901', '2025-06-12 12:20:00'),
(4, 'Angie Tatiana Ríos', 'angie.rios@gmail.com', 'cliente123', '3536789012', '2025-06-13 13:20:00'),
(4, 'Jhon Fredy Zapata', 'jhon.zapata@gmail.com', 'cliente123', '3547890123', '2025-06-14 14:20:00'),
(4, 'Yulieth Andrea Carmona', 'yulieth.carmona@gmail.com', 'cliente123', '3558901234', '2025-06-15 15:20:00'),
(4, 'Edwin Alexander Rojas', 'edwin.rojas@gmail.com', 'cliente123', '3569012345', '2025-06-16 16:20:00'),
(4, 'Jenny Paola Jiménez', 'jenny.jimenez@gmail.com', 'cliente123', '3570123456', '2025-06-17 17:20:00'),
(4, 'Alexander Bermúdez', 'alexander.bermudez@gmail.com', 'cliente123', '3581234567', '2025-06-18 18:20:00'),
(4, 'Karen Dayana Pérez', 'karen.perez@gmail.com', 'cliente123', '3592345678', '2025-06-19 19:20:00'),
(4, 'Milton Andrés Castaño', 'milton.castano@gmail.com', 'cliente123', '3603456789', '2025-06-20 10:25:00'),
(4, 'Natalia Andrea Bedoya', 'natalia.bedoya@gmail.com', 'cliente123', '3614567890', '2025-06-21 11:25:00'),
(4, 'Cristian David Henao', 'cristian.henao@gmail.com', 'cliente123', '3625678901', '2025-06-22 12:25:00'),
(4, 'Laura Sofía Uribe', 'laura.uribe@gmail.com', 'cliente123', '3636789012', '2025-06-23 13:25:00'),
(4, 'Juan David Montoya', 'juand.montoya@gmail.com', 'cliente123', '3647890123', '2025-06-24 14:25:00'),
(4, 'Sandra Patricia Lopera', 'sandra.lopera@gmail.com', 'cliente123', '3658901234', '2025-06-25 15:25:00'),
(4, 'Carlos Mario Arboleda', 'carlos.arboleda@gmail.com', 'cliente123', '3669012345', '2025-06-26 16:25:00'),
(4, 'Diana Marcela Palacio', 'diana.palacio@gmail.com', 'cliente123', '3670123456', '2025-06-27 17:25:00'),
(4, 'Javier Alonso Ramírez', 'javier.ramirez@gmail.com', 'cliente123', '3681234567', '2025-06-28 18:25:00'),
(4, 'Paula Andrea Gallego', 'paula.gallego@gmail.com', 'cliente123', '3692345678', '2025-06-29 19:25:00'),
(4, 'Mario Fernando Salazar', 'mario.salazar@gmail.com', 'cliente123', '3703456789', '2025-06-30 10:35:00'),
(4, 'Luisa Fernanda Cardona', 'luisa.cardona@gmail.com', 'cliente123', '3714567890', '2025-07-01 11:35:00'),
(4, 'Óscar Iván Valencia', 'oscar.valencia@gmail.com', 'cliente123', '3725678901', '2025-07-02 12:35:00'),
(4, 'María Alejandra Marín', 'maria.marin@gmail.com', 'cliente123', '3736789012', '2025-07-03 13:35:00'),
(4, 'Felipe Andrés Correa', 'felipe.correa@gmail.com', 'cliente123', '3747890123', '2025-07-04 14:35:00'),
(4, 'Alejandra María Soto', 'alejandra.soto@gmail.com', 'cliente123', '3758901234', '2025-07-05 15:35:00'),
(4, 'Gustavo Alonso Vélez', 'gustavo.velez@gmail.com', 'cliente123', '3769012345', '2025-07-06 16:35:00'),
(4, 'Andrea Carolina Rangel', 'andrea.rangel@gmail.com', 'cliente123', '3770123456', '2025-07-07 17:35:00'),
(4, 'Jorge Iván Mejía', 'jorge.mejia@gmail.com', 'cliente123', '3781234567', '2025-07-08 18:35:00'),
(4, 'Sara Lucía Cadavid', 'sara.cadavid@gmail.com', 'cliente123', '3792345678', '2025-07-09 19:35:00'),
(4, 'Diego Alejandro Londoño', 'diego.londono@gmail.com', 'cliente123', '3803456789', '2025-07-10 10:40:00'),
(4, 'Catalina Morales', 'catalina.morales@gmail.com', 'cliente123', '3814567890', '2025-07-11 11:40:00'),
(4, 'Juan Manuel Gutiérrez', 'juan.gutierrez@gmail.com', 'cliente123', '3825678901', '2025-07-12 12:40:00'),
(4, 'Laura Cristina Posada', 'laura.posada@gmail.com', 'cliente123', '3836789012', '2025-07-13 13:40:00'),
(4, 'Andrés Mauricio Ríos', 'andres.rios@gmail.com', 'cliente123', '3847890123', '2025-07-14 14:40:00'),
(4, 'María Fernanda Zapata', 'maria.zapata@gmail.com', 'cliente123', '3858901234', '2025-07-15 15:40:00'),
(4, 'Camilo Andrés Franco', 'camilo.franco@gmail.com', 'cliente123', '3869012345', '2025-07-16 16:40:00'),
(4, 'Paola Andrea Duque', 'paola.duque@gmail.com', 'cliente123', '3870123456', '2025-07-17 17:40:00'),
(4, 'Julián Andrés Ospina', 'julian.ospina@gmail.com', 'cliente123', '3881234567', '2025-07-18 18:40:00'),
(4, 'Natalia Andrea Giraldo', 'natalia.giraldo@gmail.com', 'cliente123', '3892345678', '2025-07-19 19:40:00'),
(4, 'Santiago Molina', 'santiago.molina@gmail.com', 'cliente123', '3903456789', '2025-07-20 10:50:00'),
(4, 'Valeria García', 'valeria.garcia@gmail.com', 'cliente123', '3914567890', '2025-07-21 11:50:00'),
(4, 'Juan Carlos Restrepo', 'juan.restrepo@gmail.com', 'cliente123', '3925678901', '2025-07-22 12:50:00'),
(4, 'Daniela Patricia López', 'daniela.lopez@gmail.com', 'cliente123', '3936789012', '2025-07-23 13:50:00'),
(4, 'Carlos Andrés Montoya', 'carlos.montoya@gmail.com', 'cliente123', '3947890123', '2025-07-24 14:50:00'),
(4, 'María Paula Arenas', 'maria.arenas@gmail.com', 'cliente123', '3958901234', '2025-07-25 15:50:00'),
(4, 'Felipe Andrés Osorio', 'felipe.osorio@gmail.com', 'cliente123', '3969012345', '2025-07-26 16:50:00'),
(4, 'Luisa María Carmona', 'luisa.carmona@gmail.com', 'cliente123', '3970123456', '2025-07-27 17:50:00'),
(4, 'Juan Diego Arcila', 'juand.arcila@gmail.com', 'cliente123', '3981234567', '2025-07-28 18:50:00'),
(4, 'Ana Sofía Velásquez', 'ana.velasquez@gmail.com', 'cliente123', '3992345678', '2025-07-29 19:50:00');

-- 3. Insertar canchas
INSERT INTO [dbo].[Canchas] ([Nombre], [TipoCancha], [Ubicacion], [Descripcion], [Activa], [FechaCreacion], [FechaActualizacion]) VALUES
('Cancha Futbol 5 - Norte', 'Fútbol 5', 'Calle 100 # 15-50, Bogotá', 'Cancha de fútbol 5 con grama sintética de última generación.', 1, '2025-06-01 10:00:00', '2025-06-01 10:00:00'),
('Cancha Baloncesto - Sur', 'Baloncesto', 'Carrera 30 # 3-20, Bogotá', 'Cancha cubierta de baloncesto con piso de madera.', 1, '2025-06-01 10:00:00', '2025-06-01 10:00:00'),
('Cancha Tenis 1 - Occidente', 'Tenis', 'Avenida Boyacá # 70-80, Bogotá', 'Cancha de tenis de polvo de ladrillo.', 1, '2025-06-01 10:00:00', '2025-06-01 10:00:00'),
('Cancha Futbol 7 - Oriente', 'Fútbol 7', 'Carrera 7 # 170-10, Bogotá', 'Cancha de fútbol 7 al aire libre con iluminación.', 1, '2025-06-01 10:00:00', '2025-06-01 10:00:00'),
('Cancha Voleibol - Centro', 'Voleibol', 'Calle 19 # 5-30, Bogotá', 'Cancha de voleibol cubierta, ideal para entrenamientos.', 1, '2025-06-01 10:00:00', '2025-06-01 10:00:00'),
('Cancha Squash 1 - Norte', 'Squash', 'Calle 127 # 7-40, Bogotá', 'Cancha de squash profesional con paredes de vidrio.', 1, '2025-06-01 10:00:00', '2025-06-01 10:00:00'),
('Cancha Futbol 5 - Chapinero', 'Fútbol 5', 'Calle 59 # 13-10, Bogotá', 'Cancha de fútbol 5 con grama natural y graderías.', 1, '2025-06-01 10:00:00', '2025-06-01 10:00:00'),
('Cancha Baloncesto - Suba', 'Baloncesto', 'Avenida Ciudad de Cali # 139-00, Bogotá', 'Cancha de baloncesto al aire libre en complejo deportivo.', 1, '2025-06-01 10:00:00', '2025-06-01 10:00:00'),
('Cancha Tenis 2 - Occidente', 'Tenis', 'Calle 26 # 68-50, Bogotá', 'Cancha de tenis rápida (cemento).', 1, '2025-06-01 10:00:00', '2025-06-01 10:00:00'),
('Cancha Futbol 7 - Usme', 'Fútbol 7', 'Carrera 14 # 97-40, Bogotá', 'Cancha de fútbol 7 con césped sintético y buenos vestuarios.', 1, '2025-06-01 10:00:00', '2025-06-01 10:00:00');

-- 4. Insertar tarifas
INSERT INTO [dbo].[Tarifas] ([IdCancha], [HoraInicio], [HoraFin], [DiaSemana], [Valor], [Descripcion], [Activa]) VALUES
(NULL, '06:00:00', '12:00:00', 'Todos', 60000.00, 'Tarifa Diurna (6 AM - 12 PM)', 1),
(NULL, '12:00:00', '18:00:00', 'Lunes', 75000.00, 'Tarifa Tarde Lunes-Viernes (12 PM - 6 PM)', 1),
(NULL, '12:00:00', '18:00:00', 'Martes', 75000.00, 'Tarifa Tarde Lunes-Viernes (12 PM - 6 PM)', 1),
(NULL, '12:00:00', '18:00:00', 'Miércoles', 75000.00, 'Tarifa Tarde Lunes-Viernes (12 PM - 6 PM)', 1),
(NULL, '12:00:00', '18:00:00', 'Jueves', 75000.00, 'Tarifa Tarde Lunes-Viernes (12 PM - 6 PM)', 1),
(NULL, '12:00:00', '18:00:00', 'Viernes', 75000.00, 'Tarifa Tarde Lunes-Viernes (12 PM - 6 PM)', 1),
(NULL, '18:00:00', '22:00:00', 'Lunes', 90000.00, 'Tarifa Noche Lunes-Jueves (6 PM - 10 PM)', 1),
(NULL, '18:00:00', '22:00:00', 'Martes', 90000.00, 'Tarifa Noche Lunes-Jueves (6 PM - 10 PM)', 1),
(NULL, '18:00:00', '22:00:00', 'Miércoles', 90000.00, 'Tarifa Noche Lunes-Jueves (6 PM - 10 PM)', 1),
(NULL, '18:00:00', '22:00:00', 'Jueves', 90000.00, 'Tarifa Noche Lunes-Jueves (6 PM - 10 PM)', 1),
(NULL, '06:00:00', '22:00:00', 'Sábado', 100000.00, 'Tarifa Fin de Semana - Sábado', 1),
(NULL, '06:00:00', '22:00:00', 'Domingo', 95000.00, 'Tarifa Fin de Semana - Domingo', 1),
(1, '18:00:00', '22:00:00', 'Todos', 110000.00, 'Tarifa Noche Premium Futbol 5 Norte', 1);

-- 5. Activar IDENTITY_INSERT para EstadosReserva
SET IDENTITY_INSERT [dbo].[EstadosReserva] ON;
INSERT INTO [dbo].[EstadosReserva] ([IdEstadoReserva], [Nombre], [Descripcion]) VALUES
(1, 'pre-reservado', 'La reserva ha sido creada, pero el pago aún no se ha confirmado.'),
(2, 'pagado', 'La reserva ha sido confirmada y el pago se ha procesado con éxito.'),
(3, 'cancelado', 'La reserva ha sido anulada por el cliente o el administrador.'),
(4, 'finalizado', 'La fecha y hora de la reserva ya han pasado.');
SET IDENTITY_INSERT [dbo].[EstadosReserva] OFF;

-- 6. Activar IDENTITY_INSERT para EstadosFactura
SET IDENTITY_INSERT [dbo].[EstadosFactura] ON;
INSERT INTO [dbo].[EstadosFactura] ([IdEstadoFactura], [Nombre], [Descripcion]) VALUES
(1, 'pendiente', 'La factura ha sido emitida, pero el pago no se ha recibido.'),
(2, 'pagado', 'El pago de la factura ha sido recibido y procesado.'),
(3, 'vencido', 'La fecha límite de pago de la factura ha pasado y no se ha recibido el pago.'),
(4, 'anulado', 'La factura ha sido cancelada.');
SET IDENTITY_INSERT [dbo].[EstadosFactura] OFF;

-- 7. Activar IDENTITY_INSERT para MetodosPago
SET IDENTITY_INSERT [dbo].[MetodosPago] ON;
INSERT INTO [dbo].[MetodosPago] ([IdMetodoPago], [Nombre], [Descripcion]) VALUES
(1, 'Efectivo', 'Pago realizado en efectivo en el establecimiento.'),
(2, 'Tarjeta Debito', 'Pago realizado con tarjeta de débito.'),
(3, 'Tarjeta Credito', 'Pago realizado con tarjeta de crédito.');
SET IDENTITY_INSERT [dbo].[MetodosPago] OFF;

-- *** 8. Inserción de Reservas y Facturas ***
-- Primero necesitamos obtener algunos IDs de clientes y canchas
-- Reserva 1: Finalizada
INSERT INTO [dbo].[Reservas] ([IdCliente], [IdCancha], [FechaReserva], [HoraInicio], [HoraFin], [ValorTotal], [IdEstadoReserva], [FechaCreacion], [FechaConfirmacion]) VALUES
(101, 1, '2025-06-05', '19:00:00', '20:00:00', 90000.00, 4, '2025-06-01 15:00:00', '2025-06-01 15:10:00');

INSERT INTO [dbo].[Facturas] ([IdReserva], [IdCliente], [IdEstadoFactura], [IdMetodoPago], [NumeroFactura], [FechaEmision], [ValorTotal], [FechaPago]) VALUES
(1, 101, 2, 3, 'FAC-0001', '2025-06-01 15:00:00', 90000.00, '2025-06-01 15:10:00');

-- Reserva 2: Finalizada
INSERT INTO [dbo].[Reservas] ([IdCliente], [IdCancha], [FechaReserva], [HoraInicio], [HoraFin], [ValorTotal], [IdEstadoReserva], [FechaCreacion], [FechaConfirmacion]) VALUES
(111, 2, '2025-06-06', '10:00:00', '11:00:00', 60000.00, 4, '2025-06-02 11:00:00', '2025-06-02 11:15:00');

INSERT INTO [dbo].[Facturas] ([IdReserva], [IdCliente], [IdEstadoFactura], [IdMetodoPago], [NumeroFactura], [FechaEmision], [ValorTotal], [FechaPago]) VALUES
(2, 111, 2, 1, 'FAC-0002', '2025-06-02 11:00:00', 60000.00, '2025-06-02 11:15:00');

-- Reserva 3: Pagada (futura)
INSERT INTO [dbo].[Reservas] ([IdCliente], [IdCancha], [FechaReserva], [HoraInicio], [HoraFin], [ValorTotal], [IdEstadoReserva], [FechaCreacion], [FechaConfirmacion]) VALUES
(102, 3, '2025-06-20', '15:00:00', '16:00:00', 75000.00, 2, '2025-06-10 09:00:00', '2025-06-10 09:30:00');

INSERT INTO [dbo].[Facturas] ([IdReserva], [IdCliente], [IdEstadoFactura], [IdMetodoPago], [NumeroFactura], [FechaEmision], [ValorTotal], [FechaPago]) VALUES
(3, 102, 2, 2, 'FAC-0003', '2025-06-10 09:00:00', 75000.00, '2025-06-10 09:30:00');

-- Reserva 4: Pagada (futura)
INSERT INTO [dbo].[Reservas] ([IdCliente], [IdCancha], [FechaReserva], [HoraInicio], [HoraFin], [ValorTotal], [IdEstadoReserva], [FechaCreacion], [FechaConfirmacion]) VALUES
(112, 1, '2025-06-22', '20:00:00', '21:00:00', 110000.00, 2, '2025-06-11 14:00:00', '2025-06-11 14:10:00');

INSERT INTO [dbo].[Facturas] ([IdReserva], [IdCliente], [IdEstadoFactura], [IdMetodoPago], [NumeroFactura], [FechaEmision], [ValorTotal], [FechaPago]) VALUES
(4, 112, 2, 3, 'FAC-0004', '2025-06-11 14:00:00', 110000.00, '2025-06-11 14:10:00');

-- Reserva 5: Pre-reservada
INSERT INTO [dbo].[Reservas] ([IdCliente], [IdCancha], [FechaReserva], [HoraInicio], [HoraFin], [ValorTotal], [IdEstadoReserva], [FechaCreacion], [FechaConfirmacion]) VALUES
(103, 2, '2025-06-25', '19:00:00', '20:00:00', 90000.00, 1, '2025-06-12 10:00:00', NULL);

INSERT INTO [dbo].[Facturas] ([IdReserva], [IdCliente], [IdEstadoFactura], [IdMetodoPago], [NumeroFactura], [FechaEmision], [ValorTotal], [FechaPago]) VALUES
(5, 103, 1, NULL, 'FAC-0005', '2025-06-12 10:00:00', 90000.00, NULL);

-- Reserva 6: Pre-reservada
INSERT INTO [dbo].[Reservas] ([IdCliente], [IdCancha], [FechaReserva], [HoraInicio], [HoraFin], [ValorTotal], [IdEstadoReserva], [FechaCreacion], [FechaConfirmacion]) VALUES
(113, 3, '2025-06-28', '08:00:00', '09:00:00', 60000.00, 1, '2025-06-12 11:00:00', NULL);

INSERT INTO [dbo].[Facturas] ([IdReserva], [IdCliente], [IdEstadoFactura], [IdMetodoPago], [NumeroFactura], [FechaEmision], [ValorTotal], [FechaPago]) VALUES
(6, 113, 1, NULL, 'FAC-0006', '2025-06-12 11:00:00', 60000.00, NULL);

-- Reserva 7: Cancelada
INSERT INTO [dbo].[Reservas] ([IdCliente], [IdCancha], [FechaReserva], [HoraInicio], [HoraFin], [ValorTotal], [IdEstadoReserva], [FechaCreacion], [FechaConfirmacion]) VALUES
(104, 1, '2025-06-08', '20:00:00', '21:00:00', 90000.00, 3, '2025-06-03 16:00:00', NULL);

INSERT INTO [dbo].[Facturas] ([IdReserva], [IdCliente], [IdEstadoFactura], [IdMetodoPago], [NumeroFactura], [FechaEmision], [ValorTotal], [FechaPago]) VALUES
(7, 104, 4, NULL, 'FAC-0007', '2025-06-03 16:00:00', 90000.00, NULL);

-- Reserva 8: Cancelada
INSERT INTO [dbo].[Reservas] ([IdCliente], [IdCancha], [FechaReserva], [HoraInicio], [HoraFin], [ValorTotal], [IdEstadoReserva], [FechaCreacion], [FechaConfirmacion]) VALUES
(114, 2, '2025-06-23', '11:00:00', '12:00:00', 60000.00, 3, '2025-06-04 09:00:00', NULL);

INSERT INTO [dbo].[Facturas] ([IdReserva], [IdCliente], [IdEstadoFactura], [IdMetodoPago], [NumeroFactura], [FechaEmision], [ValorTotal], [FechaPago]) VALUES
(8, 114, 4, NULL, 'FAC-0008', '2025-06-04 09:00:00', 60000.00, NULL);