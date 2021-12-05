using System.Collections.Generic;

namespace _14PutMethodHt.Arguments.Holders
{
    public class CardBodyValidationArgumentsHolder
    {
        public IDictionary<string, object> BodyParams { get; set; }
        
        public string ErrorMessage { get; set; }
    }
}