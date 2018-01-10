using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotSpace.Interfaces.Space;
using dotSpace.Objects.Space;

namespace AirTrafficSimulation.Controller
{
    class Translator
    {
        private SequentialSpace controlTowerSpace;
        private SequentialSpace airplaneSpace;
        private SequentialSpace runwaySpace;
        private SequentialSpace taxiwaySpace;
        
        public Translator(SequentialSpace ctspace, SequentialSpace apspace, SequentialSpace rwspace, SequentialSpace twspace)
        {
            this.controlTowerSpace = ctspace;
            this.airplaneSpace = apspace;
            this.runwaySpace = rwspace;
            this.taxiwaySpace = twspace;
        }
    }
}
