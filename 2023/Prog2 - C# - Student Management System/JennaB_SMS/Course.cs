using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 
    The following code was written by Jenna Boyes.
    For Project 1 in Programming 2, Bachelor of IT, Otago Polytechnic.

    The code below allows Course objects that have fields to be made.
    Course requires Department objects as fields.
*/

namespace JennaB_SMS
{
    public class Course
    {
        //fields
        private Department department;
        private string code, name, description;
        private int credits;
        private double fees;

        //encapsulations
        public string Code { get => code; set => code = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public int Credits { get => credits; set => credits = value; }
        public double Fees { get => fees; set => fees = value; }
        internal Department Department { get => department; set => department = value; }

        //constructor
        public Course(Department department, string code, string name, string desc, int credits, double fees)
        {
            this.Department = department;
            this.Code = code;
            this.Name = name;
            this.Description = desc;
            this.Credits = credits;
            this.Fees = fees;
        }
    }
}
