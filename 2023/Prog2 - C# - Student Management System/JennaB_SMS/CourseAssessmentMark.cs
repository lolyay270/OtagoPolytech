using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 
    The following code was written by Jenna Boyes.
    For Project 1 in Programming 2, Bachelor of IT, Otago Polytechnic.

    The code below allows for mark objects to be made. 
    Marks requires Course objects as fields.
    There are different methods to allow different marks data to be returned.
*/

namespace JennaB_SMS
{
    public class CourseAssessmentMark
    {
        //fields
        private Course course;
        private List<int> assessmentMarks;

        //encapsulation
        public Course Course { get => course; set => course = value; }
        public List<int> AssessmentMarks { get => assessmentMarks; set => assessmentMarks = value; }


        //constructor
        public CourseAssessmentMark(Course course, List<int> assessmentMarks)
        {
            this.course = course;
            this.assessmentMarks = assessmentMarks;
        }

        //methods
        public List<int> GetAllMarks()
        {
            return assessmentMarks;
        }

        public List<string> GetAllGrades()
        {
            List<string> grades = new List<string>();
            foreach (var mark in assessmentMarks)
            {
                grades.Add(MakeGrade(mark));
            }
            return grades;
        }

        public List<int> GetHighestMarks()
        {
            List<int> maxMarks = new List<int>();
            int maxValue = assessmentMarks.Max();
            foreach (int mark in assessmentMarks)
            {
                if (mark == maxValue)
                {
                    maxMarks.Add(mark);
                }
            }
            return maxMarks;
        }

        public List<int> GetLowestMarks()
        {
            List<int> minMarks = new List<int>();
            int minValue = assessmentMarks.Min();
            foreach (int mark in assessmentMarks)
            {
                if (mark == minValue)
                {
                    minMarks.Add(mark);
                }
            }
            return minMarks;
        }

        public List<int> GetFailMarks()
        {
            List<int> failMarks = new List<int>();
            foreach (int mark in assessmentMarks)
            {
                if (mark < 50)
                {
                    failMarks.Add(mark);
                }

            }
            return failMarks;
        }

        public double GetAverageMark()
        {
            int total = 0;
            foreach (int mark in assessmentMarks)
            {
                total += mark;
            }
            return total / (double)assessmentMarks.Count;

        }

        public string GetAverageGrade()
        {
            double avg = GetAverageMark();
            return MakeGrade((int)avg);
        }

        private string MakeGrade(int mark)
        {
            string grade;
            switch (mark)
            {
                case (>= 90):
                    grade = "A+";
                    break;
                case (>= 85):
                    grade = "A";
                    break;
                case (>= 80):
                    grade = "A-";
                    break;
                case (>= 75):
                    grade = "B+";
                    break;
                case (>= 70):
                    grade = "B";
                    break;
                case (>= 65):
                    grade = "B-";
                    break;
                case (>= 60):
                    grade = "C+";
                    break;
                case (>= 55):
                    grade = "C";
                    break;
                case (>= 50):
                    grade = "C-";
                    break;
                case (>= 40):
                    grade = "D";
                    break;
                case (>= 0):
                    grade = "E";
                    break;
                default:
                    throw new Exception("Grades added were not within 0 to 100");
            }
            return grade;
        }
    }
}
