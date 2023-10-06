using SeatsAeroLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Repositories
{
    public interface IFileRepositoryTyped<T,U> : IRepository<T,U>, IFileRepository
    {
        public Dictionary<U, T> Entities { get; set; }
        public List<T> LoadDataFromString(string data);
        public string GetDataAsString(List<T> data);
        public bool ElementExists(T element);
        public void AddElementToDictionary(T element);
        public void UpdateElementInDictionary(T element);
        public void RemoveElementFromDictionary(U id);
        public U GetNewID();
        public void SetID(T element, U ID);
        public  List<T> GetValueList();
    }
    public interface IFileRepository
    {
        protected string FilePath { get; set; }
        public string GetFilePath();
        public void SetFilePath(string filePath);
        protected string GetDefaultFilePath();
        protected void LoadFromFile();
        public void SaveToFile();
        IConfigSettings ConfigSettings { get; set; }
        IFileRepositoryService FileRepositoryService { get; set; }

    }
}
