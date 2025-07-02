# Proyecto Final Web

Este repositorio contiene la aplicación y los scripts de base de datos.

## Scripts

- `Proyecto_Final_Web/SQLQuery_Crear_Tablas.sql` crea la estructura de la base de datos.
- `Proyecto_Final_Web/SQLQuery_Poblar_Tablas.sql` inserta datos iniciales.
- `Scripts/HashExistingPasswords.sql` genera hashes SHA-256 para las contraseñas existentes.

## Actualización de contraseñas

Después de desplegar los cambios que implementan el almacenamiento de contraseñas con hash, ejecute el script `Scripts/HashExistingPasswords.sql` en la base de datos para convertir las contraseñas guardadas en texto plano.
