using System.Collections.Generic;
using RestSharp;

namespace _12PostMethod.Arguments.Holders
{
    public class AuthValidationArgumentsHolder
    {
        public IEnumerable<Parameter> AuthParams { get; set; }
    }
}