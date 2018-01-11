using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotSpace.Objects.Space;
using dotSpace.Interfaces.Space;
using dotSpace.Interfaces.Network;
using dotSpace.Objects.Network;

namespace AirTrafficSimulation
{
    class ControlTower //Consider making a "Mother Control Tower" / "Communication center", to redirect requests from planes
    {
        //private SpaceRepository repository;
        private SequentialSpace runwaySpace;
        private SequentialSpace taxiwaySpace;
        private SequentialSpace hangarSpace;
        private SequentialSpace airplaneSpace;

        private Controller.Translator translator;

        public ControlTower(SequentialSpace runways, SequentialSpace taxiways, SequentialSpace hangars, SequentialSpace airplanes, AirField airField) 
        {
            this.runwaySpace = runways;
            this.taxiwaySpace = taxiways;
            this.hangarSpace = hangars;
            this.airplaneSpace = airplanes;

            this.translator = new Controller.Translator(runways, taxiways, airField);

            //this.repository.AddSpace("Runways", runways);
            //this.repository.AddSpace("Taxiways", taxiways);
            //this.repository.AddSpace("Hangars", hangars);

        }

        public ITuple getRunwayClearance(string planeCredentials, string currentLocationIdentifier)
        {
            Console.WriteLine(planeCredentials + " is getting runWay clearance...");
            ITuple freeRunwaySpace = runwaySpace.Get("Runway Nr.", typeof(int));
            translator.updateGraphicalPosition(planeCredentials, currentLocationIdentifier, "" + freeRunwaySpace[0] + freeRunwaySpace[1]);
            Console.WriteLine(planeCredentials + " has gotten runWay clearance!");
            return freeRunwaySpace;
        }

        public ITuple getTaxiwayClearance(string planeCredentials, string currentLocationIdentifier, bool takeOff)
        {
            Console.WriteLine(planeCredentials+" is getting taxiWay clearance...");
            ITuple freeTaxiwaySpace;
            if (takeOff)
            {
                freeTaxiwaySpace = taxiwaySpace.Get("Taxiway T", typeof(int));
            } else
            {
                freeTaxiwaySpace = taxiwaySpace.Get("Taxiway L", typeof(int));
            }
            translator.updateGraphicalPosition(planeCredentials, currentLocationIdentifier, "" + freeTaxiwaySpace[0] + freeTaxiwaySpace[1]);
            Console.WriteLine(planeCredentials + " has gotten runWay clearance!");
            return freeTaxiwaySpace;
        }

        public void putRunway(ITuple space)
        {
            if ((string)space[0] == "Runway Nr.")
            {
                Console.WriteLine("Unlocking Runway Nr. {0} / putting it back", space[1]);
                runwaySpace.Put(space);
            }
        }

        public void putTaxiway(ITuple space)
        {
            if ((string)space[0] == "Taxiway T" || (string)space[0] == "Taxiway L")
            {
                Console.WriteLine("Unlocking Taxiway Nr. {0} / putting it back", space[1]);
                taxiwaySpace.Put(space);
            }
        }

        public void enterHangar(string planeCredentials, string currentLocationIdentifier)
        {
            translator.updateGraphicalPosition(planeCredentials, currentLocationIdentifier, "Hangar");
        }

        public void enterAirspace(string planeCredentials, string currentLocationIdentifier)
        {
            translator.updateGraphicalPosition(planeCredentials, currentLocationIdentifier, "Airspace");
        }
    }
}
