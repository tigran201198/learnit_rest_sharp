using System.IO;
using System.Net;
using _12PostMethod.Consts;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;
using RestSharp;

namespace _12PostMethod.Tests.Get
{
    public class TrelloTest : BaseTest
    {
        [Test]
        public void CheckGetBoards()
        {
            var request = RequestWithAuth(BoardsEndpoints.GetAllBoardsUrl)
                .AddQueryParameter("fields", "id,name")
                .AddUrlSegment("member", UrlParamValues.Username);
            var response = _client.Get(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_boards.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }

        [Test]
        public void CheckGetBoard()
        {
            var request = RequestWithAuth(BoardsEndpoints.GetBoardUrl)
                .AddQueryParameter("fields", "id,name")
                .AddUrlSegment("id", UrlParamValues.ExistingBoardId);
            var response = _client.Get(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_board.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
            Assert.AreEqual("New Board", responseContent.SelectToken("name").ToString());
        }
    }
}