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
        private AirField airField;


        public Translator(SequentialSpace runways, SequentialSpace taxiways, AirField airField)
        {
            this.runwaySpace = runways;
            this.taxiwaySpace = taxiways;
            this.airField = airField;
        }
        //Fire-and-forget method, to update the GUI
        public void updateGraphicalPosition(string credentials, string previousLocation, string nextLocation)
        {
            dotSpace.Objects.Space.Tuple releventDataTuple = new dotSpace.Objects.Space.Tuple(credentials, previousLocation, nextLocation);
            //Tell graphics the input information, so we can draw it
        }
        //Fire-and-forget method, to allow the logic to progress
        public void updateLogicalPosition(string planeCrededentials, string newLocationCredentials)
        {
            string planeNextLocationLock = planeCrededentials = newLocationCredentials + "-lock";
            if (newLocationCredentials == "Runway")
            {
                runwaySpace.Put(planeNextLocationLock);
            }
            else if (newLocationCredentials == "Taxiway")
            {
                taxiwaySpace.Put(planeNextLocationLock);
            }
        }
    }
}
