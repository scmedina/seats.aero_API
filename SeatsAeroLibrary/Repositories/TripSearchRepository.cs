using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Models.Entities;
using SeatsAeroLibrary.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Repositories
{
    public class TripSearchRepository : JsonFileRepository<TripSearchDataModel, int>, ITripSearchRepository
    {
        private int currentID = 0;

        public TripSearchRepository(IConfigSettings configSettings) : base() 
        {
            Initialize(configSettings);
        }

        public override void Initialize(IConfigSettings configSettings)
        {
            base.Initialize(configSettings);
            if (entities.Count > 0)
            {
                currentID = this.entities.Select(entity => entity.Key).Max();
            }
        }

        protected void UniqueIdGenerator(int startId = 1)
        {
            currentID = startId;
        }

        protected override int GenerateNewId()
        {
           return currentID++;
        }

        protected override int GetEntityId(TripSearchDataModel entity)
        {
            return entity.ID;
        }

        protected override void SetEntityId(TripSearchDataModel entity, int id)
        {
            entity.ID = id;
        }

        protected override bool CompareIDs(int id1, int id2)
        {
            return (id1 == id2);
        }

        protected override string GetDefaultFilePath()
        {
            return $@"{_configSettings.OutputDirectory}\SeatsAeroSearches.json";
        }
    }
}
