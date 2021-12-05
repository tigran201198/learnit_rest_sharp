using System.Collections.Generic;
using RestSharp;

namespace _11FinalRefactoring.Arguments.Holders
{
    public class AuthValidationArgumentsHolder
    {
        public IEnumerable<Parameter> AuthParams { get; set; }
    }
}