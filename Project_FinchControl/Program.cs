using FinchAPI;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Project_FinchControl
{

    // **************************************************
    //
    // Title: Finch Control - Menu Starter
    // Description: Finch Control Program
    // Application Type: Console
    // Author: Herkner, Bryce
    // Dated Created: 2/10/2021
    // Last Modified: 03/20/2021
    //
    // **************************************************
    public enum Command
    {
        NONE,
        MOVEFORWARD,
        MOVEBACKWARD,
        STOPMOTORS,
        WAIT,
        TURNRIGHT,
        TURNLEFT,
        LEDON,
        LEDOFF,
        GETTEMPERATURE,
        DANCE,
        STOPDANCE,
        GETLIGHTSENSORS,
        DONE        
    }
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
            for (int index = 0; index < LightReadings.Length; index++)
            {
                Console.WriteLine(
                    (index + 1).ToString().PadLeft(15) +
                    LightReadings[index].ToString("n2").PadLeft(15)
                    );
            }

            // DataRecorderDisplayTable(LightReadings);

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
        //          Alarm system Display
        //
        static void AlarmSystemDisplayMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;

            bool quitMenu = false;
            string menuChoice;

            string sensorsToMonitor = null;
            string rangeType = null;
            int lightMinMaxThresholdValue = 0;
            int temperatureMinMaxThresholdValue = 0;
            int timeToMonitor = 0;

            do
            {
                DisplayScreenHeader("Light  and Temperature Alarm Menu");
                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Set Sensors to Monitor");
                Console.WriteLine("\tb) Set Range Type");
                Console.WriteLine("\tc) Set Light Maximum/Minimum Threshold Value");
                Console.WriteLine("\td) Set Time to Monitor");
                Console.WriteLine("\te) Set Light Alarm");
                Console.WriteLine("\tf) Set temperature Alarm");
                Console.WriteLine("\tg) Set Light and temperature Alarm");
                Console.WriteLine("\th) Set temperature Maximum/Minimum Threshold Value");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();
                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        sensorsToMonitor = LightAlarmDisplaySetSensorsToMonitor();
                        break;
                    case "b":
                        rangeType = LightAlarmDisplaySetRangeType();
                        break;

                    case "c":
                        lightMinMaxThresholdValue = LightAlarmSetMinMaxThresholdValue(rangeType, finchRobot);
                        break;

                    case "d":
                        timeToMonitor = LightAlarmSetTimeToMonitor();
                        break;
                    case "e":
                        LightAlarmSetAlarm(finchRobot, sensorsToMonitor, rangeType, lightMinMaxThresholdValue, timeToMonitor);
                        break;
                    case "f":
                        TemperatureAlarmSetAlarm(finchRobot, rangeType, temperatureMinMaxThresholdValue, timeToMonitor);
                        break;
                    case "g":
                        TemperatureAndLightAlarmSetAlarm(finchRobot, sensorsToMonitor, rangeType, lightMinMaxThresholdValue, temperatureMinMaxThresholdValue, timeToMonitor);
                        break;
                    case "h":
                        temperatureMinMaxThresholdValue = TemperatureAlarmSetMinMaxThresholdValue(rangeType, finchRobot);
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

        private static void TemperatureAndLightAlarmSetAlarm
            (Finch finchRobot, 
            string sensorsToMonitor, 
            string rangeType, 
            int lightMinMaxThresholdValue, 
            int temperatureMinMaxThresholdValue, 
            int timeToMonitor)
        {
            int secondsElapsed = 0;
            bool lightThresholdExceeded = false;
            bool temperatureThresholdExceeded = false;
            int currentLightSensorValue = 0;
            double currentTemperature = 0;


            DisplayScreenHeader("Set Light and Temperature Alarm");

            Console.WriteLine($"\tSensors to monitor {sensorsToMonitor}");
            Console.WriteLine("\tRange Type: {0}", rangeType);
            Console.WriteLine("\tLight min/max Threshold value: " + lightMinMaxThresholdValue);
            Console.WriteLine("\tTemperature min/max Threshold value: " + temperatureMinMaxThresholdValue);
            Console.WriteLine($"\tTime to Monitor: {timeToMonitor}");
            Console.WriteLine();

            Console.WriteLine("Press any key to begin monitoring!");
            Console.ReadKey();
            Console.WriteLine();

            while ((secondsElapsed < timeToMonitor) && !lightThresholdExceeded && !temperatureThresholdExceeded)
            {
                currentTemperature = ConvertToFarenheit(finchRobot.getTemperature());
                switch (sensorsToMonitor)
                {
                    case "left":
                        currentLightSensorValue = finchRobot.getLeftLightSensor();
                        break;
                    case "right":
                        currentLightSensorValue = finchRobot.getRightLightSensor();
                        break;
                    case "both":
                        currentLightSensorValue = (finchRobot.getRightLightSensor() + finchRobot.getLeftLightSensor()) / 2;
                        break;
                }
                switch (rangeType)
                {
                    case "minimum":
                        if (currentLightSensorValue < lightMinMaxThresholdValue)
                        {
                            lightThresholdExceeded = true;
                        }
                        break;
                    case "maximum":
                        if (currentLightSensorValue > lightMinMaxThresholdValue)
                        {
                            lightThresholdExceeded = true;
                        }
                        break;
                }
               switch (rangeType)
                {
                    case "minimum":
                       if (currentTemperature < temperatureMinMaxThresholdValue)
                        {
                            temperatureThresholdExceeded = true;
                        }
                        break;
                    case "maximum":
                        if (currentTemperature > temperatureMinMaxThresholdValue)
                        {
                            temperatureThresholdExceeded = true;
                        }
                        break;
            }
                finchRobot.wait(1000);
                secondsElapsed++;
            }

            if (lightThresholdExceeded || temperatureThresholdExceeded)
            {
                finchRobot.noteOn(700);
                Console.WriteLine($"The {rangeType} threshold value(s) of {lightMinMaxThresholdValue} or {temperatureMinMaxThresholdValue} were exceeded by the current light sensor value(s) of {currentLightSensorValue} or {currentTemperature}.");
                DisplayContinuePrompt();
                finchRobot.noteOff();
            }
            else
            {
                Console.WriteLine($"The {rangeType} threshold value of {lightMinMaxThresholdValue} was  not exceeded by the current light sensor value of {currentLightSensorValue}.");
            }
            DisplayMenuPrompt("Light and Temperature Alarm");
        }

        private static void TemperatureAlarmSetAlarm
            (Finch finchRobot,
            string rangeType,
            int temperatureMinMaxThresholdValue,
            int timeToMonitor)
        {
            int secondsElapsed = 0;
            bool thresholdExceeded = false;
            double currentTemperature = 0;

            DisplayScreenHeader("Set Light Alarm");

            Console.WriteLine("\tRange Type: {0}", rangeType);
            Console.WriteLine("\tmin/max Threshold value: " + temperatureMinMaxThresholdValue);
            Console.WriteLine($"\tTime to Monitor: {timeToMonitor}");
            Console.WriteLine();

            Console.WriteLine("Press any key to begin monitoring!");
            Console.ReadKey();
            Console.WriteLine();

            while ((secondsElapsed < timeToMonitor) && !thresholdExceeded)
            {
                currentTemperature = ConvertToFarenheit(finchRobot.getTemperature());

                switch (rangeType)
                {
                    case "minimum":
                        if (currentTemperature < temperatureMinMaxThresholdValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;
                    case "maximum":
                        if (currentTemperature > temperatureMinMaxThresholdValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;
                }
                finchRobot.wait(1000);
                secondsElapsed++;
            }

            if (thresholdExceeded)
            {
                finchRobot.noteOn(700);
                Console.WriteLine($"The {rangeType} threshold value of {temperatureMinMaxThresholdValue} was exceeded by the current temperature value of {currentTemperature}.");
                DisplayContinuePrompt();
                finchRobot.noteOff();
            }
            else
            {
                Console.WriteLine($"The {rangeType} threshold value of {temperatureMinMaxThresholdValue} was  not exceeded by the current temperature value of {currentTemperature}.");
            }
            DisplayMenuPrompt("Light and Temperature Alarm");
        }



        private static void LightAlarmSetAlarm
            (Finch finchRobot, 
            string sensorsToMonitor, 
            string rangeType, 
            int lightMinMaxThresholdValue, 
            int timeToMonitor)
        {
            int secondsElapsed = 0;
            bool thresholdExceeded = false;
            int currentLightSensorValue = 0;

            DisplayScreenHeader("Set Light Alarm");

            Console.WriteLine($"\tSensors to monitor {sensorsToMonitor}");
            Console.WriteLine("\tRange Type: {0}", rangeType);
            Console.WriteLine("\tmin/max Threshold value: " + lightMinMaxThresholdValue);
            Console.WriteLine($"\tTime to Monitor: {timeToMonitor}");
            Console.WriteLine();

            Console.WriteLine("Press any key to begin monitoring!");
            DisplayContinuePrompt();
           
            while ((secondsElapsed < timeToMonitor) && !thresholdExceeded)
            {
                switch (sensorsToMonitor)
                {
                    case "left":
                        currentLightSensorValue = finchRobot.getLeftLightSensor();
                        break;
                    case "right":
                        currentLightSensorValue = finchRobot.getRightLightSensor();
                        break;
                    case "both":
                        currentLightSensorValue = (finchRobot.getRightLightSensor() + finchRobot.getLeftLightSensor()) / 2;
                        break;
                }
                switch (rangeType)
                {
                    case "minimum":
                        if (currentLightSensorValue < lightMinMaxThresholdValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;
                    case "maximum":
                        if (currentLightSensorValue > lightMinMaxThresholdValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;
                }
                finchRobot.wait(1000);
                secondsElapsed++;
            }

            if (thresholdExceeded)
            {
                finchRobot.noteOn(700);
                Console.WriteLine($"The {rangeType} threshold value of {lightMinMaxThresholdValue} was exceeded by the current light sensor value of {currentLightSensorValue}.");
                DisplayContinuePrompt();
                finchRobot.noteOff();                
            }
            else
            {
                Console.WriteLine($"The {rangeType} threshold value of {lightMinMaxThresholdValue} was  not exceeded by the current light sensor value of {currentLightSensorValue}.");
            }
            DisplayMenuPrompt("Light and Temperature Alarm");
        }

        private static int LightAlarmSetTimeToMonitor()
        {
            //int minMaxThresholdValue;

            DisplayScreenHeader("Setting the time to Monitor!");

            Console.WriteLine($"Enter the time you wish to monitor");
            Console.WriteLine();

            int TimeToMonitor = IsValidInt("Please enter the time you wish to monitor in whole numbers.");
            Console.WriteLine($"The value you have entered for time to monitor is {TimeToMonitor}");

            DisplayMenuPrompt("Light and Temperature Alarm");

            return TimeToMonitor;
        }
        private static int LightAlarmSetMinMaxThresholdValue(string rangeType, Finch finchRobot)
        {
            //int minMaxThresholdValue;

            DisplayScreenHeader("Light Minimum/Maximum Threshold Value");

            Console.WriteLine($"Your Finch's Left light sensor value: {finchRobot.getLeftLightSensor()}");
            Console.WriteLine($"Your Finch's Right light sensor value: {finchRobot.getRightLightSensor()}");
            Console.WriteLine();

            //Console.Write("Enter the {} light sensor value:");
            // int.TryParse(Console.ReadLine(), out minMaxThresholdValue);
            int lightMinMaxThresholdValue = IsValidInt($"Enter the {rangeType} light sensor value:");
            Console.WriteLine($"The value you have entered for the light sensor is {lightMinMaxThresholdValue}");

            DisplayMenuPrompt("Light and Temperature Alarm");

            return lightMinMaxThresholdValue;
        }
        private static int TemperatureAlarmSetMinMaxThresholdValue(string rangeType, Finch finchRobot)
        {
            //int minMaxThresholdValue;

            DisplayScreenHeader("Temperature Minimum/Maximum Threshold Value");

            Console.WriteLine($"Your Finch's current temperature is: {ConvertToFarenheit(finchRobot.getTemperature())}");
            Console.WriteLine();

            //Console.Write("Enter the {} light sensor value:");
            // int.TryParse(Console.ReadLine(), out minMaxThresholdValue);
            int temperatureMinMaxThresholdValue = IsValidInt($"Enter the {rangeType} temperature value:");
            Console.WriteLine($"The value you have entered for the temperature threshold value is {temperatureMinMaxThresholdValue}");

            DisplayMenuPrompt("Light and Temperature Alarm");

            return temperatureMinMaxThresholdValue;
        }

        static string LightAlarmDisplaySetSensorsToMonitor()
        {

            
                string sensorsToMonitor;

                DisplayScreenHeader("Sensors to Monitor");

                Console.Write("Which Sensors would you like to Monitor? (left, right, or both)");
                sensorsToMonitor = Console.ReadLine().ToLower();
                if (sensorsToMonitor == "left" || sensorsToMonitor == "right" || sensorsToMonitor == "both")
                {
                    Console.WriteLine($"The sensor(s) you have chosen is {sensorsToMonitor}");
                }
                else
                {
                    Console.WriteLine("That input is invalid. Please enter left, right, or both");
                    LightAlarmDisplaySetSensorsToMonitor();
                }
                DisplayContinuePrompt();
                return sensorsToMonitor;
           
        }              
        static string LightAlarmDisplaySetRangeType()
        {
            string rangeType;

            DisplayScreenHeader("Range Type");

            Console.Write("\tRange Type [minimum, maximum]:");
            rangeType = Console.ReadLine();
            
            if (rangeType == "minimum" || rangeType == "maximum")
            {
                Console.WriteLine($"You have chosen {rangeType} as your range type");
            }
            else
            {
                Console.WriteLine("That input is not a range type that is known to man. Please enter maximum or minimum.");
                Console.ReadKey();
                LightAlarmDisplaySetRangeType();
            }

            DisplayMenuPrompt("Light and Temperature Alarm");

            return rangeType;
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
            string menuChoice;
            bool quitMenu = false;

            (int motorSpeed, int ledBrightness, double waitSeconds, int hertz) commandParameters;
            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waitSeconds = 0;
            commandParameters.hertz = 0;


            List<Command> commands = new List<Command>();

            do
            {
                DisplayScreenHeader("User Programming Menu");

                Console.WriteLine("\ta) Set Command Parameters");
                Console.WriteLine("\tb) Add Commands");
                Console.WriteLine("\tc) View Commands");
                Console.WriteLine("\td) Execute Commands");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();


                switch (menuChoice)
                {
                    case "a":
                        commandParameters = UserProgrammingDisplayGetCommandParameters();
                        break;
                    case "b":
                        UserProgrammingDisplayGetFinchCommands(commands);
                        break;

                    case "c":
                        UserProgrammingDisplayFinchCommands(commands);
                        break;

                    case "d":
                        UserProgrammingDisplayExecuteFinchCommands(finchRobot, commands, commandParameters);
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

        private static void UserProgrammingDisplayExecuteFinchCommands(Finch finchRobot, List<Command> commands, (int motorSpeed, int ledBrightness, double waitSeconds, int hertz) commandParameters)
        {
            int motorSpeed = commandParameters.motorSpeed;
            int ledBrightness = commandParameters.ledBrightness;
            int waitMilliSeconds = (int)(commandParameters.waitSeconds * 1000);
            int hertz = commandParameters.hertz;
            string commandFeedback = "";
            const int TURNING_MOTOR_SPEED = 100;

            DisplayScreenHeader("Execute Finch Commands");

            Console.WriteLine("\tThe Finch Robot will now display it's obedience through your commands!");
            DisplayContinuePrompt();

            foreach (Command command in commands)
            {
                switch (command)
                {
                    case Command.NONE:
                        break;
                    case Command.MOVEFORWARD:
                        finchRobot.setMotors(motorSpeed, motorSpeed);
                        commandFeedback = Command.MOVEFORWARD.ToString();
                        break;
                    case Command.MOVEBACKWARD:
                        finchRobot.setMotors(-motorSpeed, -motorSpeed);
                        commandFeedback = Command.MOVEBACKWARD.ToString();                            
                        break;
                    case Command.STOPMOTORS:
                        finchRobot.setMotors(0, 0);
                        commandFeedback = Command.STOPMOTORS.ToString();
                        break;
                    case Command.WAIT:
                        finchRobot.wait(waitMilliSeconds);
                        commandFeedback = Command.WAIT.ToString();
                        break;
                    case Command.TURNRIGHT:
                        finchRobot.setMotors(TURNING_MOTOR_SPEED, -TURNING_MOTOR_SPEED);
                        commandFeedback = Command.TURNRIGHT.ToString();
                        break;
                    case Command.TURNLEFT:
                        finchRobot.setMotors(-TURNING_MOTOR_SPEED, TURNING_MOTOR_SPEED);
                        commandFeedback = Command.TURNLEFT.ToString();
                        break;
                    case Command.LEDON:
                        finchRobot.setLED(ledBrightness, ledBrightness, ledBrightness);
                        commandFeedback = Command.LEDON.ToString();
                        break;
                    case Command.LEDOFF:
                        finchRobot.setLED(0, 0, 0);
                        commandFeedback = Command.LEDOFF.ToString();
                        break;
                    case Command.GETTEMPERATURE:
                        commandFeedback = $"Temperature: {finchRobot.getTemperature().ToString("n2")}\n";
                        break;
                    case Command.GETLIGHTSENSORS:
                        commandFeedback = $"Light sensors: {finchRobot.getRightLightSensor().ToString("n2")}\n";
                        commandFeedback = $"Light sensors: {finchRobot.getLeftLightSensor().ToString("n2")}\n";
                        break;
                    case Command.DANCE:
                        finchRobot.noteOn(hertz);
                        finchRobot.setLED(ledBrightness, ledBrightness, ledBrightness);
                        finchRobot.setMotors(motorSpeed, motorSpeed);
                        commandFeedback = Command.DANCE.ToString();
                        break;
                    case Command.STOPDANCE:
                        finchRobot.noteOff();
                        finchRobot.setLED(0, 0, 0);
                        finchRobot.setMotors(0, 0);
                        commandFeedback = Command.STOPDANCE.ToString();
                        break;
                    case Command.DONE:
                        commandFeedback = Command.DONE.ToString();
                        break;
                    default:

                        break;
                }
                Console.WriteLine($"\t{commandFeedback}");
            }
        }

        private static void UserProgrammingDisplayFinchCommands(List<Command> commands)
        {
            DisplayScreenHeader("Finch Robot Commands");

            foreach (Command command in commands)
            {
                Console.WriteLine($"\t{command}");
            }

            DisplayMenuPrompt("User Programming");
        }

        private static void UserProgrammingDisplayGetFinchCommands(List<Command> commands)
        {
            Command command = Command.NONE;

            DisplayScreenHeader("Finch Robot Commands");

            //
            //      list commands
            //
            int commandCount = 1;
            Console.WriteLine("\tList of valid Commands");
            Console.WriteLine();
            Console.Write("\t-");
            foreach(string commandName in Enum.GetNames(typeof(Command)))
            {
                Console.Write($"- {commandName.ToLower()} -");
                if (commandCount % 5 == 0) Console.Write("-\n\t-");
                commandCount++;
            }
            Console.WriteLine();

            while (command != Command.DONE)
            {
                Console.Write("\tEnter Command:");

                if (Enum.TryParse(Console.ReadLine().ToUpper(), out command))
                {
                    commands.Add(command);
                }
                else
                {
                    Console.WriteLine("\t\t*************************************************");
                    Console.WriteLine("\t\tPlease enter a valid command from the list above.");
                    Console.WriteLine("\t\t*************************************************");
                }
            }

            DisplayMenuPrompt("User Programming");
        }

        private static (int motorSpeed, int ledBrightness, double waitSeconds, int hertz) UserProgrammingDisplayGetCommandParameters()
        {
            DisplayScreenHeader("Command Parameters");

            (int motorSpeed, int ledBrightness, double waitSeconds, int hertz) commandParameters;
            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waitSeconds = 0;
            commandParameters.hertz = 0;

            commandParameters.motorSpeed = IsValidIntRange("\tPlease enter a Motor speed Between 1 and 255. [1 = slow, 255 = fast]:  ", 1, 255);
            commandParameters.ledBrightness = IsValidIntRange("\tPlease enter an LED brightness Between 1 and 255. [1 = Dim, 255 = Bright]:  ", 1, 255);
            commandParameters.waitSeconds = IsValidDoubleRange("\tEnter the amount of seconds you wish to wait between 1 and 10:  ", 1, 10);
            commandParameters.hertz = IsValidIntRange("\tEnter The hertz value you wish the finch to sound ranging 0 to 22000 (0 is off and you most likely will not hear anything above 18000)", 0, 22000);

            Console.WriteLine();
            Console.WriteLine($"\tMotor Speed indicated: {commandParameters.motorSpeed}");
            Console.WriteLine($"\tLed Brightness indicated:  {commandParameters.ledBrightness}");
            Console.WriteLine($"\tWait in seconds indicated:  {commandParameters.waitSeconds}");
            Console.WriteLine($"\tHrequency requested by the user:  {commandParameters.hertz}");

            DisplayMenuPrompt("User Programming");


            return commandParameters;
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
            while (!IsValidInt)
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
        static int IsValidIntRange(string question, int min = 0, int max = 100000)
        {
            bool IsValidInt = false;
            int x = 0;
            while (!IsValidInt)
            {
                Console.Write($"{question}");
                IsValidInt = int.TryParse(Console.ReadLine(), out x);
                if (!IsValidInt || x < min || x > max)
                {
                    Console.WriteLine("That response is invalid. Please try again.");
                    IsValidInt = false;
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
                IsValidDouble = double.TryParse(Console.ReadLine(), out x);
                if (!IsValidDouble)
                {
                    Console.WriteLine("That response is invalid. Please try again.");
                }

            }
            return x;
        }
        static double IsValidDoubleRange(string question, double min = 0, double max = 100000)
        {
            bool IsValidDouble = false;
            double x = 0;
            while (!IsValidDouble)
            {
                Console.Write($"{question}");
                IsValidDouble = double.TryParse(Console.ReadLine(), out x);
                if (!IsValidDouble || x < min || x > max)
                {
                    Console.WriteLine("That response is invalid. Please try again.");
                    IsValidDouble = false;
                }

            }
            return x;
        }


            //
            // get a user response to validate the finch has connected using that input
            //
            //
            static int ValidateConnection()
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
