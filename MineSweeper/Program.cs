//Minesweeper Created by Daniel Grantham (s2083745)
//Started on 06/05/2021

using System;
using System.Diagnostics;
using System.IO;
using System.Text;
namespace MineSweeper
{
    class Program
    {
        static int cursorFlow(int menuChoice, int menuLength) //Function so if the cursor goes off the top / bottom of the menu, it flows to the other side
        {
            if (menuChoice == menuLength)
            {
                return 0;
            }
            else if (menuChoice == -1)
            {
                return menuLength - 1;
            }
            else
            {
                return menuChoice;
            }
        }
        static void selector(int menuOption, int X)
        {
            if (menuOption == X) //If the option and iterator math output cursor
            {
                Console.Write(">");
            }
            if (menuOption != X) // if they dont match output blank space
            {
                Console.Write(" ");
            }
        }
        static int menu(string type)
        {
            Console.CursorVisible = false;
            string[] mainMenu = { "Start game", "How to play", "Leaderboard", "Options", "Exit" }; //Main menu options
            string[] exit = { "Exit" };//Used for exit only options
            string[] difficulty = { "Easy", "Medium", "Hard", "Exit" };//Used to choose difficulty
            string[] yesNo = {"Yes","No"};
            int menuChoice = 0; //Int to track cursor position
            ConsoleKey input;


            while (true)
            {
                switch (type) //Checks parameter
                {
                    case "main": //If its main
                        Console.Clear();
                        Console.WriteLine(" Welcome to Minesweeper \n Please use the arrow keys to navigate the menu and enter to select an option\n");
                        menuChoice = cursorFlow(menuChoice, mainMenu.Length);
                        for (int X = 0; X != mainMenu.Length; X++) //Loop through the main menu options and print cursor with it if iterator is equal to menu option
                        {
                            selector(menuChoice, X);
                            Console.WriteLine(mainMenu[X]);
                        }
                        break;


                    case "exit":
                        for (int X = 0; X != exit.Length; X++) //Loop through the main menu options and print cursor with it if iterator is equal to menu option
                        {
                            selector(menuChoice, X);
                            Console.WriteLine(exit[X]);
                        }
                        break;


                    case "difficulty":
                        Console.WriteLine("What difficulty do you want to play?");
                        Console.WriteLine("Easy:   8x8,    15 mines");
                        Console.WriteLine("Medium: 14x14,  40 mines");
                        Console.WriteLine("Hard:   25x25,  99 mines");
                        Console.WriteLine("(Please note, the leaderboard times are only available in the easy difficulty)\n");
                        menuChoice = cursorFlow(menuChoice, difficulty.Length);
                        for (int X = 0; X != difficulty.Length; X++) //Loop through the main menu options and print cursor with it if iterator is equal to menu option
                        {
                            selector(menuChoice, X);
                            Console.WriteLine(difficulty[X]);
                        }
                        break;
                    case "Yesno":
                        Console.WriteLine("Are you sure?");
                        for (int X = 0; X != yesNo.Length; X++) //Loop through the main menu options and print cursor with it if iterator is equal to menu option
                        {
                            selector(menuChoice, X);
                            Console.WriteLine(yesNo[X]);
                        }
                        break;
                }
                while (true)
                {
                    input = Console.ReadKey(true).Key; //Waits for the user to input a key and set input to it

                    if (input == ConsoleKey.DownArrow) //If the key is the down arrow, add 1 to menuChoice then refresh the page
                    {
                        menuChoice++;
                        Console.Clear();
                        break;
                    }
                    else if (input == ConsoleKey.UpArrow) //If the key is the up arrow, subtract 1 to menuChoice then refresh the page
                    {
                        menuChoice--;
                        Console.Clear();
                        break;
                    }
                    else if (input == ConsoleKey.Enter) //If the key is enter, return the integer menuChoice to represent to currently selected menu
                    {
                        Console.Clear();
                        return menuChoice;

                    }
                }
            }
        }
        static void instructions() //Simple page for instructions
        {
            Console.WriteLine(" Rules for minesweeper:");
            Console.WriteLine(" Type in the co - ordinates of the square you would like to be revealed");
            Console.WriteLine(" The numbers on the board represent how many bombs are adjacent to a square");
            Console.WriteLine(" For example, if a square has a '3' on it, then there are 3 bombs next to that square");
            Console.WriteLine(" The bombs could be above, below, right left, or diagonal to the square");
            Console.WriteLine(" You are able to place a flag on squares you beleive to be a bomb");
            Console.WriteLine(" Avoid all the bombs and expose all the empty spaces to win Minesweeper\n");
            switch (menu("exit")) //When enter is pressed (since exit is the only option) return to the main menu
            {
                case 0:

                    break;
            }
        }
        static void printBoard(int size, char[,] boardReveal, int[,] boardLogic, char[] text)
        {
            ConsoleColor orig = Console.ForegroundColor;
            Console.Write("    "); //Padding
            for (int A = 0; A != size; A++) //Fills in lette row dynamicly with size
            {
                Console.Write(text[A] + "  ");
            }
            Console.Write("\n");
            for (int X = 0; X != size; X++)//These 2 for loop dynamicly prints to the size of the array
            {
                Console.Write(X + 1);
                for (int Y = 0; Y != size; Y++)
                {
                    if (Y == 0)
                    {
                        if (X <= 8)//Padding from single digit row number
                        {
                            Console.Write("  ");
                        }
                        else//PAdding from double digit row number
                        {
                            Console.Write(" ");
                        }
                    }
                    if (boardReveal[X, Y] == 'F') //If its a flag set the text colour to blue
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    if (boardReveal[X, Y] == '#' || boardReveal[X, Y] == 'F')//If it is unrevealed or a flag
                    {
                        Console.Write("[" + boardReveal[X, Y] + "]"); //Prints the X,Y contents of boardReveal
                    }
                    else if (boardLogic[X, Y] == 9) //If it is a mine
                    {
                        Console.ForegroundColor = ConsoleColor.Red;//Set text colour to red
                        Console.Write("[*]");
                    }
                    else if (boardLogic[X, Y] == 0 && boardReveal[X, Y] == ' ') //If it is revealed and  is a 0
                    {
                        Console.ForegroundColor = ConsoleColor.Green; //Set colour to green
                        Console.Write("[" + boardLogic[X, Y] + "]");
                    }
                    else //Otherwise it is any other number and makes the text yellow
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("[" + boardLogic[X, Y] + "]");
                    }


                    Console.ForegroundColor = orig; //Resets the origional colour
                }
                Console.Write("\n");
            }
        }
        static void placeMines(int size, int mines, int[,] boardLogic, char[,] boardReveal, int X, int Y)
        {
            int[] OffsetY = { -1, 0, 1, -1, 1, -1, 0, 1 };
            int[] OffsetX = { -1, -1, -1, 0, 0, 1, 1, 1 };
            Random rand = new Random();//Initialise the rng
            for (int A = 0; A != mines; A++)
            {
                int RandX = rand.Next(size - 1);//Creates 2 random numbers between 0 and the size -1
                int RandY = rand.Next(size - 1);
                if (boardLogic[RandX, RandY] == 9)
                {
                    A--;
                    continue;
                }
                else
                {
                    boardLogic[RandX, RandY] = 9;
                    for (int C = 0; C != OffsetX.Length; C++)
                    {
                        int placeX = RandX + OffsetX[C];
                        int placeY = RandY + OffsetY[C];
                        if (placeX != -1 && placeX != size && placeY != -1 && placeY != size && boardLogic[placeX, placeY] != 9)
                        {
                            boardLogic[placeX, placeY]++;
                        }

                    }
                }
                if (A == OffsetX.Length - 1 && boardReveal[X, Y] == ' ' && boardLogic[X, Y] != 0)
                {
                    Console.WriteLine(boardLogic[X, Y]);
                    A = 0;
                    for (int O = 0; O != size; O++)//Sets / resets the boards to default values
                    {
                        for (int I = 0; I != size; I++)
                        {
                            boardLogic[O, I] = 0;
                        }
                    }
                    continue;
                }
                if (boardLogic[Y, X] != 0)
                {
                    A = -1;
                    for (int D = 0; D != size; D++)//Sets / resets the boards to default values
                    {
                        for (int I = 0; I != size; I++)
                        {
                            boardLogic[D, I] = 0;
                        }
                    }
                    continue;
                }
            }
        }
        static int winCheck(int size, int mines, int[,] boardLogic, char[,] boardReveal)
        {
            int flaggedMines = 0;
            int openSpaces = 0;
            for (int X = 0; X != size; X++)
            {
                for (int Y = 0; Y != size; Y++)
                {
                    if (boardReveal[X, Y] == ' ' && boardLogic[X, Y] == 9)
                    {
                        return 0;
                    }
                    if (boardReveal[X, Y] == ' ' && boardLogic[X, Y] != 9)
                    {
                        openSpaces++;
                    }
                    if (boardReveal[X, Y] == 'F' && boardLogic[X, Y] == 9)
                    {
                        flaggedMines++;
                    }
                }
            }
            if (flaggedMines == mines)
            {
                return 1;
            }
            if (openSpaces == (size * size) - mines)
            {
                return 2;
            }
            else
            {
                return 3;
            }

        }
        static char[,] massReveal(int size, int[,] boardLogic, char[,] boardReveal)
        {
            int[] OffsetY = { -1, 0, 1, -1, 1, -1, 0, 1 };
            int[] OffsetX = { -1, -1, -1, 0, 0, 1, 1, 1 };
            for (int l = 0; l != 20; l++)
            {
                for (int a = 0; a != size; a++)
                {
                    for (int b = 0; b != size; b++)
                    {
                        if (boardReveal[a, b] == ' ' && boardLogic[a, b] == 0)
                        {
                            for (int c = 0; c != 8; c++)
                            {
                                if ((a + OffsetX[c] > -1 && a + OffsetX[c] < size))
                                {
                                    if (b + OffsetY[c] > -1 && b + OffsetY[c] < size)
                                    {
                                        boardReveal[a + OffsetX[c], b + OffsetY[c]] = ' ';
                                    }
                                }
                            }
                        }

                    }
                }

            }
            return boardReveal;
        }
        static int game(int difficulty)
        {
            char[] text = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' }; //Array of letters
            int size = 0, mines = 0; //Init
            switch (difficulty)
            {
                case 0: //Easy
                    size = 8;
                    mines = 1;
                    break;
                case 1: //Medium
                    size = 14;
                    mines = 40;
                    break;
                case 2: //Hard
                    size = 25;
                    mines = 99;
                    break;
            }//Takes input difficulty and applies correct size and mines
            char[,] boardReveal = new char[25, 25];//Init
            int[,] boardLogic = new int[25, 25];//Init
            int win = 4;
            string inputBuilder;
            char[] splitInput;
            bool inputError = false;
            for (int X = 0; X != size; X++)//Sets / resets the boards to default values
            {
                for (int I = 0; I != size; I++)
                {
                    boardReveal[X, I] = '#';
                    boardLogic[X, I] = 0;
                }
            }

            for (int A = 1; A != (size * size) - mines; A++) //Loops for number of turns 
            {
                Console.Clear();

                int Y = 0, X = 0;// init
                Console.WriteLine("Turn: " + A); //turn number
                if (inputError == true) //error output
                {
                    Console.WriteLine("That input is not correct");
                }

                printBoard(size, boardReveal, boardLogic, text);//prints the board
                win = winCheck(size, mines, boardLogic, boardReveal); //Checks if you have won or lost
                if (win == 0)
                {

                    Console.WriteLine("That position was a mine, that means you lose \n Press any key to continue");
                    Console.ReadKey(true);
                    return 0;
                }
                if (win == 1)
                {
                    Console.WriteLine("Congratulations, all the spaces you flagged are mines, YOU WIN  \n Press any key to continue");
                    Console.ReadKey(true);
                    return 1;
                }
                if (win == 2)
                {
                    Console.WriteLine("Congratulations, all the spaces that are not revealed are mines, YOU WIN  \n Press any key to continue");
                    Console.ReadKey(true);
                    return 2;
                }
                bool error;
                Console.CursorVisible = true; //Makes the cmd cursor visable
                error = false;
                inputBuilder = "";
                Console.WriteLine(" What position do you want to reveal (E.G. A1).\n Type flag before the position to flag the position instead (e.g. Flag A1)");
                try
                {
                    string input = Console.ReadLine(); //Waits for the user to input
                    splitInput = input.ToCharArray(); //splits it into a array of chars

                    if (splitInput.Length >= 7) //If the input is 7 characters or longer
                    {
                        string flagCheck = splitInput[0].ToString() + splitInput[1].ToString() + splitInput[2].ToString() + splitInput[3].ToString();//Checks first 4 characters
                        if (flagCheck.ToUpper() == "FLAG") //Changes them to upper case then sees if it is FLAG
                        {
                            inputBuilder = inputBuilder + splitInput[6].ToString(); //Checks the sixth character (number)
                            if (splitInput.Length == 8)
                            {
                                inputBuilder = inputBuilder + splitInput[7].ToString(); //Checks the sixth character (number)
                            }
                            else if(splitInput.Length > 8)
                            {
                                error = true;
                                A--;
                                continue;
                            }
                            Y = Int32.Parse(inputBuilder) - 1; //saves it -1 to account for arrays starting at 0
                            for (int a = 0; a != text.Length; a++) //Checks through each letter
                            {
                                if (text[a] == splitInput[5]) //If it is what was input
                                {
                                    X = (a % 26); //calculate the proper X position
                                    break;
                                }
                            }
                            if (boardReveal[Y, X] == '#')//If it isnt flagged, flag it
                            {
                                boardReveal[Y, X] = 'F';
                            }
                            else if (boardReveal[Y, X] == 'F')//If it is flagged, unflag it
                            {
                                boardReveal[Y, X] = '#';
                            }
                        }
                        else
                        {
                            error = true;
                            A--;
                            continue;
                        }
                    }
                    else if (splitInput.Length <= 3)//If the input is less than or equal to 3
                    {
                        error = true;
                        inputBuilder = splitInput[1].ToString(); //Sets the second chartacter
                        if (splitInput.Length == 3)
                        {
                            inputBuilder = inputBuilder + splitInput[2].ToString(); //Checks the third character (number)
                        }
                        Y = Int32.Parse(inputBuilder) - 1;
                        for (int a = 0; a != text.Length; a++)
                        {
                            if (text[a] == splitInput[0])
                            {
                                X = (a % 26);
                                error = false;
                                break;
                            }
                        }
                        if (error == true)
                        {
                            A--;
                            continue;
                        }
                        if (boardReveal[Y, X] == '#')
                        {
                            boardReveal[Y, X] = ' ';
                        }
                        else if (boardReveal[Y, X] == 'F')
                        {
                            while (true)
                            {
                                Console.WriteLine("Do you want to remove the flag (F) or reveal the space(U) (input nothing to go back)?");
                                string removeFlagIn = Console.ReadLine();
                                if (removeFlagIn == "U")
                                {
                                    boardReveal[Y, X] = ' ';
                                    break;
                                }
                                if (removeFlagIn == "F")
                                {
                                    boardReveal[Y, X] = '#';
                                    break;
                                }
                                if (removeFlagIn == "")
                                {
                                    error = true;
                                    A--;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("That is not a valid input");
                                }
                            }
                        }
                        else
                        {
                            error = true;
                            A--;
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine("That input is incorrect");
                        A--;
                        continue;
                    }
                }
                catch (Exception)
                {
                    A--;
                    continue;
                }
                if (A == 1)
                {
                    placeMines(size, mines, boardLogic, boardReveal, X, Y);//Places the mines
                }
                boardReveal = massReveal(size, boardLogic, boardReveal);

            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey(true);
            return (1);
        }
        static int[] chooseName(int[] name)
        {
            char[] text = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' }; //Array of letters
            int menuOption = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Please choose your name with the arrow keys and click enter to confirm");
                menuOption = cursorFlow(menuOption, 3);
                Console.WriteLine();
                Console.Write("   ");
                for (int I = 0; I != 3; I++)
                {
                    if (menuOption == I)
                    {
                        Console.Write("V");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }

                Console.Write("\n");
                Console.Write("   ");
                for (int I = 0; I != 3; I++)
                {
                    Console.Write(text[name[I]]);
                }
                Console.Write("\n");
                Console.Write("   ");
                for (int I = 0; I != 3; I++)
                {
                    if (menuOption == I)
                    {
                        Console.Write("^");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine("\n");
                Console.WriteLine("> Enter");
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.LeftArrow)
                {
                    menuOption--;
                }
                if (key == ConsoleKey.RightArrow)
                {
                    menuOption++;
                }
                if (key == ConsoleKey.UpArrow)
                {
                    name[menuOption]++;
                    name[menuOption] = cursorFlow(name[menuOption], text.Length);
                }
                if (key == ConsoleKey.DownArrow)
                {
                    name[menuOption]--;
                    name[menuOption] = cursorFlow(name[menuOption], text.Length);
                }
                if (key == ConsoleKey.Enter)
                {
                    break;
                }
            }
            return name;
        }
        static void leaderboardAdd(string Time, int[] name)
        {
            char[] text = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' }; //Array of letters

            string timePath = "GameFiles/leaderboardTime.txt";
            string namePath = "GameFiles/leaderboardName.txt";
            string[] times = { "99:99:99.99", "99:99:99.99", "99:99:99.99", "99:99:99.99", "99:99:99.99", "99:99:99.99" };
            string[] fileTimes;
            string[] names = { "ZZZ", "ZZZ", "ZZZ", "ZZZ", "ZZZ", "ZZZ" };
            string[] fileNames;
            if (!File.Exists(timePath))
            {
                File.CreateText(timePath).Close();// Create a file to write to.  
            }
            using (var timeFile = File.OpenText(timePath))
            {
                fileTimes = File.ReadAllLines(timePath);
                for (int a = 0; a != fileTimes.Length; a++)
                {
                    times[a] = fileTimes[a];
                }
                times[5] = Time;

            }

            if (!File.Exists(namePath))
            {
                File.CreateText(namePath).Close();
            }
            using (var nameFile = File.OpenText(namePath))
            {

                fileNames = File.ReadAllLines(namePath);
                for (int a = 0; a != fileTimes.Length; a++)
                {
                    names[a] = fileNames[a];
                }
                names[5] = text[name[0]].ToString() + text[name[1]].ToString() + text[name[2]].ToString();
            }

            Array.Sort(times, names);
            string[] savedTimes = new string[6];
            for (int A = 0; A != 5; A++)
            {
                savedTimes[A] = times[A];
            }

            string[] savedNames = new string[6];
            for (int A = 0; A != 5; A++)
            {
                savedNames[A] = names[A];
            }

            File.WriteAllLines(timePath, savedTimes, Encoding.UTF8);
            File.WriteAllLines(namePath, savedNames, Encoding.UTF8);
        }
        static void leaderboard()
        {
            Console.WriteLine("   Leaderboard");
            Console.WriteLine("   Name|Time");
            string timePath = "GameFiles/leaderboardTime.txt";
            string namePath = "GameFiles/leaderboardName.txt";
            int[] a = { 25, 25, 25 };
            leaderboardAdd("99:99:99.99", a);
            leaderboardAdd("99:99:99.99", a);
            leaderboardAdd("99:99:99.99", a);
            leaderboardAdd("99:99:99.99", a);
            leaderboardAdd("99:99:99.99", a);
            if (!File.Exists(timePath))
            {
                File.CreateText(timePath).Close();// Create a file to write to.
            }
            if (!File.Exists(namePath))
            {
                File.CreateText(namePath).Close();// Create a file to write to.
            }
            string[] times = System.IO.File.ReadAllLines(timePath);
            string[] names = System.IO.File.ReadAllLines(namePath);
            for (int i = 0; i != times.Length - 1; i++)
            {
                if (times[i] == "ZZZZ")
                {
                    Console.WriteLine("");
                    continue;
                }
                Console.WriteLine("   " + names[i] + ": " + times[i]);
            }
            Console.WriteLine("> Exit");
            Console.ReadKey(true);
        }
        static void prePostGame(int[] name, int autoTimer)
        {
            Stopwatch sw = new Stopwatch(); // Initialise the stopwatch
            int difficulty = menu("difficulty");// Gets the intended difficulty
            if (difficulty == 3)
            {
                return;
            }
            Console.WriteLine("Press any button for the game and timer to start");//Waits for user input
            Console.ReadKey(true);
            sw.Start();
            Console.Clear();
            int win = game(difficulty);//starts the game at the correct difficulty
            sw.Stop();
            Console.Clear();
            if ((win == 1||win==2) && difficulty == 0 && autoTimer == 1)
            {
                TimeSpan ts = sw.Elapsed; // Set the elapsed time as a TimeSpan value.

                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);// Format and display the TimeSpan value.
                Console.WriteLine("Time : " + elapsedTime);
                Console.WriteLine("Press any key to continue");
                Console.ReadKey(true);
                leaderboardAdd(elapsedTime, chooseName(name));
            }
        }
        static void option(int[] name, int defaultTime, int backgroundColour)
        {
            string[] option = { "Record minesweeper game times by default", "Change default leaderboard name", "Background colour","Reset settings to default", "Save and exit", "Exit" };
            string[] yesNo = { "No", "Yes" };
            string[] stringColour = { "Black", "Dark Blue", "Dark Cyan", "Dark Red", "Dark Magenta", "Dark yellow", "Dark Gray", "Cyan", "Magenta", "White" };
            int menuOption = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine(" Options: \n Please use up and down to navigate the options \n On options with the option shown use left and right arrows to change them. \n For options without option shown, click enter on it to proceed \n Some background colours may result in difficulty seing board in some scenarios \n please be careful using dark blue and dark yellow\n\n");

                for (int i = 0; i != option.Length; i++)
                {
                    menuOption = cursorFlow(menuOption, option.Length);
                    selector(i, menuOption);
                    Console.Write(" " + option[i]);
                    defaultTime = cursorFlow(defaultTime, yesNo.Length);
                    backgroundColour = cursorFlow(backgroundColour, stringColour.Length);
                    switch (i)
                    {
                        case 0:
                            Console.Write("[" + yesNo[defaultTime] + "]");
                            break;
                        case 2:
                            Console.Write("[" + stringColour[backgroundColour] + "]");
                            break;
                    }
                    Console.WriteLine("");
                }

                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.DownArrow)
                {
                    menuOption++;
                }
                if (key == ConsoleKey.UpArrow)
                {
                    menuOption--;
                }
                if (key == ConsoleKey.RightArrow)
                {
                    switch (menuOption)
                    {
                        case 0:
                            defaultTime++;
                            if (defaultTime == yesNo.Length)
                            {
                                defaultTime = 0;
                            }
                            break;
                        case 2:
                            backgroundColour++;
                            if (backgroundColour == stringColour.Length)
                            {
                                backgroundColour = 0;
                            }
                            break;


                    }
                }
                if (key == ConsoleKey.LeftArrow)
                {
                    switch (menuOption)
                    {
                        case 0:
                            defaultTime--;
                            break;

                        case 2:
                            backgroundColour--;
                            break;

                    }
                }
                if (key == ConsoleKey.Enter)
                {
                    switch (menuOption)
                    {
                        case 1:
                            name = chooseName(name);
                            break;
                        case 3:
                            Console.Clear();
                            switch (menu("Yesno"))
                            {
                                case 0:
                                    defaultOptions(1);
                                    break;
                                case 1:
                                    continue;
                            }
                            break;
                        case 4:
                            string optionPath = "GameFiles/options.txt";
                            string[] optionSave = { defaultTime.ToString(), name[0].ToString(), name[1].ToString(), name[2].ToString(), backgroundColour.ToString() };
                            if (!File.Exists(optionPath))
                            {
                                File.CreateText(optionPath);// Create a file to write to.  
                            }
                            File.WriteAllLines(optionPath, optionSave, Encoding.UTF8);
                            return;
                        case 5:
                            return;
                            //break;
                    }
                }
            }
        }
        static void mainMenu()
        {
            string[] fileoptions;
            string optionPath = "GameFiles/options.txt";
            Console.Clear();


            while (true)
            {
                using (var optionFile = File.OpenText(optionPath))
                {
                    fileoptions = File.ReadAllLines(optionPath);
                }
                int[] name = { Convert.ToInt32(fileoptions[1]), Convert.ToInt32(fileoptions[2]), Convert.ToInt32(fileoptions[3]) };
                ConsoleColor[] Colour = { ConsoleColor.Black, ConsoleColor.DarkBlue, ConsoleColor.DarkCyan, ConsoleColor.DarkRed, ConsoleColor.DarkMagenta, ConsoleColor.DarkYellow, ConsoleColor.DarkGray, ConsoleColor.Cyan, ConsoleColor.Magenta, ConsoleColor.White };
                Console.BackgroundColor = Colour[Convert.ToInt32(fileoptions[4])];
                if (Convert.ToInt32(fileoptions[4]) <= 12 && Convert.ToInt32(fileoptions[4]) >= 9)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }

                switch (menu("main")) //Create the main menu
                {
                    case 0:
                        prePostGame(name, Convert.ToInt32(fileoptions[0]));
                        break;
                    case 1:
                        instructions(); //Goes to the instructions page
                        break;
                    case 2:
                        leaderboard();
                        break;
                    case 3:
                        option(name, Convert.ToInt32(fileoptions[0]), Convert.ToInt32(fileoptions[4]));
                        break;
                    case 4:
                        Environment.Exit(0); //Exits the application
                        break;
                }
            }
        }
        static void defaultOptions(int type = 0)
        {
            string[] defaultOption = { "0", "0", "0", "0", "0" };
            string optionPath = "GameFiles/options.txt";
            if (type == 0)
            {
                if (!File.Exists(optionPath))
                {
                    File.CreateText(optionPath).Close();// Create a file to write to.  
                    File.WriteAllLines(optionPath, defaultOption, Encoding.UTF8);
                }
            }
            if(type == 1)
            {
                File.WriteAllLines(optionPath, defaultOption, Encoding.UTF8);
            }

        }
        static void Main(string[] args)
        {
            DirectoryInfo di = Directory.CreateDirectory("GameFiles");
            defaultOptions(0);
            mainMenu();
        }

    }
}