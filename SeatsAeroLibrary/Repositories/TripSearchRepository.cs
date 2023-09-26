using SeatsAeroLibrary.Models.Entities;
using SeatsAeroLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Repositories
{
    public class TripSearchRepository : JsonFileRepository<TripSearch>
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

        protected override int GetEntityId(TripSearch entity)
        {
            return entity.ID;
        }

        protected override void SetEntityId(TripSearch entity, int id)
        {
            entity.ID = id;
        }
    }
}
