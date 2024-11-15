using JennaB_SMS;

namespace StudentManagementSystemTest
{
    [TestClass]
    public class StudentManagementSystemTestClass
    {
        [TestMethod]
        public void TestInstitutionName()
        {
            Institution institution = new Institution("Otago Polytechnic", "Otago", "NZ");
            Assert.AreEqual("Otago Polytechnic", institution.Name);
        }

        [TestMethod]
        public void TestInstitutionRegion()
        {
            Institution institution = new Institution("Otago Polytechnic", "Otago", "NZ");
            Assert.AreEqual("Otago", institution.Region);
        }

        [TestMethod]
        public void TestInstitutionCountry()
        {
            Institution institution = new Institution("Otago Polytechnic", "Otago", "NZ");
            Assert.AreEqual("NZ", institution.Country);
        }

        //-------------------------\\

        [TestMethod]
        public void TestPersonFirstName()
        {
            Person person = new Person(1, "Jenna", "Boyes");
            Assert.AreEqual("Jenna", person.FirstName);
        }

        [TestMethod]
        public void TestPersonLastName() 
        {
            Person person = new Person(1, "Jenna", "Boyes");
            Assert.AreEqual("Boyes", person.LastName);
        }

        [TestMethod]
        public void TestPersonID() 
        {
            Person person = new Person(1, "Jenna", "Boyes");
            Assert.AreEqual(1, person.Id);
        }


        //--------------------------\\

