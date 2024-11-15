using System.Runtime.CompilerServices;

namespace _01_formative_assessment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task1();
            Task2();
            Task3();
            Task4();
            Task5();
            Task6();
            Task7();
            Task8();
            Task9();
            Task10();
            Task11();
        }

        static void Task1()
        {
            //list local variables
            double[] nums = { 45.3, 67.5, -45.6, 20.34, -33.0, 45.6 };
            double sum = 0, avg;

            for (int i = 0; i < nums.Length; i++) //loop thru all in nums[]
            {
                sum += nums[i];
            }

            avg = sum / nums.Length;
            Headings("Average number generator:");
            Console.WriteLine($"The average of the numbers is {avg:f2}");
        }

        //-------------------------------------------------------------------------------------------------------\\

        static void Task2()
        {
            Headings("FizzBuzz odd numbers 1 to 15:");

            for (int i = 1; i < 16; i += 2) //loop 1 to 15 incl, step 2
            {
                Console.WriteLine(FizzBuzz(i));
            }
        }

        static string FizzBuzz(int num)
        {

            if (num % 15 == 0) return "fizzbuzz";
            else if (num % 3 == 0) return "fizz";
            else if (num % 5 == 0) return "buzz";
            else return num.ToString();
        }

        //-------------------------------------------------------------------------------------------------------\\

        static void Task3()
        {
            //local variables
            int[] nums = { 21, 19, 68, 55, 42, 12 };
            int temp;

            Headings("Odd numbers from list:");

            //if odd show to console
            Console.Write("Odd nums are:    ");
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] % 2 == 1) Console.Write(nums[i] + ", ");
            }
            Console.WriteLine();

            //sort array asc
            for (int i = 0; i < nums.Length - 1; i++)
            {
                for (int j = 0; j < nums.Length - 1; j++)
                {
                    if (nums[j] > nums[j + 1])
                    {
                        temp = nums[j + 1];
                        nums[j + 1] = nums[j];
                        nums[j] = temp;
                    }
                }
            }

            //display sorted nums
            Console.Write("Sorted nums:   ");
            for (int i = 0; i < nums.Length; i++)
            {
                Console.Write(nums[i] + ", ");
            }
        }

        //-------------------------------------------------------------------------------------------------------\\

        static void Task4()
        {
            //local variables
            string word1, word2;
            bool anagram;

            Headings("Anagram word checker: ");

            //get user input
            do
            {
                Console.Write("Please enter a word:   ");
                word1 = Console.ReadLine();
            } while (word1 == "");
            do
            {
                Console.Write("Please enter a word to compare:   ");
                word2 = Console.ReadLine();
            } while (word2 == "");

            anagram = isAnagram(word1, word2);
            //Console.WriteLine(anagram); //debug

            //give user results
            if (anagram)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Your words are anagrams to each other");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Your words are not anagrams");
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        static bool isAnagram(string someStrOne, string someStrTwo)
        {
            //local variables
            char[] sortOne = someStrOne.ToCharArray(), sortTwo = someStrTwo.ToCharArray();

            if (someStrOne.Length != someStrTwo.Length)
            {
                return false;
            }
            else
            {
                //sort both arrays asc
                Array.Sort(sortOne);
                Array.Sort(sortTwo);

                //show for debug
                /*for (int i = 0; i < sortOne.Length; i++)
                {
                    Console.WriteLine(sortOne[i]);
                }
                for (int i = 0; i < sortTwo.Length; i++)
                {
                    Console.WriteLine(sortTwo[i]);
                }

                //failed compare
                if (sortOne.ToString() != sortTwo.ToString())
                {
                    return false;
                }*/

                for (int i = 0; i < sortOne.Length; i++)
                {
                    if (sortOne[i] != sortTwo[i]) return false;
                }
            }
            return true;
        }

        //-------------------------------------------------------------------------------------------------------\\

        static void Task5()
        {
            string temp;
            int hrs, mins;

            Headings("Hours and minutes convert to seconds: ");

            do
            {
                Console.Write("Please give num of hours:     ");
                temp = Console.ReadLine();
            } while (temp == "");
            hrs = Convert.ToInt32(temp);

            do
            {
                Console.Write("Please give num of minutes:   ");
                temp = Console.ReadLine();
            } while (temp == "");
            mins = Convert.ToInt32(temp);

            Console.WriteLine("Total number of seconds is:  " + convert(hrs, mins));
        }

        static int convert(int hrs, int mins)
        { 
            int sec = hrs * 3600 + mins * 60;
            return sec;
        }

        //-------------------------------------------------------------------------------------------------------\\

        static void Task6()
        {
            //local variables
            string sentence = "The anemone, the wild violet, the hepatica, and the funny little curled-up.";
            string[] words = sentence.Split(' ');
            int count = 0;

            //count "the"
            for (int i = 0; i < words.Length;i++)
            {
                if (words[i] == "the")
                {
                    count++;
                }
            }

            //show user
            Headings("Counting words in a sentence: ");
            Console.Write($"The word \"the\" shows up {count} time");
            if (count != 1) Console.WriteLine("s");

        }

        //-------------------------------------------------------------------------------------------------------\\

        static void Task7()
        {
            Headings("Remove vowels sim: ");
            Console.Write("Please enter a word/sentence:   ");
            string word = Console.ReadLine();

            string fixedWord = removeVowels(word);

            if (word == fixedWord) Console.WriteLine($"There are no vowels to remove: {fixedWord}"); 
            else if (fixedWord[0] == ' ') Console.WriteLine("There are only vowels, nothing to show after removal");
            else Console.WriteLine($"Your word/sentence without vowels is: {fixedWord}");
        }
        static string removeVowels(string word)
        {
            word = word.Replace('a', ' ');
            word = word.Replace('e', ' ');
            word = word.Replace('i', ' ');
            word = word.Replace('o', ' ');
            word = word.Replace('u', ' ');

            return word;
        }

        //-------------------------------------------------------------------------------------------------------\\

        static void Task8()
        {
            Headings("Palindrome Checker: ");
            Console.Write("Please give word/phrase:  ");
            string word = Console.ReadLine();

            if (IsPalindrome(word)) 
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(word + " is a palindrome");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(word + " is not a palindrome");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        static bool IsPalindrome(string word)
        {
            for (int i = 0; i < (word.Length / 2) ; i++) 
            {
                if (word[i] != word[word.Length - 1 - i])
                {
                    return false;
                }
            }
            return true;
        }

        //-------------------------------------------------------------------------------------------------------\\

        static void Task9()
        {
            //get num
            Headings("Prime number checker: ");
            Console.Write("Please enter a number:   ");
            string temp = Console.ReadLine();
            int num = Convert.ToInt32(temp);

            //check it
            bool prime = IsPrime(num);

            //show results
            if (prime)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"The number {num} is a prime number");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"The number {num} is not a prime number");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        static bool IsPrime(int num)
        {
            if (num % 2 == 0) return false; //check even first

            int root = Convert.ToInt32(Math.Sqrt(num)); //root is upper limit, no point checking something twice
            Console.WriteLine(root); //debug
            for (int i = 3; i <= root; i += 2)  //checking all odd nums starting 3, cause 1 and 2 are prime
            {
                if (num % i == 0) return false;
            }
            return true;
        }

        //-------------------------------------------------------------------------------------------------------\\

        static void Task10()
        {
            //local variables
            StreamReader sr = new StreamReader("../../../computer-jokes.txt");
            Random rand = new Random();

            //get text
            string text = sr.ReadToEnd();
            sr.Close();
            string[] jokes = text.Split('^');

            //random choose
            text = jokes[rand.Next(jokes.Length)];

            Headings("Random joke picker:");
            Console.WriteLine(text);
        }

        //-------------------------------------------------------------------------------------------------------\\

        static void Task11()
        {
            //local variables
            StreamReader sr = new StreamReader("../../../countries.txt");
            int count = 0;
            string[] countries = new string[0];

            //get file in
            while (!sr.EndOfStream)
            {
                Array.Resize(ref countries, countries.Length + 1);
                countries[count] = sr.ReadLine();
                count++;
                //debug Console.WriteLine(countries[count]);
            }
            sr.Close();

            //show user
            Headings("Countries starting with 'B'");
            for (int i = 0; i < countries.Length; i++)
            {
                string temp = countries[i];
                //debug Console.WriteLine(temp[0] + "...");
                if (temp[0] == 'B')
                {
                    Console.WriteLine(countries[i]);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------\\

        public static void Headings(string heading) 
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n\n\n" + heading);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}