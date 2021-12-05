using System;
using System.Net;
using NUnit.Framework;
using RestSharp;

namespace _04FirstNunitTestHt
{
    public class GoogleTest
    {
        private static IRestClient _client;

        [OneTimeSetUp]
        public static void InitializeRestClient() =>
            _client = new RestClient("https://google.com");

        [Test]
        public void CheckGoogleApi()
        {
            var request = new RestRequest();
            Console.WriteLine($"{_client.BaseUrl} {request.Method}");

            var response = _client.Get(request);
            Console.WriteLine(response.Content);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}