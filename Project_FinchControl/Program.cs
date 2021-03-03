using FinchAPI;
using System;
using System.Linq;

namespace Project_FinchControl
{

    // **************************************************
    //
    // Title: Finch Control - Menu Starter
    // Description: Finch Control Program
    // Application Type: Console
    // Author: Herkner, Bryce
    // Dated Created: 2/10/2021
    // Last Modified: 2/21/2021
    //
    // **************************************************

    class Program
    {
        /// <summary>
        /// first method run when the app starts up
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            SetTheme();

            DisplayWelcomeScreen();
            DisplayMenuScreen();
            DisplayClosingScreen();
        }

        /// <summary>
        /// setup the console theme
        /// </summary>
        static void SetTheme()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Main Menu                                 *
        /// *****************************************************************
        /// </summary>
        static void DisplayMenuScreen()
        {
            Console.CursorVisible = true;

            bool quitApplication = false;
            string menuChoice;

            Finch finchRobot = new Finch();
            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Connect Finch Robot");
                Console.WriteLine("\tb) Talent Show");
                Console.WriteLine("\tc) Data Recorder");
                Console.WriteLine("\td) Alarm System");
                Console.WriteLine("\te) User Programming");
                Console.WriteLine("\tf) Disconnect Finch Robot");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayConnectFinchRobot(finchRobot);
                        break;

                    case "b":
                        TalentShowDisplayMenuScreen(finchRobot);
                        break;

                    case "c":
                        DataRecorderDisplayMenuScreen(finchRobot);
                        break;

                    case "d":
                        AlarmSystemDisplayMenuScreen(finchRobot);
                        break;

                    case "e":
                        UserProgrammingDisplayMenuScreen(finchRobot);
                        break;

                    case "f":
                        DisplayDisconnectFinchRobot(finchRobot);
                        break;

                    case "q":
                        DisplayDisconnectFinchRobot(finchRobot);
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }

        #region TALENT SHOW

        /// <summary>
        /// *****************************************************************
        /// *                     Talent Show Menu                          *
        /// *****************************************************************
        /// </summary>
        static void TalentShowDisplayMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;

            bool quitTalentShowMenu = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Talent Show Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Light and Sound");
                Console.WriteLine("\tb) Dance");
                Console.WriteLine("\tc) Mixing it Uo");
                Console.WriteLine("\td) Song");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        TalentShowDisplayLightAndSound(finchRobot);
                        break;

                    case "b":
                        TalentShowDisplayDance(finchRobot);
                        break;

                    case "c":
                        TalentShowDisplayMixingItUp(finchRobot);
                        break;

                    case "d":
                        TalentShowDisplaySong(finchRobot);
                        break;

