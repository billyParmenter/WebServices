/*
* FILE          : IResolveIP.cs
* PROJECT       : Service Oriented Architecture - Assignment 3
* PROGRAMMER    : Billy Parmenter
* FIRST VERSION : October 12, 2019
*/



using System.Runtime.Serialization;
using System.ServiceModel;



namespace ResolveIP
{
    public struct IPInfo
    {
        public string City;
        public string StateProvince;
        public string Country;
        public string Organization;
        public decimal Latitude;
        public decimal Longitude;
    }

    [ServiceContract(Namespace = "http://tempuri.org/")]
    public interface IResolveIP
    {
        [OperationContract]
        [FaultContract(typeof(ValidationFault))]
        IPInfo GetInfo(string ipAddress);
    }

    [DataContract]
    public class ValidationFault
    {

        [DataMember]
        public string Message { get; set; }
    }
}
