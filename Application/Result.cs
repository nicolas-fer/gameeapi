using System.Net;
using System.Text.Json.Serialization;

namespace Application
{
    public class Result
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public IEnumerable<ErrorField> Errors { get; set; }

        [JsonIgnore]        
        public HttpStatusCode StatusCode { get; set; }
    }

    public class Result<TObject> : Result
    {
        public TObject Data { get; set; }
    }
}
