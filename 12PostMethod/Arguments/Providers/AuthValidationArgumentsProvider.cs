using System;
using System.Collections;
using _12PostMethod.Arguments.Holders;
using _12PostMethod.Consts;
using RestSharp;

namespace _12PostMethod.Arguments.Providers
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