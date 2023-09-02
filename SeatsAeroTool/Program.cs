using Autofac;
using SeatsAeroLibrary;
using SeatsAeroLibrary.Models;
using SeatsAeroLibrary.Services;
using SeatsAeroTool.Services;

namespace SeatsAeroTool
{
    internal static class Program
    {

        private static ILogger _logger;
        private static IMessenger _messenger;

        public static SeatsAeroAPI seatsAeroInfo;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Setup Global Exception Handlers before anything else
            Application.ThreadException += OnThreadException;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();


            using (var scope = WinFormsServiceContainer.BuildContainer().BeginLifetimeScope())
            {
                _logger = scope.Resolve<ILogger>();
                _messenger = scope.Resolve<IMessenger>();
            }

            seatsAeroInfo = new SeatsAeroAPI();

            seatsAeroInfo.LoadAvailability( MileageProgram.american);

            Application.Run(new MainForm());
        }


        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            OnExceptionExit((Exception)e.ExceptionObject);
        }

        private static void OnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            OnExceptionExit(e.Exception);
        }
        private static void OnExceptionExit(Exception e)
        {
            _logger?.Error(e);
            //_messenger?.ShowMessageBox("Oops! Something went wrong!", "Unexpected Exception");
            Environment.Exit(1);
        }
    }
}