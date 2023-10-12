using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using SeatsAeroLibrary;
using SeatsAeroLibrary.Services;

namespace SeatsAeroTool.Services
{
    internal class WinFormsServiceContainer : ServicesContainer
    {
        protected override ContainerBuilder BuildBuilder()
        {
            var builder =  base.BuildBuilder();

            builder.RegisterType<Messenger>()
                .As<IMessenger>()
                .SingleInstance();

            return builder;
        }
        public static IContainer BuildContainer()
        {
            if (_container != null)
                return _container;

            WinFormsServiceContainer thisContainer = new WinFormsServiceContainer();
            return thisContainer.BuildContainerInstance();
        }
    }
}
