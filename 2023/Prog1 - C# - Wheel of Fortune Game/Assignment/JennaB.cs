namespace Assignment
{
    public struct Student
    {
        public string lastName;
        public string firstName;
        public string interest;
        public int score;
    }
    internal class JennaB
    {
        //list global variables
        public static Student[] students = new Student[38];
        public static Student[] finals = new Student[10];
        public static Student player;
        public static string temp = "";
        public static bool playerSet = false;
        public static Random rand = new Random();

        //-----------------------------------------------------------------------------------------------------------------------------------------\\
        static void Main(string[] args)
        {
            Prep();
            Menu();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------\\
        public static void Menu()
        {
            do
            {
                Console.Clear();
                //title
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nThe following options are the codes you can run:\n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(@"        Sub-Program        | Type this
---------------------------|-----------
  List contestants         |     1
  Change contestant data   |     2
  The finalists            |     3
  The player               |     4
  Play game                |     5
  Exit                     |    exit");
                Console.Write("\nWhat would you like to do:   ");
                temp = Console.ReadLine().ToLower();
                Console.Clear();

                switch (temp)
                {
                    case "1":
                        ListCon();
                        break;

                    case "2":
                        ChangeData();
                        break;

                    case "3":
                        Final10();
                        break;

                    case "4":
                        WhoPlayer();
                        break;

                    case "5":
                        PlayGame();
                        break;

                    default:
                        break;
                }
            } while (temp != "exit");
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------\\
        public static void Prep()
        {
            //list variables
            StreamReader sr = new StreamReader(@"wheelOfFortune.txt");
            int[] iPlace = new int[10];
            int ranNum;

            for (int i = 0; i < students.Length; i++) //looping thru all 38 people in .txt
            {
                students[i].lastName = sr.ReadLine();
                students[i].firstName = sr.ReadLine();
                students[i].interest = sr.ReadLine();
                students[i].score = Convert.ToInt32(sr.ReadLine());
            }
            sr.Close();


            for (int i = 0; i < finals.Length; i++) //randomly selecting 10 students
            {
                ranNum = rand.Next(0, students.Length);
                while (iPlace.Contains(ranNum)) //no repeats in finals[]
                {
                    ranNum = rand.Next(0, students.Length);
                }
                iPlace[i] = ranNum;
                finals[i] = students[ranNum];
            }


        }

        //-----------------------------------------------------------------------------------------------------------------------------------------\\
        public static void ListCon()
        {
            //list local variables
            Student temp2;

            //title
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nThis is the sub-program to list all contestant's data\n");
            Console.ForegroundColor = ConsoleColor.White;

            //reorder to last name alphabetical
            for (int i = 0; i < students.Length - 1; i++)
            {
                for (int pos = 0; pos < students.Length - 1; pos++)
                {
                    if (students[pos + 1].lastName.CompareTo(students[pos].lastName) < 0)
                    {
                        temp2 = students[pos + 1];
                        students[pos + 1] = students[pos];
                        students[pos] = temp2;
                    }
                }
            }

            //show to user
            Console.WriteLine("FIRST NAME".PadRight(18) + "LAST NAME".PadRight(16) + "INTEREST".PadRight(20) + "SCORE");
            Console.WriteLine(new string('―', 60));
            for (int i = 0; i < 38; i++)
            {
                //Console.WriteLine(j);  //using for debug
                Console.Write(students[i].firstName.PadRight(18));
                Console.Write(students[i].lastName.PadRight(16));
                Console.Write(students[i].interest.PadRight(21));
                Console.WriteLine(students[i].score.ToString().PadLeft(5));
            }

            Console.WriteLine("\n\n\n");

            //exit to menu when user wants
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\n\nPress ENTER to return to the menu ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadLine();
        }


        //-----------------------------------------------------------------------------------------------------------------------------------------\\
        public static void ChangeData()
        {
            //local variable list 
            int count = 0, correct;
            int[] where = new int[0];
            bool redo = false;


            //title
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nThis is the sub-program to change a contestant's data\n");
            Console.ForegroundColor = ConsoleColor.White;


            //-------which person to change
            do //ask for new person if user uses 999 escape code
            {
                do //get name, loop if no matches
                {
                    do //name input, no null
                    {
                        Console.Write("Please enter the contestant's first name:     ");
                        temp = Console.ReadLine().ToLower();
                        Console.WriteLine();
                    } while (temp == "");

                    for (int i = 0; i < students.Length; i++) //finding all instances where temp is in first name
                    {
                        if (students[i].firstName.ToLower().Contains(temp))
                        {
                            Array.Resize(ref where, where.Length + 1);
                            where[count] = i;
                            count++;
                        }
                    }

                    if (where.Length == 0) //tell user no matches
                    {
                        Console.WriteLine($"There are no first names that include \"{temp}\" \nPlease try another name\n");
                    }
                } while (where.Length == 0);

                Console.WriteLine("\nNumber  " + "First Name".PadRight(18) + "Last Name".PadRight(16) + "Interest".PadRight(21) + "Score");
                for (int i = 0; i < where.Length; i++) //show all matches to user
                {
                    Console.Write(i.ToString().PadLeft(5) + "   ");
                    Console.Write(students[where[i]].firstName.PadRight(18));
                    Console.Write(students[where[i]].lastName.PadRight(16));
                    Console.Write(students[where[i]].interest.PadRight(21));
                    Console.WriteLine(students[where[i]].score.ToString().PadLeft(5));
                }

                do //user input make sure user enters number within length of where[]
                {
                    do //user input which person correct one, no null
                    {
                        Console.Write("\nPlease enter the number on the left of the contestant that you wish to change \n Or enter 999 to search again:    ");
                        temp = Console.ReadLine();
                        Console.WriteLine();
                    } while (temp == "");
                } while (Convert.ToInt32(temp) >= where.Length && Convert.ToInt32(temp) != 999);

                if (Convert.ToInt32(temp) == 999) //user escape code to search another person
                {
                    redo = true;
                    Array.Resize(ref where, 0);
                    count = 0;
                }
                else
                {
                    redo = false;
                }
            } while (redo == true);

            correct = where[Convert.ToInt32(temp)];

            //---------allow change
            do //input new interest, no null
            {
                Console.Write($"\n\nPlease enter the new value for {students[correct].firstName}'s interest. \nIt should be something they like:    ");
                temp = Console.ReadLine();
            } while (temp == "");
            students[correct].interest = temp;

            //rewite all students back onto txt file to save changes
            writeTxt();

            //show change
            Console.WriteLine("\nThe change is shown below:\n");
            Console.WriteLine("First Name".PadRight(18) + "Last Name".PadRight(16) + "Interest".PadRight(21) + "Score");
            Console.Write(students[correct].firstName.PadRight(18));
            Console.Write(students[correct].lastName.PadRight(16));
            Console.Write(students[correct].interest.PadRight(21));
            Console.WriteLine(students[correct].score.ToString().PadLeft(5));

            //exit to menu when user wants
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\n\nPress ENTER to return to the menu ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadLine();
        }

        public static void writeTxt()
        {
            StreamWriter sw = new StreamWriter(@"wheelOfFortune.txt");
            for (int i = 0; i < students.Length; i++)
            {
                sw.WriteLine(students[i].lastName);
                sw.WriteLine(students[i].firstName);
                sw.WriteLine(students[i].interest);
                sw.WriteLine(students[i].score);
            }
            sw.Close();
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------\\
        public static void Final10()
        {
            //title
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("The 10 finalists are listed below\n\n\n");
            Console.ForegroundColor = ConsoleColor.White;


            Console.WriteLine("First Name".PadRight(18) + "Last Name".PadRight(16) + "Interest".PadRight(21) + "Score");
            Console.WriteLine(new string('-', 60));
            for (int i = 0; i < finals.Length; i++) //loop thru all 10 finalists
            {
                Console.Write(finals[i].firstName.PadRight(18));
                Console.Write(finals[i].lastName.PadRight(16));
                Console.Write(finals[i].interest.PadRight(21));
                Console.WriteLine(finals[i].score.ToString().PadLeft(5));
            }

            //exit to menu when user wants
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\n\nPress ENTER to return to the menu ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadLine();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------\\
        public static void WhoPlayer()
        {
            //title
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("The player who has been selected to play is below\n\n\n");
            Console.ForegroundColor = ConsoleColor.White;

            if (playerSet == false) //new player before each game is played
            {
                player = finals[rand.Next(finals.Length)];
                playerSet = true;
            }
            //show player to user
            Console.WriteLine("First Name".PadRight(18) + "Last Name".PadRight(16) + "Interest".PadRight(21) + "Score");
            Console.WriteLine(new string('―', 60));
            Console.Write(player.firstName.PadRight(18));
            Console.Write(player.lastName.PadRight(16));
            Console.Write(player.interest.PadRight(21));
            Console.WriteLine(player.score.ToString().PadLeft(5));


            //exit to menu when user wants
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\n\nPress ENTER to return to the menu ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadLine();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------\\
        public static void PlayGame()
        {
            //list local variables
            Student temp2;
            int wheelSelect, money = 0, count, moneyGain;
            string[] words = { "jabberwock", "jackknifed", "knickknack", "mozzarella", "squeezable", "buttcheeks", "hokeypokey", "quadruplex", "paddywhack", "hydrophobic", "cabbagehead", "jellyfishes", "showjumping", "emphasizing", "photography", "mythological" };
            char[] guessWord = new char[0], showAlpha = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            char[] ALPHA = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' }; //making a const version to check input is in alphabet
            string correctWord;
            char guess;
            bool gotIt = false, giveUp = false;

            //title
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(@" __       __  __                            __         ______    ______  
|  \  _  |  \|  \                          |  \       /      \  /      \ 
| $$ / \ | $$| $$____    ______    ______  | $$      |  $$$$$$\|  $$$$$$\
| $$/  $\| $$| $$    \  /      \  /      \ | $$      | $$  | $$| $$_  \$$
| $$  $$$\ $$| $$$$$$$\|  $$$$$$\|  $$$$$$\| $$      | $$  | $$| $$ \    
| $$ $$\$$\$$| $$  | $$| $$    $$| $$    $$| $$      | $$  | $$| $$$$    
| $$$$  \$$$$| $$  | $$| $$$$$$$$| $$$$$$$$| $$      | $$__/ $$| $$      
| $$$    \$$$| $$  | $$ \$$     \ \$$     \| $$       \$$    $$| $$      
 \$$      \$$ \$$   \$$  \$$$$$$$  \$$$$$$$ \$$        \$$$$$$  \$$      
                                                                         
                                                                         
                                                                         
 ________                     __                                    __   
|        \                   |  \                                  |  \  
| $$$$$$$$______    ______  _| $$_    __    __  _______    ______  | $$  
| $$__   /      \  /      \|   $$ \  |  \  |  \|       \  /      \ | $$  
| $$  \ |  $$$$$$\|  $$$$$$\\$$$$$$  | $$  | $$| $$$$$$$\|  $$$$$$\| $$  
| $$$$$ | $$  | $$| $$   \$$ | $$ __ | $$  | $$| $$  | $$| $$    $$ \$$  
| $$    | $$__/ $$| $$       | $$|  \| $$__/ $$| $$  | $$| $$$$$$$$ __   
| $$     \$$    $$| $$        \$$  $$ \$$    $$| $$  | $$ \$$     \|  \  
 \$$      \$$$$$$  \$$         \$$$$   \$$$$$$  \$$   \$$  \$$$$$$$ \$$");
            Console.WriteLine("\n\n");
            Console.ForegroundColor = ConsoleColor.White;

            if (playerSet == false) //player hasnt been set yet
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error! Player not found.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Please return the the menu and find out who the chosen player will be! \nYou will need to run menu option 4");
            }
            else //player has been set, game can run
            {
                //randomly select the correct word user has to guess
                correctWord = words[rand.Next(words.Length)];

                // fill guessWord with '0's to fill the length of correctWord
                for (int i = 0; i < correctWord.Length; i++)
                {
                    Array.Resize(ref guessWord, guessWord.Length + 1);
                    guessWord[i] = '0';
                }


                do //repeat until got all letters or user gives up
                {
                    //reset variables before each spin
                    count = 0;



                    //-----------------------show user stuff-----------------------\\
                    //spin wheel and show result
                    wheelSelect = wheelSpin();

                    //show user current money
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"\n\n\nYour current money is ${money}\n\n\n\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    //showing ALPHA to ensure it is not being changed, debugging
                    /*Console.WriteLine("all letters of the alphabet:");
                    foreach (char a in ALPHA)
                    {
                        Console.Write(a.ToString().ToUpper() + ", ");
                    }
                    Console.WriteLine("\n");*/

                    //show user the remaining letters in the alphabet
                    Console.WriteLine("The letters you have not used yet are as follows:");
                    foreach (char a in showAlpha)
                    {
                        Console.Write(a.ToString().ToUpper() + "  ");
                    }
                    Console.WriteLine("\n");


                    //show user the "_" for each missing letters and letters guessed correct
                    Console.WriteLine("The letters you have correctly guessed are as follows:");
                    foreach (char a in guessWord)
                    {
                        if (a == '0')
                        {
                            Console.Write("_ ");
                        }
                        else //a == letter
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(a + " ");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                    Console.WriteLine("\n\n");



                    //-----------------------get input, check it, adjust vari-----------------------\\
                    do //loop in case not in showAlpha, already guessed
                    {
                        do //loop in case input not in ALPHA
                        {
                            do //loop in case null input
                            {
                                //user guesses letter/vowel
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("\n\nPlease enter the letter you wish to guess, or enter the vowel you would like to buy:    ");
                                temp = Console.ReadLine().ToLower();

                                if (temp == "")
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.WriteLine("\n\n\nThat is not a valid input, you cannot just press ENTER, \nIf you would like to stop playing type GIVE UP and press ENTER\n\n\n");
                                }
                                if (temp == "give up")
                                {
                                    giveUp = true;
                                }

                            } while (temp == "");

                            if (!Char.IsLetter(temp[0]))
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("\n\n\nThat is not a valid input, the inputs need to be of the alphabet, \nIf you would like to stop playing type GIVE UP and press ENTER\n\n\n");
                            }
                        } while (ALPHA.Contains(temp[0]) == false);

                        if (showAlpha.Contains(temp[0]) == false)
                        {

                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\n\n\nYou cannot guess something you have already guessed, \nIf you would like to stop playing type GIVE UP and press ENTER\n\n\n");
                        }
                    } while (showAlpha.Contains(temp[0]) == false);

                    if (giveUp == false) //user wants to keep playing
                    {
                        //set guess after checking all above
                        guess = temp[0];

                        //check correct contains guess, and count how many times
                        if (correctWord.Contains(guess) == true)
                        {
                            count = 0; //reset to 0 so doesnt sum all instances
                            for (int i = 0; i < correctWord.Length; i++)
                            {
                                if (correctWord[i] == guess)
                                {
                                    guessWord[i] = guess; //add correct letters into guessWord to show user
                                    count++;
                                }
                            }
                        }
                        else //correctWord.Contains(guess) == false
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"\n\nSorry \"{guess.ToString().ToUpper()}\" is not part of the word to guess\n\n");
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        //remove ANY guess from showAlpha[]
                        showAlpha[Array.IndexOf(showAlpha, guess)] = ' ';



                        //-----------------------change money-----------------------\\
                        //calc new money by wheelSelect * count, negative if vowel
                        if (count > 0)
                        {
                            switch (guess) //if vowel negative score
                            {
                                case 'a':
                                case 'e':
                                case 'i':
                                case 'o':
                                case 'u':
                                    moneyGain = count * -1 * wheelSelect;
                                    break;

                                default:
                                    moneyGain = count * wheelSelect;
                                    break;
                            }

                            //add the calc money to user money total
                            money = money + moneyGain;

                            //tell user they got it right and how much they win/lose
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"CONGRATULATIONS! {guess.ToString().ToUpper()} appeared {count} number of times");
                            if (moneyGain > 0)
                            {
                                Console.WriteLine($"You win ${moneyGain}\n");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else //moneyGain is < 0
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("You lose $" + Math.Abs(moneyGain) + "\n");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }



                        //-----------------------check if user got all letters-----------------------\\
                        //checking if guessWord == correctWord
                        for (count = 0; count < guessWord.Length; count++)
                        {
                            if (guessWord[count] != correctWord[count])
                            {
                                count = 33;  //something large to kick out of loop
                            }
                        }
                        if (count == correctWord.Length)
                        {
                            gotIt = true;
                        }
                        Console.WriteLine("is word fully guessed: " + gotIt);
                    }

                } while ((gotIt == false) && (giveUp == false));

                if (gotIt == true)
                {

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(@"  ______                                                     __                __ 
 /      \                                                   |  \              |  \
|  $$$$$$\  ______   _______    ______    ______   ______  _| $$_     _______ | $$
| $$   \$$ /      \ |       \  /      \  /      \ |      \|   $$ \   /       \| $$
| $$      |  $$$$$$\| $$$$$$$\|  $$$$$$\|  $$$$$$\ \$$$$$$\\$$$$$$  |  $$$$$$$| $$
| $$   __ | $$  | $$| $$  | $$| $$  | $$| $$   \$$/      $$ | $$ __  \$$    \  \$$
| $$__/  \| $$__/ $$| $$  | $$| $$__| $$| $$     |  $$$$$$$ | $$|  \ _\$$$$$$\ __ 
 \$$    $$ \$$    $$| $$  | $$ \$$    $$| $$      \$$    $$  \$$  $$|       $$|  \
  \$$$$$$   \$$$$$$  \$$   \$$ _\$$$$$$$ \$$       \$$$$$$$   \$$$$  \$$$$$$$  \$$
                              |  \__| $$                                          
                               \$$    $$                                          
                                \$$$$$$ ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"\n\nYou have correctly figured out the word {correctWord}");
                    Console.WriteLine($"\n\nYou, the player called {player.firstName} {player.lastName}, will walking away today with ${money}");
                    if (money < 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\n\n\nWe expect you to repay your debt in 1 to 2 working days, thank you");
                        Console.ForegroundColor = ConsoleColor.White;
                    }



                    //-----------------------updating txt doc with scores-----------------------\\
                    for (int i = 0; i < students.Length; i++) //finding ith place of student
                    {
                        if (students[i].lastName.Contains(player.lastName)) //lastnames are all unique
                        {
                            students[i].score = money;
                        }
                    }
                }


                else if (giveUp == true)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(@" __   __  _______  __   __    ___      _______  _______  _______ 
|  | |  ||       ||  | |  |  |   |    |       ||       ||       |
|  |_|  ||   _   ||  | |  |  |   |    |   _   ||  _____||    ___|
|       ||  | |  ||  |_|  |  |   |    |  | |  || |_____ |   |___ 
|_     _||  |_|  ||       |  |   |___ |  |_|  ||_____  ||    ___|
  |   |  |       ||       |  |       ||       | _____| ||   |___ 
  |___|  |_______||_______|  |_______||_______||_______||_______|");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"\n\nBetter luck next time. \n\n\nThe word you were trying to guess is ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(correctWord);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                //----------show user top 5 scores----------\\
                //reorder to score descending
                for (int i = 0; i < students.Length - 1; i++)
                {
                    for (int pos = 0; pos < students.Length - 1; pos++)
                    {
                        if (students[pos + 1].score > students[pos].score)
                        {
                            temp2 = students[pos + 1];
                            students[pos + 1] = students[pos];
                            students[pos] = temp2;
                        }
                    }
                }

                //show top 5 player scores
                Console.WriteLine("\n\n\nTop 5 scores of all time are shown below:\n");
                Console.WriteLine("First Name".PadRight(18) + "Last Name".PadRight(16) + "Interest".PadRight(21) + "Score");
                Console.WriteLine(new string('-', 60));
                for (int i = 0; i < 5; i++) //loop only 5 times, not show all in students[]
                {
                    Console.Write(students[i].firstName.PadRight(18));
                    Console.Write(students[i].lastName.PadRight(16));
                    Console.Write(students[i].interest.PadRight(21));
                    Console.WriteLine(students[i].score.ToString().PadLeft(5));
                }
            }

            //rewite all students back onto txt file to save changes
            writeTxt();

            //reset values
            temp = ""; //fix for menu
            playerSet = false; //force new player

            //exit to menu when user wants
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\n\nPress ENTER to return to the menu ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadLine();
        }
        public static int wheelSpin()
        {
            //list local varibles
            int[] wheel = { 300, 400, 500, 600, 700, 800, 900, 1000, 1100, 1200, 1300, 1400, 1500, 1600, 1800, 2000, 2250, 2500, 2750, 3000, 4000, 5000, -2000, -3000 };
            int selected;

            //user spins wheel to get random num from wheel[]
            Console.WriteLine("Please hit ENTER to spin the wheel");
            Console.ReadLine();
            selected = wheel[rand.Next(wheel.Length)];

            //make funny wheel spin noise that slows
            for (int i = 0; i < 10; i++)
            {
                Console.Write("tr");
                Thread.Sleep(i * 50);
            }
            for (int i = 0; i < 4; i++)
            {
                Console.Write("tick.");
                Console.Write(new string('.', i));
                Thread.Sleep(i * 200 + 400);
            }
            Thread.Sleep(1500);
            Console.Write("tick.....");

            //pause then clear and show user result
            Thread.Sleep(2000);
            Console.Clear();
            Console.Write($"You have spun");
            if (selected > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else //selected == negative
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine($" ${selected}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("You will gain this amount of money PER letter guessed correctly \nBut it will cost you this much money to buy EACH vowel");

            return selected;
        }
    }
}