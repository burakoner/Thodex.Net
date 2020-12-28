using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Thodex.Net.CoreObjects;
using Thodex.Net.Interfaces;
using Thodex.Net.RestObjects;

namespace Thodex.Net
{
    public class ThodexClient : RestClient, IRestClient, IThodexClient
    {
        #region Fields
        private static ThodexClientOptions defaultOptions = new ThodexClientOptions();
        private static ThodexClientOptions DefaultOptions => defaultOptions.Copy();

        // V1 Endpoints
        private const string Endpoints_Public_ServerTime = "v1/public/time";
        private const string Endpoints_Public_Markets = "v1/public/markets";
        private const string Endpoints_Public_MarketStatus = "v1/public/market-status";
        private const string Endpoints_Public_MarketSummary = "v1/public/market-summary";
        private const string Endpoints_Public_MarketHistory = "v1/public/market-history";
        private const string Endpoints_Public_OrderDepth = "v1/public/order-depth";

        private const string Endpoints_Market_OpenOrders = "v1/market/open-orders";
        private const string Endpoints_Market_OrderHistory = "v1/market/order-history";
        private const string Endpoints_Market_BuyLimit = "v1/market/buy-limit";
        private const string Endpoints_Market_BuyMarket = "v1/market/buy";
        private const string Endpoints_Market_SellLimit = "v1/market/sell-limit";
        private const string Endpoints_Market_SellMarket = "v1/market/sell";
        private const string Endpoints_Market_CancelOrder = "v1/market/cancel";

        private const string Endpoints_Account_Balance = "v1/account/balance";
        #endregion

        #region Constructor / Destructor
        /// <summary>
        /// Create a new instance of ThodexClient using the default options
        /// </summary>
        public ThodexClient() : this(DefaultOptions)
        {
            this.requestBodyFormat = RequestBodyFormat.FormData;
        }

        /// <summary>
        /// Create a new instance of the ThodexClient with the provided options
        /// </summary>
        public ThodexClient(ThodexClientOptions options) : base("Thodex", options, options.ApiCredentials == null ? null : new ThodexAuthenticationProvider(options.ApiCredentials, ArrayParametersSerialization.MultipleValues))
        {
            this.requestBodyFormat = RequestBodyFormat.FormData;
            arraySerialization = ArrayParametersSerialization.MultipleValues;
        }
        #endregion

        #region Core Methods
        /// <summary>
        /// Sets the default options to use for new clients
        /// </summary>
        /// <param name="options">The options to use for new clients</param>
        public static void SetDefaultOptions(ThodexClientOptions options)
        {
            defaultOptions = options;
        }

        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        public void SetApiCredentials(string apiKey, string apiSecret)
        {
            SetAuthenticationProvider(new ThodexAuthenticationProvider(new ApiCredentials(apiKey, apiSecret), ArrayParametersSerialization.MultipleValues));
        }
        #endregion

        #region Api Methods
        public WebCallResult<ThodexServerTime> ServerTime(CancellationToken ct = default) => ServerTimeAsync(ct).Result;
        public async Task<WebCallResult<ThodexServerTime>> ServerTimeAsync(CancellationToken ct = default)
        {
            var result = await SendRequest<ThodexApiResponse<ThodexServerTime>>(GetUrl(Endpoints_Public_ServerTime), method: HttpMethod.Get, cancellationToken: ct, checkResult: false, signed: false).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<ThodexServerTime>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);

            return new WebCallResult<ThodexServerTime>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        public WebCallResult<IEnumerable<ThodexMarket>> GetMarkets(CancellationToken ct = default) => GetMarketsAsync(ct).Result;
        public async Task<WebCallResult<IEnumerable<ThodexMarket>>> GetMarketsAsync(CancellationToken ct = default)
        {
            var result = await SendRequest<ThodexApiResponse<IEnumerable<ThodexMarket>>>(GetUrl(Endpoints_Public_Markets), method: HttpMethod.Get, cancellationToken: ct, checkResult: false, signed: false).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<ThodexMarket>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);

