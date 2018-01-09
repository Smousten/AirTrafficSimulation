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
    class Airplane
    {
        //private SpaceRepository airport;
        //private SequentialSpace airportSpace;
        private SequentialSpace controlTowerSpace;
        private SequentialSpace runwaySpace;
        //private SequentialSpace runwayLockSpace;
        private SequentialSpace taxiwayLandingSpace;
        private SequentialSpace taxiwayTakeOffSpace;

        private string credentials;

        public Airplane(SequentialSpace CTSpace, SequentialSpace rwSpace, SequentialSpace twtSpace, SequentialSpace twlSpace, string credentials) //SpaceRepository airportRepository)
        {
            //this.airport = airportRepository;
            this.controlTowerSpace = CTSpace;
            this.runwaySpace = rwSpace;
            //this.runwayLockSpace = rwlSpace;
            this.taxiwayLandingSpace = twlSpace;
            this.taxiwayTakeOffSpace = twtSpace;
            this.credentials = credentials;

        }
        public void landing()
        {
            //Establishing communication
            Console.WriteLine(credentials + " is Searching for control tower...");
            //ITuple controlTowerTupleInner = new dotSpace.Objects.Space.Tuple("Control Tower Nr.", typeof(int), typeof(ControlTower));
            //ITuple controlTowerTuple = airport.QueryP("Control Towers", controlTowerTupleInner);
            if (controlTowerSpace != null)
            {
                ITuple controlTowerTuple = controlTowerSpace.Query("Control Tower Nr.", typeof(int), typeof(ControlTower));
                //Console.WriteLine(controlTowerTuple);
                if (controlTowerTuple != null)
                {
                    //Getting landing clearance
                    ControlTower controlTower = (ControlTower)controlTowerTuple[2];
                    Console.WriteLine(credentials + " found control tower and getting landing clearance...");
                    ITuple freeRunwayLock = controlTower.getRunwayClearance();
                    if (freeRunwayLock != null)
                    {
                        //runwaySpace.Get(freeRunwayLock);
                        Console.WriteLine(credentials + " got landing clearance with ID " + freeRunwayLock[0] + freeRunwayLock[1]);

                        //Leave airspace, land in airport
                        //Thread.sleep(5000);

                        //Landing & Taxiway step
                        Console.WriteLine(credentials + " is searching for free landing taxiway...");
                        //ITuple freeTaxiWayTuple = taxiwaySpace.Query("Taxiway Nr.", controlTowerTuple[1], typeof(int), true);
                        ITuple freeTaxiWayTuple = taxiwayLandingSpace.Get("Taxiway L", typeof(int), typeof(int), true);
                        int barrierLimit = (int)freeTaxiWayTuple[2] - 1;
                        Console.WriteLine(credentials + " found free taxiway, " + freeTaxiWayTuple[0] + freeTaxiWayTuple[1] + " with barrier value " + (barrierLimit + 1) + " and getting free taxiway tuple...");
                        //taxiwaySpace.Get((string)freeTaxiWayTuple[0], freeTaxiWayTuple[1], barrierLimit);
                        Console.WriteLine(credentials + " is putting runway lock with ID " + freeRunwayLock[0] + freeRunwayLock[1]);
                        //runwaySpace.Put(freeRunwayLock);
                        controlTower.putRunway(freeRunwayLock);
                        Console.WriteLine(credentials + " is putting " + freeTaxiWayTuple[0] + freeTaxiWayTuple[1] + " back with new barrier " + barrierLimit);
                        taxiwayLandingSpace.Put((string)freeTaxiWayTuple[0], freeTaxiWayTuple[1], barrierLimit, (barrierLimit) > 0);
                        //System.Threading.Thread.Sleep(2500);

                        //Hanger step
                        Console.WriteLine(credentials + " is searching for " + freeTaxiWayTuple[0] + freeTaxiWayTuple[1] + " again, to increase barrier...");
                        ITuple usedTaxiWay = taxiwayLandingSpace.Get(freeTaxiWayTuple[0], freeTaxiWayTuple[1], typeof(int), typeof(bool));
                        barrierLimit = (int)usedTaxiWay[2] + 1;
                        Console.WriteLine(credentials + " is increasing " + freeTaxiWayTuple[0] + freeTaxiWayTuple[1] + "'s barrierLimit to " + barrierLimit);
                        taxiwayLandingSpace.Put((string)freeTaxiWayTuple[0], freeTaxiWayTuple[1], barrierLimit, (barrierLimit) > 0);
                        Console.WriteLine(credentials + " has safely arrived in the hangar!");
                        // Thread kill
                        //return;
                    }
                }
            }
        }

        public void takeoff()
        {

            //Establishing communication
            Console.WriteLine(credentials + " is searching for control tower...");
            //ITuple controlTowerTupleInner = new dotSpace.Objects.Space.Tuple("Control Tower Nr.", typeof(int), typeof(ControlTower));
            //ITuple controlTowerTuple = airport.QueryP("Control Towers", controlTowerTupleInner);
            if (controlTowerSpace != null)
            {
                ITuple controlTowerTuple = controlTowerSpace.Query("Control Tower Nr.", typeof(int), typeof(ControlTower));
                //Console.WriteLine(controlTowerTuple);
                if (controlTowerTuple != null)
                {
                    // Leaving hangar, entering taxiway

                    Console.WriteLine(credentials + " is searching for free taxiway...");
                    ITuple freeTaxiWayTuple = taxiwayTakeOffSpace.Get("Taxiway T", typeof(int), typeof(int), true);
                    int barrierLimit = (int)freeTaxiWayTuple[2] - 1;

                    Console.WriteLine(credentials + " found free take-off taxiway, " + freeTaxiWayTuple[0] + freeTaxiWayTuple[1] + " with barrier value " + (barrierLimit + 1) + " and getting free taxiway tuple...");
                    //System.Threading.Thread.Sleep(2500);
                    Console.WriteLine(credentials + " is putting " + freeTaxiWayTuple[0] + freeTaxiWayTuple[1] + " back with new barrier " + barrierLimit);
                    taxiwayTakeOffSpace.Put((string)freeTaxiWayTuple[0], freeTaxiWayTuple[1], barrierLimit, barrierLimit > 0);
                    //break;

                    // Leaving taxiway, entering runway

                    //Getting takeoff clearance
                    ControlTower controlTower = (ControlTower)controlTowerTuple[2];
                    Console.WriteLine(credentials + " found control tower and getting takeoff clearance...");

                    ITuple freeRunwayLock = controlTower.getRunwayClearance();
                    if (freeRunwayLock != null)
                    {
                        //Make space on the taxiway you just left
                        Console.WriteLine(credentials + " is searching for " + freeTaxiWayTuple[0] + freeTaxiWayTuple[1] + " again, to increase barrier...");
                        ITuple usedtaxiway = taxiwayTakeOffSpace.Get("Taxiway T", freeTaxiWayTuple[1], typeof(int), typeof(bool));
                        int bl = (int)usedtaxiway[2] + 1;
                        Console.WriteLine(credentials + " is increasing " + usedtaxiway[0] + usedtaxiway[1] + "'s barrierlimit to " + bl);
                        taxiwayTakeOffSpace.Put((string)usedtaxiway[0], usedtaxiway[1], bl, bl > 0);
                        Console.WriteLine(credentials + " has safely left the taxiway!");

                        //Leave the runway
                        //runwaySpace.Get(freeRunwayLock);
                        Console.WriteLine(credentials + " got takeoff clearance with ID " + freeRunwayLock[0] + freeRunwayLock[1] + " and left the airport!");
                        //leave airport, enter in airspace
                        //Thread.sleep(5000);
                        //runwaySpace.Put(freeRunwayLock);
                        controlTower.putRunway(freeRunwayLock);
                        //Airspace step
                        return;

                    }
                }
            }
        }
    }
}
