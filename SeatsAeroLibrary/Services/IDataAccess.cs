using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services
{
    public interface IDataAccess
    {
        public object ExecuteScalar(string query);
        public int ExecuteNonQuery(string query);
        public DataTable GetDataTable(string query);
        public void UpdateDataTable(DataTable dataTable, string query);
    }
}
