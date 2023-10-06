using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Services;
using SeatsAeroLibrary.Services.FlightRecordID;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Repositories
{
    public class FlightRecordBySeatTypeRepository :   
        FileRepository<FlightRecordDataModel, Guid>, IKey<FlightRecordBySeatTypeID, Guid>
    {
        protected readonly Dictionary<FlightRecordBySeatTypeID, Guid> _uniqueEntities =
            new Dictionary<FlightRecordBySeatTypeID, Guid>();
        public FlightRecordBySeatTypeRepository() : base()
        {
        }

        public Guid GetByKey(FlightRecordBySeatTypeID key)
        {
            return TypeHelper.GetById(_uniqueEntities, key);
        }

        public FlightRecordBySeatTypeID GetKey(Guid item)
        {
            FlightRecordDataModel model = GetByKey(item);
            return GetAlternateKey(model);
        }
        public FlightRecordBySeatTypeID GetAlternateKey(FlightRecordDataModel model)
        {
            FlightRecordBySeatTypeID key = new FlightRecordBySeatTypeID();
            key.Map(model);
            return key;
        }

        public bool KeyExists(FlightRecordBySeatTypeID key)
        {
            return _uniqueEntities.ContainsKey(key);
        }

        public override void AddElementToDictionary(FlightRecordDataModel element)
        {
            Guid id = GetKey(element);
            Entities.Add(id, element);
            _uniqueEntities.Add(GetKey(id), id);
        }

        public override bool ElementExists(FlightRecordDataModel element)
        {
            Guid primaryKey = GetKey(element);
            if (Entities.ContainsKey(primaryKey))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string GetDataAsString(List<FlightRecordDataModel> data)
        {
            throw new NotImplementedException();
        }

        public override Guid GetKey(FlightRecordDataModel item)
        {
            throw new NotImplementedException();
        }

        public override Guid GetNewID()
        {
            throw new NotImplementedException();
        }

        public override List<FlightRecordDataModel> LoadDataFromString(string data)
        {
            throw new NotImplementedException();
        }

        public override void RemoveElementFromDictionary(Guid id)
        {
            throw new NotImplementedException();
        }

        public override void SetID(FlightRecordDataModel element, Guid ID)
        {
            throw new NotImplementedException();
        }

        public override void UpdateElementInDictionary(FlightRecordDataModel element)
        {
            throw new NotImplementedException();
        }

        protected override string GetDefaultFilePath()
        {
            throw new NotImplementedException();
        }

        //protected override string GetDefaultFilePath()
        //{
        //    return $@"{_configSettings.OutputDirectory}\\Flight_Record_Lows_By_SeatType.json";
        //}
    }
}
