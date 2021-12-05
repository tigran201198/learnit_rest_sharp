using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;
using RestSharp;

namespace _07GetMethodValidationHt
{
    public class GetCardsTest
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
        public void CheckGetCards()
        {
            var request = RequestWithAuth("/1/lists/{list_id}/cards")
                .AddQueryParameter("fields", "id,name")
                .AddUrlSegment("list_id", "60d84769c4ce7a09f9140221");
            var response = _client.Get(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_cards.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }

        [Test]
        public void CheckGetCard()
        {
            var request = RequestWithAuth("/1/cards/{id}")
                .AddQueryParameter("fields", "id,name")
                .AddUrlSegment("id", "60e03f8328428d54e3f62252");
            var response = _client.Get(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_card.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
            Assert.AreEqual("Test card", responseContent.SelectToken("name").ToString());
        }
    }
}