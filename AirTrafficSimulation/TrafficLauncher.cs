﻿using System;
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
        public static readonly int GRID_SIZE = 500;
        public static readonly int WIDTH = GRID_SIZE;
        public static readonly int HEIGHT = GRID_SIZE;
        public static readonly int noOfRunways = 3;
        public static readonly int noOfTaxiWays = 5;
        public static readonly int taxiWayCapacity = 3;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new StartScreen());
            Airport airport = new Airport(noOfRunways, noOfTaxiWays, taxiWayCapacity);
            //airport.printElements();
            //public Airplane(SequentialSpace CTSpace, SequentialSpace rwSpace, SequentialSpace rwlSpace, SequentialSpace twSpace, string credentials)
            for(int i = 0; i < 30; i++)
            {
                Airplane airplane = new Airplane(airport.getSpace("control tower"),airport.getSpace("runway"),airport.getSpace("taxiway-out"),airport.getSpace("taxiway-in"),""+i);
                if (i < 15)
                {
                    (new System.Threading.Thread(new System.Threading.ThreadStart(() => airplane.landing()))).Start();

                } else
                {
                    (new System.Threading.Thread(new System.Threading.ThreadStart(() => airplane.takeoff()))).Start();
                }
            }
            Console.Read();

            //(new System.Threading.Thread(new System.Threading.ThreadStart(() => controlTower.run()))).Start();
        }
    }
}
