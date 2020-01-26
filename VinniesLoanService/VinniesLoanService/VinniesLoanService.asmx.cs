/*
* FILE          : VinniesLoanService.asmx.cs
* PROJECT       : SOA_A03
* PROGRAMMER    : Billy Parmenter, Michael Ramoutsakis
* FIRST VERSION : October 11, 2019
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace VinniesLoanService
{
    /// <summary>
    /// Calculates a car loan amount
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]


    /**
      * NAME    : VinniesLoanService
      * PURPOSE : VinniesLoanService class will hold all of the functionality for 
      *           the loan calculator which will be a published service. 
      */
    public class VinniesLoanService : System.Web.Services.WebService
    {
        //create an instance of the logger class
        public Logging logger = new Logging();

        [WebMethod(MessageName = "Loan", Description = "Calculate loan payment")]

        /**
          * FUNCTION    : Loan
          * DESCRIPTION : Loan function calls on all created functions to calculate
          *               the loan as well as checks for valid parameters and logs
          *               any faults/exceptions to a log file through the use of the
          *               Logger class functions. 
          * PARAMETERS  : float principle, float rate, int months
          * RETURNS     : float loan
          */
        public float Loan(float principle, float rate, int months)
        {
            float loan = 0;

            logger.Log(LoggingInfo.ErrorLevel.INFO, "Request Received - Principle: " + principle + " rate: " + rate + " months: " + months);

            string soapException = CheckAll(principle, rate, months);


            if (soapException == string.Empty)
            {
                loan = LoanPayment(principle, rate, months);
                logger.Log(LoggingInfo.ErrorLevel.INFO, "Calculating Loan Based on - Principle: " + principle + " rate: " + rate + " months: " + months);

            }
            else
            {
                logger.Log(LoggingInfo.ErrorLevel.FATAL, soapException);
                throw new SoapException(soapException, SoapException.ClientFaultCode);
            }


            logger.Log(LoggingInfo.ErrorLevel.INFO, "Replying to client - Loan: " + loan);
            return loan; 
        }

        /**
          * FUNCTION    : LoanPayment
          * DESCRIPTION : LoanPayment function calculates the monthly payments
          *               based on the user entered principle, rate and months
          * PARAMETERS  : float principle, float rate, int months
          * RETURNS     : float loanPayment
          */
        public float LoanPayment(float principle, float rate, int months)
        {
            float loanPayment = 0;
            float tempRate = (rate / 1200);
            double denominator = Math.Pow((1 + tempRate), months);

            loanPayment = ((tempRate + (tempRate / ((float)denominator - 1))) * principle);

            return loanPayment;
        }

        /**
          * FUNCTION    : CheckAll
          * DESCRIPTION : CheckAll function checks that all user entered
          *               parameters are valid and returns a string with the 
          *               error statement if any or all parameters are invalid
          * PARAMETERS  : float principle, float rate, int months
          * RETURNS     : string fault
          */
        public string CheckAll(float principle, float rate, float months)
        {
            string fault = string.Empty;

            if(CheckPrinciple(principle) == false)
            {
                fault += Environment.NewLine + "\t~principle cannot be less than or equal to 0~";
            }
            
            if(CheckRate(rate) == false)
            {
                fault += Environment.NewLine + "\t~rate      cannot be less than or equal to 0~";
            }

            if(CheckMonths(months) == false)
            {
                fault += Environment.NewLine + "\t~months    cannot be less than or equal to 0~";
            }

            return fault;
        }


        /**
          * FUNCTION    : CheckPrinciple
          * DESCRIPTION : CheckPrinciple function checks that user entered
          *               principle is valid and returns a boolean true or false 
          *               depending on the user entered value
          * PARAMETERS  : float principle
          * RETURNS     : bool true if valid, bool false if invalid
          */
        public bool CheckPrinciple(float principle)
        {
            if(principle <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /**
          * FUNCTION    : CheckRate
          * DESCRIPTION : CheckRate function checks that user entered
          *               rate is valid and returns a boolean true or false 
          *               depending on the user entered value
          * PARAMETERS  : float rate
          * RETURNS     : bool true if valid, bool false if invalid
          */
        public bool CheckRate(float rate)
        {
            if(rate <= 0 || rate > 100)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /**
          * FUNCTION    : CheckMonths
          * DESCRIPTION : CheckMonths function checks that user entered
          *               months is valid and returns a boolean true or false 
          *               depending on the user entered value
          * PARAMETERS  : float months
          * RETURNS     : bool true if valid, bool false if invalid
          */
        public bool CheckMonths(float months)
        {
            if (months <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
