/*
* FILE          : ResolveIP.cs
* PROJECT       : Service Oriented Architecture - Assignment 3
* PROGRAMMER    : Billy Parmenter
* FIRST VERSION : October 12, 2019
*/

using System;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Web.Services;
using System.Xml;

namespace ResolveIP
{

    /*
     * NAME    : ResolveIP
     * PURPOSE : This web service calls another service to get the 
     *              info of an ip address
     */
    public class ResolveIP : IResolveIP
    {
        IPInfo ipInfo;
        string ip;
        string serviceResponse;
        public Logging logger = new Logging();



        /*
         * FUNCTION    : GetInfo
         * DESCRIPTION : This web service calls another service to get the 
         *                  info of an ip address
         * PARAMETERS  : ipAddress : string : the given ip to look up
         * RETURNS     : IPInfo : the info for the ip address
         */
        [WebMethod]
        public IPInfo GetInfo(string ipAddress)
        {
            logger.Log(LoggingInfo.ErrorLevel.INFO, "Request received - ipAddress: " + ipAddress);
            ip = ipAddress;

            //Check the given ip
            ValidateIP();

            logger.Log(LoggingInfo.ErrorLevel.INFO, "IP valid, requesting information..");

            //Call ip2geo with the given ip
            CallService();

            logger.Log(LoggingInfo.ErrorLevel.INFO, "Info retrieved, formatting response");

            //format the service response string
            FormatResponse();

            logger.Log(LoggingInfo.ErrorLevel.INFO, "Replying to client");

            //return the info
            return ipInfo;
        }





        /*
         * FUNCTION    : ValidateIP
         * DESCRIPTION : Checks to see if the given ip is valid
         * PARAMETERS  : NONE
         * RETURNS     : NONE
         */
        private void ValidateIP()
        {
            string pattern = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";

            Regex check = new Regex(pattern);

            if (check.IsMatch(ip, 0) == false)
            {

                ValidationFault ipFault = new ValidationFault
                {
                    Message = "The IP given is invalid - Service was given: ",
                };

                logger.Log(LoggingInfo.ErrorLevel.FATAL, ipFault.Message + ip);

                throw new FaultException<ValidationFault>(ipFault, ipFault.Message + ip);
                
            }
        }





        /*
         * FUNCTION    : CallService
         * DESCRIPTION : Calls the secondary service to get the ip info
         * PARAMETERS  : NONE
         * RETURNS     : NONE
         */
        private void CallService()
        {
            ServiceRequest serviceRequest = new ServiceRequest();

            serviceResponse = serviceRequest.CallWebService(ip);
        }





        /*
         * FUNCTION    : FormatResponse
         * DESCRIPTION : Formats the response from the secondary service
         * PARAMETERS  : NONE
         * RETURNS     : NONE
         */
        private void FormatResponse()
        {

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(serviceResponse); // suppose that myXmlString contains "<Names>...</Names>"

            XmlNodeList xnList = xml.GetElementsByTagName("ResolveIPResult");

            ipInfo.City = xnList.Item(0)["City"].InnerText;
            ipInfo.StateProvince = xnList.Item(0)["StateProvince"].InnerText;
            ipInfo.Country = xnList.Item(0)["Country"].InnerText;
            ipInfo.Organization = xnList.Item(0)["Organization"].InnerText;
            ipInfo.Latitude = Decimal.Parse(xnList.Item(0)["Latitude"].InnerText);
            ipInfo.Longitude = Decimal.Parse(xnList.Item(0)["Longitude"].InnerText);

        }
    }
}
