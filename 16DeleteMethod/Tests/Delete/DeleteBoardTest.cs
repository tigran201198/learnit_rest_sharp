using System.Collections.Generic;
using System.Linq;
using System.Net;
using _16DeleteMethod.Consts;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

namespace _16DeleteMethod.Tests.Delete
{
    public class DeleteBoardTest : BaseTest
    {
        
        private string _createdBoardId;
        
        [SetUp]
        public void CreateBoard()
        {
            var request = RequestWithAuth(BoardsEndpoints.CreateBoardUrl)
                .AddJsonBody(new Dictionary<string, string> {{"name", "New Board"}});
            var response = _client.Post(request);
            _createdBoardId = JToken.Parse(response.Content).SelectToken("id").ToString();
        }

        [Test]
        public void CheckDeleteBoard()
        {
            var request = RequestWithAuth(BoardsEndpoints.DeleteBoardUrl)
                .AddUrlSegment("id", _createdBoardId);
            var response = _client.Delete(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(string.Empty, JToken.Parse(response.Content).SelectToken("_value").ToString());
            
            request = RequestWithAuth(BoardsEndpoints.GetAllBoardsUrl)
                .AddUrlSegment("member", UrlParamValues.Username);
            response = _client.Get(request);
            var responseContent = JToken.Parse(response.Content);
            Assert.False(responseContent.Children().Select(token => token.SelectToken("id").ToString()).Contains(_createdBoardId));
        }
    }
}