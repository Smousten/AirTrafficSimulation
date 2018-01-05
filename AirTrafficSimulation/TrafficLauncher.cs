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
        public static readonly int GRID_SIZE = 500;
        public static readonly int WIDTH = GRID_SIZE;
        public static readonly int HEIGHT = GRID_SIZE;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartScreen());
            SequentialSpace airport = new SequentialSpace();
            Airport controlTower = new Airport(airport);
            //(new System.Threading.Thread(new System.Threading.ThreadStart(() => controlTower.run()))).Start();


        }
    }
}
