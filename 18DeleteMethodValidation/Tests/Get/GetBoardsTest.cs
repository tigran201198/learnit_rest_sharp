using System.IO;
using System.Net;
using System.Threading.Tasks;
using _17DeleteMethodValidation.Consts;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;
using RestSharp;

namespace _17DeleteMethodValidation.Tests.Get
{
    public class TrelloTest : BaseTest
    {
        [Test]
        public async Task CheckGetBoards()
        {
            var request = RequestWithAuth(BoardsEndpoints.GetAllBoardsUrl, Method.Get)
                .AddQueryParameter("fields", "id,name")
                .AddUrlSegment("member", UrlParamValues.Username);
            var response = await _client.ExecuteAsync(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_boards.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }

        [Test]
        public async Task CheckGetBoard()
        {
            var request = RequestWithAuth(BoardsEndpoints.GetBoardUrl, Method.Get)
                .AddQueryParameter("fields", "id,name")
                .AddUrlSegment("id", UrlParamValues.ExistingBoardId);
            var response = await _client.ExecuteAsync(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_board.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
            Assert.AreEqual("New Board", responseContent.SelectToken("name").ToString());
        }
    }
}