using System;
using System.Collections;
using _14PutMethod.Arguments.Holders;
using _14PutMethod.Consts;
using RestSharp;

namespace _14PutMethod.Arguments.Providers
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
                    },
                    ErrorMessage = "unauthorized permission requested"
                },
            };
            yield return new object[]
            {
                new AuthValidationArgumentsHolder
                {
                    AuthParams = new[]
                    {
                        new Parameter("token", UrlParamValues.ValidToken, ParameterType.QueryString)
                    },
                    ErrorMessage = "invalid key"
                }
            };
            yield return new object[]
            {
                new AuthValidationArgumentsHolder
                {
                    AuthParams = ArraySegment<Parameter>.Empty,
                    ErrorMessage = "invalid key"
                }
            };
        }
    }
}