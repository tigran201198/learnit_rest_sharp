using _12PostMethod.Consts;
using NUnit.Framework;
using RestSharp;

namespace _12PostMethod.Tests
{
    public class BaseTest
    {
        protected static IRestClient _client;
        
        [OneTimeSetUp]
        public static void InitializeRestClient() => 
            _client = new RestClient("https://api.trello.com");

        protected IRestRequest RequestWithAuth(string resource) =>
            RequestWithoutAuth(resource).AddOrUpdateParameters(UrlParamValues.AuthQueryParams);

        protected IRestRequest RequestWithoutAuth(string resource) =>
            new RestRequest(resource);
    }
}