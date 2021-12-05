using System.Collections.Generic;

namespace _13PostMethodValidationHt.Arguments.Holders
{
    public class CardBodyValidationArgumentsHolder
    {
        public IDictionary<string, object> BodyParams { get; set; }
        
        public string ErrorMessage { get; set; }
    }
}