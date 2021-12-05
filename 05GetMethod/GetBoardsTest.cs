using System.Net;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

namespace _05GetMethod
{
    public class TrelloTest
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
        public void CheckGetBoards()
        {
            var request = RequestWithAuth("/1/members/{member}/boards")
                .AddUrlSegment("member", "learnpostman");
            var response = _client.Get(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void CheckGetBoard()
        {
            var request = RequestWithAuth("/1/boards/{id}")
                .AddUrlSegment("id", "61fe437419cdd87656ce9fa6");
            var response = _client.Get(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("New Board", JToken.Parse(response.Content).SelectToken("name").ToString());
        }
    }
}