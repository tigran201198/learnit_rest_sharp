using System.Collections.Generic;
using System.Net;
using _16DeleteMethod.Arguments.Holders;
using _16DeleteMethod.Arguments.Providers;
using _16DeleteMethod.Consts;
using NUnit.Framework;
using RestSharp;

namespace _16DeleteMethod.Tests.Create
{
    public class CreateBoardValidationTest : BaseTest
    {
        [Test]
        [TestCaseSource(typeof(BoardNameValidationArgumentsProvider))]
        public void CheckCreateBoardWithInvalidName(IDictionary<string, object> bodyParams)
        {
            var request = RequestWithAuth(BoardsEndpoints.CreateBoardUrl)
                .AddJsonBody(bodyParams);
            var response = _client.Post(request);
            
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("invalid value for name", response.Content);
        }

        [Test]
        [TestCaseSource(typeof(AuthValidationArgumentsProvider))]
        public void CheckGetBoardWithInvalidAuth(AuthValidationArgumentsHolder validationArguments)
        {
            var request = RequestWithoutAuth(BoardsEndpoints.CreateBoardUrl)
                .AddOrUpdateParameters(validationArguments.AuthParams)
                .AddJsonBody(new Dictionary<string, string> {{"name", "New item"}});
            var response = _client.Post(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.AreEqual(validationArguments.ErrorMessage, response.Content);
        }
    }
}