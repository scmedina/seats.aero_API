using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services
{
    public abstract class JsonFileRepository<T> : IRepository<T>
    {
        private readonly string filePath;
        private readonly List<T> entities;

        public JsonFileRepository(string filePath)
        {
            this.filePath = filePath;
            this.entities = LoadDataFromFile();
        }

        public IEnumerable<T> GetAll()
        {
            return entities;
        }

        public T GetById(int id)
        {
            return entities.FirstOrDefault(e => GetEntityId(e) == id);
        }

        public void Add(T entity)
        {
            int newId = GenerateNewId();
            SetEntityId(entity, newId);
            entities.Add(entity);
            SaveDataToFile();
        }

        public void Update(T entity)
        {
            int entityId = GetEntityId(entity);
            int index = entities.FindIndex(e => GetEntityId(e) == entityId);
            if (index != -1)
            {
                entities[index] = entity;
                SaveDataToFile();
            }
        }

        public void Delete(int id)
        {
            int index = entities.FindIndex(e => GetEntityId(e) == id);
            if (index != -1)
            {
                entities.RemoveAt(index);
                SaveDataToFile();
            }
        }

        private List<T> LoadDataFromFile()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string jsonData = File.ReadAllText(filePath);
                    return JsonSerializer.Deserialize<List<T>>(jsonData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data from file: {ex.Message}");
            }
            return new List<T>();
        }

        private void SaveDataToFile()
        {
            try
            {
                string jsonData = JsonSerializer.Serialize(entities);
                File.WriteAllText(filePath, jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data to file: {ex.Message}");
            }
        }

        // You should implement these methods in your model-specific code
        protected abstract int GetEntityId(T entity);
        protected abstract void SetEntityId(T entity, int id);
        protected abstract int GenerateNewId();
    }
}