            return new WebCallResult<IEnumerable<ThodexMarket>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        public WebCallResult<ThodexState> GetMarketStatus(string symbol, CancellationToken ct = default) => GetMarketStatusAsync(symbol, ct).Result;
        public async Task<WebCallResult<ThodexState>> GetMarketStatusAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "market", symbol },
            };
            var result = await SendRequest<ThodexApiResponse<ThodexState>>(GetUrl(Endpoints_Public_MarketStatus), method: HttpMethod.Get, cancellationToken: ct, checkResult: false, signed: false, parameters: parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<ThodexState>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);

            return new WebCallResult<ThodexState>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        public WebCallResult<IEnumerable<ThodexSummary>> GetMarketSummary(CancellationToken ct = default) => GetMarketSummaryAsync(ct).Result;
        public async Task<WebCallResult<IEnumerable<ThodexSummary>>> GetMarketSummaryAsync(CancellationToken ct = default)
        {
            var result = await SendRequest<ThodexApiResponse<IEnumerable<ThodexSummary>>>(GetUrl(Endpoints_Public_MarketSummary), method: HttpMethod.Get, cancellationToken: ct, checkResult: false, signed: false).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<ThodexSummary>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);

            return new WebCallResult<IEnumerable<ThodexSummary>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        public WebCallResult<IEnumerable<ThodexDeal>> GetDealsHistory(string symbol, int? last_id = null, CancellationToken ct = default) => GetDealsHistoryAsync(symbol, last_id, ct).Result;
        public async Task<WebCallResult<IEnumerable<ThodexDeal>>> GetDealsHistoryAsync(string symbol, int? last_id = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "market", symbol },
            };
            parameters.AddOptionalParameter("last_id", last_id);

            var result = await SendRequest<ThodexApiResponse<IEnumerable<ThodexDeal>>>(GetUrl(Endpoints_Public_MarketHistory), method: HttpMethod.Get, cancellationToken: ct, checkResult: false, signed: false, parameters: parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<ThodexDeal>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);

            return new WebCallResult<IEnumerable<ThodexDeal>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        public WebCallResult<ThodexOrderBook> GetOrderBook(string symbol, int limit = 100, CancellationToken ct = default) => GetOrderBookAsync(symbol, limit, ct).Result;
        public async Task<WebCallResult<ThodexOrderBook>> GetOrderBookAsync(string symbol, int limit = 100, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "market", symbol },
                { "limit", limit },
            };

            var result = await SendRequest<ThodexApiResponse<ThodexOrderBook>>(GetUrl(Endpoints_Public_OrderDepth), method: HttpMethod.Get, cancellationToken: ct, checkResult: false, signed: false, parameters: parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<ThodexOrderBook>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);

            return new WebCallResult<ThodexOrderBook>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        public WebCallResult<ThodexOrders> GetOpenOrders(string symbol, int limit = 50, int? offset = null, CancellationToken ct = default) => GetOpenOrdersAsync(symbol, limit, offset, ct).Result;
        public async Task<WebCallResult<ThodexOrders>> GetOpenOrdersAsync(string symbol, int limit = 50, int? offset = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                { "market", symbol},
                { "limit", limit},
            };
            parameters.AddOptionalParameter("offset", offset);

            var result = await SendRequest<ThodexApiResponse<ThodexOrders>>(GetUrl(Endpoints_Market_OpenOrders), method: HttpMethod.Get, cancellationToken: ct, checkResult: false, signed: true, parameters: parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<ThodexOrders>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);

            return new WebCallResult<ThodexOrders>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        public WebCallResult<ThodexOrdersHistory> GetOrderHistory(string symbol, int limit = 50, int? offset = null, CancellationToken ct = default) => GetOrderHistoryAsync(symbol, limit, offset, ct).Result;
        public async Task<WebCallResult<ThodexOrdersHistory>> GetOrderHistoryAsync(string symbol, int limit = 50, int? offset = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                { "market", symbol},
                { "limit", limit},
            };
            parameters.AddOptionalParameter("offset", offset);

            var result = await SendRequest<ThodexApiResponse<ThodexOrdersHistory>>(GetUrl(Endpoints_Market_OrderHistory), method: HttpMethod.Get, cancellationToken: ct, checkResult: false, signed: true, parameters: parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<ThodexOrdersHistory>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);

            return new WebCallResult<ThodexOrdersHistory>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        public WebCallResult<ThodexOrder> BuyLimit(string symbol, decimal amount, decimal price, CancellationToken ct = default) => BuyLimitAsync(symbol, amount, price, ct).Result;
        public async Task<WebCallResult<ThodexOrder>> BuyLimitAsync(string symbol, decimal amount, decimal price, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                { "market", symbol},
                { "amount", amount},
                { "price", price},
            };

            var result = await SendRequest<ThodexApiResponse<ThodexOrder>>(GetUrl(Endpoints_Market_BuyLimit), method: HttpMethod.Post, cancellationToken: ct, checkResult: false, signed: true, parameters: parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<ThodexOrder>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);

            return new WebCallResult<ThodexOrder>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        public WebCallResult<ThodexOrder> BuyMarket(string symbol, decimal amount, CancellationToken ct = default) => BuyMarketAsync(symbol, amount, ct).Result;
        public async Task<WebCallResult<ThodexOrder>> BuyMarketAsync(string symbol, decimal amount, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                { "market", symbol},
                { "amount", amount},
            };

            var result = await SendRequest<ThodexApiResponse<ThodexOrder>>(GetUrl(Endpoints_Market_BuyMarket), method: HttpMethod.Post, cancellationToken: ct, checkResult: false, signed: true, parameters: parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<ThodexOrder>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);

            return new WebCallResult<ThodexOrder>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        public WebCallResult<ThodexOrder> SellLimit(string symbol, decimal amount, decimal price, CancellationToken ct = default) => SellLimitAsync(symbol, amount, price, ct).Result;
        public async Task<WebCallResult<ThodexOrder>> SellLimitAsync(string symbol, decimal amount, decimal price, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                { "market", symbol},
                { "amount", amount},
                { "price", price},
            };

            var result = await SendRequest<ThodexApiResponse<ThodexOrder>>(GetUrl(Endpoints_Market_SellLimit), method: HttpMethod.Post, cancellationToken: ct, checkResult: false, signed: true, parameters: parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<ThodexOrder>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);

            return new WebCallResult<ThodexOrder>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        public WebCallResult<ThodexOrder> SellMarket(string symbol, decimal amount, CancellationToken ct = default) => SellMarketAsync(symbol, amount, ct).Result;
        public async Task<WebCallResult<ThodexOrder>> SellMarketAsync(string symbol, decimal amount, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                { "market", symbol},
                { "amount", amount},
            };

            var result = await SendRequest<ThodexApiResponse<ThodexOrder>>(GetUrl(Endpoints_Market_SellMarket), method: HttpMethod.Post, cancellationToken: ct, checkResult: false, signed: true, parameters: parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<ThodexOrder>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);

            return new WebCallResult<ThodexOrder>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        public WebCallResult<IEnumerable<long>> CancelOrder(string symbol, long order_id, CancellationToken ct = default) => CancelOrderAsync(symbol, order_id, ct).Result;
        public async Task<WebCallResult<IEnumerable<long>>> CancelOrderAsync(string symbol, long order_id, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                { "market", symbol},
                { "order_id", order_id},
            };

            var result = await SendRequest<ThodexApiResponse<IEnumerable<long>>>(GetUrl(Endpoints_Market_CancelOrder), method: HttpMethod.Post, cancellationToken: ct, checkResult: false, signed: true, parameters: parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<IEnumerable<long>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);

            return new WebCallResult<IEnumerable<long>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        public WebCallResult<ThodexBalance> GetBalances(IEnumerable<string> assets = null, CancellationToken ct = default) => GetBalancesAsync(assets, ct).Result;
        public async Task<WebCallResult<ThodexBalance>> GetBalancesAsync(IEnumerable<string> assets = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            if (assets != null && assets.Count() > 0)
                parameters.AddOptionalParameter("assets", string.Join(",", assets));

            var result = await SendRequest<ThodexApiResponse<ThodexBalance>>(GetUrl(Endpoints_Account_Balance), method: HttpMethod.Get, cancellationToken: ct, checkResult: false, signed: true, parameters: parameters).ConfigureAwait(false);
            if (!result.Success) return WebCallResult<ThodexBalance>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error);

            return new WebCallResult<ThodexBalance>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }
        #endregion

        #region Protected Methods

        protected override Error ParseErrorResponse(JToken error)
        {
            if (error["error"]["code"] == null || error["error"]["message"] == null)
                return new ServerError(error.ToString());

            return new ServerError((int)error["error"]["code"], (string)error["error"]["message"]);
        }

        protected Uri GetUrl(string endpoint)
        {
            return new Uri($"{BaseAddress.TrimEnd('/')}/{endpoint}");
        }

        #endregion

    }
}
