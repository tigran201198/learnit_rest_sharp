using System;
using System.Collections;
using _17DeleteMethodValidation.Arguments.Holders;
using _17DeleteMethodValidation.Consts;
using RestSharp;

namespace _17DeleteMethodValidation.Arguments.Providers
{
    public class GetBoardsAuthValidationArgumentsProvider : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new object[]
            {
                new AuthValidationArgumentsHolder
                {
                    AuthParams = new[]
                    {
                        Parameter.CreateParameter("key", UrlParamValues.ValidKey, ParameterType.QueryString)
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
                        Parameter.CreateParameter("token", UrlParamValues.ValidToken, ParameterType.QueryString)
                    },
                    ErrorMessage = "invalid app key"
                }
            };
            yield return new object[]
            {
                new AuthValidationArgumentsHolder
                {
                    AuthParams = ArraySegment<Parameter>.Empty,
                    ErrorMessage = "unauthorized permission requested"
                }
            };
        }
    }
}