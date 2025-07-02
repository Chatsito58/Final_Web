-- HashExistingPasswords.sql
-- Actualiza las contraseñas de la tabla Usuarios usando SHA-256
-- Ejecute este script después de desplegar los cambios que implementan
-- el almacenamiento de contraseñas con hash.

UPDATE [dbo].[Usuarios]
SET [Contrasena] = CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', [Contrasena]), 2)
WHERE LEN([Contrasena]) <> 64 OR [Contrasena] LIKE '%[^0-9A-Fa-f]%';
