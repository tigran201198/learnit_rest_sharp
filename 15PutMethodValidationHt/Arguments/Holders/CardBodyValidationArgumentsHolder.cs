using System.Collections.Generic;

namespace _15PutMethodValidationHt.Arguments.Holders
{
    public class CardBodyValidationArgumentsHolder
    {
        public IDictionary<string, object> BodyParams { get; set; }
        
        public string ErrorMessage { get; set; }
    }
}