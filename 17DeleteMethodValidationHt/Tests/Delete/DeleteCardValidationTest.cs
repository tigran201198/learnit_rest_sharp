using System.Net;
using _17DeleteMethodValidationHt.Arguments.Holders;
using _17DeleteMethodValidationHt.Arguments.Providers;
using _17DeleteMethodValidationHt.Consts;
using NUnit.Framework;
using RestSharp;

namespace _17DeleteMethodValidationHt.Tests.Delete
{
    public class DeleteCardValidationTest : BaseTest
    {
        [Test]
        [TestCaseSource(typeof(CardIdValidationArgumentsProvider))]
        public void CheckDeleteCardWithInvalidId(CardIdValidationArgumentsHolder validationArguments)
        {
            var request = RequestWithAuth(CardsEndpoints.DeleteCardUrl)
                .AddOrUpdateParameters(validationArguments.PathParams);
            var response = _client.Delete(request);
            Assert.AreEqual(validationArguments.StatusCode, response.StatusCode);
            Assert.AreEqual(validationArguments.ErrorMessage, response.Content);
        }

        [Test]
        [TestCaseSource(typeof(AuthValidationArgumentsProvider))]
        public void CheckDeleteCardWithInvalidAuth(AuthValidationArgumentsHolder validationArguments)
        {
            var request = RequestWithoutAuth(CardsEndpoints.DeleteCardUrl)
                .AddOrUpdateParameters(validationArguments.AuthParams)
                .AddUrlSegment("id", UrlParamValues.ExistingCardId);
            var response = _client.Delete(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.AreEqual(validationArguments.ErrorMessage, response.Content);
        }

        [Test]
        public void CheckDeleteCardWithAnotherUserCredentials()
        {
            var request = RequestWithoutAuth(CardsEndpoints.DeleteCardUrl)
                .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
                .AddUrlSegment("id", UrlParamValues.ExistingCardId);
            var response = _client.Delete(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.AreEqual("invalid token", response.Content);
        }
    }
}