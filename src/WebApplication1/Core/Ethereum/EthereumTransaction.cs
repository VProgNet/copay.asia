using Newtonsoft.Json;

namespace WebApplication1.Core
{
    public class EthereumTransaction
    {
        /// <summary>
        ///
        /// Example of transaction info
        /// 
        /// "hash":"0xc6ef2fc5426d6ad6fd9e2a26abeab0aa2411b7ab17f30a99d3cb96aed1d1055b",
        /// "nonce":"0x",
        /// "blockHash": "0xbeab0aa2411b7ab17f30a99d3cb9c6ef2fc5426d6ad6fd9e2a26a6aed1d1055b",
        /// "blockNumber": "0x15df", // 5599
        /// "transactionIndex":  "0x1", // 1
        /// "from":"0x407d73d8a49eeb85d32cf465507dd71d507100c1",
        /// "to":"0x85h43d8a49eeb85d32cf465507dd71d507100c1",
        /// "value":"0x7f110", // 520464
        /// "gas": "0x7f110", // 520464
        /// "gasPrice":"0x09184e72a000",
        /// "input":"0x603880600c6000396000f300603880600c6000396000f3603880600c6000396000f360",
        /// </summary>
        
        
        [JsonProperty("hash")] 
        public string Hash { get; set; }
        
        [JsonProperty("nonce")] 
        public string Nonce { get; set; }
        
        [JsonProperty("blockHash")] 
        public string BlockHash { get; set; }
        
        [JsonProperty("blockNumber")] 
        public string BlockNumber { get; set; }
        
        [JsonProperty("transactionIndex")] 
        public string TransactionIndex { get; set; }
        
        [JsonProperty("from")] 
        public string From { get; set; }
        
        [JsonProperty("to")] 
        public string To { get; set; }
        
        [JsonProperty("value")] 
        public string Value { get; set; }
        
        [JsonProperty("gas")] 
        public string Gas { get; set; }
        
        [JsonProperty("gasPrice")] 
        public string GasPrice { get; set; }
        
        [JsonProperty("input")] 
        public string Input { get; set; }                
        
    }
}