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
        private static readonly int noOfTaxiWays = 4;
        private static readonly int taxiWayCapacity = 3;
        //private static readonly int noOfPlanes = 30;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AirField airfield = new AirField();
            Application.Run(airfield);
            Airport airport = new Airport(noOfRunways, noOfTaxiWays, taxiWayCapacity);
            Controller.Translator translator = new Controller.Translator(airport.getSpace("runway"),airport.getSpace("taxiway"));

            //airport.printElements();
            //public Airplane(SequentialSpace CTSpace, SequentialSpace rwSpace, SequentialSpace rwlSpace, SequentialSpace twSpace, string credentials)
            //for (int i = 0; i < noOfPlanes; i++)
            //{
            //    Airplane airplane = new Airplane(airport.getSpace("control tower"),airport.getSpace("runway"),airport.getSpace("taxiway"),""+i);
            //    if (i < noOfPlanes/2)
            //    {
            //        (new System.Threading.Thread(new System.Threading.ThreadStart(() => airplane.landing()))).Start();

            //    } else
            //    {
            //        (new System.Threading.Thread(new System.Threading.ThreadStart(() => airplane.takeoff()))).Start();
            //    }
            //}
            //Console.Read();
        }
    }
}
