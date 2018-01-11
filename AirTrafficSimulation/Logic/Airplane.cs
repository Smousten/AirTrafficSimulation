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
        private string runwayGUILock;
        private string taxiwayGUILock;
        private readonly string airspaceName = "Airspace";
        private readonly string hangarName = "Hangar";

        private Controller.Translator translator;

        public Airplane(SequentialSpace CTSpace, SequentialSpace rwSpace, SequentialSpace twSpace, string credentials, AirField airField) //SpaceRepository airportRepository)
        {
            //this.airport = airportRepository;
            this.controlTowerSpace = CTSpace;
            this.runwaySpace = rwSpace;
            this.taxiwaySpace = twSpace;
            this.credentials = credentials;
            this.runwayGUILock = credentials + "Runway" + "-lock";
            this.taxiwayGUILock = credentials + "Taxiway" + "-lock";
            
            this.translator = new Controller.Translator(rwSpace, twSpace, airField);
        }
        // TODO: Chop out all the sections in the landing and take-off methods, 
        //          and create a system, where when you connect to a controltower for the first time,
        //          you get a command stack, which tells the plane which taxiways to use,
        //          to get to the hangar

        // TODO: Control Tower funnels everything into a serial system. Fix that.
        //          Move translator into the Airplane, from the Control Tower.
        //          

        public void flyEternally(string startingLocation)
        {
            while (true)
            {
                if (startingLocation == airspaceName)
                {
                    landing();
                    takeoff();
                } else if (startingLocation == hangarName)
                {
                    takeoff();
                    landing();
                } else
                {
                    Console.WriteLine("You are drunk, plane {0}. {1} doesn't exist. Go home.", credentials, startingLocation);
                    break;
                }
            }
        }

        public void landing()
        {
            //Establishing communication
            Console.WriteLine(credentials + " is Searching for control tower...");
            ITuple controlTowerTuple = controlTowerSpace.Query("Control Tower Nr.", typeof(int), typeof(ControlTower));
                
            //Getting landing clearance
            ControlTower controlTower = (ControlTower)controlTowerTuple[2];
            Console.WriteLine(credentials + " found control tower and getting landing clearance...");
            ITuple freeRunwayForLanding = controlTower.getRunwayClearance(credentials, airspaceName);
            
            //Leave airspace, land in airport
            updateGraphics(credentials, airspaceName, "" + freeRunwayForLanding[0] + freeRunwayForLanding[1]);
            runwaySpace.Get(runwayGUILock); //Thread.sleep(5000);

            //Landing & Taxiway step
            Console.WriteLine(credentials + " is searching for free landing taxiway...");
            ITuple freeTaxiWayTuple = taxiwaySpace.Get("Taxiway L", controlTowerTuple[1], typeof(int), true);
            int barrierLimit = (int)freeTaxiWayTuple[2] - 1;
            Console.WriteLine(credentials + " found free landing " + freeTaxiWayTuple[0] + freeTaxiWayTuple[1] + " with barrier value " + (barrierLimit + 1) + " and getting free taxiway tuple...");
            Console.WriteLine(credentials + " is putting runway lock with ID " + freeRunwayForLanding[0] + freeRunwayForLanding[1]);
            controlTower.putRunway(freeRunwayForLanding);
            updateGraphics(credentials, "" + freeRunwayForLanding[0] + freeRunwayForLanding[1], "" + freeTaxiWayTuple[0] + freeTaxiWayTuple[1]);
            Console.WriteLine(credentials + " is putting free landing " + freeTaxiWayTuple[0] + freeTaxiWayTuple[1] + "back with new barrier " + barrierLimit);
            taxiwaySpace.Put((string)freeTaxiWayTuple[0], freeTaxiWayTuple[1], barrierLimit, (barrierLimit) > 0);
            taxiwaySpace.Get(taxiwayGUILock);//System.Threading.Thread.Sleep(2500);

            //Rectify the barier
            Console.WriteLine(credentials + " is searching for same landing taxiway, to increase barrier...");
            ITuple usedTaxiWay = taxiwaySpace.Get("Taxiway L", freeTaxiWayTuple[1], typeof(int), typeof(bool));
            barrierLimit = (int)usedTaxiWay[2] + 1;
            Console.WriteLine(credentials + " is increasing landing " + usedTaxiWay[0] + usedTaxiWay[1] + "'s taxiway barrierLimit to " + barrierLimit);
            taxiwaySpace.Put((string)freeTaxiWayTuple[0], freeTaxiWayTuple[1], barrierLimit, (barrierLimit) > 0);
            
            //Arrive in the hangar
            updateGraphics(credentials, "" + usedTaxiWay[0] + usedTaxiWay[1], hangarName);
            Console.WriteLine(credentials + " has safely arrived in the hangar!");
            // Thread kill
        }

        public void takeoff()
        {
            // Leaving hangar, entering taxiway
            Console.WriteLine(credentials + " is searching for free taxiway...");
            ITuple freeTaxiWayTuple = taxiwaySpace.Get("Taxiway T", typeof(int), typeof(int), true);
            int barrierLimit = (int)freeTaxiWayTuple[2] - 1;
            Console.WriteLine(credentials + " found free take-off " + freeTaxiWayTuple[0] + freeTaxiWayTuple[1] + " with barrier value " + (barrierLimit + 1) + " and getting free taxiway tuple...");
            Console.WriteLine(credentials + " is putting free take-off " + freeTaxiWayTuple[0] + freeTaxiWayTuple[1] + " back with new barrier " + barrierLimit);
            updateGraphics(credentials, hangarName, "" + freeTaxiWayTuple[0] + freeTaxiWayTuple[1]);
            taxiwaySpace.Put((string)freeTaxiWayTuple[0], freeTaxiWayTuple[1], barrierLimit, barrierLimit > 0);
            taxiwaySpace.Get(taxiwayGUILock);

            //Establishing communication
            Console.WriteLine(credentials + " is searching for control tower...");
            ITuple controlTowerTuple = controlTowerSpace.Query("Control Tower Nr.", typeof(int), typeof(ControlTower));
            ControlTower controlTower = (ControlTower)controlTowerTuple[2];

            // Leaving taxiway, entering runway
            Console.WriteLine(credentials + " is asking the controlTower, for a free runway...");
            ITuple freeRunwayForTakeoff = controlTower.getRunwayClearance(credentials, "" + freeTaxiWayTuple[0], (int)freeTaxiWayTuple[1]);

            // Make space on the taxiway you just left
            Console.WriteLine(credentials + " is searching for same take-off " + freeTaxiWayTuple[0] + freeTaxiWayTuple[1] + ", to increase barrier...");
            ITuple usedtaxiway = taxiwaySpace.Get("Taxiway T", freeTaxiWayTuple[1], typeof(int), typeof(bool));
            barrierLimit = (int)usedtaxiway[2] + 1;
            Console.WriteLine(credentials + " is increasing take-off " + usedtaxiway[0] + usedtaxiway[1] + "'s barrierlimit to " + barrierLimit);
            taxiwaySpace.Put((string)usedtaxiway[0], usedtaxiway[1], barrierLimit, barrierLimit > 0);
            updateGraphics(credentials, "" + freeTaxiWayTuple[0] + freeTaxiWayTuple[1], "" + freeRunwayForTakeoff[0] + freeRunwayForTakeoff[1]);
            Console.WriteLine(credentials + " has safely left the taxiway!");

            //Leave the runway, enter airspace
            Console.WriteLine(credentials + " is taking off from " + freeRunwayForTakeoff[0] + freeRunwayForTakeoff[1] + " and left the airport!");
            updateGraphics(credentials, "" + freeRunwayForTakeoff[0] + freeRunwayForTakeoff[1], airspaceName);
            controlTower.putRunway(freeRunwayForTakeoff); //Thread.sleep(5000);

            //Airspace step
        }

        private void updateGraphics(string planeCredentials, string currentLocationIdentifier, string nextLocationIdentifier)
        {
            translator.updateGraphicalPosition(planeCredentials, currentLocationIdentifier, airspaceName);
        }
    }
}
