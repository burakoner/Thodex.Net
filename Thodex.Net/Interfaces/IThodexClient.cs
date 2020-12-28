using CryptoExchange.Net.Objects;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Thodex.Net.RestObjects;

namespace Thodex.Net.Interfaces
{
    public interface IThodexClient
    {
        WebCallResult<ThodexOrder> BuyLimit(string symbol, decimal amount, decimal price, CancellationToken ct = default);
        Task<WebCallResult<ThodexOrder>> BuyLimitAsync(string symbol, decimal amount, decimal price, CancellationToken ct = default);
        WebCallResult<ThodexOrder> BuyMarket(string symbol, decimal amount, CancellationToken ct = default);
        Task<WebCallResult<ThodexOrder>> BuyMarketAsync(string symbol, decimal amount, CancellationToken ct = default);
        WebCallResult<IEnumerable<long>> CancelOrder(string symbol, long order_id, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<long>>> CancelOrderAsync(string symbol, long order_id, CancellationToken ct = default);
        WebCallResult<ThodexBalance> GetBalances(IEnumerable<string> assets = null, CancellationToken ct = default);
        Task<WebCallResult<ThodexBalance>> GetBalancesAsync(IEnumerable<string> assets = null, CancellationToken ct = default);
        WebCallResult<IEnumerable<ThodexDeal>> GetDealsHistory(string symbol, int? last_id = null, CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<ThodexDeal>>> GetDealsHistoryAsync(string symbol, int? last_id = null, CancellationToken ct = default);
        WebCallResult<IEnumerable<ThodexMarket>> GetMarkets(CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<ThodexMarket>>> GetMarketsAsync(CancellationToken ct = default);
        WebCallResult<ThodexState> GetMarketStatus(string symbol, CancellationToken ct = default);
        Task<WebCallResult<ThodexState>> GetMarketStatusAsync(string symbol, CancellationToken ct = default);
        WebCallResult<IEnumerable<ThodexSummary>> GetMarketSummary(CancellationToken ct = default);
        Task<WebCallResult<IEnumerable<ThodexSummary>>> GetMarketSummaryAsync(CancellationToken ct = default);
        WebCallResult<ThodexOrders> GetOpenOrders(string symbol, int limit = 50, int? offset = null, CancellationToken ct = default);
        Task<WebCallResult<ThodexOrders>> GetOpenOrdersAsync(string symbol, int limit = 50, int? offset = null, CancellationToken ct = default);
        WebCallResult<ThodexOrderBook> GetOrderBook(string symbol, int limit = 100, CancellationToken ct = default);
        Task<WebCallResult<ThodexOrderBook>> GetOrderBookAsync(string symbol, int limit = 100, CancellationToken ct = default);
        WebCallResult<ThodexOrdersHistory> GetOrderHistory(string symbol, int limit = 50, int? offset = null, CancellationToken ct = default);
        Task<WebCallResult<ThodexOrdersHistory>> GetOrderHistoryAsync(string symbol, int limit = 50, int? offset = null, CancellationToken ct = default);
        WebCallResult<ThodexOrder> SellLimit(string symbol, decimal amount, decimal price, CancellationToken ct = default);
        Task<WebCallResult<ThodexOrder>> SellLimitAsync(string symbol, decimal amount, decimal price, CancellationToken ct = default);
        WebCallResult<ThodexOrder> SellMarket(string symbol, decimal amount, CancellationToken ct = default);
        Task<WebCallResult<ThodexOrder>> SellMarketAsync(string symbol, decimal amount, CancellationToken ct = default);
        WebCallResult<ThodexServerTime> ServerTime(CancellationToken ct = default);
        Task<WebCallResult<ThodexServerTime>> ServerTimeAsync(CancellationToken ct = default);
        void SetApiCredentials(string apiKey, string apiSecret);
    }
}
