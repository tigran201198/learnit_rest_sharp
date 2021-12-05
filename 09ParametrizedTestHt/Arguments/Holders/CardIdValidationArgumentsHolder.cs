using System.Collections.Generic;
using System.Net;
using RestSharp;

namespace _09ParametrizedTestHt.Arguments.Holders
{
    public class CardIdValidationArgumentsHolder 
    {
        public IEnumerable<Parameter> PathParams { get; set; }
        
        public string ErrorMessage { get; set; }
        
        public HttpStatusCode StatusCode { get; set; }
    }
}