        [TestMethod]
        public void TestGetAllMarks() 
        {
            Institution institution = new Institution("Otago Polytechnic", "Otago", "NZ");
            Department dept = new Department(institution, "IT");
            Course course = new Course(dept, "ID511001", "Programming 2", "Enable Learners to build simple programs", 15, 1000.00);
            CourseAssessmentMark cam = new CourseAssessmentMark(course, new List<int>() { 97, 95, 86, 40, 30 });
            Learner learner = new Learner(1, "Jenna", "Boyes", cam);

            string expected = string.Join(",", cam.AssessmentMarks);
            string actual = string.Join(",", learner.CourseAssessmentMarks.GetAllMarks());
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetAllGrades() 
        {
            Institution institution = new Institution("Otago Polytechnic", "Otago", "NZ");
            Department dept = new Department(institution, "IT");
            Course course = new Course(dept, "ID511001", "Programming 2", "Enable Learners to build simple programs", 15, 1000.00);
            CourseAssessmentMark cam = new CourseAssessmentMark(course, new List<int>() { 97, 95, 86, 40, 30 });
            Learner learner = new Learner(1, "Jenna", "Boyes", cam);

            string expected = "A+,A+,A,D,E";
            string actual = string.Join(",", learner.CourseAssessmentMarks.GetAllGrades());
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetHighestMarks() 
        {
            Institution institution = new Institution("Otago Polytechnic", "Otago", "NZ");
            Department dept = new Department(institution, "IT");
            Course course = new Course(dept, "ID511001", "Programming 2", "Enable Learners to build simple programs", 15, 1000.00);
            CourseAssessmentMark cam = new CourseAssessmentMark(course, new List<int>() { 97, 95, 86, 40, 30 });
            Learner learner = new Learner(1, "Jenna", "Boyes", cam);

            string expected = "97";
            string actual = string.Join(",", learner.CourseAssessmentMarks.GetHighestMarks());
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetLowestMarks() 
        {
            Institution institution = new Institution("Otago Polytechnic", "Otago", "NZ");
            Department dept = new Department(institution, "IT");
            Course course = new Course(dept, "ID511001", "Programming 2", "Enable Learners to build simple programs", 15, 1000.00);
            CourseAssessmentMark cam = new CourseAssessmentMark(course, new List<int>() { 97, 95, 86, 40, 30 });
            Learner learner = new Learner(1, "Jenna", "Boyes", cam);

            string expected = "30";
            string actual = string.Join(",", learner.CourseAssessmentMarks.GetLowestMarks());
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetFailMarks() 
        {
            Institution institution = new Institution("Otago Polytechnic", "Otago", "NZ");
            Department dept = new Department(institution, "IT");
            Course course = new Course(dept, "ID511001", "Programming 2", "Enable Learners to build simple programs", 15, 1000.00);
            CourseAssessmentMark cam = new CourseAssessmentMark(course, new List<int>() { 97, 95, 86, 40, 30 });
            Learner learner = new Learner(1, "Jenna", "Boyes", cam);

            string expected = "40,30";
            string actual = string.Join(",", learner.CourseAssessmentMarks.GetFailMarks());
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetAverageMark() 
        {
            Institution institution = new Institution("Otago Polytechnic", "Otago", "NZ");
            Department dept = new Department(institution, "IT");
            Course course = new Course(dept, "ID511001", "Programming 2", "Enable Learners to build simple programs", 15, 1000.00);
            CourseAssessmentMark cam = new CourseAssessmentMark(course, new List<int>() { 97, 95, 86, 40, 30 });
            Learner learner = new Learner(1, "Jenna", "Boyes", cam);

            double expected = 69.6;
            double actual = learner.CourseAssessmentMarks.GetAverageMark();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetAverageGrade() 
        {
            Institution institution = new Institution("Otago Polytechnic", "Otago", "NZ");
            Department dept = new Department(institution, "IT");
            Course course = new Course(dept, "ID511001", "Programming 2", "Enable Learners to build simple programs", 15, 1000.00);
            CourseAssessmentMark cam = new CourseAssessmentMark(course, new List<int>() { 97, 95, 86, 40, 30 });
            Learner learner = new Learner(1, "Jenna", "Boyes", cam);

            string expected = "B-";
            string actual = learner.CourseAssessmentMarks.GetAverageGrade();
            Assert.AreEqual(expected, actual);
        }

        //----------------------------\\

        [TestMethod]
        public void TestLecturerSalary()
        {
            Institution institution = new Institution("Otago Polytechnic", "Otago", "NZ");
            Department dept = new Department(institution, "IT");
            Course course = new Course(dept, "ID511001", "Programming 2", "Enable Learners to build simple programs", 15, 1000.00);
            Lecturer lecturer = new Lecturer(2, "Jenna", "Boyes", Position.LECTURER, Salary.LECTURER_SALARY, course);

            Assert.AreEqual(85000, (int)lecturer.Salary);
        }

        [TestMethod]
        public void TestSeniorLecturerSalary() 
        {
            Institution institution = new Institution("Otago Polytechnic", "Otago", "NZ");
            Department dept = new Department(institution, "IT");
            Course course = new Course(dept, "ID511001", "Programming 2", "Enable Learners to build simple programs", 15, 1000.00);
            Lecturer lecturer = new Lecturer(2, "Jenna", "Boyes", Position.SENIOR_LECTURER, Salary.SENIOR_LECTURER_SALARY, course);

            Assert.AreEqual(100000, (int)lecturer.Salary);
        }

        [TestMethod]
        public void TestPrincipalLecturerSalary() 
        {
            Institution institution = new Institution("Otago Polytechnic", "Otago", "NZ");
            Department dept = new Department(institution, "IT");
            Course course = new Course(dept, "ID511001", "Programming 2", "Enable Learners to build simple programs", 15, 1000.00);
            Lecturer lecturer = new Lecturer(2, "Jenna", "Boyes", Position.PRINCIPAL_LECTURER, Salary.PRINCIPAL_LECTURER_SALARY, course);

            Assert.AreEqual(115000, (int)lecturer.Salary);
        }

        [TestMethod]
        public void TestAssociateProfessorSalary() 
        {
            Institution institution = new Institution("Otago Polytechnic", "Otago", "NZ");
            Department dept = new Department(institution, "IT");
            Course course = new Course(dept, "ID511001", "Programming 2", "Enable Learners to build simple programs", 15, 1000.00);
            Lecturer lecturer = new Lecturer(2, "Jenna", "Boyes", Position.ASSOCIATE_PROFESSOR, Salary.ASSOCIATE_PROFESSOR_SALARY, course);

            Assert.AreEqual(130000, (int)lecturer.Salary);
        }

        [TestMethod]
        public void TestProfessorSalary() 
        {
            Institution institution = new Institution("Otago Polytechnic", "Otago", "NZ");
            Department dept = new Department(institution, "IT");
            Course course = new Course(dept, "ID511001", "Programming 2", "Enable Learners to build simple programs", 15, 1000.00);
            Lecturer lecturer = new Lecturer(2, "Jenna", "Boyes", Position.PROFESSOR, Salary.PROFESSOR_SALARY, course);

            Assert.AreEqual(145000, (int)lecturer.Salary);
        }

        //-------------------------------\\

        private List<Institution> institutions;
        private List<Department> departments;
        private List<Course> courses;

        [TestInitialize]
        public void InitializeObjects()
        {
            institutions = new List<Institution>();
            departments = new List<Department>();
            courses = new List<Course>();
        }       

        [TestMethod]
        public void TestInstitutionsAfterSeeding()
        {
            institutions = Utils.SeedInstitutions();
            Assert.AreEqual(3, institutions.Count);
        }

        [TestMethod]
        public void TestDepartmentsAfterSeeding()
        {
            institutions = Utils.SeedInstitutions();
            departments = Utils.SeedDepartments();
            Assert.AreEqual(3, departments.Count);
        }

        [TestMethod]
        public void TestCoursesAfterSeeding()
        {
            institutions = Utils.SeedInstitutions();
            departments = Utils.SeedDepartments();
            courses = Utils.SeedCourses();
            Assert.AreEqual(3, courses.Count);
        }

        [TestCleanup]
        public void CleanupObjects()
        {
            institutions.Clear();
            departments.Clear();
            courses.Clear();
        }
    }
}