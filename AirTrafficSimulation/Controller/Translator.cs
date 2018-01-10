using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotSpace.Interfaces.Space;
using dotSpace.Objects.Space;

namespace AirTrafficSimulation.Controller
{
    class Translator
    {
        private SequentialSpace runwaySpace;
        private SequentialSpace taxiwaySpace;
        public Translator(SequentialSpace runways, SequentialSpace taxiways)
        {
            this.runwaySpace = runways;
            this.taxiwaySpace = taxiways;
        }
        //Fire-and-forget method, to update the GUI
        public void updateGraphicalPosition(string credentials, string previousLocation, string nextLocation)
        {
            //Tell graphics the input information, so we can draw it
        }
        //Fire-and-forget method, to allow the logic to progress
        public void updateLogicalPosition()
        {

        }
    }
}
