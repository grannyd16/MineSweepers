//Minesweeper Created by Daniel Grantham (s2083745)
//Started on 06/05/2021

using System;
using System.Diagnostics;
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
            string[] exit = { "Exit" };
            string[] difficulty = { "Easy", "Medium", "Hard" };
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
                        Console.WriteLine("Hard:   25x25,  99 mines\n");
                        menuChoice = cursorFlow(menuChoice, difficulty.Length);
                        for (int X = 0; X != difficulty.Length; X++) //Loop through the main menu options and print cursor with it if iterator is equal to menu option
                        {
                            selector(menuChoice, X);
                            Console.WriteLine(difficulty[X]);
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
                    if (boardReveal[X, Y] == 'F')
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    if (boardReveal[X, Y] == '#' || boardReveal[X, Y] == 'F')
                    {
                        Console.Write("[" + boardReveal[X, Y] + "]"); //Prints the X,Y contents of boardReveal
                    }
                    else if (boardLogic[X, Y] == 9)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("[*]");
                    }
                   else if (boardLogic[X, Y] == 0 && boardReveal[X,Y] == ' ')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("[" + boardLogic[X, Y] + "]");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("[" + boardLogic[X, Y] + "]");
                    }


                    Console.ForegroundColor = ConsoleColor.White;
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
                    mines = 15;
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
            for (int X = 0; X != size; X++)//Sets / resets the boards to default values
            {
                for (int I = 0; I != size; I++)
                {
                    boardReveal[X, I] = '#';
                    boardLogic[X, I] = 0;
                }
            }

            for (int A = 1; A != (size * size) - mines; A++)
            {
                Console.Clear();
                int Y = 0, X = 0;// init
                Console.WriteLine("Turn: " + A);
                printBoard(size, boardReveal, boardLogic, text);//prints the board
                win = winCheck(size, mines, boardLogic, boardReveal);
                if (win == 0)
                {
                    Console.WriteLine("That position was a mine, that means you lose");
                    break;
                }
                if (win == 1)
                {
                    Console.WriteLine("Congratulations, all the spaces you falgged are mines, YOU WIN");
                    break;
                }
                if (win == 2)
                {
                    Console.WriteLine("Congratulations, all the spaces that are not revealed are mines, YOU WIN");
                    break;
                }
                bool error;

                Console.CursorVisible = true;
                error = true;
                inputBuilder = "";
                Console.WriteLine("What position do you want to reveal (E.G. A1)");
                try
                {
                    string input = Console.ReadLine();
                    splitInput = input.ToCharArray();
                    splitInput[0] = splitInput[0];
                }
                catch (Exception)
                {
                    A--;
                    continue;
                }
                if (splitInput.Length >= 7)
                {
                    string flagCheck = splitInput[0].ToString() + splitInput[1].ToString() + splitInput[2].ToString() + splitInput[3].ToString();
                    if (flagCheck.ToUpper() == "FLAG")
                    {
                        for (int a = 6; a != splitInput.Length; a++)
                        {
                            inputBuilder = inputBuilder + splitInput[a].ToString();
                        }
                        Y = Int32.Parse(inputBuilder) - 1;
                        for (int a = 0; a != text.Length; a++)
                        {
                            if (text[a] == splitInput[5])
                            {
                                X = (a % 26);
                                error = false;
                                break;
                            }
                        }
                        if (error == true)
                        {
                            Console.WriteLine("Test");
                            A--;
                            continue;
                        }
                        if (boardReveal[Y, X] == '#')
                        {
                            boardReveal[Y, X] = 'F';
                        }
                        else if (boardReveal[Y, X] == 'F')
                        {
                            boardReveal[Y, X] = '#';
                        }
                    }
                }
                else if (splitInput.Length <= 3)
                {
                    error = true;
                    for (int a = 1; a != splitInput.Length; a++)
                    {
                        inputBuilder = inputBuilder + splitInput[a].ToString();
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
                    if (boardReveal[Y, X] == 'F')
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
                                A--;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("That is not a valid input");
                            }
                        }
                    }
                }
                else
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
        static int[] chooseName()
        {
            int[] name = {0,0,0};
            return name;
        }
        static void prePostGame()
        {
            Stopwatch sw = new Stopwatch(); // Initialise the stopwatch
            int difficulty = menu("difficulty");// Gets the intended difficulty
            Console.WriteLine("Press any key to begin");//Waits for user input
            Console.ReadKey(true);
            sw.Start();
            Console.Clear();
            int win = game(difficulty);//starts the game at the correct difficulty
            sw.Stop();
            if (win == 1)
            {
                // Get the elapsed time as a TimeSpan value.
                TimeSpan ts = sw.Elapsed;
                Console.Clear();
                // Format and display the TimeSpan value.
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                Console.WriteLine("Time : " + elapsedTime);
                Console.ReadKey(true);
                leaderboard(elapsedTime, chooseName());
            }
        }
        static void mainMenu()
        {
            while (true)
            {
                switch (menu("main")) //Create the main menu
                {
                    case 0:
                        prePostGame();
                        break;
                    case 1:
                        instructions(); //Goes to the instructions page
                        break;
                    case 2: break;
                    case 3: break;
                    case 4:
                        Environment.Exit(0); //Exits the application
                        break;
                }
            }
        }
        static void Main(string[] args)
        {
            mainMenu();

        }
        }
    }
