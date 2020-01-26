/*
* FILE          : ITextService.cs
* PROJECT       : Service Oriented Architecture - Assignment 3
* PROGRAMMER    : Billy Parmenter
* FIRST VERSION : October 12, 2019
*/



using System.Runtime.Serialization;
using System.ServiceModel;



namespace TextService
{
    [ServiceContract]
    public interface ITextService
    {
        [OperationContract]
        [FaultContract(typeof(ParameterInvalid))]
        string CaseConvert(string incoming, uint flag);
    }

    [DataContract]
    public class ParameterInvalid
    {

        [DataMember]
        public string Message { get; set; }
    }
}
