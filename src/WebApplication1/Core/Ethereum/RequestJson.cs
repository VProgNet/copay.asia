using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebApplication1.Core
{
    public class RequestJson
    {
        [JsonProperty("jsonrpc")] 
        public string JsonRPC { get; set; } = "2.0";
    
        [JsonProperty("method")]
        public string Method { get; set; }
    
        [JsonProperty("params")]
        public dynamic Params { get; set; }

        [JsonProperty("id")] 
        public int Id { get; set; } = 1;
    }
}