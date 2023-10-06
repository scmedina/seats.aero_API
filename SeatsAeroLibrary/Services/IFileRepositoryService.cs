using SeatsAeroLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services
{
    public interface IFileRepositoryService
    {
        public TRepo GetRepo<TRepo>() where TRepo : IFileRepository, new();
        public void SetFileRepositoryService<TRepo>(TRepo repo) where TRepo : IFileRepository;
        public void SetFilePath(IFileRepository repo, string filePath);
    }
}
