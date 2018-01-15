using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using dotSpace.Interfaces.Network;
using dotSpace.Interfaces.Space;
using dotSpace.Objects.Network;
using dotSpace.Objects.Space;

namespace AirTrafficSimulation
{
    static class TrafficLauncher
    {
        private static readonly int noOfRunways = 2;
        private static readonly int noOfTaxiWays = 5;
        private static readonly int taxiWayCapacity = 3;
        private static readonly int noOfPlanes = 10;
        private static bool realisticMode = false;
        private static string windDirection = "N";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            AirField airfield = new AirField();
            //Application.Run(airfield);
            dotSpace.Objects.Space.Tuple setupTuple = new dotSpace.Objects.Space.Tuple(noOfRunways, noOfTaxiWays,taxiWayCapacity, noOfPlanes, airfield, realisticMode, windDirection);
            Airport airport = new Airport(setupTuple);

            //airport.printElements();
            //Console.Read();
        }
    }
}