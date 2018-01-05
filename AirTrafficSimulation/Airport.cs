using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotSpace.Interfaces.Network;
using dotSpace.Interfaces.Space;
using dotSpace.Objects.Network;
using dotSpace.Objects.Space;


namespace AirTrafficSimulation
{
    class Airport
    {
        private SpaceRepository airport;
        private SequentialSpace runWaySpace;
        private SequentialSpace runWayLockSpace;
        private SequentialSpace taxiWaySpace;
        private SequentialSpace hangerSpace;
        private SequentialSpace controlTowerSpace;
        private int noOfRunways;
        private int noOfTaxiways;
        private int noOfHangers;
        private int noOfControlTowers;
        private readonly int barrier = 3;


        public Airport(int noOfRunways, int noOfTaxiways)//, int noOfHangers, int noOfControlTowers) 
        {
            
            this.airport = new SpaceRepository();
            this.runWaySpace = new SequentialSpace();
            this.runWayLockSpace = new SequentialSpace();
            this.taxiWaySpace = new SequentialSpace();
            //this.hanger = new SequentialSpace();
            //this.controlTower = new SequentialSpace();
            this.noOfRunways = noOfRunways;
            this.noOfTaxiways = noOfTaxiways;
            //this.noOfHangers = noOfHangers;
            //this.noOfControlTowers = noOfControlTowers;
            while(noOfRunways > 0 || noOfTaxiways > 0)//|| noOfHangers > 0)
            {
                if (noOfRunways > 0)
                {
                    this.runWaySpace.Put("Runway Nr.", noOfRunways);
                    this.runWayLockSpace.Put("Runway Nr. " + noOfRunways + " -lock");
                    noOfRunways--;
                }
                if (noOfTaxiways > 0)
                {
                    this.taxiWaySpace.Put("Taxiway Nr.", noOfTaxiways,barrier);

                    noOfTaxiways--;
                }
                /*if (noOfHangers > 0)
                {
                    this.hanger.Put("Hanger Nr.", noOfHangers);
                    noOfHangers--;
                }*/
                while (noOfControlTowers > 0)
                {
                    ControlTower controlTower = new ControlTower(runWaySpace, taxiWaySpace, hangerSpace);
                    this.controlTowerSpace.Put("Control Tower Nr.", noOfControlTowers, controlTower);
                    noOfControlTowers--;
                }
            }
            this.airport.AddSpace("Runways", runWaySpace);
            this.airport.AddSpace("Runway Locks", runWayLockSpace);
            this.airport.AddSpace("Taxiways", taxiWaySpace);
            this.airport.AddSpace("Hangers", hangerSpace);
            this.airport.AddSpace("Control Towers", controlTowerSpace);
        }
    }
}
