using System.Collections.Generic;
using RestSharp;

namespace _16DeleteMethod.Arguments.Holders
{
    public class AuthValidationArgumentsHolder
    {
        public IEnumerable<Parameter> AuthParams { get; set; }

        public string ErrorMessage { get; set; }
    }
}