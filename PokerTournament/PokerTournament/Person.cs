﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTournament
{
    class Person : IComparable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ssn { get; set; }

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

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", FirstName, LastName, ssn);
        }
    }
}