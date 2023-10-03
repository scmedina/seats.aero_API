using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Services.Stats;
using SeatsAeroLibrary.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using SeatsAeroLibrary.Models.Entities;
using SeatsAeroLibrary.Profiles;

namespace SeatsAeroLibrary.Repositories
{
    public class FlightRecordRepository : JsonFileRepository<FlightRecordDataModel, Guid>, IFlightRecordRepository
    {
        public readonly Dictionary<FlightRecordUniqueID, FlightRecordDataModel> uniqueEntities = new Dictionary<FlightRecordUniqueID, FlightRecordDataModel>();

        private readonly FlightRecordIDMapper _flightRecordIDMapper;
        public FlightRecordRepository() : base(GetFilePath())
        {
            using (var scope = ServicesContainer.BuildContainer().BeginLifetimeScope())
            {
                _flightRecordIDMapper = scope.Resolve<FlightRecordIDMapper>();
            }
        }

        private static string GetFilePath()
        {
            IConfigSettings configSettings = null;
            using (var scope = ServicesContainer.BuildContainer().BeginLifetimeScope())
            {
                configSettings = scope.Resolve<IConfigSettings>();
            }
            configSettings.Load();
            string filePath = $@"{configSettings.OutputDirectory}\\Flight_Record_Lows.json";
            return filePath;
        }

        protected override bool CompareIDs(Guid id1, Guid id2)
        {
            return id1 == id2;
        }

        protected override Guid GenerateNewId()
        {
            return Guid.NewGuid();
        }

        protected override Guid GetEntityId(FlightRecordDataModel entity)
        {
            return entity.Id;
        }

        protected override void SetEntityId(FlightRecordDataModel entity, Guid id)
        {
            entity.Id = id;
        }

        public override void Add(FlightRecordDataModel entity)
        {
            FlightRecordDataModel currentEntity = null;
            if (MatchExists(entity, ref currentEntity))
            {
                UpdateIfLessThan(entity, currentEntity);
            }
            else
            {
                uniqueEntities.Add(_flightRecordIDMapper.Map(entity), entity);
                base.Add(entity);
            }            
        }

        public override void Update(FlightRecordDataModel entity)
        {
            FlightRecordDataModel currentEntity = null;
            if (MatchExists(entity, ref currentEntity))
            {
                UpdateIfLessThan(entity, currentEntity);
            }
        }

        private void UpdateIfLessThan(FlightRecordDataModel entity, FlightRecordDataModel currentEntity)
        {
            if (entity.MileageCost < currentEntity.MileageCost)
            {
                base.Update(entity);
            }
        }

        protected bool MatchExists(FlightRecordDataModel entity, ref FlightRecordDataModel outEntity)
        {
            FlightRecordUniqueID id = _flightRecordIDMapper.Map(entity);
            if (uniqueEntities.ContainsKey(id))
            {
                outEntity = uniqueEntities[id];
                return true;
            }
            return false;
        }

        protected override List<FlightRecordDataModel> GetValueList()
        {
            List < FlightRecordDataModel > results =  base.GetValueList();
            results = results
                .OrderBy(x => x.OriginRegion)
                .ThenBy(x => x.OriginAirport)
                .ThenBy(x => x.DestinationRegion)
                .ThenBy(x => x.DestinationAirport)
                .ThenBy(x => x.Direct)
                .ThenBy(x => x.SeatType)
                .ThenBy(x => x.MileageCost)
                .ThenBy(x => x.Source)
                .ThenBy(x => x.Airline)
                .ThenBy(x => x.Date)
                .ToList();
            return results;
        }
    }
}
