﻿namespace NotLookingForwardToThisExam
{
    internal class ExamRevision
    {
        static int[] sections = new int[] { 1 };
        // static int[] sections = new int[] { 1, 2, 3, 4, 5, 6 };
        static string[] questions = new string[0];
        static string[] answers = new string[0];

        static void Main(string[] args)
        {
            OpenMenu();
        }

        public static void OpenMenu()
        {
            int key;
            string userInput;
            do
            {
                Console.Clear();
                Header("menu");
                ShowQuestionSelect("display");
                Console.WriteLine();
                Highlight("\n^c^[1] ^w^Play");
                Highlight("\n^c^[2] ^w^Question Select");
                Highlight("\n^r^[0] ^w^Exit");
                Highlight("\n\n\n>>> ");

                userInput = ReadLower();
                if (!Int32.TryParse(userInput, out key)) // If the input can't be parsed to an integer, key will be zero.
                {
                    key = -1; // If userInput is not a number, set key to -1 (or any number that is not valid option)
                }
                switch (key)
                {
                    default:
                        Console.Clear();
                        Console.WriteLine("invalid selection!  try again in 1 sec");
                        Thread.Sleep(1000);
                        break;
                    case 0:
                        Console.WriteLine("\n\nClosing menu! :D byeeee");
                        Thread.Sleep(1000);
                        Console.Clear();
                        break;
                    case 1:
                        if (sections.Length == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("No question set selected! Please select a question set first.");
                            Thread.Sleep(2000);
                        }
                        else
                        {
                            GetQuestions(sections);
                            Play();
                        }
                        break;
                    case 2:
                        QuestionSelect();
                        break;
                }
            } while (key != 0);
        }


        static void QuestionSelect()
        {
            int key;
            string userInput;
            do
            {
                Console.Clear();
                Header("qselect");
                ShowQuestionSelect("search");
                Console.WriteLine("\n");
                Highlight("\n^c^[1-6] ^w^Toggle questions");
                Highlight("\n^r^[0] ^w^Back to menu");
                Highlight("\n\n\n>>>  ^c^");

                userInput = ReadLower();
                if (!Int32.TryParse(userInput, out key))
                {
                    key = -1;
                }
                if (sections.Contains(key))
                {
                    List<int> tempSections = new List<int>(sections);
                    tempSections.Remove(key);
                    sections = tempSections.ToArray();
                }
                else if (key > 0 && key <= 6)  // Ensuring the input is within the valid range
                {
                    Array.Resize(ref sections, sections.Length + 1);
                    sections[sections.Length - 1] = key;
                }
            } while (key != 0);
        }


        static void ShowQuestionSelect(string mode)
        {
            Highlight("^w^Study questions: ");
            if (mode == "search")
            {   // displays in question select screen
                for (int i = 1; i <= 6; i++)
                {
                    if (sections.Contains(i)) Highlight($"c^ [{i}] ");
                    else Highlight($"g^ [{i}] ");
                    //if (i != 6) Console.Write(",");
                }
            }
            else if (mode == "display")
            {   // displays in main menu
                switch (sections.Length)
                {
                    case 0:
                        Highlight("^g^none");
                        break;
                    default:
                        foreach (int i in sections)
                        {
                            Console.Write($" {i}");
                            if (Array.IndexOf(sections, i) != sections.Length - 1) Console.Write(", ");
                        }
                        break;
                }
            }
            Highlight("w");
        }

        static void GetQuestions(int[] sections)
        {
            string line;
            string fileName;
            Array.Resize(ref questions, 0);
            Array.Resize(ref answers, 0);
            foreach (int section in sections)
            {
                fileName = GetFileName(section);
                StreamReader r = new StreamReader(fileName);
                bool question = false;
                bool answer = false;
                while (!r.EndOfStream)
                {
                    line = r.ReadLine().Trim();
                    switch (line)
                    {
                        case null:
                            break;
                        case "Q.":
                            question = true;
                            answer = false;
                            Array.Resize(ref questions, questions.Length + 1);
                            break;
                        case "A.":
                            answer = true;
                            question = false;
                            Array.Resize(ref answers, answers.Length + 1);
                            break;
                        default:
                            if (!line.StartsWith("//"))
                            {
                                if (line.Contains("//")) line = line.Substring(0, line.IndexOf("//"));
                                if (question)
                                {
                                    questions[questions.Length - 1] += line + "\n";
                                }
                                else if (answer)
                                {
                                    answers[answers.Length - 1] += line + "\n";
                                }
                            }
                            break;
                    }
                }
            }
        }

        static void Play()
        {
            Console.WriteLine($"DEBUG: there are {questions.Length} questions");
            int[] order = new int[questions.Length];
            for (int i = 0; i < order.Length; i++) order[i] = i;
            int temp;
            int index1;
            int index2;
            Random rand = new Random();
            for (int i = 0; i < order.Length; i++)
            {
                index1 = rand.Next(order.Length);
                do
                {
                    index2 = rand.Next(order.Length);
                } while (index1 == index2);
                temp = order[index1];
                order[index1] = order[index2];
                order[index2] = temp;
            }
            foreach (int i in order)
            {
                Console.Clear();
                Highlight("^m^Question:^w^\n" + questions[i]);
                Highlight("^g^\nPress ENTER to reveal answer, M for Main Menu >>>  ^l^");
                string userInput = Console.ReadLine();

                if (userInput.ToLower() == "m")
                {
                    return;
                }

                Highlight("^w^\n\n");
                Highlight("^s^Answer^w^\n" + answers[i]);
                Highlight("^g^Press ENTER to go to next, M for Main Menu >>>  ^l^");
                userInput = Console.ReadLine();

                if (userInput.ToLower() == "m")
                {
                    return;
                }

                Highlight("^w^\n");
            }
            if (questions.Length == 0)
            {
                Console.Clear();
                Highlight("^g^\n\n\n\n\n\t\t bruh");
                Thread.Sleep(3000);
            }
            else
            {
                Console.Clear();
                Highlight("\n\n\n\t^g^End of questions");
                Thread.Sleep(1000);
            }
        }


        static string GetFileName(int section)
        {
            string fileName;
            switch (section)
            {
                case 1:
                    fileName = @"../../../Questions1.txt";
                    break;
                case 2:
                    fileName = @"../../../Questions2.txt";
                    break;
                case 3:
                    fileName = @"../../../Questions3.txt";
                    break;
                case 4:
                    fileName = @"../../../Questions4.txt";
                    break;
                case 5:
                    fileName = @"../../../Questions5.txt";
                    break;
                case 6:
                    fileName = @"../../../Questions6.txt";
                    break;
                default:
                    return null;
            }
            return fileName;
        }

        public static void Highlight(string text)
        {
            string[] split = text.Split('^');
            foreach (string s in split)
            {
                switch (s)
                {
                    case "b":
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    case "c":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        break;
                    case "g":
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        break;
                    case "l":
                        Console.ForegroundColor = ConsoleColor.Black;
                        break;
                    case "m":
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        break;
                    case "r":
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case "s":
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case "w":
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case "y":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    default:
                        Console.Write(s);
                        break;
                }
            }
        }

        static void Header(string screen)
        {
            switch (screen)
            {
                case "menu":
                    Highlight("\n  ^b^█▀█ █░░ ▄▀█ ▀█▀ █▀▀ █▀█ █▀█ █▀▄▀█   █▀█ ▄▀█ █^r^░^b^█ ^^^█░░ ▀ ^r^█▀   █▀█ █▀▀ █▀█ █ █░░ █▀█ █░█ █▀   █▀█ ▄▀█ █▀█ █▀▀ █▀█");
                    Highlight("\n  ^b^█▀▀ █▄▄ █▀█ ░█░ █▀░ █▄█ █▀▄ █░▀░█   █▀▀ █▀█ █^^^▄^^^█ ^r^█▄▄ ░ ^^^▄█   █▀▀ ██▄ █▀▄ █ █▄▄ █▄█ █▄█ ▄█   █▀▀ █▀█ █▀▀ ██▄ █▀▄");
                    Highlight("^w^\n\n\n");
                    break;
                case "qselect":
                    Highlight("\n  ^b^█▀█ █░█ █▀▀ █▀ ▀█▀ █ █▀█ █^^^▄^^^░^^^█   █^r^▀ █▀▀ █░░ █▀▀ █▀▀ ▀█▀");
                    Highlight("\n  ^b^▀▀█ █▄█ ██▄ ▄█ ░█░ █ █▄█ █^r^░^b^▀^r^█   ▄^^^█ ██▄ █▄▄ ██▄ █▄▄ ░█");
                    Highlight("\n\n  ^w^Question set 1 = Chapter 1 & 2");
                    Highlight("\n  ^w^Question set 2 = Chapter 3 & 4");
                    Highlight("\n  ^w^Question set 3 = Chapter 4 & 5");
                    Highlight("\n  ^w^Question set 4 = Chapter 7 & 8");
                    Highlight("\n  ^w^Question set 5 = Chapter 9, 10 & 11");
                    Highlight("\n  ^w^Question set 6 = Chapter 12, 13 & 14\n\n\n");

                    break;
            }
        }

        static string ReadLower()
        {
            /* this exists so we don't have to write
             * .Trim().ToLower() every time
             * we take user input
             */
            string output;
            output = Console.ReadLine().Trim().ToLower();
            return output;
        }
    }
}