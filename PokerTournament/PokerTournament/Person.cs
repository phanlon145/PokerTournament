using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTournament
{
    [Serializable]
    abstract class Person : IComparable
    {
        // fields
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ssn { get; set; }

        // constructor
        public Person(string firstName, string lastName, int ssn)
        {
            FirstName = firstName;
            LastName = lastName;
            this.ssn = ssn;
        }

        public int CompareTo(object obj)
        {
            int returnVal;
            Person temp = (Person)obj;
            if (this.ssn > temp.ssn)
                returnVal = 1;
            else
                if (this.ssn < temp.ssn)
                returnVal = -1;
            else
                returnVal = 0;
            return returnVal;
        }

        public override int GetHashCode()
        {
            return ssn;
        }

        public override bool Equals(object obj)
        {
            if (obj == DBNull.Value)
            {
                return false;
            }
            else {
                Person temp = (Person)obj;
                if (this.ssn == temp.ssn)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", ssn, FirstName, LastName, ssn);
        }
    }
}
