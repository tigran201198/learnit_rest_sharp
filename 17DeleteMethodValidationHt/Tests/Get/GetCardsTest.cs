using System.IO;
using System.Net;
using _17DeleteMethodValidationHt.Consts;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;
using RestSharp;

namespace _17DeleteMethodValidationHt.Tests.Get
{
    public class GetCardsTest : BaseTest
    {
        [Test]
        public void CheckGetCards()
        {
            var request = RequestWithAuth(CardsEndpoints.GetAllCardsUrl)
                .AddQueryParameter("fields", "id,name")
                .AddUrlSegment("list_id", UrlParamValues.ExistingListId);
            var response = _client.Get(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_cards.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }

        [Test]
        public void CheckGetCard()
        {
            var request = RequestWithAuth(CardsEndpoints.GetCardUrl)
                .AddQueryParameter("fields", "id,name")
                .AddUrlSegment("id", UrlParamValues.ExistingCardId);
            var response = _client.Get(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_card.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
            Assert.AreEqual("Test card", responseContent.SelectToken("name").ToString());
        }
    }
}