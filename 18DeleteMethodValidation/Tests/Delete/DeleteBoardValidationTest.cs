using System.Net;
using System.Threading.Tasks;
using _17DeleteMethodValidation.Arguments.Holders;
using _17DeleteMethodValidation.Arguments.Providers;
using _17DeleteMethodValidation.Consts;
using NUnit.Framework;
using RestSharp;

namespace _17DeleteMethodValidation.Tests.Delete
{
    public class GetDeleteValidationTest : BaseTest
    {
        [Test]
        [TestCaseSource(typeof(BoardIdValidationArgumentsProvider))]
        public async Task CheckDeleteBoardWithInvalidId(BoardIdValidationArgumentsHolder validationArguments)
        {
            var request = RequestWithAuth(BoardsEndpoints.DeleteBoardUrl, Method.Delete)
                .AddOrUpdateParameters(validationArguments.PathParams);
            var response = await _client.ExecuteAsync(request);
            Assert.AreEqual(validationArguments.StatusCode, response.StatusCode);
            Assert.AreEqual(validationArguments.ErrorMessage, response.Content);
        }

        [Test]
        [TestCaseSource(typeof(AuthValidationArgumentsProvider))]
        public async Task CheckDeleteBoardWithInvalidAuth(AuthValidationArgumentsHolder validationArguments)
        {
            var request = RequestWithoutAuth(BoardsEndpoints.DeleteBoardUrl, Method.Delete)
                .AddUrlSegment("id", UrlParamValues.ExistingBoardId)
                .AddOrUpdateParameters(validationArguments.AuthParams);
            var response = await _client.ExecuteAsync(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.AreEqual(validationArguments.ErrorMessage, response.Content);
        }

        [Test]
        public async Task CheckDeleteBoardWithAnotherUserCredentials()
        {
            var request = RequestWithoutAuth(BoardsEndpoints.DeleteBoardUrl, Method.Delete)
                .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
                .AddUrlSegment("id", UrlParamValues.ExistingBoardId);
            var response = await _client.ExecuteAsync(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.AreEqual("invalid token", response.Content);
        }
    }
}