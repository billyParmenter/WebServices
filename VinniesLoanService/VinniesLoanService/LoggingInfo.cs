/*
* FILE          : LoggingInfo.cs
* PROJECT       : SOA_A03
* PROGRAMMER    : Billy Parmenter, Michael Ramoutsakis
* FIRST VERSION : October 11, 2019
*/


namespace VinniesLoanService
{
    /**
      * NAME    : LoggingInfo
      * PURPOSE : The LoggingInfo class holds data members used in the LoggingClass
      */
    public class LoggingInfo
    {
        /// <summary>
        /// The file path for where to save the logs
        /// </summary>
        public const string logFilePath = @"C:\VinniesLoanService\LogFile.txt";


        /// <summary>
        /// The error levels of the logs
        /// </summary>
        public enum ErrorLevel
        {
            OFF,
            DEBUG,
            INFO,
            WARN,
            ERROR,
            FATAL,
            ALL
        }
    }
}

