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
    public abstract class FileRepository<T, U> : IRepository<T, U>
    {
        protected string _filePath;
        protected Dictionary<U, T> entities = new Dictionary<U, T>();
        protected IConfigSettings _configSettings = null;

        public FileRepository() { }

        public virtual void Initialize(IConfigSettings configSettings)
        {
            _configSettings = configSettings;
            _configSettings.Load();
            LoadFilePath();
        }

        public void LoadFilePath()
        {
            this._filePath = GetDefaultFilePath();

            if (File.Exists(_filePath))
            {
                string data = File.ReadAllText(_filePath);
                entities = BuildDictionary(LoadDataFromString(data));
            }
        }

        public virtual void SaveDataToFile()
        {
            string text = GetDataAsString(GetValueList());
            File.WriteAllText(_filePath, text);
        }
        protected virtual Dictionary<U, T> BuildDictionary(List<T> elements)
        {
            Dictionary<U, T> results = new Dictionary<U, T>();
            foreach (var element in elements)
            {
                if (!results.ContainsKey(GetEntityId(element)))
                {
                    results.Add(GetEntityId(element), element);
                }
            }
            return results;
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
            AddElement(entity);
            SaveDataToFile();
        }

        protected virtual void AddElement(T entity)
        {
            U newId = GenerateNewId();
            SetEntityId(entity, newId);
            entities.Add(GetEntityId(entity), entity);
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

        protected abstract List<T> LoadDataFromString(string data);
        protected abstract string GetDataAsString(List<T> elements);
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
