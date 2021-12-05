using System.Net;
using _11FinalRefactoringHt.Arguments.Holders;
using _11FinalRefactoringHt.Arguments.Providers;
using _11FinalRefactoringHt.Consts;
using NUnit.Framework;
using RestSharp;

namespace _11FinalRefactoringHt.Tests.Get
{
    public class GetCardsValidationTest : BaseTest
    {
        [Test]
        [TestCaseSource(typeof(CardIdValidationArgumentsProvider))]
        public void CheckGetCardWithInvalidId(CardIdValidationArgumentsHolder validationArguments)
        {
            var request = RequestWithAuth(CardsEndpoints.GetCardUrl)
                .AddOrUpdateParameters(validationArguments.PathParams);
            var response = _client.Get(request);
            Assert.AreEqual(validationArguments.StatusCode, response.StatusCode);
            Assert.AreEqual(validationArguments.ErrorMessage, response.Content);
        }

        [Test]
        [TestCaseSource(typeof(AuthValidationArgumentsProvider))]
        public void CheckGetCardWithInvalidAuth(AuthValidationArgumentsHolder validationArguments)
        {
            var request = RequestWithoutAuth(CardsEndpoints.GetCardUrl)
                .AddOrUpdateParameters(validationArguments.AuthParams)
                .AddUrlSegment("id", UrlParamValues.ExistingCardId);
            var response = _client.Get(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.AreEqual("unauthorized card permission requested", response.Content);
        }

        [Test]
        public void CheckGetCardWithAnotherUserCredentials()
        {
            var request = RequestWithoutAuth(CardsEndpoints.GetCardUrl)
                .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
                .AddUrlSegment("id", UrlParamValues.ExistingCardId);
            var response = _client.Get(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.AreEqual("invalid token", response.Content);
        }
    }
}