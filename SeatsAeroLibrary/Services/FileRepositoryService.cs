using SeatsAeroLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services
{
    public class FileRepositoryService : IFileRepositoryService
    {
        protected IConfigSettings _configSettings = null;

        public FileRepositoryService(IConfigSettings configSettings)
        {
            _configSettings = configSettings;
        }

        public TRepo GetRepo<TRepo>() where TRepo: IFileRepository, new()
        {
            TRepo repo = new TRepo();
            SetFileRepositoryService(repo);
            return repo;
        }

        public void SetFileRepositoryService<TRepo>(TRepo repo) where TRepo : IFileRepository
        {
            repo.FileRepositoryService = this;
        }

        public void SetFilePath(IFileRepository repo, string filePath)
        {
            repo.SetFilePath(filePath);
        }


    }
}
