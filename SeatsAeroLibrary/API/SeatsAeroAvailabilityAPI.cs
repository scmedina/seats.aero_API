using RestSharp;
using SeatsAeroLibrary.API.Models;
using SeatsAeroLibrary.Helpers;
using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Services.FlightFilters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static SeatsAeroLibrary.API.SeatsAeroAvailabilityAPI;

namespace SeatsAeroLibrary.API
{
    public class SeatsAeroAvailabilityAPI : SeatsAeroCounterAP<AvailabilityResultDataModel, Flight>
    {
        public FilterAggregate FilterAggregate { get; set; }

        public SeatsAeroAvailabilityAPI(MileageProgram mileageProgram, FilterAggregate filterAggregate) : base("partnerapi/availability", new string[] { "source" }, null)
        {
            Guard.AgainstMultipleSources(mileageProgram, nameof(mileageProgram));
            Guard.AgainstNull(filterAggregate, nameof(filterAggregate));

            this.FilterAggregate = filterAggregate;
            this.QueryParams = new Dictionary<string, string>();
            this.QueryParams.Add("source", mileageProgram.ToString());

            DateTime startDate = (DateTime)DateFilter.GetDateVal(filterAggregate.Filters, isEndDate: false, DateTime.Today);
            DateTime? endDate = (DateTime)DateFilter.GetDateVal(filterAggregate.Filters, isEndDate: true);
            Guard.AgainstNull(endDate, nameof(endDate));
            Guard.AgainstInvalidDateRange(startDate,(DateTime)endDate, nameof(startDate),nameof(endDate));

            this.QueryParams.Add("start_date", startDate.ToString("yyyy-MM-dd"));
            this.QueryParams.Add("end_date", ((DateTime)endDate).ToString("yyyy-MM-dd"));
            this.QueryParams.Add("take", "1000");
        }

        protected override List<Flight> GetU(AvailabilityResultDataModel? data)
        {
            return Flight.GetFilteredFlights(FilterAggregate, data?.data);
        }

        protected override bool ContinueCounter(APIResult<AvailabilityResultDataModel, List<Flight>> apiResult)
        {
            return (apiResult?.TData?.hasMore ?? false);
        }

        protected override string GetMoreURL(APIResult<AvailabilityResultDataModel, List<Flight>> apiResult)
        {
            return apiResult?.TData?.moreURL;
        }
    }
}
