using ConsoleTables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;

/* 
    The following code was written by Jenna Boyes.
    For Project 1 in Programming 2, Bachelor of IT, Otago Polytechnic.

    The code below allows users to access a menu system. 
    This runs methods to show data and add/remove people.
*/
namespace JennaB_SMS
{
    internal class Program
    {
        private static List<Learner> learners;
        private static List<Lecturer> lecturers;
        private static List<Course> courses;

        static void Main()
        {
            //setup the program
            Utils.SeedInstitutions();
            Utils.SeedDepartments();
            courses = Utils.SeedCourses();

            learners = new List<Learner>();
            Utils.ReadLearnersFromFile("../../../learners.txt", learners);

            lecturers = new List<Lecturer>();
            Utils.ReadLecturersFromFile("../../../lecturers.txt", lecturers);

            //---------------------------------\\
            //menu system
            string input;
            do
            {
                Console.Clear();
                Console.WriteLine(@"                                             
       _____ _         _         _           
      |   __| |_ _ _ _| |___ ___| |_         
      |__   |  _| | | . | -_|   |  _|        
      |_____|_| |___|___|___|_|_|_|          
                                             
                                             
 _____                                   _   
|     |___ ___ ___ ___ ___ _____ ___ ___| |_ 
| | | | .'|   | .'| . | -_|     | -_|   |  _|
|_|_|_|__,|_|_|__,|_  |___|_|_|_|___|_|_|_|  
                  |___|                      
                                             
       _____         _                       
      |   __|_ _ ___| |_ ___ _____           
      |__   | | |_ -|  _| -_|     |          
      |_____|_  |___|_| |___|_|_|_|          
            |___|");
                Console.WriteLine("\n\n\n");

                var menuTable = new ConsoleTable("Number", "Function");
                menuTable.AddRow(1, "Course Details")
                    .AddRow(2, "All Marks")
                    .AddRow(3, "All Grades")
                    .AddRow(4, "Highest Marks")
                    .AddRow(5, "Lowest Marks")
                    .AddRow(6, "Failing Marks")
                    .AddRow(7, "Average Marks")
                    .AddRow(8, "Average Grades")
                    .AddRow(9, "Add a Learner")
                    .AddRow(10, "Remove a Learner")
                    .AddRow(11, "Lecturer Details")
                    .AddRow(12, "Add a Lecturer")
                    .AddRow(99, "Exit Program");

                menuTable.Write(Format.Minimal);

                //input management
                Console.Write("\nPlease select the number to run:   ");
                input = Console.ReadLine().Trim();
                Console.Clear();
                switch (input)
                {
                    case "1":
                        CourseDetails();
                        break;
                    case "2":
                        AllMarks();
                        break;
                    case "3":
                        AllGrades();
                        break;
                    case "4":
                        HighestMarks();
                        break;
                    case "5":
                        LowestMarks();
                        break;
                    case "6":
                        FailMarks();
                        break;
                    case "7":
                        AvgMarks();
                        break;
                    case "8":
                        AvgGrades();
                        break;
                    case "9":
                        AddLearner();
                        break;
                    case "10":
                        RemoveLearner();
                        break;
                    case "11":
                        LecturerDetails();
                        break;
                    case "12":
                        AddLecturer();
                        break;
                    case "99":
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n\n\nThank you for using this SMS program");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    default:
                        ErrorMessage("\n\nERROR: invalid input detected \nPlease enter a number shown on the menu");
                        break;
                }

                //end of each menu item
                if (input != "99")
                {
                    Console.WriteLine("\n\n\nPress ENTER to return to the menu");
                    Console.ReadLine();
                }

            } while (input != "99");
        }

        //-------------menu items-------------\\
        static void CourseDetails()
        {
            Console.WriteLine(@"
   ___                        ___      _        _ _    
  / __|___ _  _ _ _ ___ ___  |   \ ___| |_ __ _(_) |___
 | (__/ _ \ || | '_(_-</ -_) | |) / -_)  _/ _` | | (_-<
  \___\___/\_,_|_| /__/\___| |___/\___|\__\__,_|_|_/__/");
            Console.WriteLine("\n\n\n");

            var courseTable = new ConsoleTable("Course", "Description", "Credits", "Fees", "Institution", "Department");
            foreach (Course course in courses)
            {
                courseTable.AddRow(
                    $"{course.Code}: {course.Name}",
                    course.Description,
                    course.Credits,
                    $"{course.Fees:C2}",
                    $"{course.Department.Institution.Name}, {course.Department.Institution.Region}, {course.Department.Institution.Country}",
                    course.Department.Name);
            }
            courseTable.Write(Format.Minimal);
        }

        static void AllMarks()
        {
            Console.WriteLine(@"
    _   _ _   __  __          _       
   /_\ | | | |  \/  |__ _ _ _| |__ ___
  / _ \| | | | |\/| / _` | '_| / /(_-<
 /_/ \_\_|_| |_|  |_\__,_|_| |_\_\/__/");
            Console.WriteLine("\n\n\n");

            List<int> allMarks;
            var MarksTable = new ConsoleTable("ID", "Name", "Course", "Marks");
            foreach (Learner learner in learners)
            {
                allMarks = learner.CourseAssessmentMarks.GetAllMarks();
                MarksTable.AddRow(learner.Id,
                   learner.FirstName + " " + learner.LastName,
                   learner.CourseAssessmentMarks.Course.Code + ": " + learner.CourseAssessmentMarks.Course.Name,
                   allMarks[0].ToString().PadLeft(3) + ", " + allMarks[1].ToString().PadLeft(3) + ", " + allMarks[2].ToString().PadLeft(3)
                    + ", " + allMarks[3].ToString().PadLeft(3) + ", " + allMarks[4].ToString().PadLeft(3));
            }
            MarksTable.Write(Format.Minimal);
        }

        static void AllGrades()
        {
            Console.WriteLine(@"
    _   _ _    ___             _        
   /_\ | | |  / __|_ _ __ _ __| |___ ___
  / _ \| | | | (_ | '_/ _` / _` / -_|_-<
 /_/ \_\_|_|  \___|_| \__,_\__,_\___/__/");
            Console.WriteLine("\n\n\n");

            List<string> allGrades;
            var GradesTable = new ConsoleTable("ID", "Name", "Course", "Grades");
            foreach (Learner learner in learners)
            {
                allGrades = learner.CourseAssessmentMarks.GetAllGrades();
                GradesTable.AddRow(learner.Id,
                   learner.FirstName + " " + learner.LastName,
                   learner.CourseAssessmentMarks.Course.Code + ": " + learner.CourseAssessmentMarks.Course.Name,
                   allGrades[0].PadRight(2) + ", " + allGrades[1].PadRight(2) + ", " + allGrades[2].PadRight(2)
                    + ", " + allGrades[3].PadRight(2) + ", " + allGrades[4].PadRight(2));
            }
            GradesTable.Write(Format.Minimal);
        }

        static void HighestMarks()
        {
            Console.WriteLine(@"
  _  _ _      _           _     __  __          _       
 | || (_)__ _| |_  ___ __| |_  |  \/  |__ _ _ _| |__ ___
 | __ | / _` | ' \/ -_|_-<  _| | |\/| / _` | '_| / /(_-<
 |_||_|_\__, |_||_\___/__/\__| |_|  |_\__,_|_| |_\_\/__/
        |___/");
            Console.WriteLine("\n\n\n");

            var MarksTable = new ConsoleTable("ID", "Name", "Course", "Marks");
            foreach (Learner learner in learners)
            {
                List<int> highMarks = learner.CourseAssessmentMarks.GetHighestMarks();

                //ConvertAll found here https://www.techiedelight.com/convert-list-of-int-to-list-of-string-in-csharp/
                //i want to use List<string> to print empty strings where there are no marks, to stop the method from throwing errors
                List<string> marksString = highMarks.ConvertAll(x => x.ToString());
                while (marksString.Count < 5)
                {
                    marksString.Add("");
                }

                MarksTable.AddRow(learner.Id,
                   learner.FirstName + " " + learner.LastName,
                   learner.CourseAssessmentMarks.Course.Code + ": " + learner.CourseAssessmentMarks.Course.Name,
                   marksString[0].ToString().PadLeft(3) + ", " + marksString[1].ToString().PadLeft(3) + ", " + marksString[2].ToString().PadLeft(3)
                    + ", " + marksString[3].ToString().PadLeft(3) + ", " + marksString[4].ToString().PadLeft(3));
            }
            MarksTable.Write(Format.Minimal);
        }

        static void LowestMarks()
        {
            Console.WriteLine(@"
  _                      _     __  __          _       
 | |   _____ __ _____ __| |_  |  \/  |__ _ _ _| |__ ___
 | |__/ _ \ V  V / -_|_-<  _| | |\/| / _` | '_| / /(_-<
 |____\___/\_/\_/\___/__/\__| |_|  |_\__,_|_| |_\_\/__/");
            Console.WriteLine("\n\n\n");

            var MarksTable = new ConsoleTable("ID", "Name", "Course", "Marks");
            foreach (Learner learner in learners)
            {
                List<int> lowMarks = learner.CourseAssessmentMarks.GetLowestMarks();
                List<string> marksString = lowMarks.ConvertAll(x => x.ToString());
                while (marksString.Count < 5)
                {
                    marksString.Add("");
                }

                MarksTable.AddRow(learner.Id,
                   learner.FirstName + " " + learner.LastName,
                   learner.CourseAssessmentMarks.Course.Code + ": " + learner.CourseAssessmentMarks.Course.Name,
                   marksString[0].ToString().PadLeft(3) + ", " + marksString[1].ToString().PadLeft(3) + ", " + marksString[2].ToString().PadLeft(3)
                    + ", " + marksString[3].ToString().PadLeft(3) + ", " + marksString[4].ToString().PadLeft(3));
            }
            MarksTable.Write(Format.Minimal);
        }

        static void FailMarks()
        {
            Console.WriteLine(@"
  ___     _ _   __  __          _       
 | __|_ _(_) | |  \/  |__ _ _ _| |__ ___
 | _/ _` | | | | |\/| / _` | '_| / /(_-<
 |_|\__,_|_|_| |_|  |_\__,_|_| |_\_\/__/");
            Console.WriteLine("\n\n\n");

            var MarksTable = new ConsoleTable("ID", "Name", "Course", "Marks");
            foreach (Learner learner in learners)
            {
                List<int> failMarks = learner.CourseAssessmentMarks.GetFailMarks();
                List<string> marksString = failMarks.ConvertAll(x => x.ToString());
                while (marksString.Count < 5)
                {
                    marksString.Add("");
                }

                MarksTable.AddRow(learner.Id,
                   learner.FirstName + " " + learner.LastName,
                   learner.CourseAssessmentMarks.Course.Code + ": " + learner.CourseAssessmentMarks.Course.Name,
                   marksString[0].ToString().PadLeft(3) + ", " + marksString[1].ToString().PadLeft(3) + ", " + marksString[2].ToString().PadLeft(3)
                    + ", " + marksString[3].ToString().PadLeft(3) + ", " + marksString[4].ToString().PadLeft(3));
            }
            MarksTable.Write(Format.Minimal);
        }

        static void AvgMarks()
        {
            Console.WriteLine(@"
    _                              __  __          _   
   /_\__ _____ _ _ __ _ __ _ ___  |  \/  |__ _ _ _| |__
  / _ \ V / -_) '_/ _` / _` / -_) | |\/| / _` | '_| / /
 /_/ \_\_/\___|_| \__,_\__, \___| |_|  |_\__,_|_| |_\_\
                       |___/");
            Console.WriteLine("\n\n\n");

            var MarksTable = new ConsoleTable("ID", "Name", "Course", "Mark");
            foreach (Learner learner in learners)
            {
                double avgMark = learner.CourseAssessmentMarks.GetAverageMark();
                MarksTable.AddRow(learner.Id,
                   learner.FirstName + " " + learner.LastName,
                   learner.CourseAssessmentMarks.Course.Code + ": " + learner.CourseAssessmentMarks.Course.Name,
                   $"{avgMark:f1}".PadLeft(5));
            }
            MarksTable.Write(Format.Minimal);
        }

        static void AvgGrades()
        {
            Console.WriteLine(@"
    _                               ___             _     
   /_\__ _____ _ _ __ _ __ _ ___   / __|_ _ __ _ __| |___ 
  / _ \ V / -_) '_/ _` / _` / -_) | (_ | '_/ _` / _` / -_)
 /_/ \_\_/\___|_| \__,_\__, \___|  \___|_| \__,_\__,_\___|
                       |___/");
            Console.WriteLine("\n\n\n");

            var GradesTable = new ConsoleTable("ID", "Name", "Course", "Grade");
            foreach (Learner learner in learners)
            {
                string avgGrade = learner.CourseAssessmentMarks.GetAverageGrade();
                GradesTable.AddRow(learner.Id,
                   learner.FirstName + " " + learner.LastName,
                   learner.CourseAssessmentMarks.Course.Code + ": " + learner.CourseAssessmentMarks.Course.Name,
                   avgGrade);
            }
            GradesTable.Write(Format.Minimal);
        }

        static void AddLearner()
        {
            int courseNum, mark1 = 999, mark2 = 999, mark3 = 999, mark4 = 999, mark5 = 999; //debug, if 999 shows, program is not functioning correctly

            Console.WriteLine(@"
    _      _    _   _                              
   /_\  __| |__| | | |   ___ __ _ _ _ _ _  ___ _ _ 
  / _ \/ _` / _` | | |__/ -_) _` | '_| ' \/ -_) '_|
 /_/ \_\__,_\__,_| |____\___\__,_|_| |_||_\___|_|");
            Console.WriteLine("\n\n\n");

            //ID 
            List<int> ids = new List<int>();
            foreach (var learner in learners)
            {
                ids.Add(learner.Id);
            }
            ids.Sort();
            int id = ids.Last() + 1;

            //first and last names
            ErrorMessage("Please type capitalised letters where needed!\n\n");
            Console.Write("Please give the learner's first name:   ");
            string first = OnlyLettersCheck(); //check only letters, no space, special, nums
            Console.Write("Please give the learner's last name:   ");
            string last = OnlyLettersCheck(); //check only letters, no space, special, nums

            //course
            Console.Write("\n\n0 - Programming 2 \n" +
                "1 - Engine Systems \n" +
                "2 - Studio Methodologies 1\n\n" +
                "Please give the number relative to the learner's course above:    ");
            bool isValid;
            do
            {
                courseNum = OnlyIntcheck();
                isValid = true;
                if (courseNum < 0 || courseNum > courses.Count - 1 )
                {
                    isValid = false;
                    ErrorMessage("Error that is an invalid input, please only type the numbers above then press ENTER");
                }
            } while (!isValid);

            //marks
            Console.WriteLine("\n\n\nPlease enter each mark, one per line");
            for (int i = 0; i < 5; i++)
            {
                int temp;
                do
                {
                    temp = OnlyIntcheck();
                    isValid = true;
                    if (temp < 0 || temp > 100)
                    {
                        isValid = false;
                        ErrorMessage("Error that is an invalid input, please only type a number 0 to 100 inclusive, then press ENTER");
                    }
                } while (!isValid);

                if (i == 0) { mark1 = temp; }
                else if (i == 1) { mark2 = temp; }
                else if (i == 2) { mark3 = temp; }
                else if (i == 3) { mark4 = temp; }
                else if (i == 4) { mark5 = temp; }
                else { throw new Exception("ERROR: New Learner adding marks failed"); }

            }
            //check with user
            Console.Clear();
            Console.WriteLine("\n\n\n");
            ConsoleTable learnerTable = new ConsoleTable("ID", "Name", "Course", "Marks");
            learnerTable.AddRow(id, first + " " + last, courses[courseNum].Name, $"{mark1}, {mark2}, {mark3}, {mark4}, {mark5}");
            learnerTable.Write(Format.Minimal);
            Console.WriteLine("\n\nIs above table showing all correct information? y/n");
            char isCorrect = OnlyYesNoCheck();

            if (isCorrect == 'y') //saving to file
            {
                Console.Clear();
                CourseAssessmentMark cam = new CourseAssessmentMark(courses[courseNum], new List<int>() { mark1, mark2, mark3, mark4, mark5 });
                learners.Add(new Learner(id, first, last, cam));
                SaveLearnersToFile();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\n\nSuccess!\nLearner has been added");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n\n\nLearner has not been added.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        static void RemoveLearner()
        {
            Console.WriteLine(@"
  ___                         _                              
 | _ \___ _ __  _____ _____  | |   ___ __ _ _ _ _ _  ___ _ _ 
 |   / -_) '  \/ _ \ V / -_) | |__/ -_) _` | '_| ' \/ -_) '_|
 |_|_\___|_|_|_\___/\_/\___| |____\___\__,_|_| |_||_\___|_|");
            Console.WriteLine("\n\n\n");

            //show user all learners
            List<int> marks = new List<int>();
            ConsoleTable learnerTable = new ConsoleTable("ID", "Name", "Course");
            foreach (Learner learner in learners)
            {
                learnerTable.AddRow($"{learner.Id}",
                    $"{learner.FirstName} {learner.LastName}",
                    $"{learner.CourseAssessmentMarks.Course.Name}");
            }
            learnerTable.Write(Format.Minimal);

            //get id from user, check valid
            int id;
            bool isValid;
            do
            {
                isValid = false;
                Console.Write("Of the above learners, please enter the ID number to remove:   ");
                id = OnlyIntcheck();
                foreach (Learner learner in learners)
                {
                    if (learner.Id.CompareTo(id) == 0)
                    {
                        isValid = true;
                    }
                }
                if (!isValid) { ErrorMessage("That is not an ID number of the students above"); }
            } while (!isValid);

            //show learner with id, double check with user
            int i = learners.FindIndex(learner => learner.Id.CompareTo(id) == 0);
            Console.WriteLine("\n\n\n");
            ConsoleTable removeTable = new ConsoleTable("ID", "Name", "Course");
            removeTable.AddRow(learners[i].Id,
                    $"{learners[i].FirstName} {learners[i].LastName}",
                    $"{learners[i].CourseAssessmentMarks.Course.Name}");
            removeTable.Write(Format.Minimal);

            Console.Write("Is this the correct learner? y/n   ");
            char remove = OnlyYesNoCheck();

            //remove learner from list and file
            if (remove == 'y')
            {
                learners.RemoveAt(i);
                SaveLearnersToFile();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\n\nSuccess!\nLearner has been removed");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        static void LecturerDetails()
        {
            Console.WriteLine(@"
  _           _                      ___      _        _ _    
 | |   ___ __| |_ _  _ _ _ ___ _ _  |   \ ___| |_ __ _(_) |___
 | |__/ -_) _|  _| || | '_/ -_) '_| | |) / -_)  _/ _` | | (_-<
 |____\___\__|\__|\_,_|_| \___|_|   |___/\___|\__\__,_|_|_/__/");
            Console.WriteLine("\n\n\n");

            ConsoleTable lecturerTable = new ConsoleTable("ID", "Name", "Position", "Institution", "Department", "Course", "Salary");
            foreach (Lecturer lecturer in lecturers) 
            {
                lecturerTable.AddRow(lecturer.Id,
                    lecturer.FirstName + " " + lecturer.LastName,
                    lecturer.Position,
                    lecturer.Course.Department.Institution.Name + ", " + lecturer.Course.Department.Institution.Region + ", " + lecturer.Course.Department.Institution.Country,
                    lecturer.Course.Department.Name,
                    lecturer.Course.Code + ": " + lecturer.Course.Name,
                    $"{(int)lecturer.Salary:C2}");
            }
            lecturerTable.Write(Format.Minimal);
        }

        static void AddLecturer()
        {
            Console.WriteLine(@"
    _      _    _   _           _                    
   /_\  __| |__| | | |   ___ __| |_ _  _ _ _ ___ _ _ 
  / _ \/ _` / _` | | |__/ -_) _|  _| || | '_/ -_) '_|
 /_/ \_\__,_\__,_| |____\___\__|\__|\_,_|_| \___|_|");
            Console.WriteLine("\n\n\n");

            //ID 
            List<int> ids = new List<int>();
            foreach (Lecturer lecturer in lecturers)
            {
                ids.Add(lecturer.Id);
            }
            ids.Sort();
            int id = ids.Last() + 1;

            //names
            ErrorMessage("Please type capitalised letters where needed!\n\n");
            Console.Write("Please give the lecturer's first name:   ");
            string first = OnlyLettersCheck(); 
            Console.Write("Please give the lecturer's last name:   ");
            string last = OnlyLettersCheck();

            //position
            List<int> validInts = new List<int>();
            Console.WriteLine("\n\n");
            foreach (int i in Enum.GetValues(typeof(Position)))          //https://www.c-sharpcorner.com/article/loop-through-enum-values-in-c-sharp/ used for showing all lect positions without hard coding them in
            {
                Console.WriteLine($" {i} - {(Position)i}");
                validInts.Add(i);
            }
            Console.Write("Please give the number relative to the lecturer's position above:    ");
            bool isValid;
            int posInt;
            do
            {
                posInt = OnlyIntcheck();
                isValid = true;
                if (!validInts.Contains(posInt))
                {
                    isValid = false;
                    ErrorMessage("Error that is an invalid input, please only type the numbers above then press ENTER");
                }
            } while (!isValid);

            //salary
            Salary salary = ConvertPositionToSalary((Position)posInt);
            //Console.WriteLine(salary + " " + (int)salary); //debug

            //course
            Console.WriteLine("\n\n");
            foreach (Course course1 in courses) 
            {
                Console.WriteLine(courses.IndexOf(course1) + " - " + course1.Name);
            }
            Console.Write("Please give the number relative to the lecturer's course above:   ");
            int courseInt;
            do
            {
                isValid = true;
                courseInt = OnlyIntcheck();
                try { Course testCourse = courses[courseInt]; }
                catch (Exception) 
                { 
                    isValid = false;
                    ErrorMessage("Error that is an invalid input, please only type the numbers above then press ENTER");
                }
            }while (!isValid);

            //check with user
            Console.Clear();
            Console.WriteLine("\n\n\n");
            ConsoleTable lecturerTable = new ConsoleTable("ID", "Name", "Position", "Salary", "Course");
            lecturerTable.AddRow(id, first + " " + last, (Position)posInt, $"{(int)salary:C2}", courses[courseInt].Name);
            lecturerTable.Write(Format.Minimal);
            Console.WriteLine("\n\nIs above table showing all correct information? y/n");
            char isCorrect = OnlyYesNoCheck();

            //saving lect to file
            if (isCorrect == 'y')
            {
                lecturers.Add(new Lecturer(id, first, last, (Position)posInt, salary, courses[courseInt]));
                StreamWriter sw = new StreamWriter("../../../lecturers.txt");
                foreach (Lecturer lect in lecturers)
                {
                    sw.WriteLine($"{lect.Id}" +
                        $",{lect.FirstName}" +
                        $",{lect.LastName}" +
                        $",{(int)lect.Position}" +
                        $",{(int)lect.Salary}" +
                        $",{courses.IndexOf(lect.Course)}");
                }
                sw.Close();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\n\nSuccess!\nLecturer has been added");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n\n\nLecturer has not been added.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        //-------------supporting methods-------------\\
        static string OnlyLettersCheck()
        {
            string input;
            bool isValid;
            do
            {
                isValid = true;
                input = Console.ReadLine();
                if (input == "" || !Regex.IsMatch(input, "^[a-zA-Z]*$"))
                {
                    isValid = false;
                    ErrorMessage("Error that is an invalid input, please only type letters then press ENTER");
                }
            } while (!isValid);
            return input;
        }

        static int OnlyIntcheck()
        {
            string input;
            bool isValid;
            do
            {
                isValid = true;
                input = Console.ReadLine();
                if (input == "" || !Regex.IsMatch(input, "^[0-9]*$"))
                {
                    isValid = false;
                    ErrorMessage("Error that is an invalid input, please only type numbers then press ENTER");
                }
            } while (!isValid);
            return int.Parse(input);
        }

        static char OnlyYesNoCheck()
        {
            string input;
            bool isValid;
            do
            {
                isValid = true;
                input = Console.ReadLine().Trim(); //return only 1 letter in case user types "yy" or "yn"
                if (input == "" || !Regex.IsMatch(input[0].ToString(), "^[yn]*$"))
                {
                    isValid = false;
                    ErrorMessage("Invalid input, please only enter 'y' or 'n'.");
                }
            } while (!isValid);
            return input[0];
        }

        static void ErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void SaveLearnersToFile()
        {
            StreamWriter sw = new StreamWriter("../../../learners.txt");
            foreach (Learner learner in learners)
            {
                List<int> marks = learner.CourseAssessmentMarks.GetAllMarks();
                sw.WriteLine($"{learner.Id}" +
                    $",{learner.FirstName}" +
                    $",{learner.LastName}" +
                    $",{courses.IndexOf(learner.CourseAssessmentMarks.Course)}" +
                    $",{marks[0]},{marks[1]},{marks[2]},{marks[3]},{marks[4]}");
            }
            sw.Close();
        }
        
        static Salary ConvertPositionToSalary(Position position)
        {
            Salary sal;
            switch ((int)position)
            {
                case 0:
                    sal = Salary.LECTURER_SALARY;
                    break;
                case 1:
                    sal = Salary.SENIOR_LECTURER_SALARY;
                    break;
                case 2:
                    sal = Salary.PRINCIPAL_LECTURER_SALARY;
                    break;
                case 3:
                    sal = Salary.ASSOCIATE_PROFESSOR_SALARY;
                    break;
                case 4:
                    sal = Salary.PROFESSOR_SALARY;
                    break;
                default:
                    throw new Exception("Unexpected Position value, please check Convert method and update");
            }
            return sal;
        }
    }
}