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

        // constructor requires both fields to be set
        // instantiates from Winnings class when
        // new Week object is created
        public Location(string name, string state)
        {
            this.Name = name;
            this.State = state;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Name, State);
        }
    }
}
