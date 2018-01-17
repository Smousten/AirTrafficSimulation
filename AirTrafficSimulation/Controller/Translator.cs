using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotSpace.Interfaces.Space;
using dotSpace.Objects.Space;

namespace AirTrafficSimulation.Controller
{
    public class Translator
    {
        private SequentialSpace runwaySpace;
        private SequentialSpace taxiwaySpace;
        private singlePlane singlePlane;
        private string planeName;


        public Translator(SequentialSpace runways, SequentialSpace taxiways, string planeName, int rw, int dir, int startx, int starty)
        {
            this.runwaySpace = runways;
            this.taxiwaySpace = taxiways;
            this.singlePlane = new singlePlane(planeName, 0, rw, dir, startx, starty, this);
            this.planeName = planeName;
        }

        public singlePlane getSingleplain()
        {
            return this.singlePlane;
        }

        //Fire-and-forget method, to update the GUI
        public void allowGraphicalMovementOfPlaneToProgress()
        {
            singlePlane.setHasLock(true);//singlePlane.setCanMove(true); // Maybe setCanMove
            //dotSpace.Objects.Space.Tuple releventDataTuple = new dotSpace.Objects.Space.Tuple(credentials, previousLocation, nextLocation);
            //Tell graphics the input information, so we can draw it
        }
        //Fire-and-forget method, to allow the logic to progress
        public void allowLogicalMovementOfPlaneToProgress()
        {
            //string planeNextLocationLock = planeCrededentials + newLocationCredentials + "-lock";
            //if (newLocationCredentials == "Runway")
            //{
            //    runwaySpace.Put(planeNextLocationLock);
            //}
            //else if (newLocationCredentials == "Taxiway")
            //{
            //    taxiwaySpace.Put(planeNextLocationLock);
            //}

            if (!singlePlane.getCanmove())
            {
                taxiwaySpace.Put(planeName + "Taxiway"/*singleplain.getRWreq()*/ + "-lock");
            }
        }

        public bool getCanmove()
        {
            return singlePlane.getCanmove();
        }
        
        //public void set

    }
}
