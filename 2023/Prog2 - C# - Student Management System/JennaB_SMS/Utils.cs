using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 
    The following code was written by Jenna Boyes.
    For Project 1 in Programming 2, Bachelor of IT, Otago Polytechnic.

    The code below holds the bulk of information for the whole project.
    It has enums, methods to seed lists of objects, and read from file methods.
    
*/

namespace JennaB_SMS
{
    //given enums
    public enum Position
    {
        LECTURER,
        SENIOR_LECTURER,
        PRINCIPAL_LECTURER,
        ASSOCIATE_PROFESSOR,
        PROFESSOR
    }
    public enum Salary
    {
        LECTURER_SALARY = 85000,
        SENIOR_LECTURER_SALARY = 100000,
        PRINCIPAL_LECTURER_SALARY = 115000,
        ASSOCIATE_PROFESSOR_SALARY = 130000,
        PROFESSOR_SALARY = 145000
    }

    public static class Utils
    {
        private static List<Institution> institutions = new List<Institution>();
        public static List<Institution> SeedInstitutions()
        {
            institutions.Add(new Institution("Otago Polytechnic", "Otago", "NZ"));
            institutions.Add(new Institution("ARA", "Canterbury", "NZ"));
            institutions.Add(new Institution("SIT", "Southland", "NZ"));
            return institutions;
        }

        private static List<Department> departments = new List<Department>();
        public static List<Department> SeedDepartments()
        {
            departments.Add(new Department(institutions[0], "IT"));
            departments.Add(new Department(institutions[1], "Art"));
            departments.Add(new Department(institutions[0], "Automotive"));
            return departments;
        }

        private static List<Course> courses = new List<Course>();
        public static List<Course> SeedCourses()
        {
            courses.Add(new Course(departments[0], "ID511001", "Programming 2", "Enable Learners to build simple programs", 15, 1000.00));
            courses.Add(new Course(departments[2], "AE304001", "Engine Systems", "How do car do the BRRROOOOOM", 10, 746.20));
            courses.Add(new Course(departments[1], "VA501002", "Studio Methodologies 1", "Draw so much my wrist dies", 15, 953.25));
            return courses;
        }

        public static void ReadLearnersFromFile(string filePath, List<Learner> learners)
        {
            StreamReader sr = new StreamReader(filePath);
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] split = line.Split(',');
                CourseAssessmentMark cam = new CourseAssessmentMark(courses[int.Parse(split[3])], new List<int>() { int.Parse(split[4]), int.Parse(split[5]), int.Parse(split[6]), int.Parse(split[7]), int.Parse(split[8]) });
                learners.Add(new Learner(int.Parse(split[0]), split[1], split[2], cam));
            }
            sr.Close();
        }

        public static void ReadLecturersFromFile(string filePath, List<Lecturer> lecturers)
        {
            StreamReader sr = new StreamReader(filePath);
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] split = line.Split(',');
                lecturers.Add(new Lecturer(int.Parse(split[0]), split[1], split[2], (Position)int.Parse(split[3]), (Salary)int.Parse(split[4]), courses[int.Parse(split[5])])); 
            }
            sr.Close();
        }
    }
}
