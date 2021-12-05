using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using _13PostMethodValidation.Consts;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

namespace _13PostMethodValidation.Tests.Create
{
    public class CreateBoardTest : BaseTest
    {
        private string _createdBoardId;
        
        [Test]
        public void CheckCreateBoard()
        {
            var boardName = "New Board" + DateTime.Now.ToLongTimeString();
            
            var request = RequestWithAuth(BoardsEndpoints.CreateBoardUrl)
                .AddJsonBody(new Dictionary<string, string> {{"name", boardName}});
            var response = _client.Post(request);
            
            var responseContent = JToken.Parse(response.Content);

            _createdBoardId = responseContent.SelectToken("id").ToString();
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(boardName, responseContent.SelectToken("name").ToString());
            
            request = RequestWithAuth(BoardsEndpoints.GetAllBoardsUrl)
                .AddQueryParameter("fields", "id,name")
                .AddUrlSegment("member", UrlParamValues.Username);
            response = _client.Get(request);
            responseContent = JToken.Parse(response.Content);
            Assert.True(responseContent.Children().Select(token => token.SelectToken("name").ToString()).Contains(boardName));
        }

        [TearDown]
        public void DeleteCreatedBoard()
        {
            var request = RequestWithAuth(BoardsEndpoints.DeleteBoardUrl)
                .AddUrlSegment("id", _createdBoardId);
            var response = _client.Delete(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}