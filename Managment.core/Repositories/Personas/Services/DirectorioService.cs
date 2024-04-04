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

        public async override Task storePersona(Persona persona)
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
            await command.ExecuteNonQueryAsync();
        }

        public async override Task<bool> deletePersonaByIdentificacion(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            using (var transaction = await connection.BeginTransactionAsync())
            {
                var checkCommand = connection.CreateCommand();
                checkCommand.CommandText = "SELECT COUNT(1) FROM Personas WHERE Id = @Id;";
                checkCommand.Parameters.AddWithValue("@Id", id);
                var result = await checkCommand.ExecuteScalarAsync();
                bool IsValid = false;
                if (Convert.ToInt32(result) == 0)
                {
                    return IsValid;
                }

                var command = connection.CreateCommand();

                // Primero, borrar todas las facturas asociadas a la persona
                command.CommandText = "DELETE FROM Facturas WHERE PersonaId = @Id;";
                command.Parameters.AddWithValue("@Id", id);
                await command.ExecuteNonQueryAsync();

                // Luego, borrar la persona
                command.CommandText = "DELETE FROM Personas WHERE Id = @Id;";
                // El parámetro @Id ya está añadido y tiene el valor deseado.
                await command.ExecuteNonQueryAsync();

                await transaction.CommitAsync();

                IsValid = true;
                return IsValid;
            }
        }

        public async override Task<IEnumerable<Persona>> findPersonas()
        {
            var personas = new List<Persona>();
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Nombre, ApellidoPaterno, ApellidoMaterno, Identificacion FROM Personas;";

            using (var reader = await command.ExecuteReaderAsync())
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

        public async override Task<Persona?> findPersonaByIdentificacion(string id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Nombre, ApellidoPaterno, ApellidoMaterno, Identificacion FROM Personas WHERE Identificacion = @Id;";
            command.Parameters.AddWithValue("@Id", id);

            using (var reader = await command.ExecuteReaderAsync())
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

            // Retorna null si no hay persona encontrada
            return null;
        }
    }

}
