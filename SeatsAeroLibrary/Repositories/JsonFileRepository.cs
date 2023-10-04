using Autofac;
using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Repositories
{
    public abstract class JsonFileRepository<T,U> : IRepository<T,U>
    {
        protected readonly string _filePath;
        protected readonly Dictionary<U,T> entities;
        protected readonly IConfigSettings _configSettings = null;

        public JsonFileRepository() : this(null) { }
        public JsonFileRepository(string filePath)
        {
            using (var scope = ServicesContainer.BuildContainer().BeginLifetimeScope())
            {
                _configSettings = scope.Resolve<IConfigSettings>();
            }
            _configSettings.Load();
            if (String.IsNullOrWhiteSpace(filePath))
            {
                this._filePath = GetDefaultFilePath();
            }
            else
            {
                this._filePath = filePath;
            }
            List<T> elements = LoadDataFromFile();
            entities = elements.ToDictionary(e => GetEntityId(e));
        }

        protected abstract string GetDefaultFilePath();

        public IEnumerable<T> GetAll()
        {
            return entities.Values;
        }

        public T GetById(U id)
        {
            if (entities.ContainsKey(id))
            {
                return entities[id];
            }
            return default;
        }

        public virtual void Add(T entity)
        {
            U newId = GenerateNewId();
            SetEntityId(entity, newId);
            entities.Add(GetEntityId(entity), entity);
            SaveDataToFile();
        }

        public virtual void Update(T entity)
        {
            U entityId = GetEntityId(entity);

            if (entities.ContainsKey(entityId))
            {
                entities[entityId] = entity;
                SaveDataToFile();
            }
        }

        public void Delete(U id)
        {
            if (entities.ContainsKey(id))
            {
                entities.Remove(id);
                SaveDataToFile();
            }
        }

        private List<T> LoadDataFromFile()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    string jsonData = File.ReadAllText(_filePath);
                    return JsonSerializer.Deserialize<List<T>>(jsonData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data from file: {ex.Message}");
            }
            return new List<T>();
        }

        protected virtual void SaveDataToFile()
        {
            try
            {
                FileIO.ExportJsonFile(GetValueList(), _filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data to file: {ex.Message}");
            }
        }

        protected virtual List<T> GetValueList()
        {
            return entities.Values.ToList();
        }

        // You should implement these methods in your model-specific code
        protected abstract bool CompareIDs(U id1, U id2);
        protected abstract U GetEntityId(T entity);
        protected abstract void SetEntityId(T entity, U id);
        protected abstract U GenerateNewId();
    }
}
