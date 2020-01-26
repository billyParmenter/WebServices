/*
* FILE          : Logging.cs
* PROJECT       : SOA_A03
* PROGRAMMER    : Billy Parmenter, Michael Ramoutsakis
* FIRST VERSION : October 11, 2019
*/

using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace VinniesLoanService
{
    /**
      * NAME    : Logging
      * PURPOSE : The Logging class will log a message to a file that is defined in the 
      *             LoggingInfo class. It will log the ErrorLevel (see LoggingInfo.ErrorLevel),
      *             the date and time it was logged, the line in the file it was logged, 
      *             the method it was called in, the given message, and if present the exception
      *             caught.
      */
    public class Logging
    {
        //File to save logs to
        private readonly string logFile = "";

        //Max error level to log
        private readonly LoggingInfo.ErrorLevel MaxLevel = LoggingInfo.ErrorLevel.ALL;

        //Min error level to log
        private readonly LoggingInfo.ErrorLevel MinLevel = LoggingInfo.ErrorLevel.OFF;





        /**
          * FUNCTION    : Logging
          * DESCRIPTION : Initalizes a logging class with no parameters, 
          *                 the default max is ALL and min is OFF
          * PARAMETERS  : NONE
          * RETURNS     : NONE
          */
        public Logging()
        {
            //get the log file path from logging info
            logFile = LoggingInfo.logFilePath;
        }





        /**
          * FUNCTION    : Logging
          * DESCRIPTION : Initializes a logging class with two parameters 
          *                 only error levels equal to the max(first parameter) 
          *                 and min(second parameter) are logged. If the 
          *                 max is greater than the min then throw an exception
          * PARAMETERS  : LoggingInfo.ErrorLevel max : The max error level to be logged
          *               LoggingInfo.ErrorLevel min : The min error level to be logged
          * RETURNS     : NONE
          */
        public Logging(LoggingInfo.ErrorLevel max, LoggingInfo.ErrorLevel min)
        {
            if (max < min)
            {
                throw new System.ArgumentException("The first argument value cannot be greater than the second argument, " +
                    "see LoggingInfo for LoggingInfo.ErrorLevel values.");
            }

            logFile =  LoggingInfo.logFilePath;

            MaxLevel = max;

            MinLevel = min;
        }





        /**
          * FUNCTION    : Logging
          * DESCRIPTION : Initalizes a loggin class with one parameter, only that 
          *                 error level will be logged.
          * PARAMETERS  : LoggingInfo.ErrorLevel single : The only error level to be logged
          * RETURNS     : NONE
          */
        public Logging( LoggingInfo.ErrorLevel single)
        {
            logFile =  LoggingInfo.logFilePath;

            MaxLevel = single;

            MinLevel = single;
        }





        /**
          * FUNCTION    : CheckFile
          * DESCRIPTION : Check if the file path exists, if not then create it
          * PARAMETERS  : NONE
          * RETURNS     : NONE
          */
        private void CheckFile()
        {
            if (!File.Exists(logFile))
            {
                File.Create(logFile);
                Console.WriteLine("File created.");
            }
        }






        /**
          * FUNCTION    : Log
          * DESCRIPTION : Log when there is an exception and will not log if the message is blank or spaces.
          * PARAMETERS  : LoggingInfo.ErrorLevel errorLevel : The error level of the log to be logged
                          string message : The messeage of the log
                          Exception ex : The exception being logged
          * RETURNS     : NONE
          */
        public void Log( LoggingInfo.ErrorLevel errorLevel,
                        string message,
                        Exception ex)
        {
            if (string.IsNullOrEmpty(message) == false)
            {
                Log(errorLevel,
                    message + Environment.NewLine + "  Exception caught:" + Environment.NewLine + "   " + ex.ToString());
            }
        }






        /**
          * FUNCTION    : Log
          * DESCRIPTION : Log a message, and will not log if the message is blank or spaces.
          * PARAMETERS  : LoggingInfo.ErrorLevel errorLevel : The error level of the log to be logged
                          string message : The messeage of the log
          * RETURNS     : NONE
          */
        public void Log( LoggingInfo.ErrorLevel errorLevel,
                        string message)
        {
            if (string.IsNullOrEmpty(message) == false && CheckLevel(errorLevel) == true)
            {
                string logMessage = string.Format("{0} [{1}] - {2}" + Environment.NewLine + "\t{3}\n", GetErrorLevelString(errorLevel), DateTime.Now, message, GetStackTrace());
                SaveToFile(logMessage);
            }
        }





        /**
          * FUNCTION    : SaveToFile
          * DESCRIPTION : Save the message to the log file
          * PARAMETERS  : NONE
          * RETURNS     : NONE
          */
        private void SaveToFile(string logMessage)
        {
            using (StreamWriter sw = File.AppendText(logFile))
            {
                sw.WriteLine(logMessage);
            }
        }





        /**
          * FUNCTION    : CheckLevel
          * DESCRIPTION : Check that the error level is within the desired capture levels
          * PARAMETERS  : LoggingInfo.ErrorLevel level : The error level to be tested against
          *                 the max and min error levels. If the logs error level is ALL or
          *                 OFF then the method will throw an exception
          * RETURNS     : bool : true if the error level is between or equal to the max and min error levels
          *                    : false if it is outside the max and min values
          */
        private bool CheckLevel( LoggingInfo.ErrorLevel level)

        {
            bool returnValue = false;

            if (level ==  LoggingInfo.ErrorLevel.ALL || level ==  LoggingInfo.ErrorLevel.OFF)
            {
                throw new System.ArgumentException("A message being logged can not have an error level equal to OFF or ALL.");
            }

            else if (MinLevel <= level && level <= MaxLevel)
            {
                returnValue = true;
            }

            return returnValue;
        }



        /// <summary>
        /// Determines the stacktrace of the calling method's caller.
        /// </summary>
        /// <returns>The stacktrace</returns>
        private string GetStackTrace()
        {
            System.Diagnostics.StackFrame[] frame = new System.Diagnostics.StackTrace(true).GetFrames();

            int i = 0;

            while (i < frame.Length && frame[i].GetFileName().Contains("Logging.cs"))
            {
                i++;
            }

            return frame[i].ToString();
        }



        /**
          * FUNCTION    : GetErrorLevelString
          * DESCRIPTION : Used for generating the log message, gets a string for the corresponding error level
          * PARAMETERS  : LoggingInfo.ErrorLevel errorLevel : The error level that is to be converted to a string
          * RETURNS     : String : The string version of errorLevel
          */
        private string GetErrorLevelString( LoggingInfo.ErrorLevel errorLevel)
        {
            string returnString = "";

            int errorIntValue = (int)errorLevel;

            switch (errorIntValue)
            {
                case 1:
                    returnString = "DEBUG";
                    break;
                case 2:
                    returnString = "INFO ";
                    break;
                case 3:
                    returnString = "WARN ";
                    break;
                case 4:
                    returnString = "ERROR";
                    break;
                case 5:
                    returnString = "FATAL";
                    break;
            }

            return returnString;
        }
    }
}
