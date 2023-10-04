using System.Net;
using System.Threading.Tasks;
using _17DeleteMethodValidation.Arguments.Holders;
using _17DeleteMethodValidation.Arguments.Providers;
using _17DeleteMethodValidation.Consts;
using NUnit.Framework;
using RestSharp;

namespace _17DeleteMethodValidation.Tests.Update
{
    public class UpdateBoardValidationTest : BaseTest
    {
        [Test]
        [TestCaseSource(typeof(BoardIdValidationArgumentsProvider))]
        public async Task CheckUpdateBoardWithInvalidId(BoardIdValidationArgumentsHolder validationArguments)
        {
            var request = RequestWithAuth(BoardsEndpoints.UpdateBoardUrl, Method.Put)
                .AddOrUpdateParameters(validationArguments.PathParams);
            var response = await _client.ExecuteAsync(request);
            Assert.AreEqual(validationArguments.StatusCode, response.StatusCode);
            Assert.AreEqual(validationArguments.ErrorMessage, response.Content);
        }

        [Test]
        [TestCaseSource(typeof(AuthValidationArgumentsProvider))]
        public async Task CheckUpdateBoardWithInvalidAuth(AuthValidationArgumentsHolder validationArguments)
        {
            var request = RequestWithoutAuth(BoardsEndpoints.UpdateBoardUrl, Method.Put)
                .AddUrlSegment("id", UrlParamValues.ExistingBoardId)
                .AddOrUpdateParameters(validationArguments.AuthParams);
            var response = await _client.ExecuteAsync(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.AreEqual(validationArguments.ErrorMessage, response.Content);
        }

        [Test]
        public async Task CheckUpdateBoardWithAnotherUserCredentials()
        {
            var request = RequestWithoutAuth(BoardsEndpoints.UpdateBoardUrl, Method.Put)
                .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
                .AddUrlSegment("id", UrlParamValues.ExistingBoardId);
            var response = await _client.ExecuteAsync(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.AreEqual("invalid token", response.Content);
        }
    }
}