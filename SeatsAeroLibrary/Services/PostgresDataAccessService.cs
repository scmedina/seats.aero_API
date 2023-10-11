using Microsoft.VisualBasic;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                        commandBuilder.ConflictOption = ConflictOption.OverwriteChanges;
                        commandBuilder.SetAllValues = false;

                        adapter.UpdateCommand = commandBuilder.GetUpdateCommand(true);
                        adapter.InsertCommand = commandBuilder.GetInsertCommand(true);
                        adapter.DeleteCommand = commandBuilder.GetDeleteCommand(true);

                        try
                        {
                            adapter.Update(dataTable);
                        }
                        catch(Exception ex)
                        {
                            throw;
                            AttemptToRetreiveProblemQuery(adapter, DataViewRowState.ModifiedCurrent);
                        }
                    }
                }
            }
        }
        ///     ''' <remarks></remarks>
        private static string AttemptToRetreiveProblemQuery( DbDataAdapter dataAdapter, DataViewRowState type)
        {
            DbCommand command = null/* TODO Change to default(_) if this is not a reference type */;
            string result = null;
            switch (type)
            {
                case DataViewRowState.Deleted:
                    {
                        command = dataAdapter.DeleteCommand;
                        result = command.CommandText;
                        break;
                    }

                case DataViewRowState.ModifiedCurrent:
                    {
                        command = dataAdapter.UpdateCommand;
                        result = FormatUpdateStatement(command.CommandText);
                        break;
                    }

                case DataViewRowState.Added:
                    {
                        command = dataAdapter.InsertCommand;
                        result = FormatInsertStatement(command.CommandText);
                        break;
                    }

                default:
                    {
                        return "[Unable to retrieve problem query]";
                    }
            }

            foreach (DbParameter parameter in command.Parameters)
            {
                result = Strings.Replace(result, "?", (parameter.Value == null ? "Null" : string.Format("'{0}'", parameter.Value).ToString()), 1, 1);

            }

            return result;
        }

        private static string FormatUpdateStatement(string value)
        {
            string result = value;

            string[] split = Regex.Split(value, @"\s+SET\s+|\s+WHERE\s+", RegexOptions.IgnoreCase);
            if (split.Count() < 3)
            {
                System.Diagnostics.Debugger.Break(); return result;
            }
            else if (string.IsNullOrEmpty(split[0]) | string.IsNullOrEmpty(split[1]) | string.IsNullOrEmpty(split[2]))
            {
                System.Diagnostics.Debugger.Break(); return result;
            }

            split[1] = Strings.Replace(split[1], "?", "?" + Constants.vbNewLine);
            result = string.Format("{1} SET{0}{2}{0}WHERE{3}", Constants.vbNewLine, split[0], split[1], split[2]);
            return result;
        }

        private static string FormatInsertStatement(string value)
        {
            string result = value;

            // Invalid split on an INSERT statment at ") VALUES (".
            string[] split = Regex.Split(value, @"\(|\)\s+Values\s+\(|\)", RegexOptions.IgnoreCase);
            if (split.Count() < 3)
            {
                System.Diagnostics.Debugger.Break(); return result;
            }
            else if (string.IsNullOrEmpty(split[0]) | string.IsNullOrEmpty(split[1]) | string.IsNullOrEmpty(split[2]))
            {
                System.Diagnostics.Debugger.Break(); return result;
            }
            else if (split[0].Trim().ToUpper().Contains("INSERT") == false)
            {
                System.Diagnostics.Debugger.Break(); return result;
            }
            else if (string.IsNullOrEmpty(split[3]) == false)
            {
                System.Diagnostics.Debugger.Break(); return result;
            }

            Regex r = new Regex(@"\A.*?(\,[^\,]+){1,3}|(\,[^\,]+){1,4}");
            split[1] = r.Replace(split[1], "$&" + Constants.vbNewLine);
            split[2] = r.Replace(split[2], "$&" + Constants.vbNewLine);

            result = string.Format("{1} ({0}{2}) VALUES ({0}{3})", Constants.vbNewLine, split[0], split[1], split[2]);
            return result;
        }


    }
}
