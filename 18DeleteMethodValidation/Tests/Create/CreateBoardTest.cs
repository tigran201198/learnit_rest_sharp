using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using _17DeleteMethodValidation.Consts;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

namespace _17DeleteMethodValidation.Tests.Create
{
    public class CreateBoardTest : BaseTest
    {
        private string _createdBoardId;
        
        [Test]
        public async Task CheckCreateBoard()
        {
            var boardName = "New Board" + DateTime.Now.ToLongTimeString();
            
            var request = RequestWithAuth(BoardsEndpoints.CreateBoardUrl, Method.Post)
                .AddJsonBody(new Dictionary<string, string> {{"name", boardName}});
            var response = await _client.ExecuteAsync(request);
            
            var responseContent = JToken.Parse(response.Content);

            _createdBoardId = responseContent.SelectToken("id").ToString();
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(boardName, responseContent.SelectToken("name").ToString());
            
            request = RequestWithAuth(BoardsEndpoints.GetAllBoardsUrl, Method.Get)
                .AddQueryParameter("fields", "id,name")
                .AddUrlSegment("member", UrlParamValues.Username);
            response = await _client.ExecuteAsync(request);
            responseContent = JToken.Parse(response.Content);
            Assert.True(responseContent.Children().Select(token => token.SelectToken("name").ToString()).Contains(boardName));
        }

        [TearDown]
        public async Task DeleteCreatedBoard()
        {
            var request = RequestWithAuth(BoardsEndpoints.DeleteBoardUrl, Method.Delete)
                .AddUrlSegment("id", _createdBoardId);
            var response = await _client.ExecuteAsync(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}