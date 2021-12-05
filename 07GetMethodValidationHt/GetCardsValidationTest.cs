using System.Net;
using NUnit.Framework;
using RestSharp;

namespace _07GetMethodValidationHt
{
    public class GetCardsValidationTest
    {
        private static IRestClient _client;
        
        [OneTimeSetUp]
        public static void InitializeRestClient() => 
            _client = new RestClient("https://api.trello.com");

        private IRestRequest RequestWithAuth(string resource) =>
            new RestRequest(resource)
                .AddQueryParameter("key", "fb04999a731923c2e3137153b1ad5de0")
                .AddQueryParameter("token", "b73120fb537fceb444050a2a4c08e2f96f47389931bd80253d2440708f2a57e1");

        [Test]
        public void CheckGetCardWithInvalidId()
        {
            var request = RequestWithAuth("/1/cards/{id}")
                .AddUrlSegment("id", "invalid");
            var response = _client.Get(request);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("invalid id", response.Content);
        }

        [Test]
        public void CheckGetCardWithInvalidAuth()
        {
            var request = new RestRequest("/1/cards/{id}")
                .AddUrlSegment("id", "60e03f8328428d54e3f62252");
            var response = _client.Get(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.AreEqual("unauthorized card permission requested", response.Content);
        }

        [Test]
        public void CheckGetCardWithAnotherUserCredentials()
        {
            var request = new RestRequest("/1/cards/{id}")
                .AddQueryParameter("key", "8b32218e6887516d17c84253faf967b6")
                .AddQueryParameter("token", "492343b8106e7df3ebb7f01e219cbf32827c852a5f9e2b8f9ca296b1cc604955")
                .AddUrlSegment("id", "60e03f8328428d54e3f62252");
            var response = _client.Get(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.AreEqual("invalid token", response.Content);
        }
    }
}