                    case "q":
                        quitTalentShowMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitTalentShowMenu);
        }

        //
        //
        //          Data Recorder Menu
        //
        //
        //
        static void DataRecorderDisplayMenuScreen(Finch finchRobot)
        {
            int numberOfDataPoints = 0;
            double dataPointFrequency = 0;
            double[] temperatures = null;
            double[] LightReadings = null;

            Console.CursorVisible = true;

            bool quitMenu = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Data Recorder Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Number of Data Points");
                Console.WriteLine("\tb) Frequency of Data Points");
                Console.WriteLine("\tc) Get Temperature Data");
                Console.WriteLine("\td) Show Temperature Data");
                Console.WriteLine("\te) Get Light Data");
                Console.WriteLine("\tf) Show Light Sensor Data");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        numberOfDataPoints = DataRecorderDidsplayGetNumberOfDataPoints();
                        break;

                    case "b":
                        dataPointFrequency = DataRecorderDidsplayGetDataPointFrequency();
                        break;

                    case "c":
                        temperatures = DataRecorderDisplayGetData(numberOfDataPoints, dataPointFrequency, finchRobot);
                        break;

                    case "d":
                        DataRecorderDisplayGetData(temperatures);
                        break;
                    case "e":
                        LightReadings = DataRecorderDisplayGetDataLights(numberOfDataPoints, dataPointFrequency, finchRobot);
                        break;
                    case "f":
                        DataRecorderDisplayGetLightData(LightReadings);
                        break;
                    case "q":
                        quitMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitMenu);
        }

        private static void DataRecorderDisplayGetData(double[] temperatures)
        {
            DisplayScreenHeader("Now we can show you the temperature data that has been collected!");

            DataRecorderDisplayTable(temperatures);

            DisplayContinuePrompt();
        }
        private static void DataRecorderDisplayGetLightData(double[] LightReadings)
        {
            DisplayScreenHeader("Now we can show you the Light Sensor data that has been collected!");

            DataRecorderDisplayTable(LightReadings);

            DisplayContinuePrompt();
        }

        static void DataRecorderDisplayTable(double[] temperatures)
        {
            //
            //      display table headers
            //
            Console.WriteLine(
                "Recording #".PadLeft(15) +
                "temp".PadLeft(15)
                );
            Console.WriteLine(
                "-----------".PadLeft(15) +
                "-----------".PadLeft(15)
                );

            //
            //  display table data
            //
            for (int index = 0; index < temperatures.Length; index++)
            {
                Console.WriteLine(
                    (index + 1).ToString().PadLeft(15) +
                    temperatures[index].ToString("n2").PadLeft(15)
                    );
            }
        }
        
        private static double[] DataRecorderDisplayGetData(int numberOfDataPoints, double dataPointFrequency, Finch finchRobot)
        {
            //
            //  Temp
            //

            double[] temperatures = new double[numberOfDataPoints];
            DisplayScreenHeader("Get that Data!");

            Console.WriteLine($"Number of data points:{numberOfDataPoints}");

            Console.WriteLine($"The Frequency at which the data is displayed:{dataPointFrequency}");

            Console.WriteLine();
            Console.WriteLine("\tThe Finch Robot is ready to begin Recording your temperature Data!");
            DisplayContinuePrompt();

            for (int index = 0; index < numberOfDataPoints; index++)
            {
                temperatures[index] = ConvertToFarenheit(finchRobot.getTemperature());
                Console.WriteLine($"\tReading{index + 1}: {temperatures[index].ToString("n2")}");
                int waitInSeconds = (int)(dataPointFrequency * 1000);
                finchRobot.wait(waitInSeconds);
            }
            DisplayContinuePrompt();
            DisplayScreenHeader("Get Data");

            Console.WriteLine();
            Console.WriteLine("\tTable of Temperatures");
            Console.WriteLine();
            DataRecorderDisplayTable(temperatures);

            Console.WriteLine($"The avedrage of the readings is {temperatures.Average()}");

            DisplayContinuePrompt();

            return temperatures;

        }
        private static double[] DataRecorderDisplayGetDataLights(int numberOfDataPoints, double dataPointFrequency, Finch finchRobot)
        {
            double[] LightReadings = new double[numberOfDataPoints];
            DisplayScreenHeader("Get that Data!");

            Console.WriteLine($"Number of data points:{numberOfDataPoints}");

            Console.WriteLine($"The Frequency at which the data is displayed:{dataPointFrequency}");

            Console.WriteLine();
            Console.WriteLine("\tThe Finch Robot is ready to begin Recording your Light Sensor Data!");
            DisplayContinuePrompt();

            for (int index = 0; index < numberOfDataPoints; index++)
            {
                LightReadings[index] = finchRobot.getRightLightSensor();
                Console.WriteLine($"\tReading{index + 1}: {LightReadings[index].ToString("n2")}");
                int waitInSeconds = (int)(dataPointFrequency * 1000);
                finchRobot.wait(waitInSeconds);
            }
            DisplayContinuePrompt();
            DisplayScreenHeader("Get Data");

            Console.WriteLine();
            Console.WriteLine("\tTable of Light Sensor Data");
            Console.WriteLine();
            DataRecorderDisplayTable(LightReadings);

            Console.WriteLine($"The avedrage of the readings is {LightReadings.Average()}");

            DisplayContinuePrompt();

            return LightReadings;
        }
        /// <summary>
        /// we are getting a double for frequency to use it to display the data points
        /// </summary>
        /// <returns>frequency of Data Points</returns>
        private static double DataRecorderDidsplayGetDataPointFrequency()
        {
            //double dataPointFrequency;
            //string userResponse;

            //DisplayScreenHeader("\tFrequency of Data Points");

            //Console.Write("Please enter the frequency that you wish the data to be displayed");
            //userResponse = Console.ReadLine();

            ////  validate user input
            //double.TryParse(userResponse, out dataPointFrequency);

            double dataPointFrequency = IsValidDouble("Please enter the frequency that you wish the data to be displayed");

            DisplayContinuePrompt();

            return dataPointFrequency;
        }

        /// <summary>
        /// get the number of data points from the user and display them. also validate user input and reprompt for input
        /// </summary>
        /// <returns></returns>
        private static int DataRecorderDidsplayGetNumberOfDataPoints()
        {
            //int numberOfDataPoints;
            //string userResponse;

            //DisplayScreenHeader("\tNumber of Data Points");

            //Console.Write("Please enter the number of Data Points you wish to Display in the Array");
            //userResponse = Console.ReadLine();

            ////  validate user input
            //int.TryParse(userResponse, out numberOfDataPoints);

            int numberOfDataPoints = IsValidInt("Please enter the number of Data Points you wish to Display in the Array");

            DisplayContinuePrompt();

            return numberOfDataPoints;
        }


        //
        //
        //
        //          Alarm system Display
        //
        //
        //
        static void AlarmSystemDisplayMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;
            Console.WriteLine("Sorry this section is still under construction...");
            DisplayContinuePrompt();
        }

        //
        //
        //
        //          user programming screen
        //
        //
        //

        static void UserProgrammingDisplayMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;
            Console.WriteLine("Sorry this section is still under construction...");
            DisplayContinuePrompt();
        }
        
        /// <summary>
        /// *****************************************************************
        /// *               Talent Show > Light and Sound                   *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void TalentShowDisplayLightAndSound(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Light and Sound");

            Console.WriteLine("\tThe Finch robot will now show off its glowing talent!");
            DisplayContinuePrompt();

            for (int lightSoundLevel = 0; lightSoundLevel < 255; lightSoundLevel++)
            {
                finchRobot.setLED(lightSoundLevel, lightSoundLevel, lightSoundLevel);
                finchRobot.noteOn(lightSoundLevel * 100);
            }
            for (int lightSoundLevel = 255; lightSoundLevel > 0; lightSoundLevel--)
            {
                finchRobot.setLED(lightSoundLevel, lightSoundLevel, lightSoundLevel);
                finchRobot.noteOn(lightSoundLevel * 100);
                finchRobot.noteOff();
            }

            DisplayMenuPrompt("Talent Show Menu");
        }

        //
        //  Talent Show Song
        //
        static void TalentShowDisplaySong(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Song Time");

            Console.WriteLine("\tThe Finch robot will now show off its enormous Pipes!");
            DisplayContinuePrompt();

            finchRobot.noteOn(1975);
            finchRobot.wait(500);
            finchRobot.noteOn(1760);
            finchRobot.wait(500);
            finchRobot.noteOn(1567);
            finchRobot.wait(1000);
            finchRobot.noteOn(1975);
            finchRobot.wait(500);
            finchRobot.noteOn(1760);
            finchRobot.wait(500);
            finchRobot.noteOn(1567);
            finchRobot.wait(1000);
            finchRobot.noteOff();
            finchRobot.noteOn(1567);
            finchRobot.wait(250);
            finchRobot.noteOff();
            finchRobot.noteOn(1567);
            finchRobot.wait(250);
            finchRobot.noteOff();
            finchRobot.noteOn(1567);
            finchRobot.wait(250);
            finchRobot.noteOff();
            finchRobot.noteOn(1567);
            finchRobot.wait(250);
            finchRobot.noteOff();
            finchRobot.noteOn(1760);
            finchRobot.wait(250);
            finchRobot.noteOff();
            finchRobot.noteOn(1760);
            finchRobot.wait(250);
            finchRobot.noteOff();
            finchRobot.noteOn(1760);
            finchRobot.wait(250);
            finchRobot.noteOff();
            finchRobot.noteOn(1760);
            finchRobot.wait(250);
            finchRobot.noteOff();
            finchRobot.noteOn(1975);
            finchRobot.wait(500);
            finchRobot.noteOn(1760);
            finchRobot.wait(500);
            finchRobot.noteOn(1567);
            finchRobot.wait(1000);
            finchRobot.noteOff();

            DisplayMenuPrompt("Talent Show Menu");
        }

        //
        //      Talent Show > Dancin'
        //

        static void TalentShowDisplayDance(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Dance");

            Console.WriteLine("\tThe Finch robot will display its sick dance moves!");

            DisplayContinuePrompt();

            finchRobot.setMotors(200, 100);
            finchRobot.wait(1000);
            finchRobot.setMotors(0, 0);
            finchRobot.setMotors(-200, -100);
            finchRobot.wait(1000);
            finchRobot.setMotors(0, 0);

            finchRobot.setMotors(100, 200);
            finchRobot.wait(1000);
            finchRobot.setMotors(0, 0);
            finchRobot.setMotors(-100, -200);
            finchRobot.wait(1000);
            finchRobot.setMotors(0, 0);
            finchRobot.setMotors(200, 100);
            finchRobot.wait(1000);
            finchRobot.setMotors(0, 0);
            finchRobot.setMotors(-200, -100);
            finchRobot.wait(1000);
            finchRobot.setMotors(0, 0);

            finchRobot.setMotors(100, 200);
            finchRobot.wait(1000);
            finchRobot.setMotors(0, 0);
            finchRobot.setMotors(-100, -200);
            finchRobot.wait(1000);
            finchRobot.setMotors(0, 0);



            DisplayMenuPrompt("Talent Show Menu");

        }

        //
        //      Talent Show > Mixing it up
        //

        static void TalentShowDisplayMixingItUp(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Mixing it up");

            Console.WriteLine("\tThe Finch robot will now do all of the cool things I know how to tell it to do!");

            DisplayContinuePrompt();
            finchRobot.setLED(255, 0, 0);
            finchRobot.setMotors(200, 100);
            finchRobot.wait(1000);
            finchRobot.setLED(0, 255, 0);
            finchRobot.setMotors(0, 0);
            finchRobot.setMotors(-200, -100);
            finchRobot.wait(1000);
            finchRobot.setMotors(0, 0);
            for (int lightSoundLevel = 0; lightSoundLevel < 255; lightSoundLevel++)
            {
                finchRobot.setLED(lightSoundLevel, lightSoundLevel, lightSoundLevel);
                finchRobot.noteOn(lightSoundLevel * 100);
            }
            finchRobot.setMotors(100, 200);
            finchRobot.wait(1000);
            finchRobot.setMotors(0, 0);
            finchRobot.setMotors(-100, -200);
            finchRobot.wait(1000);
            finchRobot.setMotors(0, 0);
            finchRobot.setMotors(200, 100);
            finchRobot.wait(1000);
            finchRobot.setMotors(0, 0);
            finchRobot.setMotors(-200, -100);
            finchRobot.wait(1000);
            finchRobot.setMotors(0, 0);
            for (int lightSoundLevel = 255; lightSoundLevel > 0; lightSoundLevel--)
            {
                finchRobot.setLED(lightSoundLevel, lightSoundLevel, lightSoundLevel);
                finchRobot.noteOn(lightSoundLevel * 100);
                finchRobot.noteOff();
            }
            finchRobot.setLED(0, 0, 255);
            finchRobot.setMotors(100, 200);
            finchRobot.wait(1000);
            finchRobot.setMotors(0, 0);
            finchRobot.setMotors(-100, -200);
            finchRobot.wait(1000);
            finchRobot.setMotors(0, 0);

            DisplayMenuPrompt("Talent Show Menu");
        }
        #endregion

        #region FINCH ROBOT MANAGEMENT

        /// <summary>
        /// *****************************************************************
        /// *               Disconnect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void DisplayDisconnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Disconnect Finch Robot");

            Console.WriteLine("\tAbout to disconnect from the Finch robot.");
            DisplayContinuePrompt();

            finchRobot.disConnect();

            Console.WriteLine("\tThe Finch robot is now disconnected.");

            DisplayMenuPrompt("Main Menu");
        }

        /// <summary>
        /// *****************************************************************
        /// *                  Connect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>notify if the robot is connected</returns>
        static bool DisplayConnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            bool robotConnected;

            DisplayScreenHeader("Connect Finch Robot");

            Console.WriteLine("\tAbout to connect to Finch robot. Please be sure the USB cable is connected to the robot and computer now.");
            DisplayContinuePrompt();

            robotConnected = finchRobot.connect();

            // TODO test connection and provide user feedback - text, lights, sounds
            int red = IsValidInt("Please enter a value bwteen 1-255 to set your red LED and verify connection.");
            finchRobot.setLED(red, 0, 0);
          //  ValidateConnection();

            DisplayMenuPrompt("Main Menu");

            //
            // reset finch robot
            //
            finchRobot.setLED(0, 0, 0);
            finchRobot.noteOff();

            return robotConnected;
        }

        #endregion

        #region USER INTERFACE

        /// <summary>
        /// *****************************************************************
        /// *                     Welcome Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tFinch Control");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Closing Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using Finch Control!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// display menu prompt
        /// </summary>
        static void DisplayMenuPrompt(string menuName)
        {
            Console.WriteLine();
            Console.WriteLine($"\tPress any key to return to the {menuName} Menu.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }
        //
        //
        //      validating integer input
        //
        //
        //
        static int IsValidInt(string question)
        {
            bool IsValidInt = false;
            int x = 0;
            while(!IsValidInt)
            {
                Console.Write($"{question}");
                IsValidInt = int.TryParse(Console.ReadLine(), out x);
                if (!IsValidInt)
                {
                    Console.WriteLine("That response is invalid. Please try again.");
                }

            }
            return x;
        }
        //
        //  validate double input
        //
        static double IsValidDouble(string question)
        {
            bool IsValidDouble = false;
            double x = 0;
            while (!IsValidDouble)
            {
                Console.Write($"{question}");
                IsValidDouble = Double.TryParse(Console.ReadLine(), out x);
                if (!IsValidDouble)
                {
                    Console.WriteLine("That response is invalid. Please try again.");
                }

            }
            return x;
        }



        //
        // get a user response to validate the finch has connected using that input
        //
        //
        static int  ValidateConnection()
        {
            int red = IsValidInt("Enter a value for Red between 1 and 255 so we can verify that your finch has been connected.");
            return red;
        }

        //
        //
        //          convert to farenheit method
        //
        //
        static double ConvertToFarenheit(double celciusValue)
        {
            return (celciusValue * 1.8) + 32;
        }
        #endregion
    }
}
