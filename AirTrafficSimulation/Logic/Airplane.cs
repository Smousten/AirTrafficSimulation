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

        private string windDirection;
        private bool realisticmode;

        public Airplane(SequentialSpace CTSpace, SequentialSpace rwSpace, SequentialSpace twSpace, string credentials, AirField airField, bool realisticMode, string windDirection) //SpaceRepository airportRepository)
        {
            //this.airport = airportRepository;
            this.controlTowerSpace = CTSpace;
            this.runwaySpace = rwSpace;
            this.taxiwaySpace = twSpace;
            this.credentials = credentials;
            this.runwayGUILock = credentials + "Runway" + "-lock";
            this.taxiwayGUILock = credentials + "Taxiway" + "-lock";

            this.translator = new Controller.Translator(rwSpace, twSpace, airField);

            this.windDirection = windDirection;
            this.realisticmode = realisticMode;

        }
        public void flyEternally(string startingLocation)
        {
            while (true)
            {
                if (startingLocation == "Airspace")
                {
                    efficientLanding();
                    efficientTakeoff();
                }
                else if (startingLocation == "Hanger")
                {
                    efficientTakeoff();
                    efficientLanding();
                }
                else
                {
                    Console.WriteLine("You are drunk, go home.");
                    break;
                }
            }
        }
        public void landing()
        {
            if (realisticmode)
            {
                realisticLanding();
            }
            else
            {
                efficientLanding();
            }
        }

        public void takeoff()
        {
            if (realisticmode)
            {
                realisticTakeoff();
            }
            else
            {
                efficientTakeoff();
            }
        }


        public void efficientLanding()
        {
            //Establishing communication
            Console.WriteLine(credentials + " is Searching for control tower...");
            ITuple controlTowerTuple = controlTowerSpace.Query("Control Tower Nr.", typeof(int), typeof(ControlTower));

            //Getting landing clearance
            ControlTower controlTower = (ControlTower)controlTowerTuple[2];
            Console.WriteLine(credentials + " found control tower and getting landing clearance...");
            ITuple freeRunwayForLanding = controlTower.getRunwayClearance(credentials, airspaceName);

            switch ((int)freeRunwayForLanding[1])
            {
                case 00:
                    //Leave airspace, land in airport
                    runwayToTaxiway("Alfa", controlTower, freeRunwayForLanding);

                    // Leaving taxiway Afa, entering taxiway Beta
                    taxiwayToTaxiway("Alfa", "Beta");

                    // Leaving taxiway Beta, entering hangar
                    taxiwayToHangar("Beta");
                    break;

                case 18:
                    //Leave airspace, land in airport
                    runwayToTaxiway("Delta", controlTower, freeRunwayForLanding);

                    taxiwayToTaxiway("Delta", "Epsilon");

                    taxiwayToTaxiway("Epsilon", "Alfa");

                    taxiwayToTaxiway("Alfa", "Beta");
                    // Leaving taxiway Charlie, entering hangar
                    taxiwayToHangar("Beta");
                    break;


                case 90:
                    //Leave airspace, land in airport
                    runwayToTaxiway("Beta", controlTower, freeRunwayForLanding);

                    // Leaving taxiway Beta, entering hangar
                    taxiwayToHangar("Beta");
                    break;
                case 27:
                    // Leave airspace, land in aiport
                    runwayToTaxiway("Epsilon", controlTower, freeRunwayForLanding);

                    // Leaving taxiway epsilon, entering taxiway Alfa
                    taxiwayToTaxiway("Epsilon", "Alfa");

                    // Leaving taxiway Alfa, entering taxiway Beta
                    taxiwayToTaxiway("Alfa", "Beta");

                    // Leaving taxiway Beta, entering hangar
                    taxiwayToHangar("Beta");
                    break;

            }
        }
        public void efficientTakeoff()
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
                    ControlTower controlTower = (ControlTower)controlTowerTuple[2];
                    while (true)
                    {
                        hangarToTaxiway("Charlie");

                        if (controlTower.isRunwayFreeForTakeoff(00) != null)
                        {
                            ITuple freeRunwayS = controlTower.isRunwayFreeForTakeoff(00);
                            taxiwayToRunway("Charlie", freeRunwayS, controlTower);
                            break;

                        }
                        taxiwayToTaxiway("Charlie", "Delta");
                        if (controlTower.isRunwayFreeForTakeoff(90) != null)
                        {
                            ITuple freeRunwayW = controlTower.isRunwayFreeForTakeoff(90);
                            taxiwayToRunway("Delta", freeRunwayW, controlTower);
                            break;
                        }
                        taxiwayToTaxiway("Delta", "Epsilon");
                        if (controlTower.isRunwayFreeForTakeoff(18) != null)
                        {
                            ITuple freeRunwayN = controlTower.isRunwayFreeForTakeoff(18);
                            taxiwayToRunway("Epsilon", freeRunwayN, controlTower);
                            break;
                        }
                        taxiwayToTaxiway("Alfa", "Beta");
                        if (controlTower.isRunwayFreeForTakeoff(27) != null)
                        {
                            ITuple freeRunwayE = controlTower.isRunwayFreeForTakeoff(27);
                            taxiwayToRunway("Beta", freeRunwayE, controlTower);
                            break;
                        }
                        taxiwayToHangar("Beta");
                    }

                }
            }
        }

        private void runwayToTaxiway(string taxiway, ControlTower ct, ITuple frwT)
        {
            updateGraphics(credentials, airspaceName, "" + frwT[0] + frwT[1]);
            //runwaySpace.Get(runwayGUILock);

            Console.WriteLine(credentials + " is searching for free landing taxiway...");
            ITuple freeTaxiWayTupleA = taxiwaySpace.Get("Taxiway", taxiway, typeof(int), true);
            int barrierLimitA = (int)freeTaxiWayTupleA[2] - 1;
            Console.WriteLine(credentials + " found free taxiway with barrier value " + (barrierLimitA + 1) + " and getting free taxiway tuple...");
            Console.WriteLine(credentials + " is putting runway lock with ID " + frwT[0] + frwT[1]);
            ct.putRunway(frwT);
            updateGraphics(credentials, "" + frwT[0] + frwT[1], "" + freeTaxiWayTupleA[0] + freeTaxiWayTupleA[1]);
            Console.WriteLine(credentials + " is putting free taxiway back with new barrier " + barrierLimitA);
            taxiwaySpace.Put((string)freeTaxiWayTupleA[0], freeTaxiWayTupleA[1], barrierLimitA, (barrierLimitA) > 0);
            //taxiwaySpace.Get(taxiwayGUILock);
            //System.Threading.Thread.Sleep(2500);
        }

        private void taxiwayToTaxiway(string fromTw, string toTw)
        {
            Console.WriteLine(credentials + " is searching for taxiway " + fromTw + ", to increase barrier...");
            ITuple freeTaxiWayTupleB = taxiwaySpace.Get("Taxiway", toTw, typeof(int), true);
            ITuple usedTaxiWayTupleA = taxiwaySpace.Get("Taxiway", fromTw, typeof(int), typeof(bool));
            int usedbarrierLimitA = (int)usedTaxiWayTupleA[2] + 1;
            Console.WriteLine(credentials + " is increasing landing " + usedTaxiWayTupleA[0] + usedTaxiWayTupleA[1] + "'s taxiway barrierLimit to " + usedbarrierLimitA);
            taxiwaySpace.Put("Taxiway", fromTw, usedbarrierLimitA, (usedbarrierLimitA > 0));
            //UPADTE GRAPHICS TAXIWAY TO TAXIWAY
            int barrierLimitB = (int)freeTaxiWayTupleB[2] - 1;
            taxiwaySpace.Put("Taxiway", toTw, barrierLimitB, (barrierLimitB > 0));
        }

        private void taxiwayToHangar(string fromTw)
        {
            Console.WriteLine(credentials + " is searching for taxiway " + fromTw + ", to increase barrier...");
            ITuple usedTaxiWayTuple = taxiwaySpace.Get("Taxiway", fromTw, typeof(int), typeof(bool));
            int usedbarrierLimit = (int)usedTaxiWayTuple[2] + 1;
            Console.WriteLine(credentials + " is increasing landing " + usedTaxiWayTuple[1] + "'s taxiway barrierLimit to " + usedbarrierLimit);
            taxiwaySpace.Put("Taxiway", usedTaxiWayTuple[1], usedbarrierLimit, (usedbarrierLimit) > 0);
            Console.WriteLine(credentials + " has safely arrived in the hangar!");
            updateGraphics(credentials, "" + usedTaxiWayTuple[0] + usedTaxiWayTuple[1], hangarName);
            Console.WriteLine(credentials + " has safely arrived in the hangar!");
        }
        private void hangarToTaxiway(string taxiway)
        {
            // Leaving hangar, entering taxiway
            Console.WriteLine(credentials + " is searching for free taxiway...");
            ITuple freeTaxiWayTuple = taxiwaySpace.Get("Taxiway", taxiway, typeof(int), true);
            int barrierLimit = (int)freeTaxiWayTuple[2] - 1;

            Console.WriteLine(credentials + " found free take-off taxiway with barrier value " + (barrierLimit + 1) + " and getting free taxiway tuple...");
            //System.Threading.Thread.Sleep(2500);                      
            Console.WriteLine(credentials + " is putting free taxiway back with new barrier " + barrierLimit);
            taxiwaySpace.Put((string)freeTaxiWayTuple[0], freeTaxiWayTuple[1], barrierLimit, barrierLimit > 0);
            //break;

        }
        private void taxiwayToRunway(string taxiway, ITuple frwL, ControlTower ct)
        {
            //Make space on the taxiway you just left
            Console.WriteLine(credentials + " is searching for same take-off taxiway, to increase barrier...");
            ITuple usedtaxiway = taxiwaySpace.Get("Taxiway", taxiway, typeof(int), typeof(bool));
            int bl = (int)usedtaxiway[2] + 1;
            Console.WriteLine(credentials + " is increasing take-off taxiway " + usedtaxiway[1] + "'s barrierlimit to " + bl);

            ct.getRunway(frwL);
            taxiwaySpace.Put((string)usedtaxiway[0], usedtaxiway[1], bl, bl > 0);
            Console.WriteLine(credentials + " has safely left the taxiway!");

            //Leave the runway
            //runwaySpace.Get(freeRunwayLock);
            Console.WriteLine(credentials + " got takeoff clearance with ID " + frwL[0] + frwL[1] + " and left the airport!");
            //leave airport, enter in airspace
            //Thread.sleep(5000);
            //runwaySpace.Put(freeRunwayLock);
            ct.putRunway(frwL);
            //Airspace step

        }



        public void realisticLanding()
        {
            //Establishing communication
            Console.WriteLine(credentials + " is Searching for control tower...");
            ITuple controlTowerTuple = controlTowerSpace.Query("Control Tower Nr.", typeof(int), typeof(ControlTower));

            //Getting landing clearance
            ControlTower controlTower = (ControlTower)controlTowerTuple[2];
            Console.WriteLine(credentials + " found control tower and getting landing clearance...");


            switch (windDirection)
            {
                case "S":
                    ITuple freeLandingRunwayS = runwaySpace.Get("Runway Nr.", 00);

                    runwayToTaxiway("Alfa", controlTower, freeLandingRunwayS);

                    taxiwayToTaxiway("Alfa", "Beta");

                    taxiwayToHangar("Beta");


                    break;
                case "N":
                    ITuple freeLandingRunwayN = runwaySpace.Get("Runway Nr.", 18);

                    runwayToTaxiway("Delta", controlTower, freeLandingRunwayN);

                    taxiwayToTaxiway("Delta", "Epsilon");

                    taxiwayToTaxiway("Epsilon", "Alfa");

                    taxiwayToTaxiway("Alfa", "Beta");

                    taxiwayToHangar("Beta");

                    break;
                case "W":
                    ITuple freeLandingRunwayW = runwaySpace.Get("Runway Nr.", 90);

                    runwayToTaxiway("Beta", controlTower, freeLandingRunwayW);

                    break;
                case "E":
                    ITuple freeLandingRunwayE = runwaySpace.Get("Runway Nr.", 27);

                    runwayToTaxiway("Epsilon", controlTower, freeLandingRunwayE);

                    taxiwayToTaxiway("Epsilon", "Alfa");

                    taxiwayToTaxiway("Alfa", "Beta");

                    taxiwayToHangar("Beta");

                    break;
            }
        }

        public void realisticTakeoff()
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
                    ControlTower controlTower = (ControlTower)controlTowerTuple[2];
                    switch (windDirection)
                    {
                        case "S":
                            hangarToTaxiway("Charlie");
                            ITuple freeTakeoffRunwayS = runwaySpace.Query("Runway Nr.", 00);
                            taxiwayToRunway("Charlie", freeTakeoffRunwayS, controlTower);
                            break;
                        case "N":
                            hangarToTaxiway("Charlie");
                            taxiwayToTaxiway("Charlie", "Delta");
                            taxiwayToTaxiway("Delta", "Episilon");
                            ITuple freeTakeoffRunwayN = runwaySpace.Query("Runway Nr.", 18);
                            taxiwayToRunway("Epsilon", freeTakeoffRunwayN, controlTower);
                            break;
                        case "W":
                            hangarToTaxiway("Charlie");
                            taxiwayToTaxiway("Charlie", "Delta");
                            ITuple freeTakeoffRunwayW = runwaySpace.Query("Runway Nr.", 90);
                            taxiwayToRunway("Delta", freeTakeoffRunwayW, controlTower);
                            break;
                        case "E":
                            hangarToTaxiway("Charlie");
                            taxiwayToTaxiway("Charlie", "Delta");
                            taxiwayToTaxiway("Delta", "Episilon");
                            taxiwayToTaxiway("Epsilon", "Alfa");
                            ITuple freeTakeoffRunwayE = runwaySpace.Query("Runway Nr.", 27);
                            taxiwayToRunway("Alfa", freeTakeoffRunwayE, controlTower);
                            break;
                    }
                }
            }
        }

        private void updateGraphics(string planeCredentials, string currentLocationIdentifier, string nextLocationIdentifier)
        {
            translator.updateGraphicalPosition(planeCredentials, currentLocationIdentifier, airspaceName);
        }
    }
}
