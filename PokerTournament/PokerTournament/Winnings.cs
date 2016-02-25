using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTournament
{
    // Winnings class contains array of Week objects and
    // sum of winnings for a single person
    [Serializable]
    class Winnings : PokerFace
    {
        // fields
        //
        // Weeks object composed of Location and single-week winnings
        public Week[] Weeks { get; set; }

        // TotalWinnings is a derived field that should update
        // whenever there is a change in the Weeks array
        public double TotalWinnings
        {
            get
            {
                return TotalWinnings;
            }
            set
            {
                TotalWinnings = CalculateTotalWinnings();
            }
        }

        // constructor instantiates empty Weeks array
        public Winnings()
        {
            this.Weeks = new Week[8];
            for (int x = 0; x < Weeks.Length; ++x)
                Weeks[x] = new Week();
        }

        // method calculates total winnings for derived field
        private double CalculateTotalWinnings()
        {
            double sum = 0;

            foreach(Week week in Weeks)
            {
                sum += week.Winning;
            }

            return sum;
        }

        // method adds new Week object to array at specified position
        // should this be moved to an interface?
        // also, Location could be instantiated outside of this method first
        public void AddWeek(int weekNumber, string locationName, string locationState, double winningAmount)
        {
            // create new Week object
            Weeks[weekNumber] = new Week(new Location(locationName, locationState), winningAmount);

            // update total winnings
            CalculateTotalWinnings();
        }
    }
}
