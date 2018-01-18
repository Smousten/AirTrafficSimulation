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
     class TrafficLauncher
    {
        //private static readonly int noOfRunways = 2;
        //private static readonly int noOfTaxiWays = 5;
        //private static readonly int taxiWayCapacity = 3;
        //private static readonly int noOfPlanes = 5;
        //private static bool realisticMode = true;
        //private static int windDirection = 18;


        

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //AirField airfield = new AirField();
            //(new System.Threading.Thread(new System.Threading.ThreadStart(() => airfield.run()))).Start();
            //airfield.run();
            // Application.Run(airfield);
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            
            prepAirport prepa = new prepAirport();
            (new System.Threading.Thread(new System.Threading.ThreadStart(() => prepa.run()))).Start();
            //prepGraphics prepg = new prepGraphics(prepa.getAp());
            //(new System.Threading.Thread(new System.Threading.ThreadStart(() => prepg.run()))).Start();


            //ITuple setupTuple = new dotSpace.Objects.Space.Tuple(noOfRunways, noOfTaxiWays, taxiWayCapacity, noOfPlanes, /*airfield,*/ realisticMode, windDirection);
            //Airport airport = new Airport(setupTuple);
            //Application.Run(prepGraphics.getAirfield());
            //airport.printElements();
            //Console.Read();
        }

        
    }

    public class prepGraphics
    {
        private static AirField airfield;
        public prepGraphics(Airport ap)
        {
            
            airfield =  new AirField(ap);
            //(new System.Threading.Thread(new System.Threading.ThreadStart(() => airfield.run()))).Start();

            
        }
        public void run ()
        {
            
            
        }

        public static AirField getAirfield()
        {
            return airfield;
        }
    }

    public class prepAirport
    {
        private static readonly int noOfRunways = 2;
        private static readonly int noOfTaxiWays = 5;
        private static readonly int taxiWayCapacity = 3;
        private static readonly int noOfPlanes = 100;
        private static bool realisticMode = false;
        private static int windDirection = 18;
        private Airport airport;
        public prepAirport()
        {
            ITuple setupTuple = new dotSpace.Objects.Space.Tuple(noOfRunways, noOfTaxiWays, taxiWayCapacity, noOfPlanes, prepGraphics.getAirfield(), realisticMode, windDirection);
            //Airport airport = new Airport(setupTuple);
            this.airport = new Airport(setupTuple);
            //(new System.Threading.Thread(new System.Threading.ThreadStart(() => airfield.run()))).Start();


        }
        public void run()
        {


        }
        public Airport getAp()
        {
            return this.airport;
        }
        
    }

}