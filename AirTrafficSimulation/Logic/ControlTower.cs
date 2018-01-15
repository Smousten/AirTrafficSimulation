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

        public ControlTower(SequentialSpace runways, SequentialSpace taxiways, SequentialSpace hangars, SequentialSpace airplanes) 
        {
            this.runwaySpace = runways;
            this.taxiwaySpace = taxiways;
            this.hangarSpace = hangars;
            this.airplaneSpace = airplanes;

            //this.repository.AddSpace("Runways", runways);
            //this.repository.AddSpace("Taxiways", taxiways);
            //this.repository.AddSpace("Hangars", hangars);

        }
        public ITuple getRunwayClearance(string planeCredentials, string currentLocationName)
        {
            return getRunwayClearance(planeCredentials, currentLocationName, 0);
        }

        public ITuple getRunwayClearance(string planeCredentials, string currentLocationName, int currentLocationCredential)
        {
            ITuple freeRunwaySpace;
            Console.WriteLine(planeCredentials + " found a Control Tower and is getting clearance for accessing the runway...");
            if (currentLocationName == "Taxiway")
            {
                freeRunwaySpace = runwaySpace.Get("Runway Nr.", currentLocationCredential);
            }
            else
            {
                freeRunwaySpace = runwaySpace.Get("Runway Nr.", typeof(int));
            }
            Console.WriteLine(planeCredentials + " has been granted clearance to access " + freeRunwaySpace[0] + freeRunwaySpace[1]);
            return freeRunwaySpace;
        }

        public ITuple isRunwayFreeForTakeoff(int runwayNr)
        {
            ITuple freeRunwaySpace = runwaySpace.QueryP("Runway Nr.", runwayNr);
            if (freeRunwaySpace != null)
            {
                return freeRunwaySpace;
            }
            return null;
        }
        public void getRunway(ITuple space)
        {
            if((string)space[0] == "Runway Nr.")
            {
                Console.WriteLine("Locking Runway Nr. " + space[1] +"/ getting key");
                runwaySpace.Get(space);
            }
        }

        public void putRunway(ITuple space)
        {
            if ((string)space[0] == "Runway Nr.")
            {
                Console.WriteLine("Unlocking Runway Nr. {0} / putting it back",space[1]);
                runwaySpace.Put(space);
            }
        }
    }
}
