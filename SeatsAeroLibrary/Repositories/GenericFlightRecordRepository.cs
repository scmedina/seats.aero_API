using SeatsAeroLibrary.Models.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using SeatsAeroLibrary.Profiles;
using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Services.FlightRecordID;
using System.Diagnostics.Eventing.Reader;
using SeatsAeroLibrary.Services;

namespace SeatsAeroLibrary.Repositories
{
    public abstract class GenericFlightRecordRepository<T> : CsvFileRepository<FlightRecordDataModel, Guid>, IFlightRecordRepository where T: IFlightRecordID, new()
    {
        public readonly Dictionary<T, KeyValuePair<Guid, FlightRecordDataModel>> uniqueEntities = new Dictionary<T, KeyValuePair<Guid, FlightRecordDataModel>>();

        public GenericFlightRecordRepository() : base()
        {
        }

        public override void Initialize(IConfigSettings configSettings)
        {
            base.Initialize(configSettings);
            BuildUniqueDictionary();
            if (_fileFixed)
            {
                SaveDataToFile();
            }
        }
        

        private bool _fileFixed = false;
        private void BuildUniqueDictionary()
        {
            uniqueEntities.Clear();
            for (int i = entities.Count-1; i >= 0; i--)
            {
                var entity = entities.ElementAt(i);
                T id = GetT(entity.Value);
                if (entity.Value.MileageCost == 0)
                {
                    entities.Remove(entity.Key);
                    _fileFixed = true;
                }
                else if (uniqueEntities.ContainsKey(id))
                {
                    entities.Remove(entity.Key);
                    _fileFixed = true;
                }
                else
                {
                    uniqueEntities.Add(id, entity);
                }
            }
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
                base.AddElement(entity);
                uniqueEntities.Add(GetT(entity), entities.Last());
                SaveDataToFile();
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
            if (entity.MileageCost > 0)
            {
                if (currentEntity.MileageCost == 0 )
                {
                    base.Update(entity);
                }
                else if (entity.MileageCost < currentEntity.MileageCost)
                {
                    base.Update(entity);
                }
            }
        }

        protected bool MatchExists(FlightRecordDataModel entity, ref FlightRecordDataModel outEntity)
        {
            T id = GetT(entity);
            if (uniqueEntities.ContainsKey(id))
            {
                outEntity = uniqueEntities[id].Value;
                return true;
            }
            return false;
        }

        protected override List<FlightRecordDataModel> GetValueList()
        {
            if (entities.Count != uniqueEntities.Count)
            { 
                throw new Exception("entities and uniqueEntities are out of sync"); 
            }
            else if (entities.Count <= 1)
            {
                return entities.Values.ToList();
            }
           return uniqueEntities.OrderBy(kvp => kvp.Key)
                .Select(kvp => kvp.Value.Value)
                .ToList();
        }

        protected virtual T GetT(FlightRecordDataModel entity)
        {
            T id = new T();
            id.Map(entity);
            return id;
        }

    }
}
