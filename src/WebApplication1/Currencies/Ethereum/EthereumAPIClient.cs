using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Numerics;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using WebApplication1.Core;

namespace WebApplication1.Currencies.Ethereum
{
    public class EthereumAPIClient : IEthereumAPIClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly Dictionary<string, RequestJson> _rpcCalls = new Dictionary<string, RequestJson>();
        
        public EthereumAPIClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _rpcCalls["AllWallets"] = new RequestJson()
            {
                Method = "eth_accounts",
            };

            _rpcCalls["GetBalance"] = new RequestJson()
            {
                Method = "eth_getBalance"
            };
            
            _rpcCalls["SendTransaction"] = new RequestJson()
            {
                Method = "eth_sendTransaction"
            };

            _rpcCalls["GetBlockByNumber"] = new RequestJson()
            {
                Method = "eth_getBlockByNumber"
            };
            
            //Returns the number of most recent block.
            _rpcCalls["BlockNumber"] = new RequestJson()
            {
                Id = 83,
                Method = "eth_blockNumber"
            };


            _rpcCalls["TransactionByHash"] = new RequestJson()
            {
                Method = "eth_getTransactionByHash"
            };
        }
        
        [HttpGet("allwallets")]
        public async Task<JObject> GetAllAddresses()
        {                                                
            JObject o = await getJsonTask(_rpcCalls["AllWallets"]);
            return o;
        }


        [HttpGet("wallet/{wallet}/balance")]
        public async Task<JObject> GetBalance(string wallet)
        {

            RequestJson ro = _rpcCalls["GetBalance"];
            ro.Params = new List<string>()
            {
                wallet, 
                "latest"
            };
            JObject o = await getJsonTask(ro);
            
            //converting balance to human-readable format
            string balance = o["result"].ToString();

            BigInteger b1 = FromHexToBigInteger(balance);
            
            double dbl2 = Math.Pow(10,18);
            double dbl = (double) (b1) / dbl2;
            o["result"] = dbl.ToString() + " eth";
            
            
            return o;
        }

        public async Task<JObject> GetTransactionHistory(string wallet)
        {
            //very slow on big amount of blocks.
            JObject o = await getJsonTask(_rpcCalls["BlockNumber"]);
            BigInteger blockCount = FromHexToBigInteger(o["result"].ToString());

            List<EthereumTransaction> transactions = new List<EthereumTransaction>();
            
            for (BigInteger i = 1; i <= blockCount; i++)
            {
                RequestJson ro = _rpcCalls["GetBlockByNumber"];                
                ro.Params = new dynamic[] { FromBigIntegerToHex(i), true};                
                o = await getJsonTask(ro);
                
                EthereumBlock block = JsonConvert.DeserializeObject<EthereumBlock>(o["result"].ToString());

                transactions.AddRange(RelatedTransactions(block, wallet));
            }
            
            JArray retList = new JArray();

            foreach (var transaction in transactions)
            {
                JObject t = JObject.Parse(JsonConvert.SerializeObject(transaction));               
                retList.Add(t);
            }

            o["result"] = retList;
            return o;
        }

        public async Task<JObject> SendTransaction(EthereumTransactionDTO ethereumTransaction)
        {
            RequestJson ro = _rpcCalls["SendTransaction"];
            //string st = JsonConvert.SerializeObject(transaction, Formatting.None);
            ro.Params = ethereumTransaction;
            JObject o = await getJsonTask(ro);
            
            return o;
        }

        public async Task<JObject> TransactionState(string hash)
        {
            RequestJson ro = _rpcCalls["TransactionByHash"];
            ro.Params = new string[] {hash};

            JObject o = await getJsonTask(ro);

            if (String.IsNullOrWhiteSpace(o["result"].ToString()))
            {
                return o;
            }
                
            
            JObject justResult = JObject.Parse(o["result"].ToString());

            //if blockHash is null, transaction not calculated yet
            string status = justResult["blockHash"].ToString();
            
            if ( String.IsNullOrWhiteSpace(status))
            {
                justResult.Add("status", "pending");
            }
            else
            {
                justResult.Add("status", "processed");
            }            
    
            o["result"] = justResult;
            return o;
        }

        private async Task<JObject> getJsonTask(RequestJson rpcCall)        
        {
            string rpcString = JsonConvert.SerializeObject(rpcCall, Formatting.None);
            //rpcString = rpcString.Replace("\\", "");
            
            var client = _httpClientFactory.CreateClient("ganacheClient");                        
            
            var content = new StringContent(rpcString, Encoding.UTF8, "application/json");
            
            var response = await  client.PostAsync("", content);
            
            Stream receiveStream = await response.Content.ReadAsStreamAsync();
            StreamReader readStream = new StreamReader (receiveStream, Encoding.UTF8);
            

            string json = "";
            
            while (!readStream.EndOfStream)
            {
                json = readStream.ReadLine();
            }
            JObject o =  JObject.Parse(json);

            return o;
        }

        private BigInteger FromHexToBigInteger(string st)
        {
            BigInteger b1 = BigInteger.Parse(st.Remove(0, 2), NumberStyles.AllowHexSpecifier | NumberStyles.HexNumber);
            return b1;
        }

        private string FromBigIntegerToHex(BigInteger i)
        {
            return "0x" + i.ToString("x");
        }

        private List<EthereumTransaction> RelatedTransactions(EthereumBlock block, string wallet)
        {
            List<EthereumTransaction> transList = new List<EthereumTransaction>();            
            foreach (EthereumTransaction currTransaction in block.Transactions)           
            {
                if (currTransaction.From == wallet || currTransaction.To == wallet)
                {
                    transList.Add(currTransaction);
                }
            }
            return transList;
        }
        
        
    }
}