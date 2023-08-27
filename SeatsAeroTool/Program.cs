using SeatsAeroLibrary;

namespace SeatsAeroTool
{
    internal static class Program
    {

        public static SeatsAeroAPI seatsAeroInfo;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            seatsAeroInfo = new SeatsAeroAPI();
            seatsAeroInfo.LoadAvailability(MileageProgram.lifemiles);

            Application.Run(new MainForm());
        }
    }
}