using System.Collections.Generic;
using RestSharp;

namespace _14PutMethod.Arguments.Holders
{
    public class AuthValidationArgumentsHolder
    {
        public IEnumerable<Parameter> AuthParams { get; set; }

        public string ErrorMessage { get; set; }
    }
}