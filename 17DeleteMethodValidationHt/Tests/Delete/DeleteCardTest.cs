using System.Collections.Generic;
using System.Linq;
using System.Net;
using _17DeleteMethodValidationHt.Consts;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

namespace _17DeleteMethodValidationHt.Tests.Delete
{
    public class DeleteCardTest : BaseTest
    {
        private string _createdCardId;
        
        [SetUp]
        public void CreateCard()
        {
            var request = RequestWithAuth(CardsEndpoints.CreateCardUrl)
                .AddJsonBody(new Dictionary<string, string>
                {
                    {"name", "New Card"},
                    {"idList", UrlParamValues.ExistingListId}
                });
            var response = _client.Post(request);
            _createdCardId = JToken.Parse(response.Content).SelectToken("id").ToString();
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void CheckDeleteCard()
        {
            var request = RequestWithAuth(CardsEndpoints.DeleteCardUrl)
                .AddUrlSegment("id", _createdCardId);
            var response = _client.Delete(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(null, JToken.Parse(response.Content).SelectToken("_value"));
            
            request = RequestWithAuth(CardsEndpoints.GetAllCardsUrl)
                .AddUrlSegment("list_id", UrlParamValues.ExistingListId);
            response = _client.Get(request);
            var responseContent = JToken.Parse(response.Content);
            Assert.False(responseContent.Children().Select(token => token.SelectToken("id").ToString()).Contains(_createdCardId));
        }
    }
}