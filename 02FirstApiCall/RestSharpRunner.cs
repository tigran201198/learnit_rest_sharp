using System;
using RestSharp;

namespace _02FirstApiCall
{
    public class RestSharpRunner
    {
        public static void Main(string[] args)
        {
            var request = new RestRequest();
            var client = new RestClient("https://api.trello.com");
            Console.WriteLine($"{client.BaseUrl} {request.Method}");

            client.Get(request);
        }
    }
}