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

        public ControlTower(SequentialSpace runways, SequentialSpace taxiways, SequentialSpace hangars) 
        {
            this.runwaySpace = runways;
            this.taxiwaySpace = taxiways;
            this.hangarSpace = hangars;

            //this.repository.AddSpace("Runways", runways);
            //this.repository.AddSpace("Taxiways", taxiways);
            //this.repository.AddSpace("Hangars", hangars);

        }
        public string getLandingClearance()
        {
            ITuple freeRunwaySpace = runwaySpace.QueryP("Runway Nr.", typeof(int));//("runWay", typeof(SequentialSpace)); //Finds the relevant category
            if (freeRunwaySpace != null)
            {
                string freeRunwayLock = freeRunwaySpace[0] + " " + freeRunwaySpace[1] + " -lock";
                return (freeRunwayLock);
                //freeRunwaySpace.QueryAll("Runway"); //Finds a free runway
            }
            return null;
        }
    }
}
