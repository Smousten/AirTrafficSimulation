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

        public ControlTower(SequentialSpace runwaySpace, SequentialSpace taxiwaySpace, SequentialSpace hangarSpace, SequentialSpace airplaneSpace) 
        {
            this.runwaySpace = runwaySpace;
            this.taxiwaySpace = taxiwaySpace;
            this.hangarSpace = hangarSpace;
            this.airplaneSpace = airplaneSpace;

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
            if (currentLocationName == "Taxiway T" || currentLocationName == "Taxiway T")
            {
                freeRunwaySpace = runwaySpace.Get("Runway Nr.",currentLocationCredential);
            } else
            {
                freeRunwaySpace = runwaySpace.Get("Runway Nr.", typeof(int));
            }
            Console.WriteLine(planeCredentials + " has been granted clearance to access " + freeRunwaySpace[0] + freeRunwaySpace[1]);
            return freeRunwaySpace;
        }

        /*public ITuple getTaxiwayClearance(string planeCredentials, string currentLocationIdentifier, bool takeOff)
        {
            ITuple freeTaxiwaySpace;
            if (takeOff)
            {
                freeTaxiwaySpace = taxiwaySpace.Get("Taxiway T", typeof(int));
            } else
            {
                freeTaxiwaySpace = taxiwaySpace.Get("Taxiway L", typeof(int));
            }
            return freeTaxiwaySpace;
        }*/

        public void putRunway(ITuple space)
        {
            if ((string)space[0] == "Runway Nr.")
            {
                Console.WriteLine("Unlocking Runway Nr. {0} / putting it back", space[1]);
                runwaySpace.Put(space);
            }
        }

        /*public void putTaxiway(ITuple space)
        {
            if ((string)space[0] == "Taxiway T" || (string)space[0] == "Taxiway L")
            {
                Console.WriteLine("Unlocking Taxiway Nr. {0} / putting it back", space[1]);
                taxiwaySpace.Put(space);
            }
        }*/
    }
}
