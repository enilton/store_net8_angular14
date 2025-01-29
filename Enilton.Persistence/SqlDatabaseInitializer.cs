using Microsoft.Data.SqlClient;

namespace Enilton.Persistence
{
    public class DatabaseInitializer
    {
        private readonly string _connectionString;

        public DatabaseInitializer(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void InitializeDatabase()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                //// Criar o banco de dados, se não existir
                //var createDatabaseCommand = @"
                //IF DB_ID('MyDatabase') IS NULL
                //BEGIN
                //    CREATE DATABASE MyDatabase;
                //END";
                //ExecuteCommand(createDatabaseCommand, connection);

                // Selecionar o banco criado
                connection.ChangeDatabase("eniltonstore");

                // Criar a tabela Order, se não existir
                var createOrderTableCommand = @"
                IF OBJECT_ID('dbo.Orders', 'U') IS NULL
                BEGIN
                    CREATE TABLE Orders (
                        Id UNIQUEIDENTIFIER PRIMARY KEY,
                        CustomerId UNIQUEIDENTIFIER NOT NULL,
                        OrderDate DATETIME NOT NULL,
                        TotalAmount DECIMAL(18, 2) NOT NULL,
                        Status INT NOT NULL
                    );
                END";
                ExecuteCommand(createOrderTableCommand, connection);

                // Criar a tabela OrderItem, se não existir
                var createOrderItemTableCommand = @"
                IF OBJECT_ID('dbo.OrderItems', 'U') IS NULL
                BEGIN
                    CREATE TABLE OrderItems (
                        Id UNIQUEIDENTIFIER PRIMARY KEY,
                        OrderId UNIQUEIDENTIFIER NOT NULL,
                        ProductId UNIQUEIDENTIFIER NOT NULL,
                        ProductName NVARCHAR(100) NOT NULL,
                        Quantity DECIMAL(18, 2) NOT NULL,
                        UnitPrice DECIMAL(18, 2) NOT NULL,
                        TotalPrice DECIMAL(18, 2) NOT NULL,
                        FOREIGN KEY (OrderId) REFERENCES Orders(Id) ON DELETE CASCADE
                    );
                END";
                ExecuteCommand(createOrderItemTableCommand, connection);
            }
        }

        private void ExecuteCommand(string commandText, SqlConnection connection)
        {
            using (var command = new SqlCommand(commandText, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
