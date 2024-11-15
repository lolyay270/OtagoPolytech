using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 
    The following code was written by Jenna Boyes.
    For Project 1 in Programming 2, Bachelor of IT, Otago Polytechnic.

    The code below allows Institution objects to be made.
*/

namespace JennaB_SMS
{
    public class Institution
    {
        //fields
        private string name, region, country;

        //encapsulations
        public string Name { get => name; set => name = value; }
        public string Region { get => region; set => region = value; }
        public string Country { get => country; set => country = value; }

        //constructor
        public Institution(string name, string region, string country)
        {
            this.Name = name;
            this.Region = region;
            this.Country = country;
        }
    }
}
