using System.Collections.Generic;
using System.Net;
using _17DeleteMethodValidationHt.Arguments.Holders;
using _17DeleteMethodValidationHt.Arguments.Providers;
using _17DeleteMethodValidationHt.Consts;
using NUnit.Framework;
using RestSharp;

namespace _17DeleteMethodValidationHt.Tests.Create
{
    public class CreateBoardValidationTest : BaseTest
    {
        [Test]
        [TestCaseSource(typeof(CardBodyValidationArgumentsProvider))]
        public void CheckCreateCardWithInvalidName(CardBodyValidationArgumentsHolder validationArguments)
        {
            var request = RequestWithAuth(CardsEndpoints.CreateCardUrl)
                .AddJsonBody(validationArguments.BodyParams);
            var response = _client.Post(request);
            
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(validationArguments.ErrorMessage, response.Content);
        }

        [Test]
        [TestCaseSource(typeof(AuthValidationArgumentsProvider))]
        public void CheckCreateCardWithInvalidAuth(AuthValidationArgumentsHolder validationArguments)
        {
            var request = RequestWithoutAuth(CardsEndpoints.CreateCardUrl)
                .AddOrUpdateParameters(validationArguments.AuthParams)
                .AddJsonBody(new Dictionary<string, string>
                {
                    {"name", "New item"},
                    {"idList", UrlParamValues.ExistingListId}
                });
            var response = _client.Post(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.AreEqual(validationArguments.ErrorMessage, response.Content);
        }

        [Test]
        public void CheckCreateCardWithAnotherUserCredentials()
        {
            var request = RequestWithoutAuth(CardsEndpoints.CreateCardUrl)
                .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
                .AddJsonBody(new Dictionary<string, string>
                {
                    {"name", "New item"},
                    {"idList", UrlParamValues.ExistingListId}
                });
            var response = _client.Post(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.AreEqual("invalid token", response.Content);
        }
    }
}