using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using WebApplication1.Core;
using WebApplication1.Currencies;
using WebApplication1.Currencies.Ethereum;

namespace WebApplication1.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class EthereumController : Controller
    {
        private readonly IEthereumAPIClient _ethereumApiClient;
        
        public EthereumController(IEthereumAPIClient ethereumApiClient)
        {
            _ethereumApiClient = ethereumApiClient;
        }

        // GET api/ethereum
        
        /// <summary>
        /// Show all addresses in current wallet
        /// </summary>
        [HttpGet("addresses")]
        public async Task<JsonResult> GetAllAddresses()
        {
            //List<Wallet> wallets = await _ethereumApiClient.GetAllWallets();           
            return Json(await _ethereumApiClient.GetAllAddresses());
        }
        
        /// <summary>
        /// Show balance of address in eth
        /// </summary>
        /// <param name="address"> ethereum address </param>
        /// <returns>balance of address</returns>
        [HttpGet("balance/{address}")]
        public async Task<JsonResult> GetBalance(string address)
        {
            return Json(await _ethereumApiClient.GetBalance(address));
        }
        
        
        /// <summary>
        /// Form and send transaction to ganache-cli
        /// you need to specify just param value
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/ethereum/transaction/send
        ///     {
        ///        "from": "0x09714c59a987995a8d6fde74a4ca76f5ddee4857",
        ///        "to": "0x6764b011e527f2705d5a53512654586194e6ae87",
        ///        "gas": "0x76c0",
        ///        "gasPrice": "0x6691BA",
        ///        "value": "0x6691BA"
        ///     }
        ///
        /// </remarks>
        /// <param name="ethereumTransaction">ethereum transaction. For example look at https://github.com/ethereum/wiki/wiki/JSON-RPC#eth_sendtransaction</param>
        /// <returns></returns>
        [HttpPost("transaction/send")]
        public async Task<JsonResult> SendTransaction([FromBody] EthereumTransactionDTO ethereumTransaction)
        {
            return Json(await _ethereumApiClient.SendTransaction(ethereumTransaction));
        }
        
        
        /// <summary>
        /// get state of transaction
        /// added "state" data when return current transaction
        /// If blockhash is null, then transaction pending
        /// else transaction processed
        /// </summary>
        /// <param name="hash">hash of transaction</param>
        /// <returns></returns>
        [HttpGet("transaction/state/{hash}")]
        public async Task<JsonResult> SendTransaction(string hash)
        {
            return Json(await _ethereumApiClient.TransactionState(hash));
        }

        /// <summary>
        /// history of all transactions related to given address
        /// scans all blocks in the chain and search for transactions,
        /// that have value "address" in "from" or "to"  
        /// </summary>
        /// <param name="address">ethereum address</param>
        /// <returns></returns>
        [HttpGet("history/{address}")]
        public async Task<JsonResult> GetTransactionHistory(string address)
        {
            return Json(await _ethereumApiClient.GetTransactionHistory(address));
        }
    }
}