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
        //Fire-and-forget method, for the logic to update the GUI
        public void updateGraphicalPosition(string planeCredentials, string previousLocationCredentials, string nextLocationCredentials)
        {
            // Tell graphics the input information, so we can draw it
            dotSpace.Objects.Space.Tuple relevantDataTuple = new dotSpace.Objects.Space.Tuple(planeCredentials, previousLocationCredentials, nextLocationCredentials);
            //airField.UpdatePlanePosition(planeCredentials, previousLocation, nextLocation);
            
        }
        
        //Fire-and-forget method, for the GUI to allow the logic to progress onto a taxiway, or a runway
        public void updateLogicalPosition(string planeCredentials, string newLocationCredentials)
        {
            string planeNextLocationLock = planeCredentials + newLocationCredentials + "-lock";
            if (newLocationCredentials == "Runway")
            {
                runwaySpace.Put(planeNextLocationLock);
            } else if (newLocationCredentials == "Taxiway")
            {
                taxiwaySpace.Put(planeNextLocationLock);
            }
        }
    }
}
