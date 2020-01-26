/*
 *  FILE : ServiceRequest.cs
 *  PROJECT : SOA_A02
 *  PROGRAMMER : Billy Parmenter, Michael Ramoutsakis
 *  DATE : September 18, 2019
 *  
 */

using System;
using System.IO;
using System.Net;
using System.Xml;


namespace ResolveIP
{
    /*
    * NAME : ServiceRequest
    * PURPOSE : This class represents a web service request, it will format the request bassed on the service, method, and parameters. It then
    *               send the request and formats the request
    */
    class ServiceRequest
    {
        string ip;


        // FUNCTION : ServiceRequest
        // DESCRIPTION :
        //      The constructor
        // PARAMETERS :
        //      None
        // RETURNS :
        //      None
        public ServiceRequest()
        {
        }





        // FUNCTION : CallWebService
        // DESCRIPTION :
        //      Sends the request and gets the response
        // PARAMETERS :
        //      Service - service, the service to send the request to
        //      ServiceMethod - method, the method to request
        // RETURNS :
        //      string - the response from the service
        public string CallWebService(string ip)
        {
            this.ip = ip;
            //create the request
            XmlDocument soapEnvelopeXml = CreateSoapEnvelope();
            HttpWebRequest webRequest = CreateWebRequest();
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            string soapResult;


            using (WebResponse webResponse = webRequest.GetResponse())
            {
                using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = reader.ReadToEnd();
                }
                return (soapResult);
            }

        }






        // FUNCTION : CreateWebRequest
        // DESCRIPTION :
        //      Starts the creation of the webrequest
        // PARAMETERS :
        //      string - url, the url to request form
        // RETURNS :
        //      HttpWebRequest - the created webRequest
        private HttpWebRequest CreateWebRequest()
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("http://ws.cdyne.com/ip2geo/ip2geo.asmx?WSDL");
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }





        // FUNCTION : CreateSoapEnvelope
        // DESCRIPTION :
        //      Creates and formats the XML request
        // PARAMETERS :
        //      Service - service, the service to send the request to
        //      ServiceMethod - method, the method to request
        // RETURNS :
        //      HttpWebRequest - the created webRequest
        private XmlDocument CreateSoapEnvelope()
        {
            XmlDocument soapEnvelope = new XmlDocument();

            string envelopeString = @"<soap:Envelope xmlns:soap=""http://www.w3.org/2003/05/soap-envelope"" xmlns:tns=""http://ws.cdyne.com/""><soap:Header/><soap:Body><tns:ResolveIP><tns:ipAddress>";
            envelopeString += ip;
            envelopeString += "</tns:ipAddress><tns:licenseKey>0</tns:licenseKey></tns:ResolveIP></soap:Body></soap:Envelope>";

            soapEnvelope.LoadXml(envelopeString);

            return soapEnvelope;
        }






        // FUNCTION : InsertSoapEnvelopeIntoWebRequest
        // DESCRIPTION :
        //      Creates and formats the XML request
        // PARAMETERS :
        //      XmlDocument - soapEnvelopeXml, the envelope to insert into
        //      HttpWebRequest - webRequest, the request to insert
        // RETURNS :
        //      NONE
        private void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }
    }



}

