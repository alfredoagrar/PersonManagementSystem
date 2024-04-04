using Managment.core.Database.Interfaces;
using Microsoft.Data.Sqlite;

namespace Managment.core.Database.Services
{
    internal class SqliteDatabaseInitializer : DatabaseInitializer
    {
        private readonly string _connectionString;

        public SqliteDatabaseInitializer(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override void InitializeDatabase()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
                CREATE TABLE IF NOT EXISTS Personas (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nombre TEXT NOT NULL,
                    ApellidoPaterno TEXT NOT NULL,
                    ApellidoMaterno TEXT,
                    Identificacion TEXT NOT NULL UNIQUE
                );

                CREATE TABLE IF NOT EXISTS Facturas (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    PersonaId INTEGER NOT NULL,
                    Monto DECIMAL NOT NULL,
                    FechaEmision DATETIME NOT NULL,
                    FOREIGN KEY (PersonaId) REFERENCES Personas(Id)
                );
                ";
            command.ExecuteNonQuery();
        }
    }
}
