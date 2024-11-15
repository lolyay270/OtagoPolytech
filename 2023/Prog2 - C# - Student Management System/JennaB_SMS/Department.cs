using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 
    The following code was written by Jenna Boyes.
    For Project 1 in Programming 2, Bachelor of IT, Otago Polytechnic.

    The code below allows Department objects to be made.
    Departments requires Instituion objects to have as fields.
*/

namespace JennaB_SMS
{
    public class Department
    {
        //fields
        private Institution institution;
        private string name;

        //encapsulations
        public string Name { get => name; set => name = value; }
        internal Institution Institution { get => institution; set => institution = value; }

        //constructor
        public Department(Institution institution, string name)
        {
            this.Institution = institution;
            this.Name = name;
        }
    }
}
