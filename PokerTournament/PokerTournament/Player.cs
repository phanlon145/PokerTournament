using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTournament
{
    [Serializable]
    class Player : Person
    {
        public Winnings Winnings { get; set; }

        public Player(string firstName, string lastName, int ssn) : base(firstName, lastName, ssn)
        {
            this.Winnings = new Winnings();
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}",  LastName, FirstName, ssn, Winnings.CalculateTotalWinnings());
        }
    }
}
