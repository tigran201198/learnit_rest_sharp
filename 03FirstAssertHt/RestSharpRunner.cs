using System;
using System.Net;
using NUnit.Framework;
using RestSharp;

namespace _03FirstAssertHt
{
    public class RestSharpRunner
    {
        public static void Main(string[] args)
        {
            var request = new RestRequest();
            var client = new RestClient("https://google.com");
            Console.WriteLine($"{client.BaseUrl} {request.Method}");

            var response = client.Get(request);
            Console.WriteLine(response.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}