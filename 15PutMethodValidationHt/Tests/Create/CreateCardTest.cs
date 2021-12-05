using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using _15PutMethodValidationHt.Consts;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

namespace _15PutMethodValidationHt.Tests.Create
{
    public class CreateCardTest : BaseTest
    {
        private string _createdCardId;
        
        [Test]
        public void CheckCreateCard()
        {
            var cardName = "New Card" + DateTime.Now.ToLongTimeString();
            
            var request = RequestWithAuth(CardsEndpoints.CreateCardUrl)
                .AddJsonBody(new Dictionary<string, string>
                {
                    {"name", cardName},
                    {"idList", UrlParamValues.ExistingListId}
                });
            var response = _client.Post(request);
            
            var responseContent = JToken.Parse(response.Content);

            _createdCardId = responseContent.SelectToken("id").ToString();
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(cardName, responseContent.SelectToken("name").ToString());
            
            request = RequestWithAuth(CardsEndpoints.GetAllCardsUrl)
                .AddUrlSegment("list_id", UrlParamValues.ExistingListId);
            response = _client.Get(request);
            responseContent = JToken.Parse(response.Content);
            Assert.True(responseContent.Children().Select(token => token.SelectToken("name").ToString()).Contains(cardName));
        }

        [TearDown]
        public void DeleteCreatedCard()
        {
            var request = RequestWithAuth(CardsEndpoints.DeleteCardUrl)
                .AddUrlSegment("id", _createdCardId);
            var response = _client.Delete(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}