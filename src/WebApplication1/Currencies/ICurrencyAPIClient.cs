using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WebApplication1.Core;

namespace WebApplication1.Currencies
{
    public interface ICurrencyAPIClient
    {
        //Проверка баланса кошелька, история транзакций, отправка транзакций и проверка его состояния
        Task<JObject> GetBalance(string wallet);
        Task<JObject> GetTransactionHistory(string wallet);
        Task<JObject> SendTransaction(EthereumTransactionDTO ethereumTransaction);
        Task<JObject> TransactionState(string transaction);
        Task<JObject> GetAllAddresses();
    }
}