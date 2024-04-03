using Managment.core.Repositories.Personas.Interfaces;
using Managment.core.Repositories.Personas.Models;
using Microsoft.Data.Sqlite;

namespace Managment.core.Repositories.Personas.Services
{
    internal class DirectorioService : PersonasBase
    {
        private readonly string _connectionString;

        public DirectorioService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override void storePersona(Persona persona)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText =
            @"
                INSERT INTO Personas (Nombre, ApellidoPaterno, ApellidoMaterno, Identificacion) 
                VALUES (@Nombre, @ApellidoPaterno, @ApellidoMaterno, @Identificacion);
            ";
            command.Parameters.AddWithValue("@Nombre", persona.Nombre);
            command.Parameters.AddWithValue("@ApellidoPaterno", persona.ApellidoPaterno);
            command.Parameters.AddWithValue("@ApellidoMaterno", persona.ApellidoMaterno ?? string.Empty); // Considerando que ApellidoMaterno es opcional.
            command.Parameters.AddWithValue("@Identificacion", persona.Identificacion);
            command.ExecuteNonQuery();
        }

        public override void deletePersonaByIdentificacion(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText =
            @"
                DELETE FROM Personas WHERE Id = @Id;
            ";
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
        }

        public override IEnumerable<Persona> findPersonas()
        {
            var personas = new List<Persona>();
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Nombre, ApellidoPaterno, ApellidoMaterno, Identificacion FROM Personas;";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    personas.Add(new Persona
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        ApellidoPaterno = reader.GetString(2),
                        ApellidoMaterno = reader.IsDBNull(3) ? null : reader.GetString(3),
                        Identificacion = reader.GetString(4)
                    });
                }
            }

            return personas;
        }

        public override Persona findPersonaByIdentificacion(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Nombre, ApellidoPaterno, ApellidoMaterno, Identificacion FROM Personas WHERE Id = @Id;";
            command.Parameters.AddWithValue("@Id", id);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Persona
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        ApellidoPaterno = reader.GetString(2),
                        ApellidoMaterno = reader.IsDBNull(3) ? null : reader.GetString(3),
                        Identificacion = reader.GetString(4)
                    };
                }
            }

            // Retorna una instancia 'vacía' o 'predeterminada' de Persona si no se encuentra ninguna coincidencia.
            return new Persona
            {
                Id = 0, // Considerado como "vacío" o un identificador no válido.
                Nombre = string.Empty,
                ApellidoPaterno = string.Empty,
                ApellidoMaterno = string.Empty,
                Identificacion = string.Empty
            };
        }
    }

}
