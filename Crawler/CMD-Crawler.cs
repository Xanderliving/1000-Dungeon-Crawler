using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace Crawler
{
    /**
     * The main class of the Dungeon Crawler Application
     * 
     * You may add to your project other classes which are referenced.
     * Complete the templated methods and fill in your code where it says "Your code here".
     * Do not rename methods or variables which already exist or change the method parameters.
     * You can do some checks if your project still aligns with the spec by running the tests in UnitTest1
     * 
     * For Questions do contact us!
     */
    public class CMDCrawler
    {
        /**
         * use the following to store and control the next movement of the yser
         */
        public enum PlayerActions {NOTHING, NORTH, EAST, SOUTH, WEST, PICKUP, ATTACK, QUIT };

        private PlayerActions action = PlayerActions.NOTHING;
        public int[] position = { 0, 0 };
        public int[] LPosition = { 0, 0 };

        /** tracks if the game is running         */
        private bool active = true;

        /* Tracks is game is now active */
        private bool Change = false;

        /*Turns on Advanced mode */
        private bool AdvancedF = false;

        /*Finds the movment */
        private string Movment;

        /* Sets monsters Health*/
        private int MHealth = 1;

        /* Records players points */
        private int Points = 0;
        /// <summary>
        /// Use this object member to store the loaded map.
        /// </summary>
        private char[][] originalMap = new char[0][];
        /// <summary>

        /// Used to push the updated map
        /// </summary>
        private char[][] UpdateMap = new char[0][];
        /**
         * Reads user input from the Console
         * 
         * Please use and implement this method to read the user input.
         * 
         * Return the input as string to be further processed
         * 
         */
        private string ReadUserInput()
        {
            string inputRead = Console.ReadLine();

                return inputRead;
        }

        /**
         * Processed the user input string
         * 
         * takes apart the user input and does control the information flow
         *  * initializes the map ( you must call InitializeMap)
         *  * starts the game when user types in Play
         *  * sets the correct playeraction which you will use in the Update
         *  
         *  DO NOT read any information from command line in here but only act upon what the method receives.
         */
        public void ProcessUserInput(string input)
        {
            /* Records what the user has entered and how to react to it */
            if (input == "load Simple.map")
            {
                /*Loads Simple map */
                InitializeMap("Simple.map");
            }
            if (input == "load Simple2.map")
            {
                /*Loads Simple2 map */
                InitializeMap("Simple2.map");
            }
            /* Loads Advanced Map */
            if (input == "load Advanced.map")
                {
                    /*Loads Advanced map */
                    InitializeMap("Advanced.map");

                }
                /* Loads Advanced Mode */
                if (input == "Advanced")
                {
                    AdvancedF = true;
                    Console.WriteLine("Advanced mode ACTIVATE");
                }
                /* starts the game */
                if (input == "play" && Change == false)
                {
                    Change = true;
                }
                /*Records movment*/
                if (Change == true)
                {
                    Movment = input;
                }
            
            
            /*Tells the code how the user wants to move the player */
            if (Change == true)
            {
                if (input.ToUpper().Equals("D"))
                {

                    action = PlayerActions.EAST;
                }


                if (input.ToUpper().Equals("W"))
                {

                    action = PlayerActions.NORTH;

                }
                if (input.ToUpper().Equals("S"))
                {

                    action = PlayerActions.SOUTH;

                }
                if (input.ToUpper().Equals("A"))
                {

                    action = PlayerActions.WEST;

                }
                if (input == "play")
                {
                    action = PlayerActions.NOTHING;

                }
                /* Attacks the monster */
                if (input.Equals(" "))
                {
                    action = PlayerActions.ATTACK;
                }
            }



        }

        /**
         * The Main Game Loop. 
         * It updates the game state.
         * 
         * This is the method where you implement your game logic and alter the state of the map/game
         * use playeraction to determine how the character should move/act
         * the input should tell the loop if the game is active and the state should advance
         * 
         * Returns true if the game could be updated and is ongoing
         */
        public bool Update(bool active)
        {
            bool working = false;
            
                if (action == PlayerActions.NORTH)
                {
                /* End the game */
                if (UpdateMap[position[0] - 1][position[1]] == 'X')
                {
                    position[0] -= 1;
                    active = false;
                    Change = false;
                    this.active = false;
                    return false;
                }
                /* Records Points */
                if (UpdateMap[position[0] - 1][position[1]] == 'C')
                {
                    Points = +1;
                }
                /* Finds user position, moves them and stops moving if they hit a wall or monster */
                if (UpdateMap[position[0] - 1][position[1]] != '#' && UpdateMap[position[0] - 1][position[1]] != 'M')
                    {
                        UpdateMap[position[0]][position[1]] = '-';
                        working = true; position[0] -= 1;
                        UpdateMap[position[0]][position[1]] = '@';
                    }
                    Console.WriteLine("You have " + Points +" points");
                }
                if (action == PlayerActions.EAST)
                {
                /* End the game */
                    if (UpdateMap[position[0]][position[1]+ 1] == 'X')
                {
                    active = false;
                    Change = false;
                    return false;
                }
                /* Records Points */
                if (UpdateMap[position[0]][position[1]+ 1] == 'C')
                {
                    Points = +1;
                }
                /* Finds user position, moves them and stops moving if they hit a wall or monster */
                if (UpdateMap[position[0]][position[1] + 1] != '#' && UpdateMap[position[0]][position[1] + 1] != 'M')
                            {
                                UpdateMap[position[0]][position[1]] = '-';
                                working = true; 
                                position[1] += 1;
                                UpdateMap[position[0]][position[1]] = '@';
                            }
                Console.WriteLine("You have " + Points + " points");
                }
                if (action == PlayerActions.SOUTH)
                {
                /* End the game */
                if (UpdateMap[position[0] + 1][position[1]] == 'X')
                {
                    active = false;
                    Change = false;
                    return false;
                }
                /* Records Points */
                if (UpdateMap[position[0] + 1][position[1]] == 'C')
                {
                    Points = +1;
                }
                /* Finds user position, moves them and stops moving if they hit a wall or monster */
                if (UpdateMap[position[0] + 1][position[1]] != '#' && UpdateMap[position[0] +1][position[1]] != 'M')              
                    {
                        UpdateMap[position[0]][position[1]] = '-';
                        working = true; position[0] += 1;
                        UpdateMap[position[0]][position[1]] = '@';
                    }
                Console.WriteLine("You have " + Points + " points");
                }
                if (action == PlayerActions.WEST)
                {
                /* End the game */
                if (UpdateMap[position[0]][position[1] -1] == 'X')
                {
                    active = false;
                    Change = false;
                    return false;
                }
                /* Records Points */
                if (UpdateMap[position[0]][position[1] - 1] == 'C')
                {
                    Points = +1;
                }
                /* Finds user position, moves them and stops moving if they hit a wall or monster */
                if (UpdateMap[position[0]][position[1] - 1] != '#' && UpdateMap[position[0]][position[1] - 1] != 'M')
                        {
                            UpdateMap[position[0]][position[1]] = '-';                            
                            working = true; position[1] -= 1;
                            UpdateMap[position[0]][position[1]] = '@';
                        }
                Console.WriteLine("You have " + Points + " points");
                }
                if (AdvancedF == true)
                {
                if (action == PlayerActions.ATTACK)
                {
                    /* Damages monster */
                    if (UpdateMap[position[0] - 1][position[1]] == 'M')
                    {
                        MHealth -= 1;
                        UpdateMap[position[0] - 1][position[1]] = '-';
                    }
                    if (UpdateMap[position[0] + 1][position[1]] == 'M')
                    {
                        MHealth -= 1;
                        UpdateMap[position[0] + 1][position[1]] = '-';
                    }
                    if (UpdateMap[position[0]][position[1] - 1] == 'M')
                    {
                        MHealth -= 1;
                        UpdateMap[position[0]][position[1] - 1] = '-';
                    }
                    if (UpdateMap[position[0]][position[1] + 1] == 'M')
                    {
                        MHealth -= 1;
                        UpdateMap[position[0]][position[1] + 1] = '-';
                    }
                }
                }

            


            return working;
        }

        /**
         * The Main Visual Output element. 
         * It draws the new map after the player did something onto the screen.
         * 
         * This is the method where you implement your the code to draw the map ontop the screen
         * and show the move to the user. 
         * 
         * The method returns true if the game is running and it can draw something, false otherwise.
        */
        public bool PrintMap()
        { 
            /*Prints Map */
            for (int y = 0; y < UpdateMap.Length; y++)
            {
                Console.WriteLine(UpdateMap[y]);
            }

            return true;
        }
        /**
         * Additional Visual Output element. 
         * It draws the flavour texts and additional information after the map has been printed.
         * 
         * This is the method does not need to be used unless you want to output somethign else after the map onto the screen.
         * 
         */
        public bool PrintExtraInfo()
        {


            // Your code here

            return true;
        }

        /**
        * Map and GameState get initialized
        * mapName references a file name 
        * Do not use abosolute paths but use the files which are relative to the executable.
        * 
        * Create a private object variable for storing the map in Crawler and using it in the game.
        */
        public bool InitializeMap(String mapName)
        {
            string FilePath = Environment.CurrentDirectory + "/maps/" + mapName;
            bool initSuccess = false;


                /*Reads file */
                string[] text2 = File.ReadAllLines(FilePath);
                List<string> ListOfText = new List<string>();
                //char[][] MapArray = new char[10][];

                /* Reads how many lines the file has */
                for (int i = 0; i < text2.Length; i++)
                {
                    ListOfText.Add(text2[i]);
                }
                originalMap = new char[ListOfText.Count][];
                UpdateMap = new char[ListOfText.Count][];
            /*Works out the Y axis */
            for (int y = 0; y < ListOfText.Count; y++)
                {
                    string text1 = ListOfText[y];
                    originalMap[y] = text1.ToCharArray();
                    UpdateMap[y] = text1.ToCharArray();
            }

            
            for (int a = 0; a < UpdateMap.Length; a++)
            {
                
                for (int j = 0; j < UpdateMap[a].Length; j++)
                {
                    if (UpdateMap[a][j] == 'S')
                    {
                        originalMap[a][j] = '@';
                        UpdateMap[a][j] = '@';
                    }
                    if (UpdateMap[a][j] == '@')
                        {
                        position[0] = a;
                        position[1] = j;

                        }
                }

            }

            initSuccess = true;
            
            return initSuccess;
        }

        /**
         * Returns a representation of the currently loaded map
         * before any move was made.
         * This map should not change when the player moves
         */
        public char[][] GetOriginalMap()
        {

            char[][] map = originalMap;

           


            return map;
        }

        /*
         * Returns the current map state and contains the player's move
         * without altering it 
         */
        public char[][] GetCurrentMapState()
        {
            // the map should be map[y][x]
            char[][] map = UpdateMap;

            // Your code here


            return map;
        }

        /**
         * Returns the current position of the player on the map
         * 
         * The first value is the y coordinate and the second is the x coordinate on the map
         */
        public int[] GetPlayerPosition()
        {
            //int[] position = {0,0};
            /* sets player to start position*/

            
            return position;
        }

        /**
        * Returns the next player action
        * 
        * This method does not alter any internal state
        */
        public int GetPlayerAction()
        {
 
                int movment = (int)action;

            return movment;
        }


        public bool GameIsRunning()
        {
            bool running = false;
            /* Stop and starts game */
            if (Change == true)
            {
                running = true;
            }
            else if (Change == false)
            {
                running = false;
            }
            return running;
        }

        /**
         * Main method and Entry point to the program
         * ####
         * Do not change! 
        */
        static void Main(string[] args)
        {
            CMDCrawler crawler = new CMDCrawler();

            string input = string.Empty;
            Console.WriteLine("Welcome to the Commandline Dungeon!" +Environment.NewLine+ 
                "May your Quest be filled with riches!"+Environment.NewLine);
            
            // Loops through the input and determines when the game should quit
            while (crawler.active && crawler.action != PlayerActions.QUIT)
            {
                Console.WriteLine("Your Command: ");
                input = crawler.ReadUserInput();
                Console.WriteLine(Environment.NewLine);

                crawler.ProcessUserInput(input);
            
                crawler.Update(crawler.active);
                crawler.PrintMap();
                crawler.PrintExtraInfo();
            }

            Console.WriteLine("See you again" +Environment.NewLine+ 
                "In the CMD Dungeon! ");


        }


    }
}
