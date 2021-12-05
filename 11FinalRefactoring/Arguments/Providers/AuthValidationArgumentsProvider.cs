using System;
using System.Collections;
using _11FinalRefactoring.Arguments.Holders;
using _11FinalRefactoring.Consts;
using RestSharp;

namespace _11FinalRefactoring.Arguments.Providers
{
    public class AuthValidationArgumentsProvider : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new object[]
            {
                new AuthValidationArgumentsHolder
                {
                    AuthParams = new[]
                    {
                        new Parameter("key", UrlParamValues.ValidKey, ParameterType.QueryString)
                    }
                }
            };
            yield return new object[]
            {
                new AuthValidationArgumentsHolder
                {
                    AuthParams = new[]
                    {
                        new Parameter("token", UrlParamValues.ValidToken, ParameterType.QueryString)
                    }
                }
            };
            yield return new object[]
            {
                new AuthValidationArgumentsHolder
                {
                    AuthParams = ArraySegment<Parameter>.Empty
                }
            };
        }
    }
}