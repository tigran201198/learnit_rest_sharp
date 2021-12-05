using System.Collections.Generic;
using RestSharp;

namespace _15PutMethodValidation.Arguments.Holders
{
    public class AuthValidationArgumentsHolder
    {
        public IEnumerable<Parameter> AuthParams { get; set; }

        public string ErrorMessage { get; set; }
    }
}