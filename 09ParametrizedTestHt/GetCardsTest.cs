using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;
using RestSharp;

namespace _09ParametrizedTestHt
{
    public class GetCardsTest : BaseTest
    {
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