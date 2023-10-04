using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Models.DataModels;
using SeatsAeroLibrary.Models.Entities;
using SeatsAeroLibrary.Profiles;
using SeatsAeroLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Services
{
    public class FlightRecordService : IFlightRecordService
    {
        private readonly IFlightRecordRepository _flightRecordRepository;
        private readonly FlightRecordDataModelMapper _flightRecordDataModelMapper;
        private readonly FlightRecordMapper _flightRecordMapper;
        private readonly ILogger _logger;

        public FlightRecordService(IFlightRecordRepository flightRecordRepository, FlightRecordDataModelMapper flightRecordDataModelMapper, FlightRecordMapper flightRecordMapper, ILogger logger)
        {
            _flightRecordRepository = flightRecordRepository;
            _flightRecordDataModelMapper = flightRecordDataModelMapper;
            _flightRecordMapper = flightRecordMapper;
            _logger = logger;
        }

        public void AddRecord(Flight flight)
        {
            _logger.Info($"Adding flight record: {flight}");
            FlightRecordDataModel data = _flightRecordMapper.Map(flight);
            _flightRecordRepository.Add(data);
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
