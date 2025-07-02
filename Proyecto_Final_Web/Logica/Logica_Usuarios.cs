using System.Data;
using Microsoft.Data.SqlClient;
using Proyecto_Final_Web.Models;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace Proyecto_Final_Web.Logica
{
    public class Logica_Usuarios
    {
        private readonly string _connectionString;

        public Logica_Usuarios(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConexionBD");
        }

        public async Task<Usuario?> AutenticarUsuario(string correo, string clave)
        {
            Usuario? usuario = null;

            try
            {
                // Hashear la clave con SHA256
                string claveHash;
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(clave));
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    claveHash = builder.ToString();
                }

                using (var conexion = new SqlConnection(_connectionString))
                {
                    await conexion.OpenAsync();

                    const string consulta = @"
                        SELECT 
                            u.IdUsuario,
                            u.IdRol,
                            u.NombreCompleto,
                            u.Correo,
                            u.Contrasena,
                            u.Telefono,
                            u.FechaRegistro,
                            r.Nombre AS RolNombre,
                            r.Descripcion AS RolDescripcion
                        FROM Usuarios u
                        INNER JOIN Roles r ON u.IdRol = r.IdRol
                        WHERE u.Correo = @correo AND u.Contrasena = @clave";

                    using (var comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@correo", correo);
                        comando.Parameters.AddWithValue("@clave", claveHash);

                        using (var reader = await comando.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                usuario = new Usuario
                                {
                                    IdUsuario = reader.GetInt32("IdUsuario"),
                                    IdRol = reader.GetInt32("IdRol"),
                                    NombreCompleto = reader.GetString("NombreCompleto"),
                                    Correo = reader.GetString("Correo"),
                                    Contrasena = reader.GetString("Contrasena"),
                                    Telefono = reader.IsDBNull("Telefono") ? null : reader.GetString("Telefono"),
                                    FechaRegistro = reader.GetDateTime("FechaRegistro"),
                                    IdRolNavigation = new Role
                                    {
                                        IdRol = reader.GetInt32("IdRol"),
                                        Nombre = reader.GetString("RolNombre"),
                                        Descripcion = reader.GetString("RolDescripcion")
                                    }
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Loggear el error adecuadamente
                Console.WriteLine($"Error al autenticar usuario: {ex.Message}");
                throw; // Opcional: relanzar o manejar según tu arquitectura
            }

            return usuario;
        }
    }
}