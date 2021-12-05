using System.Net;
using NUnit.Framework;
using RestSharp;

namespace _08BaseTest
{
    public class GetBoardsValidationTest : BaseTest
    {
        [Test]
        public void CheckGetBoardWithInvalidId()
        {
            var request = RequestWithAuth("/1/boards/{id}")
                .AddUrlSegment("id", "invalid");
            var response = _client.Get(request);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("invalid id", response.Content);
        }

        [Test]
        public void CheckGetBoardWithInvalidAuth()
        {
            var request = RequestWithoutAuth("/1/boards/{id}")
                .AddUrlSegment("id", "61fe437419cdd87656ce9fa6");
            var response = _client.Get(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.AreEqual("unauthorized permission requested", response.Content);
        }

        [Test]
        public void CheckGetBoardWithAnotherUserCredentials()
        {
            var request = RequestWithoutAuth("/1/boards/{id}")
                .AddQueryParameter("key", "8b32218e6887516d17c84253faf967b6")
                .AddQueryParameter("token", "492343b8106e7df3ebb7f01e219cbf32827c852a5f9e2b8f9ca296b1cc604955")
                .AddUrlSegment("id", "61fe437419cdd87656ce9fa6");
            var response = _client.Get(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.AreEqual("invalid token", response.Content);
        }
    }
}