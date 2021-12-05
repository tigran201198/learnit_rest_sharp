﻿using System;
using System.Collections;
using _17DeleteMethodValidation.Arguments.Holders;
using _17DeleteMethodValidation.Consts;
using RestSharp;

namespace _17DeleteMethodValidation.Arguments.Providers
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