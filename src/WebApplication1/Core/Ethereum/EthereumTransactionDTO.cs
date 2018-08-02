using Newtonsoft.Json;
namespace WebApplication1.Core
{
    public class EthereumTransactionDTO
    {
        [JsonProperty("from")] 
        public string From { get; set; }
    
        [JsonProperty("to")]
        public string To { get; set; }
    
        [JsonProperty("gas")]
        public string Gas { get; set; }

        [JsonProperty("gasPrice")] 
        public string GasPrice { get; set; }
        
        [JsonProperty("value")]
        public string Value { get; set; }
        
        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("nonce")]
        public string Nonce { get; set; }
        
    }
}