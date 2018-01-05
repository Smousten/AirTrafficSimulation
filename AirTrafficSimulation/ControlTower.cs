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
    class ControlTower
    {
        private SequentialSpace airport;
        public ControlTower(SequentialSpace space)
        {
            this.airport = space;
        }
    }
}
