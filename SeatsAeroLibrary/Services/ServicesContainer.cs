using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using NLog;
using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Profiles;
using SeatsAeroLibrary.Repositories;
using SeatsAeroLibrary.Services.API.Factories;
using SeatsAeroLibrary.Services.Sort;

namespace SeatsAeroLibrary.Services
{
    /// <summary>
    /// This services container class uses <see href="https://autofac.readthedocs.io/en/latest/getting-started/index.html">Autofac</see> to register services for dependency injection
    /// </summary>
    public class ServicesContainer
    {
        protected static IContainer _container;

        protected virtual ContainerBuilder BuildBuilder()
        {
            var builder = new ContainerBuilder();


            builder.RegisterType<ConfigSettings>()
                .As<IConfigSettings>()
                .SingleInstance();

            builder.RegisterType<FlightSorter>()
                .As(typeof(ISorter<Flight>))
                .SingleInstance();

            builder.RegisterType<FlightRecordService>()
                .As<IFlightRecordService>()
                .SingleInstance();

            builder.RegisterType<FlightRecordDataModelMapper>()
                .AsSelf();

            builder.RegisterType<FlightRecordMapper>()
                .AsSelf();

            builder.RegisterType<StatisticsRepository>()
                .As<IStatisticsRepository>()
                .SingleInstance();

            builder.RegisterType<TripSearchRepository>()
                .As<ITripSearchRepository>()
                .SingleInstance();

            builder.RegisterType<TripSearchService>()
                .As<ITripSearchService>()
                .SingleInstance();

            builder.RegisterType<SearchCriteriaService>()
                .As<ISearchCriteriaService>()
                .SingleInstance();

            builder.RegisterType<APIWithFiltersFactory>()
                .As<IAPIWithFiltersFactory>()
                .SingleInstance();

            builder.RegisterType<BaseAPIFactory>()
                .As<IAPIFactory>()
                .SingleInstance();

            //#if (DEBUG)
            //            // If in debug mode, log to console instead of file
            //            builder.RegisterType<DebuggerLogger>()
            //                .As<ILogger>()
            //                .SingleInstance();
            //#else
            builder.RegisterType<Logger>()
                .As<ILogger>()
                .WithParameter(new TypedParameter(typeof(NLog.Logger),
                                LogManager.GetCurrentClassLogger()))
                .SingleInstance();
//#endif

            //builder.RegisterType<Messenger>()
            //    .As<IMessenger>()
            //    .SingleInstance();

            //builder.RegisterType<InventoryRepository>()
            //    .As<IInventoryRepository>();

            //builder.RegisterType<InventoryService>()
            //    .As<IInventoryService>();

            //builder.RegisterType<InventoryMapper>()
            //    .AsSelf();

            return builder;
        }

        public virtual IContainer BuildContainerInstance()
        {
            if (_container != null)
                return _container;

            var builder = BuildBuilder();
            _container = builder.Build();

            return _container;
        }


        public  static IContainer BuildContainer()
        {
            if (_container != null)
                return _container;

            ServicesContainer thisContainer = new ServicesContainer();
            return thisContainer.BuildContainerInstance();
        }


    }
}
