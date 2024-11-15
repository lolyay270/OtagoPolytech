using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 
    The following code was written by Jenna Boyes.
    For Project 1 in Programming 2, Bachelor of IT, Otago Polytechnic.

    The code below allows Learner objects to be made.
    Learner is a child of Person and requires Marks objects as fields.
*/

namespace JennaB_SMS
{
    public class Learner : Person
    {
        //fields
        private CourseAssessmentMark courseAssessmentMarks;

        //encapsulation
        public CourseAssessmentMark CourseAssessmentMarks { get => courseAssessmentMarks; set => courseAssessmentMarks = value; }

        //constructor
        public Learner(int id, string firstName, string lastName, CourseAssessmentMark courseAssessmentMarks) : base(id, firstName, lastName)
        {
            this.courseAssessmentMarks = courseAssessmentMarks;
        }
    }
}
