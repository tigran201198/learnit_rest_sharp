using System.Net;
using _16DeleteMethodHt.Arguments.Holders;
using _16DeleteMethodHt.Arguments.Providers;
using _16DeleteMethodHt.Consts;
using NUnit.Framework;
using RestSharp;

namespace _16DeleteMethodHt.Tests.Update
{
    public class UpdateCardValidationTest : BaseTest
    {
        [Test]
        [TestCaseSource(typeof(CardIdValidationArgumentsProvider))]
        public void CheckUpdateCardWithInvalidId(CardIdValidationArgumentsHolder validationArguments)
        {
            var request = RequestWithAuth(CardsEndpoints.GetCardUrl)
                .AddOrUpdateParameters(validationArguments.PathParams);
            var response = _client.Put(request);
            Assert.AreEqual(validationArguments.StatusCode, response.StatusCode);
            Assert.AreEqual(validationArguments.ErrorMessage, response.Content);
        }

        [Test]
        [TestCaseSource(typeof(AuthValidationArgumentsProvider))]
        public void CheckUpdateCardWithInvalidAuth(AuthValidationArgumentsHolder validationArguments)
        {
            var request = RequestWithoutAuth(CardsEndpoints.GetCardUrl)
                .AddOrUpdateParameters(validationArguments.AuthParams)
                .AddUrlSegment("id", UrlParamValues.ExistingCardId);
            var response = _client.Put(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.AreEqual(validationArguments.ErrorMessage, response.Content);
        }

        [Test]
        public void CheckUpdateCardWithAnotherUserCredentials()
        {
            var request = RequestWithoutAuth(CardsEndpoints.GetCardUrl)
                .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
                .AddUrlSegment("id", UrlParamValues.ExistingCardId);
            var response = _client.Put(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.AreEqual("invalid token", response.Content);
        }
    }
}