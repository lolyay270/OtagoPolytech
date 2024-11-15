using System.Collections.Concurrent;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Game
{
    internal class Program
    {
        static Random rand = new Random();
        static int[] roomRowA = { 0, 0, 0, 0, 0 };
        static int[] roomRowB = { 0, 0, 0, 0, 0 };
        static int[] roomRowC = { 0, 0, 0, 0, 0 };
        static int[] roomRowD = { 0, 0, 0, 0, 0 };
        static int[] roomRowE = { 0, 0, 0, 0, 0 };

        /*
         0 = Main path & new game room
         1 = Treasure key room
         2 = More Oxygen room
         3 = Trap room
         4 = Flashlight/lantern room
         */

        public static int oxygen = 100;
        public static string user; //all user inputs so we don't make tons of variables
        public static int gold = 0; //total gold counter
        public static int treasureChestOpen = 0; //chest in a2
        public static bool keyGet = false;
        public static bool hasLight = false;
        public static bool roomLocked = true;
        public static bool secretTreasureTaken = false;
        public static bool mainTreasureTaken = false;
        public static bool switchFlipped = false;
        public static bool roomEntered = false; //stop boss fight repeating

        static void Main(string[] args)
        {
            getRoomType();

            string menuSelect;

            do
            {
                Console.Clear();

                Console.WriteLine("1  Start New Game \n" +
                    "2  Rules/Tutorial\n" +
                    "0  Exit Game");
                menuSelect = Console.ReadLine();

                Console.Clear();

                switch (menuSelect)
                {
                    case "1":
                        Console.WriteLine("Welcome! You are a diver looking for gold on a sunken pirate ship that you have found at the bottom of the sea." +
                "\nExplore rooms in the ship but be careful not to run out of oxygen and beware of Captain Boblin.");
                        Console.WriteLine("Press ENTER to continue"); //pause so user can read intro and clear so user doesn't read intro again
                        Console.ReadLine();
                        Console.Clear();
                        RoomA1();
                        break;

                    case "2":
                        Help();
                        break;

                    case "0":
                        Console.WriteLine("Exiting game!");
                        Environment.Exit(0);
                        break;

                    default:
                        break;

                    //temporary to get to new rooms easy
                    case "move":
                        //switchFlipped = true;
                        CultRoom();
                        break;
                }


            } while (menuSelect != "0");

            // Debug Console.WriteLine's to check the random room generation
            /*foreach (int room in roomRowA)
            {
                Console.WriteLine(room);
            }

            foreach (int room in roomRowB)
            {
                Console.WriteLine(room);
            }

            foreach (int room in roomRowC)
            {
                Console.WriteLine(room);
            }

            foreach (int room in roomRowD)
            {
                Console.WriteLine(room);
            }

            foreach (int room in roomRowE)
            {
                Console.WriteLine(room);
            }*/


        }

        static void printBaseRoomMessage(int roomType,int oxy)
        {
            switch (roomType)
            {
                case 0:
                    Console.WriteLine("The ocean is still and a few fish swim by.\n"); //nothing happens
                    break;
                case 1:
                    Console.WriteLine("A something tiny glints under some seaweed.\n");   //key room
                    break;
                case 2:
                    Console.WriteLine("A stream of bubbles are floating from a strange plant.\n" +
                        "You think quickly and connect the fill pipe of your scuba tank to the bubbles.\n" +  //more oxygen
                        $"Your oxygen meter has been filled with {oxy}% more oxygen.");
                    break;
                case 3:
                    Console.WriteLine("A large mine chained to the ocean floor has a red blinking light.\n" +   //trap room
                        "I wouldn't get too close if I were you.\n");
                    break;
            }
        }

        static void darkRoomMessage(bool enterRoom)
        {
            if (enterRoom)
            {
                Console.WriteLine("You enter a dark room, you can't see anything.\n");
                return;
            }

            Console.WriteLine("There is nothing to see.\nPerhaps there was a light lying around here somewhere...\n");
        }

        static int alterOxygen(int amountO2)
        {
            int newO2 = oxygen + amountO2;

            switch (newO2)
            {
                case <= 0:
                    Console.WriteLine("You ran out of oxygen and drowned!");
                    Thread.Sleep(2000);
                    DeathRoom();
                    break;
                case < 20:
                    Console.WriteLine("!!! O2 Levels Low !!!");
                    break;
                case > 100:
                    newO2 = 100;
                    break;
                default:
                    break;
            }

            /*if (newO2 > 100) 
            {
                newO2 = 100;
            }

            if (newO2 < 20)
            {
                Console.WriteLine("!!! O2 Levels Low !!!");
            }

            if (newO2 <= 0)
            {
                Console.WriteLine("You ran out of oxygen and drowned!");
                Thread.Sleep(2000);
                DeathRoom();
            }*/

            Console.WriteLine($"O2 Level: {newO2}%");

            return newO2;
        }

        static void DeathRoom()
        {
            Console.Clear();
            Thread.Sleep(1000);

            for (int i = 0; i < 3; i++)
            {
                Console.Write(" . ");
                Thread.Sleep(1000);
            }

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Red;

            Thread.Sleep(1000);
            Console.WriteLine("\n\n\n\n\t\tYou died.\n\n\n");
            Thread.Sleep(3000);

            Console.ForegroundColor = ConsoleColor.White;
            Environment.Exit(0);
        }

        static void getRoomType()
        {
            //random gen 0-9 to get the chances for each type of room
            int[] ranNums = new int[8];
            for (int i = 0; i < ranNums.Length; i++)
            {
                //set the room to the correct code
                ranNums[i] = rand.Next(9);
                switch (ranNums[i])
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        ranNums[i] = 0; //nothing happens
                        break;

                    case 4:
                    case 5:
                        ranNums[i] = 1; //key room
                        break;

                    case 6:
                    case 7:
                        ranNums[i] = 2; //more oxygen
                        break;

                    case 8:
                        ranNums[i] = 3; //trap
                        break;
                }
            }

            //if there are no keys force a room to have key
            if (ranNums.Contains(4) == false)
            {
                int num = rand.Next(8);
                ranNums[num] = 1;
            }

            for (int i = 0; i < ranNums.Length; i++)
            {
                //a3, a4, a5, b1, b4, b5, c4, c5 are all the random rooms

                roomRowA[2] = ranNums[0]; //a3
                roomRowA[3] = ranNums[1]; //a4
                roomRowA[4] = ranNums[2]; //a5
                roomRowB[0] = ranNums[3]; //b1
                roomRowB[3] = ranNums[4]; //b4
                roomRowB[4] = ranNums[5]; //b5
                roomRowC[3] = ranNums[6]; //c4
                roomRowC[4] = ranNums[7]; //c5
            }
        }

        static void getGold(int goldAdd)
        {
            gold += goldAdd;
            return;
        }

        static void restart()
        {
            // Resetting the variables to their default state
            oxygen = 100;
            user = string.Empty;
            gold = 0;
            treasureChestOpen = 0;
            keyGet = false;
            hasLight = false;
            roomLocked = true;
            secretTreasureTaken = false;
            mainTreasureTaken = false;
            switchFlipped = false;
            roomEntered = false;

            // Monologue
            Console.Clear();
            Thread.Sleep(1000);

            Console.WriteLine("You unzip your backpack and start frantically searching for something as if your life depended on it.");
            Thread.Sleep(2000);
            Console.WriteLine("Suddenly you feel something eerily familiar within your grasp..");
            Thread.Sleep(2000);
            Console.WriteLine("Almost poetically, in your moment of weakness hope had taken form and appeared right in front of you.");
            Thread.Sleep(2000);
            Console.WriteLine("A small tear runs down your cheek as you slowly look down to gaze into their eyes one final time.");
            Thread.Sleep(4000);

            for (int i = 0; i < 3; i++)
            {
                Console.Write(". ");
                Thread.Sleep(1000);
            }

            Console.Clear();

            Thread.Sleep(2000);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\n         B A N G !       \n\n");
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(2000);
        }


        static void Help()    //if users ask for help
        {
            Console.ForegroundColor = ConsoleColor.Blue;  //making it blue to stand out
            Console.WriteLine("You must try to get as much treasure as possible and escape without loosing your life.\n" +
                "You can die in many ways like running out of oxygen, being lost forever, traps etc.\n" +
                "The escape boat is very close to where you started your exploration.\n\n" +
                "To navigate through the ocean and sunken ship use directions like NORTH, SOUTH, EAST, WEST.\n" +
                "or simply use the first letter of each direction as a shorthand.\n\n" +
                "Remember to search around for treasure using LOOK and picking stuff up with TAKE.\n" +
                "\nIf all else fails, as a last resort you can always type RESTART to return to the menu.\n");
            Console.WriteLine("Press Enter to continue:");
            Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
        }




        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------\\
        // Main rooms
        static void RoomA1()            //perma lit by sunlight        made by Jenna
        {


            //      lore / description of location / hint of where to go
            Console.WriteLine("You are in a shallow waterway. A couple colorful fish swim by and a baby ray cruises the surface of the sand.\n" +
                "The sunlight shines through the wobbly surface and casts dancing patterns on the oceans floor.\n" +
                "The only thing to note is the small sunken boat to the north.\n");
            //after completing more than false exit this will be added to the lore above             the boat to leave is to the west of you now 


            //user chooses what to do in the room in loop so choose multiple things and game doesn't end
            //making a variable for when the user finishes the game it doesn't stay in the loop forever (instead of while true loop)
            bool move = false;
            while (move == false)
            {
                Console.Write(">>   ");
                user = Console.ReadLine();
                user = user.ToLower(); //making it all lower case so we don't have to check for capital letters
                switch (user)
                {
                    case "n":
                    case "north":
                        oxygen = alterOxygen(-4);
                        move = true;
                        RoomA2();
                        break;

                    case "east":
                    case "e":
                        oxygen = alterOxygen(-4);
                        move = true;
                        RoomB1();
                        break;

                    case "south":
                    case "west":
                    case "s":
                    case "w":
                        oxygen = alterOxygen(-1);
                        Console.WriteLine("You cannot move that way\n");
                        break;

                    case "help":
                        Help();
                        break;

                    case "gold":
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Your Gold: {gold}\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;

                    case "look":
                    case "take":
                        Console.WriteLine("There is nothing in this area to pick up\n");
                        break;

                    case "restart":
                        restart();
                        return;

                    default:
                        Console.WriteLine("That is an invalid input. Type HELP if you need help\n");
                        break;
                }

            }

        }

        static void RoomA2()
        {
            //lit in water

            //Room Description     ---     Made by Taylah
            Console.WriteLine($"Ahead is small sunken rowboat, with two goblin skeletons, one clutching a small chest. \n In the distance to the east is a large shadow.\n");

            //Movement 
            bool move = false;
            while (move == false)
            {
                Console.Write(">>   ");
                string user = Console.ReadLine();
                user = user.ToLower(); //making it all lower case so we don't have to check for capital letters
                switch (user)
                {
                    case "e":
                    case "east":
                        oxygen = alterOxygen(-4);
                        RoomB2();
                        move = true;
                        break;
                    case "n":
                    case "north":
                        oxygen = alterOxygen(-4);
                        RoomA3();
                        move = true;
                        break;

                    case "s":
                    case "south":
                        oxygen = alterOxygen(-4);
                        RoomA1();
                        move = true;
                        break;

                    case "w":
                    case "west":
                        if (gold <= 0)
                        {
                            string temp;
                            Console.WriteLine($"Are you sure you want to leave?\nYou only have {gold}\n gold.\n");
                            Console.Write(">>   ");
                            temp = Console.ReadLine().ToLower();
                            switch (temp)
                            {
                                case "yes":
                                case "y":
                                    //FalseEndingRoom
                                    FalseEndingRoom();
                                    move = true;
                                    break;
                                default:
                                    Console.WriteLine("You have remained in the room for now.\n");
                                    break;
                            }
                        }
                        else
                        {
                            FalseEndingRoom();
                            move = true;
                        }
                        break;

                    case "help":
                        Help();
                        break;

                    case "gold":
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Your Gold: {gold}\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;

                    case "open":
                    case "view":
                        if (treasureChestOpen != 0)
                        {
                            Console.WriteLine("Chest has already been opened!\n");
                        }
                        else
                        {
                            Console.WriteLine("You opened the chest.\n");
                            treasureChestOpen = 1;
                        }
                        break;

                    case "look":
                        Console.WriteLine("You see the skeleton of a once lively Goblin clutching a chest as if it were their beloved.\n");
                        break;
                    case "take":
                        switch (treasureChestOpen)
                        {
                            case 0:
                                Console.WriteLine("You can't do that. The chest is closed.\n");
                                break;
                            case 1:
                                Console.WriteLine("You find and take 10 gold. Yay!\n");
                                getGold(10);
                                treasureChestOpen = 2;
                                break;
                            case 2:
                                Console.WriteLine("The gold has already been snatched!\n");
                                break;
                        }
                        break;

                    case "restart":
                        restart();
                        return;

                    default:
                        Console.WriteLine("That is an invalid input. Type HELP if you need help\n");
                        break;
                }
            }
        }

        static void RoomB2()
        {

            //      lore / description of location / hint of where to go
            Console.WriteLine("The water is magically clear here. It allows you to see most of the areas around you.\n" +
                "A reef can be seen to the north, and the sunken rowboat to the west.\n" +
                "To the east is a giant rock that you can’t swim around.\n" +
                "But south you can’t see what’s there because the sand has been stirred up.\n");
            //after completing more than false exit this will be added to the lore above             the boat to leave is to the west of you now 



            //user chooses what to do in the room in loop so choose multiple things and game doesn't end
            //making a variable for when the user finishes the game it doesn't stay in the loop forever (instead of while true loop)
            bool move = false;
            while (move == false)
            {
                Console.Write(">>   ");
                user = Console.ReadLine();
                user = user.ToLower(); //making it all lower case so we don't have to check for capital letters
                switch (user)
                {
                    case "west":
                    case "w":
                        oxygen = alterOxygen(-4);
                        move = true;
                        RoomA2();
                        break;

                    case "n":
                    case "north":
                        oxygen = alterOxygen(-4);
                        move = true;
                        RoomB3();
                        break;

                    case "south":
                    case "s":
                        oxygen = alterOxygen(-4);
                        move = true;
                        RoomB1();
                        break;

                    case "east":
                    case "e":
                        oxygen = alterOxygen(-1);
                        Console.WriteLine("You cannot move that way\n");
                        break;

                    case "help":
                        Help();
                        break;

                    case "gold":
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Your Gold: {gold}\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;

                    case "look":
                    case "take":
                        Console.WriteLine("There is nothing in this area to pick up\n");
                        break;

                    case "restart":
                        restart();
                        return;

                    default:
                        Console.WriteLine("That is an invalid input. Type HELP if you need help\n");
                        break;
                }

            }
        }

        static void RoomB3()
        {
            //      lore / description of location / hint of where to go
            Console.WriteLine("There's a small reef. There's a school of beautiful fire orange fish\n" +
                "with the purest neon blue spots, darting around the reef, probably in a feeding frenzy.\n" +
                "The little light that breaks through the water bounces off their bodies almost like diamonds in the sun.\n" +
                "There’s a sudden dull flicker of light to the east.\n");
            //after completing more than false exit this will be added to the lore above             the boat to leave is to the west of you now 


            //user chooses what to do in the room in loop so choose multiple things and game doesn't end
            //making a variable for when the user finishes the game it doesn't stay in the loop forever (instead of while true loop)
            bool move = false;
            while (move == false)
            {
                Console.Write(">>   ");
                user = Console.ReadLine();
                user = user.ToLower(); //making it all lower case so we don't have to check for capital letters
                switch (user)
                {
                    case "s":
                    case "south":
                        oxygen = alterOxygen(-4);
                        move = true;
                        RoomB2();
                        break;

                    case "east":
                    case "e":
                        oxygen = alterOxygen(-4);
                        move = true;
                        RoomC3();
                        break;

                    case "north":
                    case "n":
                        oxygen = alterOxygen(-4);
                        move = true;
                        RoomB4();
                        break;
                    case "west":
                    case "w":
                        oxygen = alterOxygen(-1);
                        Console.WriteLine("You cannot move that way\n");
                        break;

                    case "help":
                        Help();
                        break;

                    case "gold":
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Your Gold: {gold}\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;

                    case "look":
                    case "take":
                        Console.WriteLine("There is nothing in this area to pick up\n");
                        break;

                    case "restart":
                        restart();
                        return;

                    default:
                        Console.WriteLine("That is an invalid input. Type HELP if you need help\n");
                        break;
                }

            }
        }

        static void RoomC3()
        {
            //      lore / description of location / hint of where to go
            Console.WriteLine("You have entered a ship. As you enter the side of the hull, you can see several skeletons, a cannon is broken standing upright against the opposite wall " +
                "\na faint light source is coming from under the cannon, as you approach the light source, " +
                "\nyou see a skeleton of a mage, carrying a mana stone, which can be used as a light source.\n");

            //after completing more than false exit this will be added to the lore above             the boat to leave is to the west of you now 


            //user chooses what to do in the room in loop so choose multiple things and game doesn't end
            //making a variable for when the user finishes the game it doesn't stay in the loop forever (instead of while true loop)
            bool move = false;
            while (move == false)
            {
                Console.Write(">>   ");
                user = Console.ReadLine();
                user = user.ToLower(); //making it all lower case so we don't have to check for capital letters
                switch (user)
                {
                    case "s":
                    case "south":
                        oxygen = alterOxygen(-4);
                        move = true;
                        RoomC2();
                        break;

                    case "west":
                    case "w":
                        oxygen = alterOxygen(-4);
                        move = true;
                        RoomB3();
                        break;

                    case "north":
                    case "east":
                    case "n":
                    case "e":
                        oxygen = alterOxygen(-1);
                        Console.WriteLine("You cannot move that way\n");
                        break;

                    case "help":
                        Help();
                        break;

                    case "gold":
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Your Gold: {gold}\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;

                    case "look":
                    case "take":
                        if (hasLight == false)
                        {
                            Console.WriteLine("you take the Light source\n");
                            hasLight = true;
                        }
                        else
                        {
                            Console.WriteLine("you already have the light source.\n");
                        }
                        break;

                    case "restart":
                        restart();
                        return;

                    default:
                        Console.WriteLine("That is an invalid input. Type HELP if you need help\n");
                        break;
                }

            }
        }

        static void RoomC2()
        {
            //      lore / description of location / hint of where to go
            if (hasLight)
            {
                Console.WriteLine("A pile of crates sits in the corner of the room.\n" +
                "Broken and smashed causing the contents to spill out, littering the room with broken glass.\n" +
                "There seems to be a hole in a wall to the south of you. Do you think you can fit through?\n");
            }
            else
            {
                darkRoomMessage(true);
            }
            //after completing more than false exit this will be added to the lore above             the boat to leave is to the west of you now 



            //user chooses what to do in the room in loop so choose multiple things and game doesn't end
            //making a variable for when the user finishes the game it doesn't stay in the loop forever (instead of while true loop)
            bool move = false;
            while (move == false)
            {
                Console.Write(">>   ");
                user = Console.ReadLine();
                user = user.ToLower(); //making it all lower case so we don't have to check for capital letters

                if (hasLight)
                {
                    switch (user)
                    {
                        case "s":
                        case "south":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomC1();
                            break;

                        case "north":
                        case "n":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomC3();
                            break;

                        case "east":
                        case "e":
                        case "west":
                        case "w":
                            oxygen = alterOxygen(-1);
                            Console.WriteLine("You cannot move that way\n");
                            break;

                        case "help":
                            Help();
                            break;

                        case "gold":
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"Your Gold: {gold}\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        case "look":
                        case "take":
                            Console.WriteLine("There is nothing in this area to pick up\n");
                            break;

                        case "restart":
                            restart();
                            return;

                        default:
                            Console.WriteLine("That is an invalid input. Type HELP if you need help\n");
                            break;
                    }

                }
                else
                {
                    switch (user)
                    {
                        case "s":
                        case "south":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomC1();
                            break;

                        case "north":
                        case "n":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomC3();
                            break;

                        case "east":
                        case "e":
                        case "west":
                        case "w":
                            oxygen = alterOxygen(-1);
                            Console.WriteLine("You cannot move that way\n");
                            break;

                        case "help":
                            Help();
                            break;

                        case "gold":
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"Your Gold: {gold}\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        case "restart":
                            restart();
                            return;

                        default:
                            darkRoomMessage(false);
                            break;
                    }
                }

            }
        }

        static void RoomC1()
        {
            //      lore / description of location / hint of where to go
            if (hasLight)
            {
                Console.WriteLine("You've entered a room were broken planks allow rays of sunlight to dance amidst the wreckage.\n" +
                    "Remnants of barrels and crates litter the room, their contents long gone. A weathered desk with scattered parchment and \n" +
                    "a rusted compass stands against a wall.\n" +
                    "A partially collapsed doorway leads to the east, whispering secrets beckoning exploration.\n");
            }
            else
            {
                darkRoomMessage(true);
            }
            //after completing more than false exit this will be added to the lore above             the boat to leave is to the west of you now 


            //user chooses what to do in the room in loop so choose multiple things and game doesn't end
            //making a variable for when the user finishes the game it doesn't stay in the loop forever (instead of while true loop)
            bool move = false;
            while (move == false)
            {
                Console.Write(">>   ");
                user = Console.ReadLine();
                user = user.ToLower(); //making it all lower case so we don't have to check for capital letters

                if (hasLight)
                {
                    switch (user)
                    {
                        case "east":
                        case "e":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomD1();
                            break;

                        case "north":
                        case "n":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomC2();
                            break;

                        case "s":
                        case "south":
                        case "west":
                        case "w":
                            oxygen = alterOxygen(-1);
                            Console.WriteLine("You cannot move that way\n");
                            break;

                        case "help":
                            Help();
                            break;

                        case "gold":
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"Your Gold: {gold}\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        case "look":
                        case "take":
                            Console.WriteLine("There is nothing in this area to pick up\n");
                            break;

                        case "restart":
                            restart();
                            return;

                        default:
                            Console.WriteLine("That is an invalid input. Type HELP if you need help\n");
                            break;
                    }
                }
                else
                {
                    switch (user)
                    {
                        case "east":
                        case "e":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomD1();
                            break;

                        case "north":
                        case "n":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomC2();
                            break;

                        case "s":
                        case "south":
                        case "west":
                        case "w":
                            oxygen = alterOxygen(-1);
                            Console.WriteLine("You cannot move that way\n");
                            break;

                        case "help":
                            Help();
                            break;

                        case "gold":
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"Your Gold: {gold}\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        case "restart":
                            restart();
                            return;

                        default:
                            darkRoomMessage(false);
                            break;
                    }
                }


            }
        }

        static void RoomD1()
        {
            //      lore / description of location / hint of where to go
            if (hasLight)
            {
                Console.WriteLine("Empty rum and mead bottles litter the floor.\n" +
                                    "Cooking pots and utensils are stacked precariously in a pile.\n" +
                                    "There seems to be a wall to the north of you.\n");
            }
            else
            {
                darkRoomMessage(true);
            }

            //after completing more than false exit this will be added to the lore above             the boat to leave is to the west of you now 


            //user chooses what to do in the room in loop so choose multiple things and game doesn't end
            //making a variable for when the user finishes the game it doesn't stay in the loop forever (instead of while true loop)
            bool move = false;
            while (move == false)
            {
                Console.Write(">>   ");
                user = Console.ReadLine();
                user = user.ToLower(); //making it all lower case so we don't have to check for capital letters
                if (hasLight)
                {
                    switch (user)
                    {
                        case "east":
                        case "e":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomE1();
                            break;

                        case "west":
                        case "w":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomC1();
                            break;

                        case "s":
                        case "south":
                        case "north":
                        case "n":
                            oxygen = alterOxygen(-1);
                            Console.WriteLine("You cannot move that way\n");
                            break;

                        case "help":
                            Help();
                            break;

                        case "gold":
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"Your Gold: {gold}\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        case "look":
                        case "take":
                            Console.WriteLine("There is nothing in this area to pick up\n");
                            break;

                        case "restart":
                            restart();
                            return;

                        default:
                            Console.WriteLine("That is an invalid input. Type HELP if you need help\n");
                            break;
                    }
                }
                else
                {
                    switch (user)
                    {
                        case "east":
                        case "e":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomE1();
                            break;

                        case "west":
                        case "w":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomC1();
                            break;

                        case "s":
                        case "south":
                        case "north":
                        case "n":
                            oxygen = alterOxygen(-1);
                            Console.WriteLine("You cannot move that way\n");
                            break;

                        case "help":
                            Help();
                            break;

                        case "gold":
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"Your Gold: {gold}\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        case "restart":
                            restart();
                            return;

                        default:
                            darkRoomMessage(false);
                            break;
                    }
                }

            }
        }

        static void RoomE1()
        {
            //      lore / description of location / hint of where to go
            if (hasLight)
            {
                Console.WriteLine("Small fish bones fill the floor, and you notice crabs darting around the room\n" +
                    "grabbing the little food they have left from the fish.\n" +
                    "There seems to be something blocking you from the east.\n");
            }
            else
            {
                darkRoomMessage(true);
            }
            //after completing more than false exit this will be added to the lore above             the boat to leave is to the west of you now 


            //user chooses what to do in the room in loop so choose multiple things and game doesn't end
            //making a variable for when the user finishes the game it doesn't stay in the loop forever (instead of while true loop)
            bool move = false;
            while (move == false)
            {
                Console.Write(">>   ");
                user = Console.ReadLine();
                user = user.ToLower(); //making it all lower case so we don't have to check for capital letters
                if (hasLight)
                {
                    switch (user)
                    {
                        case "west":
                        case "w":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomD1();
                            break;

                        case "north":
                        case "n":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomE2();
                            break;

                        case "east":
                        case "e":
                        case "s":
                        case "south":
                            oxygen = alterOxygen(-4);
                            Console.WriteLine("You cannot move that way\n");
                            break;

                        case "help":
                            Help();
                            break;

                        case "gold":
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"Your Gold: {gold}\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        case "look":
                        case "take":
                            Console.WriteLine("There is nothing in this area to pick up\n");
                            break;

                        case "restart":
                            restart();
                            return;

                        default:
                            Console.WriteLine("That is an invalid input. Type HELP if you need help\n");
                            break;
                    }
                }
                else
                {
                    switch (user)
                    {
                        case "west":
                        case "w":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomD1();
                            break;

                        case "north":
                        case "n":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomE2();
                            break;

                        case "east":
                        case "e":
                        case "s":
                        case "south":
                            oxygen = alterOxygen(-4);
                            Console.WriteLine("You cannot move that way\n");
                            break;

                        case "help":
                            Help();
                            break;

                        case "gold":
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"Your Gold: {gold}\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        case "restart":
                            restart();
                            return;

                        default:
                            darkRoomMessage(false);
                            break;
                    }
                }

            }
        }

        static void RoomE2()
        {

            //      lore / description of location / hint of where to go
            if (hasLight)
            {
                Console.WriteLine("You startle a fish and he hurriedly swims out a hole in the ship.\n" +
                    "To the west is a dark wooden door with a rusted old padlock.\n" +
                    "Maybe it could be unlocked somehow?\n");
            }
            else
            {
                darkRoomMessage(true);
            }
            //after completing more than false exit this will be added to the lore above             the boat to leave is to the west of you now 


            //user chooses what to do in the room in loop so choose multiple things and game doesn't end
            //making a variable for when the user finishes the game it doesn't stay in the loop forever (instead of while true loop)
            bool move = false;
            while (move == false)
            {
                Console.Write(">>   ");
                user = Console.ReadLine();
                user = user.ToLower(); //making it all lower case so we don't have to check for capital letters
                if (hasLight)
                {
                    switch (user)
                    {
                        case "s":
                        case "south":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomE1();
                            break;

                        case "north":
                        case "n":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomE3();
                            break;

                        case "west":
                        case "w":
                            if (!roomLocked)
                            {
                                move = true;
                                RoomD2();
                            }
                            else
                            {
                                Console.WriteLine("The door doesn't budge. Perhaps there was a key lying around here somewhere...\n");
                            }
                            break;

                        case "east":
                        case "e":
                            oxygen = alterOxygen(-1);
                            Console.WriteLine("You cannot move that way\n");
                            break;

                        case "help":
                            Help();
                            break;

                        case "gold":
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"Your Gold: {gold}\n\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        case "look":
                        case "take":
                            Console.WriteLine("There is nothing in this area to pick up\n");
                            break;

                        case "restart":
                            restart();
                            return;

                        default:
                            Console.WriteLine("That is an invalid input. Type HELP if you need help\n");
                            break;
                    }
                }
                else
                {
                    switch (user)
                    {
                        case "s":
                        case "south":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomE1();
                            break;

                        case "north":
                        case "n":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomE3();
                            break;

                        case "west":
                        case "w":
                            if (!roomLocked)
                            {
                                move = true;
                                RoomD2();
                            }
                            else
                            {
                                darkRoomMessage(false);
                            }

                            break;

                        case "east":
                        case "e":
                            oxygen = alterOxygen(-1);
                            Console.WriteLine("You cannot move that way\n");
                            break;

                        case "help":
                            Help();
                            break;

                        case "gold":
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"Your Gold: {gold}\n\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        case "restart":
                            restart();
                            return;

                        default:
                            darkRoomMessage(false);
                            break;
                    }
                }

            }
        }

        static void RoomD2()
        {
            //      lore / description of location / hint of where to go
            if (hasLight)
            {
                Console.WriteLine("Wow! Lucky you!\n" +
                    "You've discovered the Goblin Pirate's secret treasure!\n" +
                    "Tons upon tons of gold, gems and goodies that the whole family will love!\n");
            }
            else
            {
                darkRoomMessage(true);
            }

            //after completing more than false exit this will be added to the lore above             the boat to leave is to the west of you now 


            //user chooses what to do in the room in loop so choose multiple things and game doesn't end
            //making a variable for when the user finishes the game it doesn't stay in the loop forever (instead of while true loop)
            bool move = false;
            while (move == false)
            {
                Console.Write(">>   ");
                user = Console.ReadLine();
                user = user.ToLower(); //making it all lower case so we don't have to check for capital letters
                if (hasLight)
                {
                    switch (user)
                    {
                        case "east":
                        case "e":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomE2();
                            break;

                        case "north":
                        case "n":
                        case "s":
                        case "south":
                        case "west":
                        case "w":
                            oxygen = alterOxygen(-1);
                            Console.WriteLine("You cannot move that way\n");
                            break;

                        case "help":
                            Help();
                            break;

                        case "gold":
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"Your Gold: {gold}\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        case "look":
                        case "take":
                            if (!secretTreasureTaken)
                            {
                                Console.WriteLine("You found the treasure!\n");
                                getGold(100);
                                secretTreasureTaken = true;
                            }
                            else
                            {
                                Console.WriteLine("The treasure is already gone!\n");
                            }
                            break;

                        case "restart":
                            restart();
                            return;

                        default:
                            Console.WriteLine("That is an invalid input. Type HELP if you need help\n");
                            break;
                    }
                }
                else
                {
                    switch (user)
                    {
                        case "east":
                        case "e":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomE2();
                            break;

                        case "north":
                        case "n":
                        case "s":
                        case "south":
                        case "west":
                        case "w":
                            oxygen = alterOxygen(-1);
                            Console.WriteLine("You cannot move that way\n");
                            break;

                        case "help":
                            Help();
                            break;

                        case "gold":
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"Your Gold: {gold}\n\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        case "restart":
                            restart();
                            return;

                        default:
                            darkRoomMessage(false);
                            break;
                    }
                }

            }
        }
        static void CultRoom()
        {
            string userResponse;


            Console.WriteLine("You have found swirly circle stair case.\n " +
               "You grabbed the railing but it crumbled under the pressure, its rusted over time. \n" +
               "there seems to be a glowing light source coming from the bottom of the stairs.\n " +
               "Theres suddenly a heavy feeling in the water weighing you down.\n" +
               "\n you've reached the bottom of the stairs struggling to keep your self suspended in the water, there seems to be a force pushing you down.\n" +
               "You look around the room trying to figure out what is pulling you down.\n " +
               "Thats when you see candles, lit candles under the water, perfectly vertical like nothing could push it over\n");

            Console.WriteLine("A voice booms through the room.");
            Console.WriteLine("3 Questions is all it takes to leave this place\nBewarnd that your answer is your fate\n" +
                "If you get all 3 riddles correct you will be free to leave.");
            Console.WriteLine("Do you want to partake in the voices game?\n" +
                "Yes or No");

             userResponse = Console.ReadLine().ToLower();

            if (userResponse == "yes")
            {
                Console.WriteLine("Great, let's play");
                Q1();
            }
            else if (userResponse == "no")
            {
                Console.WriteLine("As you wish");
                DeathRoom();
            }
            else
            {
                Console.WriteLine("Invalid input");
            }

        }
        
        static void Q1()
        {
            string Temp;
            int ans;
            Console.WriteLine("I am a mothers child and a fathers child but nobody's son.\n What am I?");
            Console.WriteLine("1. A Grandson \n 2. A Daughter \n3.A Father \n 4.An Uncle");
  
            Temp = Console.ReadLine();
            ans = Convert.ToInt32(Temp);
            if (ans == 1)
            {
                DeathRoom();
            }
            else if  (ans == 2)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Next Riddle is ready.");
                Q2();
            }
            else if (ans == 3)
            {
                DeathRoom();
            }
            else  
            {
                DeathRoom();
            }
        }
        static void Q2()
        {
            string Temp;
            int ans;

            Console.WriteLine("your second riddle.\n " +
                "Pick me up and scratch my head. I’ll turn red and then black. What am I?");
            Console.WriteLine("1. A Candle\n 2. A Caterpillar\n 3. A Match\n 4. None of above");

            Temp = Console.ReadLine();
            ans = Convert.ToInt32(Temp);

            if (ans == 1)
            {
                DeathRoom();
            }
            else if (ans == 2)
            {
                DeathRoom();
            }
            else if (ans == 3)
            {
                Thread.Sleep(1000);
                Console.WriteLine("mmmmmm... Next riddle ");
                Q3();
            }
            else
            {
                DeathRoom();
            }
        }
        static void Q3()
        {
            string Temp;
            int ans;

            Console.WriteLine("Your Final riddle.\n " +
                "If you have it, you want to share it. If you share it, you don’t have it anymore. What is it?");
            Console.WriteLine("1. Love\n 2. A Secret\n 3. Talent\n 4. An Idea");

            Temp = Console.ReadLine();
            ans = Convert.ToInt32(Temp);

            if (ans == 1)
            {
                DeathRoom();
            }
            else if (ans == 2)
            {
                Console.WriteLine("I stay true to my word, you may leave to room but never come back");
                //Player could possble get pust in a random room or just continue from where they were 
                //in the above room
            }
            else if (ans == 3)
            {
                DeathRoom();
            }
            else
            {
                DeathRoom();
            }
        }

        static void RoomE3()
        {
            //      lore / description of location / hint of where to go
            if (hasLight)
            {
                Console.WriteLine("As you step into the room, an inexplicable sense of unease washes\n " +
                    "over you, prickling the hairs on the back of your neck.\n " +
                    "The water around you seams heavy with anticipation, as if\n " +
                    "unseen eyes are watching your every move. Empty walls surround you, devoid of any hint of life.\n " +
                    "A solitary chair, firmly affixed to the floor, seems to taunt you.\n" +
                    "Casting an eerie shadow in the dim light.\n");
            }
            else
            {
                darkRoomMessage(true);
            }

            //after completing more than false exit this will be added to the lore above             the boat to leave is to the west of you now 


            //user chooses what to do in the room in loop so choose multiple things and game doesn't end
            //making a variable for when the user finishes the game it doesn't stay in the loop forever (instead of while true loop)
            bool move = false;
            while (move == false)
            {
                Console.Write(">>   ");
                user = Console.ReadLine();
                user = user.ToLower(); //making it all lower case so we don't have to check for capital letters
                if (hasLight)
                {
                    switch (user)
                    {
                        case "west":
                        case "w":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomD3();
                            break;

                        case "north":
                        case "n":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomE4();
                            break;

                        case "s":
                        case "south":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomE2();
                            break;

                        case "east":
                        case "e":
                            oxygen = alterOxygen(-1);
                            Console.WriteLine("You cannot move that way\n");
                            break;

                        case "help":
                            Help();
                            break;

                        case "gold":
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"Your Gold: {gold}\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        case "look":
                        case "take":
                            Console.WriteLine("There is nothing in this area to pick up\n");
                            break;

                        case "restart":
                            restart();
                            return;

                        default:
                            Console.WriteLine("That is an invalid input. Type HELP if you need help\n");
                            break;
                    }
                }
                else
                {
                    switch (user)
                    {
                        case "west":
                        case "w":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomD3();
                            break;

                        case "north":
                        case "n":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomE4();
                            break;

                        case "s":
                        case "south":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomE2();
                            break;

                        case "east":
                        case "e":
                            oxygen = alterOxygen(-1);
                            Console.WriteLine("You cannot move that way\n");
                            break;

                        case "help":
                            Help();
                            break;

                        case "gold":
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"Your Gold: {gold}\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        case "restart":
                            restart();
                            return;

                        default:
                            darkRoomMessage(false);
                            break;
                    }
                }

            }
        }

        static void RoomD3()
        {
            //      lore / description of location / hint of where to go
            if (hasLight)
            {
                Console.WriteLine("This room seems to have collapsed.\n" +
                "The walls have caved in along broken wooden beams leaning across the room.\n" +
                "There seems to be enough room for you to climb over and under the beams.\n");
            }
            else
            {
                darkRoomMessage(true);
            }

            //after completing more than false exit this will be added to the lore above             the boat to leave is to the west of you now 


            //user chooses what to do in the room in loop so choose multiple things and game doesn't end
            //making a variable for when the user finishes the game it doesn't stay in the loop forever (instead of while true loop)
            bool move = false;
            while (move == false)
            {
                Console.Write(">>   ");
                user = Console.ReadLine();
                user = user.ToLower(); //making it all lower case so we don't have to check for capital letters
                if (hasLight)
                {
                    switch (user)
                    {
                        case "east":
                        case "e":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomE3();
                            break;

                        case "north":
                        case "n":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomD4();
                            break;

                        case "s":
                        case "south":
                        case "west":
                        case "w":
                            oxygen = alterOxygen(-1);
                            Console.WriteLine("You cannot move that way\n");
                            break;

                        case "help":
                            Help();
                            break;

                        case "gold":
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"Your Gold: {gold}\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        case "look":
                        case "take":
                            Console.WriteLine("There is nothing in this area to pick up\n");
                            break;

                        case "restart":
                            restart();
                            return;

                        default:
                            Console.WriteLine("That is an invalid input. Type HELP if you need help\n");
                            break;
                    }
                }
                else
                {
                    switch (user)
                    {
                        case "east":
                        case "e":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomE3();
                            break;

                        case "north":
                        case "n":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomD4();
                            break;

                        case "s":
                        case "south":
                        case "west":
                        case "w":
                            oxygen = alterOxygen(-1);
                            Console.WriteLine("You cannot move that way\n");
                            break;

                        case "help":
                            Help();
                            break;

                        case "gold":
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"Your Gold: {gold}\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        case "restart":
                            restart();
                            return;

                        default:
                            darkRoomMessage(false);
                            break;
                    }
                }

            }
        }

        static void RoomD4()
        {
            //Trap Room
            //      lore / description of location / hint of where to go
            if (hasLight)
            {
                Console.WriteLine("Tight trip wire lines are strung across the door way.\n" +
                "You carefully cut the lines so the traps do not activate.\n" +
                "A large flip switch is located on the east wall. It looks really heavy but also important. \n" +
                "The wires from the switch lead go through the wall and into the room to the east.\n");
            }
            else
            {
                darkRoomMessage(true);
            }


            //after completing more than false exit this will be added to the lore above             the boat to leave is to the west of you now 


            //user chooses what to do in the room in loop so choose multiple things and game doesn't end
            //making a variable for when the user finishes the game it doesn't stay in the loop forever (instead of while true loop)
            bool move = false;
            while (move == false)
            {
                Console.Write(">>   ");
                user = Console.ReadLine();
                user = user.ToLower(); //making it all lower case so we don't have to check for capital letters
                if (hasLight)
                {
                    switch (user)
                    {
                        case "s":
                        case "south":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomD3();
                            break;

                        case "north":
                        case "n":
                        case "east":
                        case "e":
                        case "west":
                        case "w":
                            oxygen = alterOxygen(-1);
                            Console.WriteLine("You cannot move that way\n");
                            break;

                        case "use":
                        case "use switch":
                        case "activate":
                            switchFlipped = true;
                            Console.WriteLine("as you pull the switch, hear a heavy thud from the other room  to the east.");
                            if (!switchFlipped)
                            {
                                switchFlipped = true;
                                roomEntered = true;
                                Console.WriteLine("as you pull the switch, hear a heavy thud from the other room  to the east.\n");

                            }
                            else
                            {
                                Console.WriteLine("The switch has already been switched!\n");
                            }
                            break;

                        case "help":
                            Help();
                            break;

                        case "gold":
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"Your Gold: {gold}\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        case "look":
                        case "take":
                            Console.WriteLine("There is a switch hidden in the corner\n");
                            break;

                        case "restart":
                            restart();
                            return;

                        default:
                            Console.WriteLine("That is an invalid input. Type HELP if you need help\n");
                            break;

                            //who ever codes this room, please use      switchFlipped = true    and it makes a deep thud noise    when user flips it, tyvm
                            //also use roomEntered==true to stop switch from being flipped, cause boblin will already be dead
                    }
                }
                else
                {
                    switch (user)
                    {
                        case "s":
                        case "south":
                            oxygen = alterOxygen(-4);
                            move = true;
                            RoomD3();
                            break;

                        case "north":
                        case "n":
                        case "east":
                        case "e":
                        case "west":
                        case "w":
                            oxygen = alterOxygen(-1);
                            Console.WriteLine("You cannot move that way\n");
                            break;

                        case "help":
                            Help();
                            break;

                        case "gold":
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"Your Gold: {gold}\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        case "restart":
                            restart();
                            return;

                        default:
                            darkRoomMessage(false);
                            break;
                    }
                }

            }
        }

        static void RoomE4()
        {
            //Boss Room
            //      lore / description of location / hint of where to go

            if (!switchFlipped && !roomEntered)
            {
                roomEntered = true;
                bool boblinAlive = true;
                Console.WriteLine("You walk into a room that seems to be the old Captains room.\n" +
                    "The room looks barely damaged considering the ship took so much damage and then sunk.\n" +
                    "There is a large wooden chair behind and solid wooden desk. A hammock still drapes in the corner.\n\n");
                Thread.Sleep(5000);
                Console.WriteLine("Suddenly a skeleton hand flies from a hole in the ceiling and slams the heavy door closed.\n" +
                    "Startled you freeze up. A rotting undead goblin falls from his hiding place in the ceiling above the door.\n" +
                    "While he descends his razor sharp sword slices through the oxygen tanks on your back.\n" +
                    "A large burst of bubbles rise away from you, but you hold your breath so you don't breathe in water.\n\n");
                Thread.Sleep(5000);
                Console.WriteLine("Spinning quickly you can see the knarly looking goblin holding his perfect condition cutlass in one hand.\n" +
                    "Behind him you can see the word 'Boblin' crudely carved into the back of the door and door frame hundreds of times.\n" +
                    "This must be Captain Boblin and it looks like he has been cursed to remain in his quarters.\n\n" +
                    "You have to think quickly before you can't hold your breath any longer\n" +
                    "Do you try to take the shiny cutlass, or try to escape deeper into the captain's quarters?\n");
                oxygen = 10; //forcing low oxygen
                alterOxygen(0); //using method for low oxygen warnings
                while (boblinAlive)
                {
                    Console.Write(">>   ");
                    user = Console.ReadLine();
                    user = user.ToLower(); //making it all lower case so we don't have to check for capital letters
                    switch (user)
                    {
                        case "take":
                        case "cutlass":
                        case "take cutlass":
                        case "sword":
                        case "take sword":
                            oxygen = alterOxygen(-2);
                            Console.WriteLine("You reach for the cutlass and grab the skeleton hand wielding it.\n" +
                                "You pull as hard as possible and stumble backwards, almost bashing your head on Captain Boblin's massive desk.\n" +
                                "Once you right yourself you can see that the Captain is no longer wielding the cutlass, but he is also missing his skeleton arm.\n" +
                                "You have to choose between attacking Boblin with his own sword, or fleeing further into his quarters\n");
                            while (boblinAlive)
                            {
                                Console.Write(">>   ");
                                string choose = Console.ReadLine();
                                choose = choose.ToLower(); //making it all lower case so we don't have to check for capital letters
                                switch (choose)
                                {
                                    case "cutlass":
                                    case "sword":
                                    case "attack":
                                        oxygen = alterOxygen(-2);
                                        Console.WriteLine("You use all the adrenaline in your veins to quickly swipe at Captain Boblin with his shiny cutlass.\n" +
                                            "This surprises the rotting goblin and he instinctively raises his only remaining hand to protect himself.\n" +
                                            "You cannot slow or adjust your swing so the blade digs into the bony fingers until...");
                                        Thread.Sleep(5000);
                                        Console.ForegroundColor = ConsoleColor.Magenta;
                                        Console.WriteLine("F    L    A    S    H");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Thread.Sleep(2000);
                                        Console.WriteLine("A bright purple light fills the underwater room. The source seems to be a gold ring with purple gem on Captain Boblin's finger.\n" +
                                            "The light slowly fades and you can see the shock in the goblin's face. The light returns for an instant super bright flash.\n" +
                                            "You finishing swinging the sword but hit no other resistances.\n" +
                                            "By the time your eyes have adjusted there is no sign of Captain Boblin any more.\n" +
                                            "The only things remaining are the ring with the purple gem smashed, and the cutlass in your hand.\n" +
                                            "You look down at the cutlass and there is rust quickly growing down towards the handle. This makes you drop the blade.\n" +
                                            "You fear to pick either up as you do not want to become cursed as Captain Boblin was.\n");
                                        boblinAlive = false;
                                        // = alterOxygen(50); //temporary to bug test
                                        break;

                                    case "help":
                                        Console.WriteLine("Enter key words to help you defeat Captain Boblin, eg to use his cutlass type CUTLASS and press enter\n");
                                        break;

                                    case "restart":
                                        restart();
                                        return;

                                    default:
                                        Console.WriteLine("That is an invalid input. Because you hesitated you feel like you need to breathe even more.\n");
                                        oxygen = alterOxygen(-1);
                                        Console.WriteLine("Type HELP if you need help\n");
                                        break;
                                }
                            }
                            break;

                        case "flee":
                        case "escape":
                        case "run":
                            Console.WriteLine("You turn and start running deeper into the Captain's quarters. The whole room is a mess and you stumble.\n" +
                                "As soon as Captain Boblin sees you running he starts swinging his sword...\n");
                            Thread.Sleep(5000);
                            Console.WriteLine("And it connects with the side of your head. An immense pain sears above your ear and red water flows from it.\n");
                            Thread.Sleep(5000);
                            DeathRoom();
                            break;

                        case "help":
                            Console.WriteLine("Enter key words to help you defeat Captain Boblin, eg to use his cutlass type CUTLASS and press enter\n");
                            break;

                        case "restart":
                            restart();
                            return;

                        default:
                            Console.WriteLine("That is an invalid input. Because you hesitated you feel like you need to breathe even more.\n");
                            oxygen = alterOxygen(-1);
                            Console.WriteLine("Type HELP if you need help\n");
                            break;
                    }
                }
            }
            else if (!switchFlipped && roomEntered) //boblin killed but switch still off
            {
                Console.WriteLine("The only things remaining after the fight are the ring with the purple gem smashed, and the rusty cutlass on the floor.\n" +
                    "You fear to pick either up as you do not want to become cursed as Captain Boblin was.\n");
            }
            else //switchFlipped == true
            {
                Console.WriteLine("A large stone almost covers the entire entrance into the room. You manage to squeeze your way past it.\n" +
                    "A giant hole in the ceiling shows ropes and pulleys in the floor above.\n" +
                    "You turn and look back at the rock and see a skeleton arm that is connected to a body crushed under the boulder.\n" +
                    "The tip of a rusted cutlass peeks out from behind the rock, this must've been the skeleton's weapon.\n" +
                    "The back of the door frame has the word 'boblin' carved into it many times.\n\n" +
                    "Except for the hole in the ceiling, room looks barely touched considering the ship took so much damage and then sunk.\n" +
                    "There is a large wooden chair behind and solid wooden desk. A hammock still drapes in the corner.\n\n");
            }

            //user chooses what to do in the room in loop so choose multiple things and game doesn't end
            //making a variable for when the user finishes the game it doesn't stay in the loop forever (instead of while true loop)
            bool move = false;
            while (move == false)
            {
                Console.Write(">>   ");
                user = Console.ReadLine();
                user = user.ToLower(); //making it all lower case so we don't have to check for capital letters
                switch (user)
                {
                    case "s":
                    case "south":
                        oxygen = alterOxygen(-4);
                        move = true;
                        RoomE3();
                        break;

                    case "north":
                    case "n":
                        oxygen = alterOxygen(-4);
                        move = true;
                        RoomE5();
                        break;

                    case "east":
                    case "e":
                    case "west":
                    case "w":
                        oxygen = alterOxygen(-1);
                        Console.WriteLine("You cannot move that way\n");
                        break;

                    case "help":
                        Help();
                        break;

                    case "gold":
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Your Gold: {gold}");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;

                    case "look":
                    case "take":
                        Console.WriteLine("You cannot pick up anything in this room\n");
                        break;

                    case "restart":
                        restart();
                        return;

                    default:
                        Console.WriteLine("That is an invalid input. Type HELP if you need help\n");
                        break;
                }
            }
        }

        static void RoomE5()
        {
            // Treasure Room
            //      lore / description of location / hint of where to go

            Console.WriteLine("This is RoomE5, main treasure room");
            Console.WriteLine("You enter a chamber bathed in a golden glow, You've found Captain Boblins fabled treasure. \n" +
                "Walls adorned with gleaming gold coins and glittering gemstones.\n" +
                "Elaborate chests, their ornate designs hinting to the riches that are within, stand in careful 5 chest tower.\n");
            //after completing more than false exit this will be added to the lore above             the boat to leave is to the west of you now 


            //user chooses what to do in the room in loop so choose multiple things and game doesn't end
            //making a variable for when the user finishes the game it doesn't stay in the loop forever (instead of while true loop)
            bool move = false;
            while (move == false)
            {
                Console.Write(">>   ");
                user = Console.ReadLine();
                user = user.ToLower(); //making it all lower case so we don't have to check for capital letters
                switch (user)
                {
                    case "s":
                    case "south":
                        oxygen = alterOxygen(-4);
                        move = true;
                        RoomE4();
                        break;

                    case "east":
                    case "e":
                    case "north":
                    case "n":
                    case "west":
                    case "w":
                        oxygen = alterOxygen(-1);
                        Console.WriteLine("You cannot move that way\n");
                        break;

                    case "help":
                        Help();
                        break;

                    case "gold":
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Your Gold: {gold}");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;

                    case "look":
                    case "take":
                        if (!mainTreasureTaken)
                        {
                            Console.WriteLine("You found Captain Boblin's secret treasure! You're now reach beyond your wildest dreams!\n");
                            Console.WriteLine("You also found an O2 canister which has replenished your supply by 50%!\n");
                            getGold(100);
                            oxygen = alterOxygen(50);
                        }
                        else
                        {
                            Console.WriteLine("Captain Boblin's treasure is already gone!\n");
                        }
                        break;

                    case "restart":
                        restart();
                        return;

                    default:
                        Console.WriteLine("That is an invalid input. Type HELP if you need help\n");
                        break;
                }

            }
        }


        static void FalseEndingRoom()
        {
            // Room description        ---        Made by John
            string temp = string.Empty;

            Console.WriteLine("Should I really be doing this..?");
            Thread.Sleep(200);
            Console.WriteLine("Do I really want to leave now..?");
            Thread.Sleep(200);
            Console.WriteLine("What if there was more..?");
            Thread.Sleep(500);
            Console.WriteLine("Maybe it's for the best...\n");
            Thread.Sleep(200);
            Console.WriteLine("Should I leave or should I stay?\n");

            bool move = false;
            while (!move)
            {
                Console.Write(">>   ");
                temp = Console.ReadLine();
                temp = temp.ToLower();

                switch (temp)
                {
                    case "leave":
                    case "exit":
                        if (gold > 0)
                        {
                            Console.WriteLine($"Congrats! You found treasure!\nYou found {gold} gold.\nGoodbye.");
                        }
                        else
                        {
                            Console.WriteLine("Good riddance.\nBetter luck in Hell.");
                        }

                        move = true;
                        return;

                    case "stay":
                    case "east":
                    case "e":
                        move = true;
                        RoomA2();
                        break;

                    case "gold":
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Your Gold: {gold}");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;

                    case "restart":
                        restart();
                        return;

                    default:
                        Console.WriteLine("Huh?");
                        break;
                }
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------\\
        //Random Gen rooms
        // Oxygen - -pickup or instant - done 
        //Key - pickup - 
        //Random Trap room - not instant kill - 

        static void RandomRoomOxygen(int oxy)
        {
            oxygen = alterOxygen(+oxy); // adds oxygen 
        }
        //Random Room commands
        static void Randomcommands(int oxy)
        {
            Console.WriteLine("Random room options pick");
            string user = Console.ReadLine().ToLower();
            
            switch(user)
            {
                case "oxygen":
                    {
                        RandomRoomOxygen(oxy);
                        break;
                    }
                case "take":
                case "look":
                    {
                        Keyavalible();
                        break;
                    }
                case "touch":
                case "grab":
                    DeathRoom();
                    break;
            }

        }
        public static bool Complete = true;
        static void Keyavalible()
        {
           
            if(Complete == true)
            {
                RandomRoomKey();
            }
        }

        static void RandomRoomKey()
        {
            bool again = true;
            
            int pick = rand.Next(0, 8);
            while (again == true)
            {
                switch (pick)
                {
                    case 0:
                        {
                            Console.WriteLine("you picked up a key");
                            roomLocked = false;
                            again = false;
                            Complete = false;
                            break;
                        }
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        {
                            Console.WriteLine("Nothing here");
                            again = false;
                            break;
                        }
                }



            }


        }

        static void RoomA3()
        {
            Console.WriteLine("Your in A3");
            int oxy = rand.Next(1, 15);
            printBaseRoomMessage(roomRowA[2],oxy);
            bool move = false;
            while (!move)
            {
                Console.Write(">>   ");
                user = Console.ReadLine();
                user = user.ToLower(); //making it all lower case so we don't have to check for capital letters
                switch (user)
                {
                    case "south":
                    case "s":
                        oxygen = alterOxygen(-4);
                        move = true;
                        RoomA2();
                        break;
                    case "n":
                    case "north":
                    case "w":
                    case "west":
                    case "east":
                    case "e":

                        oxygen = alterOxygen(-1);
                        Console.WriteLine("You cannot move that way\n");
                        break;

                    case "help":
                        Help();
                        break;

                    case "gold":
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Your Gold: {gold}");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;

                    case "look":
                    case "take":
                        Console.WriteLine("There is nothing in this area to pick up\n");
                        break;



                    case "rand":
                        Randomcommands(oxy);
                        break;

                    case "restart":
                        restart();
                        return;

                    default:
                        Console.WriteLine("That is an invalid input. Type HELP if you need help\n");                    
                        break;

                }
            }
        }

        static void RoomA4()
        {
            Console.WriteLine("Your in A4");
            //printBaseRoomMessage(roomRowA[3]);
            int oxy = rand.Next(1, 15);
            RandomRoomOxygen(oxy);
            printBaseRoomMessage(roomRowA[3],oxy);
            bool move = false;
            while (!move)
            {
                Console.Write(">>   ");
                user = Console.ReadLine();
                user = user.ToLower(); //making it all lower case so we don't have to check for capital letters
                switch (user)
                {
                    case "n":
                    case "north":
                        oxygen = alterOxygen(-4);
                        move = true;
                        RoomA5();
                        break;

                    case "w":
                    case "west":
                    case "east":
                    case "e":
                    case "south":
                    case "s":
                        oxygen = alterOxygen(-1);
                        Console.WriteLine("You cannot move that way\n");
                        break;

                    case "help":
                        Help();
                        break;

                    case "gold":
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Your Gold: {gold}");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;

                    case "look":
                    case "take":
                        Console.WriteLine("There is nothing in this area to pick up\n");
                        break;

                    case "restart":
                        restart();
                        return;

                    default:
                        Console.WriteLine("That is an invalid input. Type HELP if you need help\n");
                        break;

                }
            }
        }

        static void RoomA5()
        {
            Console.WriteLine("Your in A5");
            //printBaseRoomMessage(roomRowA[4]);
            Console.WriteLine("Your in A5");
            int oxy = rand.Next(1, 15);
            RandomRoomOxygen(oxy);
            printBaseRoomMessage(roomRowA[4], oxy);
            bool move = false;
            while (!move)
            {
                Console.Write(">>   ");
                user = Console.ReadLine();
                user = user.ToLower(); //making it all lower case so we don't have to check for capital letters
                switch (user)
                {
                    case "south":
                    case "s":
                        oxygen = alterOxygen(-4);
                        move = true;
                        RoomA4();
                        break;
                    case "east":
                    case "e":
                        oxygen = alterOxygen(-4);
                        move = true;
                        RoomB5();
                        break;

                    case "w":
                    case "west":
                    case "n":
                    case "north":

                        oxygen = alterOxygen(-1);
                        Console.WriteLine("You cannot move that way\n");
                        break;

                    case "help":
                        Help();
                        break;

                    case "gold":
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Your Gold: {gold}");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;

                    case "look":
                    case "take":
                        Console.WriteLine("There is nothing in this area to pick up\n");
                        break;

                    case "restart":
                        restart();
                        return;

                    default:
                        Console.WriteLine("That is an invalid input. Type HELP if you need help\n");
                        break;

                }
            }
        }

        static void RoomB1()
        {

            Console.WriteLine("Your in B1");
            //printBaseRoomMessage(roomRowB[0]);
            int oxy = rand.Next(1, 15);
            RandomRoomOxygen(oxy);
            printBaseRoomMessage(roomRowB[0],oxy);
            bool move = false;
            while (!move)
            {
                Console.Write(">>   ");
                user = Console.ReadLine();
                user = user.ToLower(); //making it all lower case so we don't have to check for capital letters
                switch (user)
                {
                    case "n":
                    case "north":
                        oxygen = alterOxygen(-4);
                        move = true;
                        RoomB2();
                        break;
                    case "w":
                    case "west":
                        oxygen = alterOxygen(-4);
                        move = true;
                        RoomA1();
                        break;

                    case "east":
                    case "south":
                    case "e":
                    case "s":
                        oxygen = alterOxygen(-1);
                        Console.WriteLine("You cannot move that way\n");
                        break;

                    case "help":
                        Help();
                        break;

                    case "gold":
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Your Gold: {gold}");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;

                    case "look":
                    case "take":
                        Console.WriteLine("There is nothing in this area to pick up\n");
                        break;

                    case "restart":
                        restart();
                        return;

                    default:
                        Console.WriteLine("That is an invalid input. Type HELP if you need help\n");
                        break;

                }
            }

        }

        static void RoomB4()
        {
            Console.WriteLine("Your in B4");
            //printBaseRoomMessage(roomRowB[3]);
            int oxy = rand.Next(1, 15);
            RandomRoomOxygen(oxy);
            printBaseRoomMessage(roomRowB[3], oxy);
            bool move = false;
            while (!move)
            {
                Console.Write(">>   ");
                user = Console.ReadLine();
                user = user.ToLower(); //making it all lower case so we don't have to check for capital letters
                switch (user)
                {
                    case "south":
                    case "s":
                        oxygen = alterOxygen(-4);
                        move = true;
                        RoomB3();
                        break;
                    case "east":
                    case "e":
                        oxygen = alterOxygen(-4);
                        move = true;
                        RoomC4();
                        break;


                    case "n":
                    case "north":
                        oxygen = alterOxygen(-4);
                        move = true;
                        RoomB5();
                        break;
                    case "w":
                    case "west":
                        oxygen = alterOxygen(-1);
                        Console.WriteLine("You cannot move that way\n");
                        break;

                    case "help":
                        Help();
                        break;

                    case "gold":
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Your Gold: {gold}");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;

                    case "look":
                    case "take":
                        Console.WriteLine("There is nothing in this area to pick up\n");
                        break;

                    case "restart":
                        restart();
                        return;

                    default:
                        Console.WriteLine("That is an invalid input. Type HELP if you need help\n");
                        break;

                }
            }
        }

        static void RoomB5()
        {
            Console.WriteLine("Your in B5");
            //printBaseRoomMessage(roomRowB[4]);
            int oxy = rand.Next(1, 15);
            RandomRoomOxygen(oxy);
            printBaseRoomMessage(roomRowB[4], oxy);
            bool move = false;
            while (!move)
            {
                Console.Write(">>   ");
                user = Console.ReadLine();
                user = user.ToLower(); //making it all lower case so we don't have to check for capital letters
                switch (user)
                {
                    case "n":
                    case "north":
                        oxygen = alterOxygen(-4);
                        move = true;
                        DeathRoom();
                        break;

                    case "w":
                    case "west":
                        move = true;
                        RoomA5();
                        break;

                    case "south":
                    case "s":
                        oxygen = alterOxygen(-4);
                        move = true;
                        RoomB4();
                        break;

                    case "e":
                    case "east":
                        oxygen = alterOxygen(-1);
                        Console.WriteLine("You cannot move that way\n");
                        break;

                    case "help":
                        Help();
                        break;

                    case "gold":
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Your Gold: {gold}");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;

                    case "look":
                    case "take":
                        Console.WriteLine("There is nothing in this area to pick up\n");
                        break;

                    case "restart":
                        restart();
                        return;

                    default:
                        Console.WriteLine("That is an invalid input. Type HELP if you need help\n");
                        break;

                }
            }
        }

        static void RoomC4()
        {
            Console.WriteLine("Your in C4");
            //printBaseRoomMessage(roomRowC[3]);
            int oxy = rand.Next(1, 15);
            RandomRoomOxygen(oxy);
            printBaseRoomMessage(roomRowC[3], oxy);
            bool move = false;
            while (!move)
            {
                Console.Write(">>   ");
                user = Console.ReadLine();
                user = user.ToLower(); //making it all lower case so we don't have to check for capital letters
                switch (user)
                {
                    case "n":
                    case "north":
                        oxygen = alterOxygen(-4);
                        move = true;
                        RoomC5();
                        break;
                    case "w":
                    case "west":
                        oxygen = alterOxygen(-4);
                        move = true;
                        RoomB4();
                        break;
                    case "east":
                    case "e":
                    case "south":
                    case "s":
                        oxygen = alterOxygen(-1);
                        Console.WriteLine("You cannot move that way\n");
                        break;

                    case "help":
                        Help();
                        break;

                    case "gold":
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Your Gold: {gold}");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;

                    case "look":
                    case "take":
                        Console.WriteLine("There is nothing in this area to pick up\n");
                        break;

                    case "restart":
                        restart();
                        return;

                    default:
                        Console.WriteLine("That is an invalid input. Type HELP if you need help\n");
                        break;

                }
            }
        }

        static void RoomC5()
        {
            Console.WriteLine("Your in C5");
            //printBaseRoomMessage(roomRowC[4]);
            int oxy = rand.Next(1, 15);
            RandomRoomOxygen(oxy);
            printBaseRoomMessage(roomRowC[4], oxy);
            bool move = false;
            while (!move)
            {
                Console.Write(">>   ");
                user = Console.ReadLine();
                user = user.ToLower(); //making it all lower case so we don't have to check for capital letters
                switch (user)
                {
                    case "south":
                    case "s":
                        oxygen = alterOxygen(-4);
                        move = true;
                        RoomC4();
                        break;

                    case "east":
                    case "e":
                        oxygen = alterOxygen(-4);
                        move = true;
                        RoomD5();
                        break;

                    case "w":
                    case "west":
                    case "n":
                    case "north":
                        oxygen = alterOxygen(-1);
                        Console.WriteLine("You cannot move that way\n");
                        break;

                    case "help":
                        Help();
                        break;

                    case "gold":
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Your Gold: {gold}");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;

                    case "look":
                    case "take":
                        Console.WriteLine("There is nothing in this area to pick up\n");
                        break;

                    case "restart":
                        restart();
                        return;

                    default:
                        Console.WriteLine("That is an invalid input. Type HELP if you need help\n");
                        break;

                }
            }
        }

        static void RoomD5()
        {
            // whirlpool room, can be moved to another room or stay put by Taylah

            //Random rand = new Random();
            int stolen = rand.Next(2);
            Console.WriteLine("As you enter the space, you see a whirlpool, and as you get close \n" +
                "it begins to pull you into the Swirling Water!");
            Thread.Sleep(2000);
            switch (stolen)
            {
                case 0:
                    int whiskedAway = rand.Next(3);
                    Console.WriteLine("You are getting whisked away, where will you end up!");
                    switch (whiskedAway)
                    {
                        case 0:
                            Console.WriteLine("you have been taken back to the Start!!");
                            Thread.Sleep(1750);
                            Console.Clear();
                            RoomA1();
                            return;

                        case 1:
                            Console.WriteLine("you have been taken to a random room, good luck!!");
                            Thread.Sleep(1750);
                            Console.Clear();
                            RoomA4();
                            return;

                        case 2:
                            Console.WriteLine("You have been pulled into the boss room!!");
                            Thread.Sleep(1750);
                            Console.Clear();
                            RoomE4();
                            return;
                    }
                    break;

                case 1:
                    break;
            }

            Console.WriteLine("You manage to stop yourself from being pulled into the whirlpool, nothing of surrounds the pool.");
            Console.Write(">>   ");
            user = Console.ReadLine();
            user = user.ToLower(); //making it all lower case so we don't have to check for capital letters
            bool move = false;
            while (!move)
            {
                switch (user)
                {
                    case "w":
                    case "west":
                        oxygen = alterOxygen(-4);
                        move = true;
                        RoomC5();
                        break;
                    case "south":
                    case "s":
                    case "east":
                    case "e":
                    case "n":
                    case "north":
                        oxygen = alterOxygen(-1);
                        Console.WriteLine("You cannot move that way\n");
                        break;

                    case "help":
                        Help();
                        break;

                    case "gold":
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Your Gold: {gold}");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;

                    case "look":
                    case "take":
                        Console.WriteLine("There is nothing in this area to pick up\n");
                        break;

                    case "restart":
                        restart();
                        return;

                    default:
                        Console.WriteLine("That is an invalid input. Type HELP if you need help\n");
                        break;

                }


            }
        }
    }
}

