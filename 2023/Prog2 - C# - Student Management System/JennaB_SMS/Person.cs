using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 
    The following code was written by Jenna Boyes.
    For Project 1 in Programming 2, Bachelor of IT, Otago Polytechnic.

    The code below allows Person objects to be made.
*/

namespace JennaB_SMS
{
    public class Person
    {
        //fields
        protected int id;
        protected string lastName;
        protected string firstName;

        //encapsulation
        public int Id { get => id; set => id = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }

        //constructor
        public Person(int id, string firstName, string lastName)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
        }
    }
}
