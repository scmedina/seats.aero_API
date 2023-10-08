using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services
{
    public class PostgresDataAccessService: IDataAccess
    {
        private string ConnectionString { get; }
        private IConfigSettings _configSettings;


        public PostgresDataAccessService(IConfigSettings configSettings)
        {
            _configSettings = configSettings;
            _configSettings.Load();
            ConnectionString = _configSettings.GetConnectionString();
        }

        public PostgresDataAccessService(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public PostgresDataAccessService(string host, string user, string pass, string database)
        {
            ConnectionString = $"Host={host};Username={user};Password={pass};Database={database}";
        }

        public object ExecuteScalar(string query)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(query, connection))
                {
                    return command.ExecuteScalar();
                }
            }
        }

        public int ExecuteNonQuery(string query)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(query, connection))
                {
                    return command.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetDataTable(string query)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var adapter = new NpgsqlDataAdapter(query, connection))
                {
                    var dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }

        public void UpdateDataTable(DataTable dataTable, string query)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var adapter = new NpgsqlDataAdapter(query, connection))
                {
                    using (var commandBuilder = new NpgsqlCommandBuilder(adapter))
                    {
                        adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                        adapter.InsertCommand = commandBuilder.GetInsertCommand();
                        adapter.DeleteCommand = commandBuilder.GetDeleteCommand();

                        adapter.Update(dataTable);
                    }
                }
            }
        }
    }
}
