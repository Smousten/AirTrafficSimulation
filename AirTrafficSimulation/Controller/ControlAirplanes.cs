using dotSpace.Interfaces.Space;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AirTrafficSimulation.Controller
{
    class ControlAirplanes
    {
        private int halfformHeight, halfformWidth;
        private int taxiWidth, taxiLength;
        private int runWayLength, runWayWidth, zebraWidth, zebraLength, linespace, runWayLength2;
        private int taxiPosXL, taxiPosXR, taxiPosYU, taxiPosYL;

        public void createAirfield()
        {
            AirField af = new AirField();
            (new Thread(new ThreadStart(() => af.run()))).Start();
            Thread airfield = new Thread(new System.Threading.ThreadStart(() => af.airField())).Start(); 
            Task.Delay(Timeout.Infinite);
        }

        public ITuple getAirportBoundaries()
        {
            return new dotSpace.Objects.Space.Tuple(this.halfformHeight, this.halfformWidth, this.taxiWidth, this.taxiLength, this.runWayLength, this.runWayWidth, this.runWayLength2,
                this.zebraLength, this.zebraWidth, this.linespace, this.taxiPosXL, this.taxiPosXR, this.taxiPosYL, this.taxiPosYU);
        }
    }
}
