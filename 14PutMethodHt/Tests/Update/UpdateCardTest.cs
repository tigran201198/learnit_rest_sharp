using System;
using System.Collections.Generic;
using System.Net;
using _14PutMethodHt.Consts;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

namespace _14PutMethodHt.Tests.Update
{
    public class UpdateCardTest : BaseTest
    {
        [Test]
        public void CheckUpdateCard()
        {
            var updatedName = "Updated Card" + DateTime.Now.ToLongTimeString();
            var request = RequestWithAuth(CardsEndpoints.UpdateCardUrl)
                .AddUrlSegment("id", UrlParamValues.CardIdToUpdate)
                .AddJsonBody(new Dictionary<string, string> {{"name", updatedName}});
            var response = _client.Put(request);

            var responseContent = JToken.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(updatedName, responseContent.SelectToken("name").ToString());

            request = RequestWithAuth(CardsEndpoints.GetCardUrl)
                .AddUrlSegment("id", UrlParamValues.CardIdToUpdate);
            response = _client.Get(request);
            responseContent = JToken.Parse(response.Content);
            Assert.AreEqual(updatedName, responseContent.SelectToken("name").ToString());
        }
    }
}