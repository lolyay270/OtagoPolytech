namespace _02_formative_assessment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Task1();
            //Task2();
            //Task3();
            //Task4();
            //Task5();
            //Task6();
            //Task7();
            //Task8();
            //Task9();
            Task10();
        }

        static void Task1()
        {
            //take lists given, merge with AddRange to vari called allProgLanguages
            //use Add to add "Rust" to new vari
            //use Remove to delete "Swift" from new vari
            //loop to C.WL all in new vari

            //local variables
            List<string> progLangOne = new List<string>() { "C#", "JavaScript", "Kotlin", "Python" };
            List<string> progLangTwo = new List<string>() { "C++", "Go", "Swift", "TypeScript" };
            List<string> allProgLanguages = new List<string>();

            //merge lists
            allProgLanguages.AddRange(progLangOne);
            allProgLanguages.AddRange(progLangTwo);

            //add "Rust" and remove "Swift"
            allProgLanguages.Add("Rust");
            allProgLanguages.Remove("Swift");

            //show all to console
            foreach (string lang in allProgLanguages)
            {
                Console.WriteLine(lang);
            }
        }

        static void Task2()
        {
            //given list
            List<int> nums = new List<int>() { 10, 20, 30, 40, 50 };

            //Use Insert for 25 at 2nd pos in list, c.wl list
            nums.Insert(1, 25);
            foreach (int num in nums)
            {
                Console.Write(num + ", ");
            }
            Console.WriteLine();

            //use Contains to find 35, assign output into hasNumber35, c.wl hasnum35
            bool hasNumber35 = nums.Contains(35);
            Console.WriteLine("Number 35 in list: " + hasNumber35);

            //use Find first pop over 30, assign pop into firstNumberGreaterThan30, c.wl vari
            int firstNumberGreaterThan30 = nums.Find(x => x>30);
            Console.WriteLine("First num greater than 30: " + firstNumberGreaterThan30);

            //use Sort on list for asc nums, c.wl list
            nums.Sort();
            foreach (int num in nums)
            {
                Console.Write(num + ", ");
            }
            Console.WriteLine();
        }

        static void Task3()
        {
            //given list
            List<string> bookTitles = new List<string>() { "The Great Gatsby", "To Kill a Mockingbird", "1984", "Brave New World" };

            //use Count and assign to totalBooks, c.wl total
            int totalBooks = bookTitles.Count();
            Console.WriteLine("Total books in list: " + totalBooks);

            //use Contains check "Brave New World", assign to hasBraveNewWorld, c.wl has...
            bool hasBraveNewWorld = bookTitles.Contains("Brave New World");
            Console.WriteLine("Brave New World in list: " + hasBraveNewWorld);

            //use FindIndex for "1984", assign to index1984, c.wl index...
            int index1984 = bookTitles.FindIndex(x => x.Equals("1984"));
            Console.WriteLine("Index of 1984: " + index1984);

            //use Clear to empty list, c.wl list
            bookTitles.Clear();
            Console.Write("Books after clear: ");
            foreach (string book in bookTitles)
            {
                Console.Write(book + ", ");
            }
            Console.WriteLine();
        }

        static void Task4()
        {
            //given list
            List<int> numbers = new List<int>() { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 };

            //linq query to sum all even pop in list and display
            int query = numbers.Where(num => (num % 2 == 0))
                               .Sum();

            Console.WriteLine("Sum of all even nums: " + query);
        }

        static void Task5()
        {
            //given list
            List<string> countries = new List<string>
            {
                "Argentina",
                "Australia",
                "Brazil",
                "Canada",
                "Egypt",
                "France",
                "India",
                "Italy",
                "Mexico",
                "Netherlands",
                "South Africa",
                "United States",
            };

            //linq query to display all starting with "i" or "I"
            var query = countries.Where(here => here[0].ToString().ToUpper().Contains('I'));

            Console.Write("Countries starting with \"I\": ");
            foreach (string here in query) 
            {
                Console.Write(here + ", ");
            }
            Console.WriteLine();
        }

        static void Task6()
        {
            //given list
            List<double> temperatures = new List<double>() { 24.5, 23.8, 25.3, 22.6, 26.1, 27.5, 21.9 };

            //calc avg
            Console.WriteLine($"Average temp: {temperatures.Average():f2}");

            //find max
            Console.WriteLine("Max temp: " + temperatures.Max());

            //find all >25 store in new list
            var query = temperatures.Where(temp => temp > 25.0);
            List<double> above25 = new List<double>();
            Console.Write("All temps above 25: ");
            foreach (double temp in query)
            {
                above25.Add(temp);
                Console.Write(temp + ", ");
            }
        }

        static void Task7()
        {
            //given list
            List<int> scores = new List<int>() { 78, 89, 92, 65, 70, 85, 92, 78, 93, 80 };

            //find max
            Console.WriteLine("max value: " + scores.Max());

            //remove dupes, store new list
            List<int> noDupes = new List<int>();
            foreach (int num in scores)
            {
                Console.WriteLine("num: " + num + "   first: " + scores.IndexOf(num) + "  last: " + scores.LastIndexOf(num));
                if (!noDupes.Contains(num))
                {
                    noDupes.Add(num);
                }
            }
            foreach (int num in noDupes)
            {
                Console.Write(num + ", ");
            }
            
        }

        static void Task8()
        {
            //given list
            List<string> words = new List<string>() { "apple", "banana", "orange", "grape", "kiwi", "pineapple" };

            //find words that contain 'a' but end with 'e' (case sensitive), store new list
            var query = words.Where(word => word.Contains('a') && word[word.Length - 1] == 'e');
            List<string> result = query.ToList();
            foreach (string word in query) 
            {
                Console.Write(word + ", ");
            }
            
            //find longest word
            string longestWord;
            longestWord = "";
            foreach (string word in words) 
            {
                if (word.Length > longestWord.Length)
                {
                    longestWord = word;
                }
            }
            //var query2 = words.Where(word => Math.Max(word.Length); 
            Console.WriteLine("\nlongest word: " + longestWord);
        }
        static void Task9()
        {
            //given list
            List<int> cityPopulations = new List<int>() { 5000000, 3000000, 1200000, 8000000, 2000000, 4500000, 6000000 };

            //find top 3 nums, store new list
            cityPopulations.Sort();
            List<int> maxPops = new List<int>();
            for (int i = 0; i < 3; i++)
            {
                maxPops.Add(cityPopulations[cityPopulations.Count - 1 - i]);
            }
            Console.Write("Max populations: ");
            foreach (int pop in maxPops) Console.Write(pop + ", ");

            //cal total pop
            int totalPop = cityPopulations.Sum();
            Console.WriteLine("\nTotal population: " + totalPop);
        }

        static void Task10()
        {
            //array of ints
            List<int> nums = new List<int> { 21, 19, 68, 55, 42, 12 };

            //itterate over, check odd num, display odds
            var query = nums.Where(i => (i % 2) == 1);
            Console.Write("\nOdd numbers: ");
            foreach (int i in query) Console.Write(i + ", ");

            //sort array asc, display
            nums.Sort();
            Console.Write("\nSorted nums: ");
            foreach (int i in nums) Console.Write(i + ", ");

            //week 1 task 10
            //read in computer-jokes.txt, store in array
            //randomly select and display
            //i have no idea how to do this is LINQ and my wrist is giving in so yep im done
        }
    }
}