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
        private SpaceRepository airport;
        private string credentials;

        public Airplane(SpaceRepository airportRepository, string credentials)
        {
            this.airport = airportRepository;
            this.credentials = credentials;

        }
        public void landing()
        {
            while(true)
            {
                //Establishing communication
                Console.WriteLine(credentials + " is Searching for control tower...");
                //ITuple controlTowerTupleInner = new dotSpace.Objects.Space.Tuple("Control Tower Nr.", typeof(int), typeof(ControlTower));
                //ITuple controlTowerTuple = airport.QueryP("Control Towers", controlTowerTupleInner);
                ITuple controlTowerTuple = airport.Query("Control Tower Nr.", typeof(int), typeof(ControlTower));
                if (controlTowerTuple != null)
                {
                    //Getting landing clearance
                    ControlTower controlTower = (ControlTower) controlTowerTuple[2];
                    Console.WriteLine(credentials + " found control tower and getting landing clearance...");
                    string freeRunwayLock = controlTower.getLandingClearance();
                    if (freeRunwayLock != null)
                    {
                        airport.Get(freeRunwayLock);
                        Console.WriteLine(credentials + " got landing clearance with ID " + freeRunwayLock);
                        //Leave airspace, land in airport
                        //Thread.sleep(5000);
                        //Landing & Taxiway step
                        while (true) //airport.Query(freeTaxiWayTuple[0],freeTaxiWayTuple[1],)
                        {
                            Console.WriteLine(credentials + " is searching for free taxiway...");
                            ITuple freeTaxiWayTuple = airport.Query("Taxiway Nr.", controlTowerTuple[1], typeof(int));
                            int barrierLimit = (int)freeTaxiWayTuple[2];
                            if (barrierLimit > 0) //If taxiway isn't full
                            {
                                Console.WriteLine(credentials + " found free taxiway with barrier value " + barrierLimit + " and getting free taxiway tuple...");
                                airport.Get((string)freeTaxiWayTuple[0], freeTaxiWayTuple[1], barrierLimit);
                                Console.WriteLine(credentials + " is putting runway lock with ID " + freeRunwayLock);
                                airport.Put(freeRunwayLock);
                                Console.WriteLine(credentials + " is putting free taxiway back with new barrier " + barrierLimit--);
                                airport.Put((string)freeTaxiWayTuple[0], freeTaxiWayTuple[1], barrierLimit--);
                                break;
                            }
                            //Thread.sleep(2500)
                        }
                        //Hanger step
                        Console.WriteLine(credentials + " is searching for same taxiway, to increase barrier...");
                        ITuple usedTaxiWay = airport.Get("Taxiway Nr.", controlTowerTuple[1], typeof(int));
                        int bL = (int)usedTaxiWay[2];
                        Console.WriteLine(credentials + " is increasing taxiway barrierLimit to " + bL++);
                        airport.Put((string)usedTaxiWay[0], usedTaxiWay[1], bL++);
                        Console.WriteLine(credentials + " has safely arrived in the hangar!");
                        //Thread.kill
                        break;
                    }
                    Console.WriteLine(credentials + " did not find a free runway, asking again...");
                }
                Console.WriteLine(credentials + " did not find a control tower, trying again...");
            }
        }
        public void takeoff()
        {

        }
    }
}
