﻿using System;
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
        //private SequentialSpace runwaySpace;
        //private SequentialSpace runwayLockSpace;
        private SequentialSpace taxiwaySpace;

        private string credentials;
        private string runwayGUILock;
        private string taxiwayGUILock;
        private readonly string airspaceName = "Airspace";
        private readonly string hangarName = "Hangar";

        private Controller.Translator translator;

        private int windDirection;
        private bool realisticmode;

        public Airplane(SequentialSpace CTSpace, SequentialSpace twSpace, string credentials, bool realisticMode, int windDirection, int dir, int startx, int starty) //SpaceRepository airportRepository)
        {
            //this.airport = airportRepository;
            this.controlTowerSpace = CTSpace;
            //this.runwaySpace = rwSpace;
            this.taxiwaySpace = twSpace;
            this.credentials = credentials;
            this.runwayGUILock = credentials + "Runway" + "-lock";
            this.taxiwayGUILock = credentials + "Taxiway" + "-lock";

            this.translator = new Controller.Translator(twSpace, credentials, windDirection, dir, startx, starty);

            this.windDirection = windDirection;
            this.realisticmode = realisticMode;

        }

        public Controller.Translator getTrans()
        {
            return this.translator;
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
            ITuple freeRunwayForLanding = controlTower.getRunwayClearance(credentials);

            switch ((int)freeRunwayForLanding[1])
            {
                case 0:
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

                    taxiwayToTaxiway("Delta", "Echo");

                    taxiwayToTaxiway("Echo", "Alfa");

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
                    runwayToTaxiway("Echo", controlTower, freeRunwayForLanding);

                    // Leaving taxiway Echo, entering taxiway Alfa
                    taxiwayToTaxiway("Echo", "Alfa");

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
                        ITuple freeRunwayS = controlTower.isRunwayFreeForTakeoff(credentials,0);
                        if (freeRunwayS != null)
                        {
                            
                            taxiwayToRunway("Charlie", freeRunwayS, controlTower, "efficient");
                            break;

                        }
                        taxiwayToTaxiway("Charlie", "Delta");
                        ITuple freeRunwayW = controlTower.isRunwayFreeForTakeoff(credentials,90);
                        if (freeRunwayW != null)
                        {
                            taxiwayToRunway("Delta", freeRunwayW, controlTower, "efficient");
                            break;
                        }
                        taxiwayToTaxiway("Delta", "Echo");
                        ITuple freeRunwayN = controlTower.isRunwayFreeForTakeoff(credentials,0);
                        if (freeRunwayN != null)
                        {
                            taxiwayToRunway("Echo", freeRunwayN, controlTower, "efficient");
                            break;
                        }
                        taxiwayToTaxiway("Echo", "Alfa");
                        ITuple freeRunwayE = controlTower.isRunwayFreeForTakeoff(credentials,90);
                        if (freeRunwayE != null)
                        {
                            taxiwayToRunway("Alfa", freeRunwayE, controlTower, "efficient");
                            break;
                        }
                        taxiwayToTaxiway("Alfa", "Beta");
                        taxiwayToHangar("Beta");
                    }

                }
            }
        }

        private void runwayToTaxiway(string taxiway, ControlTower ct, ITuple frwT)
        {
             translator.allowGraphicalMovementOfPlaneToProgress(); //updateGraphics(credentials, airspaceName, "" + frwT[0] + frwT[1]);
            //runwaySpace.Get(runwayGUILock);

            Console.WriteLine(credentials + " is searching for free landing taxiway " + taxiway);
            ITuple freeTaxiWayTupleTo = taxiwaySpace.Get("Taxiway", taxiway, typeof(int), true);
            int barrierLimitA = (int)freeTaxiWayTupleTo[2] - 1;
            Console.WriteLine(credentials + " found free taxiway " + taxiway + " with barrier value " + (barrierLimitA + 1) + " and getting free taxiway tuple...");
            Console.WriteLine(credentials + " is putting runway lock with ID " + frwT[0] + frwT[1]);
            ct.putRunway(credentials, frwT);
            //updateGraphics(credentials, "" + frwT[0] + frwT[1], "" + freeTaxiWayTupleTo[0] + freeTaxiWayTupleTo[1]);
            Console.WriteLine(credentials + " is putting free taxiway "+taxiway+" back with lower barrier " + barrierLimitA);
            taxiwaySpace.Put((string)freeTaxiWayTupleTo[0], freeTaxiWayTupleTo[1], barrierLimitA, (barrierLimitA) > 0);
            taxiwaySpace.Get(taxiwayGUILock);
        }

        private void taxiwayToTaxiway(string fromTw, string toTw)
        {
            translator.allowGraphicalMovementOfPlaneToProgress();
            Console.WriteLine(credentials + " is searching for used taxiway " + fromTw + ", to increase barrier...");
            ITuple freeTaxiWayTupleTo = taxiwaySpace.Get("Taxiway", toTw, typeof(int), true);
            ITuple usedTaxiWayTupleFrom = taxiwaySpace.Get("Taxiway", fromTw, typeof(int), typeof(bool));

            int usedbarrierLimitA = (int)usedTaxiWayTupleFrom[2] + 1;
            Console.WriteLine(credentials + " is increasing " + usedTaxiWayTupleFrom[0] + " " + usedTaxiWayTupleFrom[1] + "'s taxiway barrierLimit to " + usedbarrierLimitA);
            taxiwaySpace.Put("Taxiway", fromTw, usedbarrierLimitA, (usedbarrierLimitA > 0));

            int barrierLimitB = (int)freeTaxiWayTupleTo[2] - 1;

            Console.WriteLine(credentials + " is decreasing " + freeTaxiWayTupleTo[0] + " " + freeTaxiWayTupleTo[1] + "'s taxiway barrierLimit to " + barrierLimitB);
            taxiwaySpace.Put("Taxiway", toTw, barrierLimitB, (barrierLimitB > 0));
            taxiwaySpace.Get(taxiwayGUILock);
        }

        private void taxiwayToHangar(string fromTw)
        {
            translator.allowGraphicalMovementOfPlaneToProgress();
            Console.WriteLine(credentials + " is searching for used taxiway " + fromTw + ", to increase barrier...");
            ITuple usedTaxiWayTuple = taxiwaySpace.Get("Taxiway", fromTw, typeof(int), typeof(bool));
            int usedbarrierLimit = (int)usedTaxiWayTuple[2] + 1;
            Console.WriteLine(credentials + " is increasing " + usedTaxiWayTuple[1] + "'s taxiway barrierLimit to " + usedbarrierLimit);
            taxiwaySpace.Put("Taxiway", usedTaxiWayTuple[1], usedbarrierLimit, (usedbarrierLimit) > 0);
            Console.WriteLine(credentials + " has safely arrived in the hangar!");
            //updateGraphics(credentials, "" + usedTaxiWayTuple[0] + usedTaxiWayTuple[1], hangarName);
        }
        private void hangarToTaxiway(string taxiway)
        {
            // Leaving hangar, entering taxiway
            translator.allowGraphicalMovementOfPlaneToProgress();
            Console.WriteLine(credentials + " is searching for free taxiway " + taxiway);
            ITuple freeTaxiWayTuple = taxiwaySpace.Get("Taxiway", taxiway, typeof(int), true);
            int barrierLimit = (int)freeTaxiWayTuple[2] - 1;

            Console.WriteLine(credentials + " found free take-off taxiway " + taxiway + " with barrier value " + (barrierLimit + 1) + " and getting free taxiway tuple...");       
            Console.WriteLine(credentials + " is putting free taxiway " + taxiway + " back with new barrier " + barrierLimit);
            taxiwaySpace.Put((string)freeTaxiWayTuple[0], freeTaxiWayTuple[1], barrierLimit, barrierLimit > 0);
            taxiwaySpace.Get(taxiwayGUILock);
        }
        private void taxiwayToRunway(string taxiway, ITuple frwL, ControlTower ct, string mode)
        {
            //Make space on the taxiway you just left
            translator.allowGraphicalMovementOfPlaneToProgress();
            if (mode == "realistic")
            {
                ct.getRunway(credentials, frwL);
            }
            Console.WriteLine(credentials + " is searching for same take-off taxiway, to increase barrier...");
            ITuple usedtaxiway = taxiwaySpace.Get("Taxiway", taxiway, typeof(int), typeof(bool));
            int bl = (int)usedtaxiway[2] + 1;
            Console.WriteLine(credentials + " is increasing take-off taxiway " + usedtaxiway[1] + "'s barrierlimit to " + bl);


            taxiwaySpace.Put((string)usedtaxiway[0], usedtaxiway[1], bl, bl > 0);
            Console.WriteLine(credentials + " has safely left the taxiway!");

            //Leave the runway
            taxiwaySpace.Get(taxiwayGUILock);
            Console.WriteLine(credentials + " got takeoff clearance with ID " + frwL[0] + frwL[1] + " and left the airport!");
            ct.putRunway(credentials, frwL);
            translator.allowGraphicalMovementOfPlaneToProgress();
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
                case 0:
                    ITuple freeLandingRunwayS = controlTower.getSpecificRunway("Runway Nr.", 0);

                    runwayToTaxiway("Alfa", controlTower, freeLandingRunwayS);

                    taxiwayToTaxiway("Alfa", "Beta");

                    taxiwayToHangar("Beta");


                    break;
                case 18:
                    ITuple freeLandingRunwayN = controlTower.getSpecificRunway("Runway Nr.", 0);

                    runwayToTaxiway("Charlie", controlTower, freeLandingRunwayN);
                    
                    taxiwayToHangar("Charlie");

                    break;
                case 90:
                    ITuple freeLandingRunwayW = controlTower.getSpecificRunway("Runway Nr.", 90);

                    runwayToTaxiway("Beta", controlTower, freeLandingRunwayW);

                    taxiwayToHangar("Beta");

                    break;
                case 27:
                    ITuple freeLandingRunwayE = controlTower.getSpecificRunway("Runway Nr.", 90);

                    runwayToTaxiway("Delta", controlTower, freeLandingRunwayE);

                    taxiwayToTaxiway("Delta", "Charlie");
                    
                    taxiwayToHangar("Charlie");

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
                        case 0:
                            hangarToTaxiway("Charlie");
                            ITuple freeTakeoffRunwayS = controlTower.getQueryRunway("Runway Nr.", 0);
                            taxiwayToRunway("Charlie", freeTakeoffRunwayS, controlTower, "realistic");
                            break;
                        case 18:
                            hangarToTaxiway("Beta");
                            taxiwayToTaxiway("Beta", "Alfa");
                            ITuple freeTakeoffRunwayN = controlTower.getQueryRunway("Runway Nr.", 0);
                            taxiwayToRunway("Alfa", freeTakeoffRunwayN, controlTower, "realistic");
                            break;
                        case 90:
                            hangarToTaxiway("Charlie");
                            taxiwayToTaxiway("Charlie", "Delta");
                            ITuple freeTakeoffRunwayW = controlTower.getQueryRunway("Runway Nr.", 90);
                            taxiwayToRunway("Delta", freeTakeoffRunwayW, controlTower, "realistic");
                            break;
                        case 27:
                            hangarToTaxiway("Beta");
                            ITuple freeTakeoffRunwayE = controlTower.getQueryRunway("Runway Nr.", 90);
                            Console.WriteLine(credentials + " succesfully found runway");
                            taxiwayToRunway("Beta", freeTakeoffRunwayE, controlTower, "realistic");
                            break;
                    }
                }
            }
        }

        //private void updateGraphics(string planeCredentials, string currentLocationIdentifier, string nextLocationIdentifier)
        //{
        //   // translator.updateGraphicalPosition(planeCredentials, currentLocationIdentifier, airspaceName);
        //}
    }
}
