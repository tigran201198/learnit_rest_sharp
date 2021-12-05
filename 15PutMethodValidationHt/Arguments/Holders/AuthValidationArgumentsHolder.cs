using System.Collections.Generic;
using RestSharp;

namespace _15PutMethodValidationHt.Arguments.Holders
{
    public class AuthValidationArgumentsHolder
    {
        public IEnumerable<Parameter> AuthParams { get; set; }
        
        public string ErrorMessage { get; set; }
    }
}