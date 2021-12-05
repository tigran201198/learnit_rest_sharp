using System.Collections;
using System.Net;
using _11FinalRefactoringHt.Arguments.Holders;
using RestSharp;

namespace _11FinalRefactoringHt.Arguments.Providers
{
    public class CardIdValidationArgumentsProvider : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new object[]
            {
                new CardIdValidationArgumentsHolder
                {
                    ErrorMessage = "invalid id",
                    StatusCode = HttpStatusCode.BadRequest,
                    PathParams = new[] {new Parameter("id", "invalid", ParameterType.UrlSegment)}
                }
            };
            yield return new object[]
            {
                new CardIdValidationArgumentsHolder
                {
                    ErrorMessage = "The requested resource was not found.",
                    StatusCode = HttpStatusCode.NotFound,
                    PathParams = new[] {new Parameter("id", "60e03f8328428d54e3f62251", ParameterType.UrlSegment)}
                }
            };
        }
    }
}