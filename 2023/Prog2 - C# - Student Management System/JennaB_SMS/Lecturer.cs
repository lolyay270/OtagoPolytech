using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 
    The following code was written by Jenna Boyes.
    For Project 1 in Programming 2, Bachelor of IT, Otago Polytechnic.

    The code below allows Lecturer object to be made.
    Lecturer requires Course objects as fields. 
*/

namespace JennaB_SMS
{
    public class Lecturer : Person
    {
        //fields
        private Position position;
        private Salary salary;
        private Course course;

        //encapsulation 
        public Position Position { get => position; set => position = value; }
        public Salary Salary { get => salary; set => salary = value; }
        public Course Course { get => course; set => course = value; }

        //constructor
        public Lecturer(int id, string firstName, string lastName, Position position, Salary salary, Course course) : base(id, firstName, lastName) 
        {
            this.Position = position;
            this.Salary = salary;
            this.Course = course;
        }
    }
}
