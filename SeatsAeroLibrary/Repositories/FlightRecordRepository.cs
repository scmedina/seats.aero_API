using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Services;
using SeatsAeroLibrary.Services.FlightRecordID;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Repositories
{
    public class FlightRecordRepository : IFlightRecordRepository, IRepository<FlightRecordDataModel, Guid>
    {
        private IConfigSettings _configSettings;
        private IDataAccess _dataAccess;
        public void Initialize(IConfigSettings configSettings, IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
            Initialize(configSettings);
        }
        public void Initialize(IConfigSettings configSettings)
        {
            _configSettings = configSettings;
            configSettings.Load();
        }

        public void Add(FlightRecordDataModel entity)
        {
            FlightRecordByDateID lowKey = new FlightRecordByDateID();
            lowKey.Map(entity);
            string lowKeyString = FileIO.GetAsJsonString(lowKey);

            string query = $"SELECT * FROM flight_lows WHERE {lowKey.JsonQuery("low_key")} AND is_low = true order by mileage asc";
            using (DataTable dt = _dataAccess.GetDataTable(query))
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow rowMin = dt.Rows[0];
                    if ((int)rowMin["mileage"] <= entity.MileageCost)
                    {
                        return;
                    }

                    foreach (DataRow row in dt.Rows)
                    {
                        row["is_low"] = false;
                        row["date_removed"] = DateTime.Now;
                    }
                }

                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);
                dr["ID"] = Guid.NewGuid();
                dr["is_low"] = true;
                dr["values"] = FileIO.GetAsJsonString(entity);
                dr["low_key"] = lowKeyString;
                dr["mileage"] = entity.MileageCost;
                _dataAccess.UpdateDataTable(dt, query);
            }

        }

        public IEnumerable<FlightRecordDataModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public FlightRecordDataModel GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(FlightRecordDataModel entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
