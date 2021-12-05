using System.Collections;
using System.Net;
using _16DeleteMethod.Arguments.Holders;
using RestSharp;

namespace _16DeleteMethod.Arguments.Providers
{
    public class BoardIdValidationArgumentsProvider : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new object[]
            {
                new BoardIdValidationArgumentsHolder
                {
                    ErrorMessage = "invalid id",
                    StatusCode = HttpStatusCode.BadRequest,
                    PathParams = new[] {new Parameter("id", "invalid", ParameterType.UrlSegment)}
                }
            };
            yield return new object[]
            {
                new BoardIdValidationArgumentsHolder
                {
                    ErrorMessage = "The requested resource was not found.",
                    StatusCode = HttpStatusCode.NotFound,
                    PathParams = new[] {new Parameter("id", "60d847d9aad2437cb984f8e1", ParameterType.UrlSegment)}
                }
            };
        }
    }
}