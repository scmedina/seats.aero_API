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
    public abstract class FileRepository<T, U> : IFileRepositoryTyped<T, U>
    {
        public Dictionary<U, T> Entities { get; set; } = new Dictionary<U, T>();
        public IConfigSettings ConfigSettings { get; set; }
        public IFileRepositoryService FileRepositoryService { get; set; }
        string FilePath { get; set; }
        string IFileRepository.FilePath { get => ); set => throw new NotImplementedException(); }

        public FileRepository() { }


        public void Initialize(IConfigSettings configSettings)
        {
            ConfigSettings = configSettings;
            ConfigSettings.Load();
            LoadFromFile();
        }

        public void LoadFromFile()
        {
            string filePath = GetFilePath();
            if (File.Exists(filePath))
            {
                string data = File.ReadAllText(filePath);
                List<T> elements = LoadDataFromString(data);
                BuildDictionary(elements);
            }
        }
        protected void  BuildDictionary(List<T> elements)
        {
            if (Entities == null)
            {
                Entities = new Dictionary<U, T>();
            }
            foreach (var element in elements)
            {
                if (ElementExists(element))
                {
                    UpdateElementInDictionary(element);
                }
            }
        }

        public void SetFilePath(string filePath)
        {
            this.FilePath = FilePath;
        }

        public string GetFilePath()
        {
            if (string.IsNullOrWhiteSpace(FilePath))
            {
                return GetDefaultFilePath();
            }
            return FilePath;
        }

        public void SaveToFile()
        {
            string filePath = GetFilePath();
            string text = GetDataAsString(GetAll().ToList());
            File.WriteAllText(filePath, text);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return GetValueList();
        }
        public virtual List<T> GetValueList()
        {
            return Entities.Values.ToList();
        }
        public void Add(T entity)
        {
            if (ElementExists(entity))
            {
                Update(entity);
            }
            else
            {
                AddElementToDictionary(entity);
                SaveToFile();
            }
        }
        public void Update(T entity)
        {
            if (ElementExists(entity))
            {
                UpdateElementInDictionary(entity);
                SaveToFile();
            }
        }

        public void Delete(U id)
        {
            if (Entities.ContainsKey(id))
            {
                RemoveElementFromDictionary(id);
                SaveToFile();
            }
        }


        public abstract void SetID(T element, U ID);
        public abstract U GetNewID();
        public abstract bool ElementExists(T element);
        public abstract string GetDataAsString(List<T> data);
        public abstract List<T> LoadDataFromString(string data);
        public abstract void AddElementToDictionary(T element);
        public abstract void UpdateElementInDictionary(T element);
        public abstract void RemoveElementFromDictionary(U id);
        protected abstract string GetDefaultFilePath();

        string IFileRepository.GetDefaultFilePath()
        {
            ;
        }

        public abstract U GetKey(T item);

        public virtual T GetByKey(U key)
        {
            return TypeHelper.GetById(Entities, key);
        }

        public virtual bool KeyExists(U key)
        {
            return Entities.ContainsKey(key);
        }
    }
}
