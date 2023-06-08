using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartStatsV3.Models
{
    public class Circuit
    {
        public int CircuitId { get; private set; }
        public string Name { get; private set; }

        public Circuit(int circuitId, string name)
        {
            CircuitId = circuitId;
            Name = name;
        }
    }

}
