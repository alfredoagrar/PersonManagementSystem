using Managment.core.Repositories.Facturas.Interfaces;
using Managment.core.Repositories.Facturas.Models;
using Microsoft.Data.Sqlite;

namespace Managment.core.Repositories.Facturas.Services
{
    internal class VentasService : FacturasBase
    {
        private readonly string _connectionString;

        public VentasService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override async Task<IEnumerable<Factura>> findFacturasByPersonaAsync(int personaId)
        {
            List<Factura> facturas = new List<Factura>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT Id, PersonaId, Monto, FechaEmision
                    FROM Facturas
                    WHERE PersonaId = @PersonaId;
                ";
                command.Parameters.AddWithValue("@PersonaId", personaId);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        facturas.Add(new Factura
                        {
                            Id = reader.GetInt32(0),
                            PersonaId = reader.GetInt32(1),
                            Monto = reader.GetDecimal(2),
                            FechaEmision = reader.GetDateTime(3)
                        });
                    }
                }
            }

            return facturas; // Retorna una lista de facturas, que puede estar vacía.
        }

        public override async Task storeFacturaAsync(Factura factura)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    INSERT INTO Facturas (PersonaId, Monto, FechaEmision)
                    VALUES (@PersonaId, @Monto, @FechaEmision);
                ";
                command.Parameters.AddWithValue("@PersonaId", factura.PersonaId);
                command.Parameters.AddWithValue("@Monto", factura.Monto);
                command.Parameters.AddWithValue("@FechaEmision", factura.FechaEmision);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
