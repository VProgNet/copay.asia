﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace WebApplication1.Core
{
    public class EthereumBlock
    {
        /// <summary>
        /// 
        /// Example of block info
        /// 
        /// "number": "0x1b4", // 436
        /// "hash": "0xe670ec64341771606e55d6b4ca35a1a6b75ee3d5145a99d05921026d1527331",
        /// "parentHash": "0x9646252be9520f6e71339a8df9c55e4d7619deeb018d2a3f2d21fc165dde5eb5",
        /// "nonce": "0xe04d296d2460cfb8472af2c5fd05b5a214109c25688d3704aed5484f9a7792f2",
        /// "sha3Uncles": "0x1dcc4de8dec75d7aab85b567b6ccd41ad312451b948a7413f0a142fd40d49347",
        /// "logsBloom": "0xe670ec64341771606e55d6b4ca35a1a6b75ee3d5145a99d05921026d1527331",
        /// "transactionsRoot": "0x56e81f171bcc55a6ff8345e692c0f86e5b48e01b996cadc001622fb5e363b421",
        /// "stateRoot": "0xd5855eb08b3387c0af375e9cdb6acfc05eb8f519e419b874b6ff2ffda7ed1dff",
        /// "miner": "0x4e65fda2159562a496f9f3522f89122a3088497a",
        /// "difficulty": "0x027f07", // 163591
        /// "totalDifficulty":  "0x027f07", // 163591
        /// "extraData": "0x0000000000000000000000000000000000000000000000000000000000000000",
        /// "size":  "0x027f07", // 163591
        /// "gasLimit": "0x9f759", // 653145
        /// "gasUsed": "0x9f759", // 653145
        /// "timestamp": "0x54e34e8e" // 1424182926
        /// "transactions": [{...},{ ... }] 
        /// "uncles": ["0x1606e5...", "0xd5145a9..."]
        /// </summary>
        
        [JsonProperty("number")] 
        public string Number { get; set; }
        
        [JsonProperty("hash")] 
        public string Hash { get; set; }
        
        [JsonProperty("parentHash")] 
        public string ParentHash { get; set; }
        
        [JsonProperty("sha3Uncles")] 
        public string Sha3Uncles { get; set; }
        
        [JsonProperty("logsBloom")] 
        public string LogsBloom { get; set; }
        
        [JsonProperty("transactionsRoot")] 
        public string TransactionsRoot { get; set; }
        
        [JsonProperty("stateRoot")] 
        public string StateRoot { get; set; }
        
        [JsonProperty("miner")] 
        public string Miner { get; set; }
        
        [JsonProperty("difficulty")] 
        public string Difficulty { get; set; }
        
        [JsonProperty("totalDifficulty")] 
        public string TotalDifficulty { get; set; }
        
        [JsonProperty("extraData")] 
        public string ExtraData { get; set; }
        
        [JsonProperty("size")] 
        public string Size { get; set; }
        
        [JsonProperty("gasLimit")] 
        public string GasLimit { get; set; }
        
        [JsonProperty("gasUsed")] 
        public string GasUsed { get; set; }
        
        [JsonProperty("timestamp")] 
        public string Timestamp { get; set; }
        
        [JsonProperty("transactions")] 
        public List<EthereumTransaction> Transactions { get; set; }
        
        [JsonProperty("uncles")] 
        public List<string> Uncles { get; set; }       
        
        
    }
}