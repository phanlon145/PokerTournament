using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTournament
{
    // Week class keeps a single week's location and winnings tied
    // together within the array of a Winnings object
    [Serializable]
    class Week
    {
        // fields
        public Location Location { get; set; }
        public double Winning { get; set; }

        public Week()
        {
            Location = new Location();
            Winning = 0;
        }

        // constructor requires both fields to be set
        public Week(Location location, double winningAmount)
        {
            this.Location = location;
            this.Winning = winningAmount;
        }
    }
}
