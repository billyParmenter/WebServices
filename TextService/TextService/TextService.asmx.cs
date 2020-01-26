/*
* FILE          : TextService.cs
* PROJECT       : Service Oriented Architecture - Assignment 3
* PROGRAMMER    : Billy Parmenter
* FIRST VERSION : October 12, 2019
*/



using System.ServiceModel;
using System.Web.Services;



namespace TextService
{

    /*
     * NAME    : TextService
     * PURPOSE : This web service checks the parameters, converts 
     *              the string and then returns the converted string
     */
    [WebService(Description = "Converts cases", Name = "TextService", Namespace = "TextService")]
    public class TextService : ITextService
    {
        public Logging logger = new Logging();



        /*
         * FUNCTION    : Case
         * DESCRIPTION : This web service checks the parameters, converts 
         *                  the string and then returns the converted string
         * PARAMETERS  : incoming : string : the given string to convert
         *               flag     : int    : the given flag for converting
         * RETURNS     : string : the converted string
         */
        [WebMethod(MessageName = "Case", Description = "Convert text to upper or lower case.")]
        public string Case(string incoming, uint flag)
        {
            logger.Log(LoggingInfo.ErrorLevel.INFO, "Request received - incoming: " + incoming + " - flag: " + flag.ToString());

            ParameterCheck(incoming, flag);

            string outgoing = CaseConvert(incoming, flag);

            logger.Log(LoggingInfo.ErrorLevel.INFO, "Replying to client - incoming: " + incoming + " - flag: " + flag.ToString() + " - outgoing: " + outgoing);

            return outgoing;
        }





        /*
         * FUNCTION    : ThrowException
         * DESCRIPTION : Throws a parameterInvalid exception with a given 
         *                  message and loggs the exception
         * PARAMETERS  : message : The exceptions message
         * RETURNS     : NONE
         */
        private void ThrowException(string message)
        {
            ParameterInvalid parameterInvalid = new ParameterInvalid
            {
                Message = message,
            };

            FaultException<ParameterInvalid> fault = new FaultException<ParameterInvalid>(parameterInvalid, parameterInvalid.Message);

            logger.Log(LoggingInfo.ErrorLevel.FATAL, fault.Message);

            throw fault;
        }





        /*
         * FUNCTION    : CaseConvert
         * DESCRIPTION : Converts the case of the given string based on the given flag
         * PARAMETERS  : incoming : string : the given string to convert
         *               flag     : int    : the given flag for converting
         * RETURNS     : string : the converted string
         */
        public string CaseConvert(string incoming, uint flag)
        {

            string outgoing = "";

            switch (flag)
            {
                case 1:

                    outgoing = incoming.ToUpper();
                    logger.Log(LoggingInfo.ErrorLevel.INFO, "Converting incoming to uppercase - incoming: " + incoming + " - outging: " + outgoing);
                    break;

                case 2:

                    outgoing = incoming.ToLower();
                    logger.Log(LoggingInfo.ErrorLevel.INFO, "Converting incoming to lowerercase - incoming: " + incoming + " - outging: " + outgoing);
                    break;
            }

            return outgoing;
        }





        /*
         * FUNCTION    : ParameterCheck
         * DESCRIPTION : Checks to see if the given parameters are valid
         * PARAMETERS  : incoming : string : the given string to convert
         *               flag     : int    : the given flag for converting
         * RETURNS     : NONE
         */
        public void ParameterCheck(string incoming, uint flag)
        {

            if (string.IsNullOrEmpty(incoming))
            {
                ThrowException("The given string was null or empty");
            }
            else if (flag < 1 || flag > 2)
            {
                ThrowException("The given flag was not 1 or 2 - Given flag: " + flag.ToString());
            }

        }
    }
}
 