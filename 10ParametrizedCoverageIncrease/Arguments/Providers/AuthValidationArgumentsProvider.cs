using System;
using System.Collections;
using _10ParametrizedCoverageIncrease.Arguments.Holders;
using RestSharp;

namespace _10ParametrizedCoverageIncrease.Arguments.Providers
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
                        new Parameter("key", "fb04999a731923c2e3137153b1ad5de0", ParameterType.QueryString)
                    }
                }
            };
            yield return new object[]
            {
                new AuthValidationArgumentsHolder
                {
                    AuthParams = new[]
                    {
                        new Parameter("token", "b73120fb537fceb444050a2a4c08e2f96f47389931bd80253d2440708f2a57e1", ParameterType.QueryString)
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