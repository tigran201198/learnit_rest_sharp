using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using _17DeleteMethodValidation.Consts;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

namespace _17DeleteMethodValidation.Tests.Delete
{
    public class DeleteBoardTest : BaseTest
    {
        
        private string _createdBoardId;
        
        [SetUp]
        public async Task CheckCreateBoard()
        {
            var request = RequestWithAuth(BoardsEndpoints.CreateBoardUrl, Method.Post)
                .AddJsonBody(new Dictionary<string, string> {{"name", "New Board"}});
            var response = await _client.ExecuteAsync(request);
            _createdBoardId = JToken.Parse(response.Content).SelectToken("id").ToString();
        }

        [Test]
        public async Task CheckDeleteBoard()
        {
            var request = RequestWithAuth(BoardsEndpoints.DeleteBoardUrl, Method.Delete)
                .AddUrlSegment("id", _createdBoardId);
            var response = await _client.ExecuteAsync(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(string.Empty, JToken.Parse(response.Content).SelectToken("_value").ToString());
            
            request = RequestWithAuth(BoardsEndpoints.GetAllBoardsUrl, Method.Get)
                .AddUrlSegment("member", UrlParamValues.Username);
            response = await _client.ExecuteAsync(request);
            var responseContent = JToken.Parse(response.Content);
            Assert.False(responseContent.Children().Select(token => token.SelectToken("id").ToString()).Contains(_createdBoardId));
        }
    }
}