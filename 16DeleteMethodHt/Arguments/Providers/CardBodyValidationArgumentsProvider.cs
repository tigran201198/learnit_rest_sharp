using System.Collections;
using System.Collections.Generic;
using _16DeleteMethodHt.Arguments.Holders;
using _16DeleteMethodHt.Consts;

namespace _16DeleteMethodHt.Arguments.Providers
{
    public class CardBodyValidationArgumentsProvider : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new object[]
            {
                new CardBodyValidationArgumentsHolder
                {
                    BodyParams = new Dictionary<string, object>
                    {
                        {"name", 123},
                        {"idList", UrlParamValues.ExistingListId}
                    },
                    ErrorMessage = "invalid value for name"
                }
            };
            yield return new object[]
            {
                new CardBodyValidationArgumentsHolder
                {
                    BodyParams = new Dictionary<string, object>
                    {
                        {"name", "New card"}
                    },
                    ErrorMessage = "invalid value for idList"
                }
            };
            yield return new object[]
            {
                new CardBodyValidationArgumentsHolder
                {
                    BodyParams = new Dictionary<string, object>
                    {
                        {"name", "New card"},
                        {"idList", "invalid"}
                    },
                    ErrorMessage = "invalid value for idList"
                }
            };
        }
    }
}