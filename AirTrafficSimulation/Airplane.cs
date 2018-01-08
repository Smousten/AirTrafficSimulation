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
        private SequentialSpace airportSpace;
        private SequentialSpace controlTowerSpace;
        private SequentialSpace runwaySpace;
        private SequentialSpace runwayLockSpace;
        private SequentialSpace taxiwaySpace;

        private string credentials;

        public Airplane(SequentialSpace CTSpace, SequentialSpace rwSpace, SequentialSpace rwlSpace, SequentialSpace twSpace, string credentials) //SpaceRepository airportRepository)
        {
            //this.airport = airportRepository;
            this.controlTowerSpace = CTSpace;
            this.runwaySpace = rwSpace;
            this.runwayLockSpace = rwlSpace;
            this.taxiwaySpace = twSpace;
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
                    string freeRunwayLock = controlTower.getLandingClearance();
                    if (freeRunwayLock != null)
                    {
                        runwayLockSpace.Get(freeRunwayLock);
                        Console.WriteLine(credentials + " got landing clearance with ID " + freeRunwayLock);
                        //Leave airspace, land in airport
                        //Thread.sleep(5000);
                        //Landing & Taxiway step
                        while (true) //airport.Query(freeTaxiWayTuple[0],freeTaxiWayTuple[1],)
                        {
                            Console.WriteLine(credentials + " is searching for free taxiway...");
                            ITuple freeTaxiWayTuple = taxiwaySpace.Query("Taxiway Nr.", controlTowerTuple[1], typeof(int));
                            int barrierLimit = (int)freeTaxiWayTuple[2];
                            if (barrierLimit > 0) //If taxiway isn't full
                            {
                                Console.WriteLine(credentials + " found free taxiway with barrier value " + barrierLimit + " and getting free taxiway tuple...");
                                taxiwaySpace.Get((string)freeTaxiWayTuple[0], freeTaxiWayTuple[1], barrierLimit);
                                Console.WriteLine(credentials + " is putting runway lock with ID " + freeRunwayLock);
                                runwayLockSpace.Put(freeRunwayLock);
                                Console.WriteLine(credentials + " is putting free taxiway back with new barrier " + barrierLimit--);
                                taxiwaySpace.Put((string)freeTaxiWayTuple[0], freeTaxiWayTuple[1], barrierLimit--);
                                //System.Threading.Thread.Sleep(2500);
                                break;
                            }
                            
                        }
                        //Hanger step
                        Console.WriteLine(credentials + " is searching for same taxiway, to increase barrier...");
                        ITuple usedTaxiWay = taxiwaySpace.Get("Taxiway Nr.", controlTowerTuple[1], typeof(int));
                        int bL = (int)usedTaxiWay[2];
                        Console.WriteLine(credentials + " is increasing taxiway barrierLimit to " + bL++);
                        taxiwaySpace.Put((string)usedTaxiWay[0], usedTaxiWay[1], bL++);
                        Console.WriteLine(credentials + " has safely arrived in the hangar!");
                        // Thread kill
                        return;

                    }
                }
            
                Console.WriteLine(credentials + " did not find a free runway, asking again...");
            }
            Console.WriteLine(credentials + " did not find a control tower, trying again...");
        }

        public void takeoff()
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
                    // Leaving hangar, entering taxiway
                    while (true) //airport.Query(freeTaxiWayTuple[0],freeTaxiWayTuple[1],)
                    {
                        Console.WriteLine(credentials + " is searching for free taxiway...");
                        ITuple freeTaxiWayTuple = taxiwaySpace.Query("Taxiway Nr.", controlTowerTuple[1], typeof(int));
                        int barrierLimit = (int)freeTaxiWayTuple[2];
                        if (barrierLimit > 0) //If taxiway isn't full
                        {
                            Console.WriteLine(credentials + " found free taxiway with barrier value " + barrierLimit + " and getting free taxiway tuple...");
                            taxiwaySpace.Get((string)freeTaxiWayTuple[0], freeTaxiWayTuple[1], barrierLimit);
                            //System.Threading.Thread.Sleep(2500);                      
                            Console.WriteLine(credentials + " is putting free taxiway back with new barrier " + barrierLimit--);
                            taxiwaySpace.Put((string)freeTaxiWayTuple[0], freeTaxiWayTuple[1], barrierLimit--);
                            break;
                        }

                    }
                    // Leaving taxiway, entering runway
                    Console.WriteLine(credentials + " is searching for same taxiway, to increase barrier...");
                    ITuple usedTaxiWay = taxiwaySpace.Get("Taxiway Nr.", controlTowerTuple[1], typeof(int));
                    int bL = (int)usedTaxiWay[2];
                    Console.WriteLine(credentials + " is increasing taxiway barrierLimit to " + bL++);
                    taxiwaySpace.Put((string)usedTaxiWay[0], usedTaxiWay[1], bL++);
                    Console.WriteLine(credentials + " has safely left the taxiway!");

                    //Getting takeoff clearance
                    ControlTower controlTower = (ControlTower)controlTowerTuple[2];
                    Console.WriteLine(credentials + " found control tower and getting takeoff clearance...");
                    string freeRunwayLock = controlTower.getLandingClearance();
                    if (freeRunwayLock != null)
                    {
                        runwayLockSpace.Get(freeRunwayLock);
                        Console.WriteLine(credentials + " got takeoff clearance with ID " + freeRunwayLock + " and left the airport!");
                        //leave airport, enter in airspace
                        //Thread.sleep(5000);
                        //Airspace step
                        return;

                    }
                    Console.WriteLine(credentials + " did not find a free runway, asking again...");
                }

                Console.WriteLine(credentials + " did not find a control tower, asking again...");
            }

        }
    }
}
