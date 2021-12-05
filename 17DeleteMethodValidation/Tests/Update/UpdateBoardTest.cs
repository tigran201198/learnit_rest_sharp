using System;
using System.Collections.Generic;
using System.Net;
using _17DeleteMethodValidation.Consts;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

namespace _17DeleteMethodValidation.Tests.Update
{
    public class UpdateBoardTest : BaseTest
    {
        [Test]
        public void CheckUpdateBoard() {
            var updatedName = "Updated Name" + DateTime.Now.ToLongTimeString();
            var request = RequestWithAuth(BoardsEndpoints.UpdateBoardUrl)
                .AddUrlSegment("id", UrlParamValues.BoardIdToUpdate)
                .AddJsonBody(new Dictionary<string, string>{{"name", updatedName}});
            var response = _client.Put(request);
            
            var responseContent = JToken.Parse(response.Content);
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(updatedName, responseContent.SelectToken("name").ToString());
            
            request = RequestWithAuth(BoardsEndpoints.GetBoardUrl)
                .AddUrlSegment("id", UrlParamValues.BoardIdToUpdate);
            response = _client.Get(request);
            responseContent = JToken.Parse(response.Content);
            Assert.AreEqual(updatedName, responseContent.SelectToken("name").ToString());
        }
    }
}