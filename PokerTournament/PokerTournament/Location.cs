using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTournament
{
    class Location
    {
        public string Name { get; set; }
        public string State { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", Name, State);
        }
    }
}
