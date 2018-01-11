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
        private SequentialSpace taxiwaySpace;

        private string credentials;

        public Airplane(SequentialSpace CTSpace, SequentialSpace rwSpace, SequentialSpace twSpace, string credentials) //SpaceRepository airportRepository)
        {
            //this.airport = airportRepository;
            this.controlTowerSpace = CTSpace;
            this.runwaySpace = rwSpace;
            //this.runwayLockSpace = rwlSpace;
            this.taxiwaySpace = twSpace;
            this.credentials = credentials;

        }
        public void flyEternally(string startingLocation)
        {
            while(true)
            {
                if (startingLocation == "Airspace")
                {
                    landing();
                    takeoff();
                } else if (startingLocation == "Hangar")
                {
                    takeoff();
                    landing();
                } else
                {
                    Console.WriteLine("You are drunk, go home.");
                    break;
                }
            }
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
                        ITuple freeTaxiWayTuple = taxiwaySpace.Get("Taxiway L", controlTowerTuple[1], typeof(int), true);
                        int barrierLimit = (int)freeTaxiWayTuple[2] - 1;
                        Console.WriteLine(credentials + " found free taxiway with barrier value " + (barrierLimit + 1) + " and getting free taxiway tuple...");
                        //taxiwaySpace.Get((string)freeTaxiWayTuple[0], freeTaxiWayTuple[1], barrierLimit);
                        Console.WriteLine(credentials + " is putting runway lock with ID " + freeRunwayLock[0] + freeRunwayLock[1]);
                        //runwaySpace.Put(freeRunwayLock);
                        controlTower.putRunway(freeRunwayLock);
                        Console.WriteLine(credentials + " is putting free taxiway back with new barrier " + barrierLimit);
                        taxiwaySpace.Put((string)freeTaxiWayTuple[0], freeTaxiWayTuple[1], barrierLimit, (barrierLimit) > 0);
                        //System.Threading.Thread.Sleep(2500);

                        //Hanger step
                        Console.WriteLine(credentials + " is searching for same landing taxiway, to increase barrier...");
                        ITuple usedTaxiWay = taxiwaySpace.Get("Taxiway L", controlTowerTuple[1], typeof(int), typeof(bool));
                        barrierLimit = (int)usedTaxiWay[2] + 1;
                        Console.WriteLine(credentials + " is increasing landing " + usedTaxiWay[1] + "'s taxiway barrierLimit to " + barrierLimit);
                        taxiwaySpace.Put((string)freeTaxiWayTuple[0], freeTaxiWayTuple[1], barrierLimit, (barrierLimit) > 0);
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
                    ITuple freeTaxiWayTuple = taxiwaySpace.Get("Taxiway T", controlTowerTuple[1], typeof(int), true);
                    int barrierLimit = (int)freeTaxiWayTuple[2] - 1;

                    Console.WriteLine(credentials + " found free take-off taxiway with barrier value " + (barrierLimit + 1) + " and getting free taxiway tuple...");
                    //System.Threading.Thread.Sleep(2500);                      
                    Console.WriteLine(credentials + " is putting free taxiway back with new barrier " + barrierLimit);
                    taxiwaySpace.Put((string)freeTaxiWayTuple[0], freeTaxiWayTuple[1], barrierLimit, barrierLimit > 0);
                    //break;

                    // Leaving taxiway, entering runway

                    //Getting takeoff clearance
                    ControlTower controlTower = (ControlTower)controlTowerTuple[2];
                    Console.WriteLine(credentials + " found control tower and getting takeoff clearance...");

                    ITuple freeRunwayLock = controlTower.getRunwayClearance();
                    if (freeRunwayLock != null)
                    {
                        //Make space on the taxiway you just left
                        Console.WriteLine(credentials + " is searching for same take-off taxiway, to increase barrier...");
                        ITuple usedtaxiway = taxiwaySpace.Get("Taxiway T", controlTowerTuple[1], typeof(int), typeof(bool));
                        int bl = (int)usedtaxiway[2] + 1;
                        Console.WriteLine(credentials + " is increasing take-off taxiway " + usedtaxiway[1] + "'s barrierlimit to " + bl);
                        taxiwaySpace.Put((string)usedtaxiway[0], usedtaxiway[1], bl, bl > 0);
                        Console.WriteLine(credentials + " has safely left the taxiway!");

                        //Leave the runway
                        //runwaySpace.Get(freeRunwayLock);
                        Console.WriteLine(credentials + " got takeoff clearance with ID " + freeRunwayLock[0] + freeRunwayLock[1] + " and left the airport!");
                        //leave airport, enter in airspace
                        //Thread.sleep(5000);
                        //runwaySpace.Put(freeRunwayLock);
                        controlTower.putRunway(freeRunwayLock);
                        //Airspace step

                    }
                }
            }
        }
    }
}
