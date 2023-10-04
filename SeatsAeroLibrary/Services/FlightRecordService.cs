using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Models.Entities;
using SeatsAeroLibrary.Profiles;
using SeatsAeroLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services
{
    public class FlightRecordService : IFlightRecordService
    {
        private readonly FlightRecordMapper _flightRecordMapper;
        private readonly ILogger _logger;
        private readonly List<IFlightRecordRepository> _flightRecordRepositories = new List<IFlightRecordRepository>();
        private readonly IConfigSettings _configSettings;
        public FlightRecordService(FlightRecordMapper flightRecordMapper, ILogger logger, IConfigSettings configSettings)
        {
            _flightRecordMapper = flightRecordMapper;
            _logger = logger;
            _configSettings = configSettings;
        }

        public void AddRepositoryType<TRepo>() where TRepo : IFlightRecordRepository, new()
        {
            TRepo repo = new TRepo();
            repo.Initialize(_configSettings);

            _flightRecordRepositories.Add(repo);
        }

        public void AddRecord(Flight flight)
        {
            if (_flightRecordRepositories == null || _flightRecordRepositories.Count == 0)
            {
                return;
            }

            _logger.Info($"Adding flight record: {flight}");
            FlightRecordDataModel data = _flightRecordMapper.Map(flight);
            foreach (var repo in _flightRecordRepositories)
            {
                repo.Add(data);
            }
        }
        public void AddRecords(List<Flight> flights)
        {
            foreach (var flight in flights)
            {
                AddRecord(flight);
            }
        }
        //public List<FlightRecord> GetAll()
        //{
        //    _logger.Info("Getting all flight records");
        //    var records = _flightRecordRepository.GetAll();
        //    return _flightRecordDataModelMapper.Map(records.ToList()).ToList();
        //}
    }
}
