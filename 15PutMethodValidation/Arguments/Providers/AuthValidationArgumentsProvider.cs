using System;
using System.Collections;
using _15PutMethodValidation.Arguments.Holders;
using _15PutMethodValidation.Consts;
using RestSharp;

namespace _15PutMethodValidation.Arguments.Providers
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