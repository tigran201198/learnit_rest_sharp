using System.Net;
using System.Threading.Tasks;
using _17DeleteMethodValidation.Arguments.Holders;
using _17DeleteMethodValidation.Arguments.Providers;
using _17DeleteMethodValidation.Consts;
using NUnit.Framework;
using RestSharp;

namespace _17DeleteMethodValidation.Tests.Get
{
    public class GetBoardsValidationTest : BaseTest
    {
        [Test]
        [TestCaseSource(typeof(BoardIdValidationArgumentsProvider))]
        public async Task CheckGetBoardWithInvalidId(BoardIdValidationArgumentsHolder validationArguments)
        {
            var request = RequestWithAuth(BoardsEndpoints.GetBoardUrl, Method.Get)
                .AddOrUpdateParameters(validationArguments.PathParams);
            var response = await _client.ExecuteAsync(request);
            Assert.AreEqual(validationArguments.StatusCode, response.StatusCode);
            Assert.AreEqual(validationArguments.ErrorMessage, response.Content);
        }

        [Test]
        [TestCaseSource(typeof(GetBoardsAuthValidationArgumentsProvider))]
        public async Task CheckGetBoardWithInvalidAuth(AuthValidationArgumentsHolder validationArguments)
        {
            var request = RequestWithoutAuth(BoardsEndpoints.GetBoardUrl, Method.Get)
                .AddUrlSegment("id", UrlParamValues.ExistingBoardId)
                .AddOrUpdateParameters(validationArguments.AuthParams);
            var response = await _client.ExecuteAsync(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.AreEqual(validationArguments.ErrorMessage, response.Content);
        }

        [Test]
        public async Task CheckGetBoardWithAnotherUserCredentials()
        {
            var request = RequestWithoutAuth(BoardsEndpoints.GetBoardUrl, Method.Get)
                .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
                .AddUrlSegment("id", UrlParamValues.ExistingBoardId);
            var response = await _client.ExecuteAsync(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.AreEqual("invalid token", response.Content);
        }
    }
}