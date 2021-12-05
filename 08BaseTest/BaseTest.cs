using NUnit.Framework;
using RestSharp;

namespace _08BaseTest
{
    public class BaseTest
    {
        protected static IRestClient _client;
        
        [OneTimeSetUp]
        public static void InitializeRestClient() => 
            _client = new RestClient("https://api.trello.com");

        protected IRestRequest RequestWithAuth(string resource) =>
            RequestWithoutAuth(resource)
                .AddQueryParameter("key", "fb04999a731923c2e3137153b1ad5de0")
                .AddQueryParameter("token", "b73120fb537fceb444050a2a4c08e2f96f47389931bd80253d2440708f2a57e1");

        protected IRestRequest RequestWithoutAuth(string resource) =>
            new RestRequest(resource);
    }
}