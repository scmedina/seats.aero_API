using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Repositories
{
    public class TripSearchRepository : JsonFileRepository<TripSearchDataModel>
    {
        private int currentID;
        public TripSearchRepository(string filePath) : base(filePath)
        {
            currentID = this.entities.Select(entity => GetEntityId(entity)).Max();
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
    }
}
