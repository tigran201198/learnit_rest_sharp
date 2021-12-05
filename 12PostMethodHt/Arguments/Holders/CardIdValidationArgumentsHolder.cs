using System.Collections.Generic;
using System.Net;
using RestSharp;

namespace _12PostMethodHt.Arguments.Holders
{
    public class CardIdValidationArgumentsHolder 
    {
        public IEnumerable<Parameter> PathParams { get; set; }
        
        public string ErrorMessage { get; set; }
        
        public HttpStatusCode StatusCode { get; set; }
    }
}