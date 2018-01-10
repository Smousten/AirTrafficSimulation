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
        public ITuple getRunwayClearance()
        {
            ITuple freeRunwaySpace = runwaySpace.Get("Runway Nr.", typeof(int));//("runWay", typeof(SequentialSpace)); //Finds the relevant category
            if (freeRunwaySpace != null)
            {
                //string freeRunwayLock = freeRunwaySpace[0] + " " + freeRunwaySpace[1] + " -lock";
                //return (freeRunwayLock);
                //freeRunwaySpace.QueryAll("Runway"); //Finds a free runway
                return freeRunwaySpace;
            }
            return null;
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
