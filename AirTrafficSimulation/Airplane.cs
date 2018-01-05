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

        public Airplane(SpaceRepository airportRepository, string credentials)
        {
            this.airport = airportRepository;

        }
        public void run()
        {
            while(true)
            {
                ITuple controlTowerTuple = airport.QueryP("Control Tower Nr.", typeof(int), typeof(ControlTower)); 
                if (controlTowerTuple != null)
                {
                    ControlTower controlTower = (ControlTower) controlTowerTuple[2];
                    string freeRunwayLock = controlTower.getLandingClearance();
                    if (freeRunwayLock != null)
                    {
                        airport.Get(freeRunwayLock);
                        //Leave airspace, land in airport
                        //Thread.sleep(5000);
                        while (true) //airport.Query(freeTaxiWayTuple[0],freeTaxiWayTuple[1],)
                        {
                            ITuple freeTaxiWayTuple = airport.Query("Taxiway Nr.", controlTowerTuple[1], typeof(int));
                            int barrierLimit = (int)freeTaxiWayTuple[2];
                            if (barrierLimit > 0)
                            {
                                airport.Get((string)freeTaxiWayTuple[0], freeTaxiWayTuple[1], barrierLimit);
                                airport.Put(freeRunwayLock);
                                airport.Put((string)freeTaxiWayTuple[0], freeTaxiWayTuple[1], barrierLimit--);
                                break;
                            }
                            //Thread.sleep(2500)
                        }
                        //Thread.kill
                    }
                }
            }
        }
    }
}
