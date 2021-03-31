using CryptoExchange.Net;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Newtonsoft.Json.Linq;
using Thodex.Net.CoreObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Thodex.Net.SocketObjects;
using Thodex.Net.RestObjects;
using Newtonsoft.Json;
using Thodex.Net.Enums;
using Thodex.Net.Converters;
using Thodex.Net.Helpers;
using CryptoExchange.Net.Converters;
using System.Net;

namespace Thodex.Net
{
    /// <summary>
    /// Client for the Thodex Websocket API
    /// </summary>
    public partial class ThodexSocketClient : SocketClient, ISocketClient
    {
        public bool Authendicated { get; protected set; }

        #region WS-Methods
        /* Account Balance (Private) */
        protected const string WSMethods_Asset_Query = "asset.query";
        protected const string WSMethods_Asset_Update = "asset.update";
        protected const string WSMethods_Asset_Subscribe = "asset.subscribe";
        protected const string WSMethods_Asset_Unsubscribe = "asset.unsubscribe";
        protected const string WSMethods_Asset_History = "asset.history";

        /* Trades (Public) */
        protected const string WSMethods_Deals_Query = "deals.query";
        protected const string WSMethods_Deals_Update = "deals.update";
        protected const string WSMethods_Deals_Subscribe = "deals.subscribe";
        protected const string WSMethods_Deals_Unsubscribe = "deals.unsubscribe";

        /* Order Book (Public) */
        protected const string WSMethods_Depth_Query = "depth.query";
        protected const string WSMethods_Depth_Update = "depth.update";
        protected const string WSMethods_Depth_Subscribe = "depth.subscribe";
        protected const string WSMethods_Depth_Unsubscribe = "depth.unsubscribe";

        /* Klines (Public) */
        protected const string WSMethods_Kline_Query = "kline.query";
        protected const string WSMethods_Kline_Update = "kline.update";
        protected const string WSMethods_Kline_Subscribe = "kline.subscribe";
        protected const string WSMethods_Kline_Unsubscribe = "kline.unsubscribe";

        /* Order Tracker (Private) */
        protected const string WSMethods_Order_Query = "order.query";
        protected const string WSMethods_Order_Update = "order.update";
        protected const string WSMethods_Order_Subscribe = "order.subscribe";
        protected const string WSMethods_Order_Unsubscribe = "order.unsubscribe";
        protected const string WSMethods_Order_History = "order.history";

        /* Price Updates (Public) */
        protected const string WSMethods_Price_Query = "price.query";
        protected const string WSMethods_Price_Update = "price.update";
        protected const string WSMethods_Price_Subscribe = "price.subscribe";
        protected const string WSMethods_Price_Unsubscribe = "price.unsubscribe";

        /* State Updates (Public) */
        protected const string WSMethods_State_Query = "state.query";
        protected const string WSMethods_State_Update = "state.update";
        protected const string WSMethods_State_Subscribe = "state.subscribe";
        protected const string WSMethods_State_Unsubscribe = "state.unsubscribe";

        /* StopLoss Tracker ? (Private) */
        protected const string WSMethods_StopLoss_Query = "stoploss.query";
        protected const string WSMethods_StopLoss_Update = "stoploss.update";
        protected const string WSMethods_StopLoss_Subscribe = "stoploss.subscribe";
        protected const string WSMethods_StopLoss_Unsubscribe = "stoploss.unsubscribe";
        protected const string WSMethods_StopLoss_History = "stoploss.history";

        /* Today Updates (Public) */
        protected const string WSMethods_Today_Query = "today.query";
        protected const string WSMethods_Today_Update = "today.update";
        protected const string WSMethods_Today_Subscribe = "today.subscribe";
        protected const string WSMethods_Today_Unsubscribe = "today.unsubscribe";

        /* Server Queries */
        protected const string WSMethods_ServerAuth_Query = "server.auth";
        protected const string WSMethods_ServerPing_Query = "server.ping";
        protected const string WSMethods_ServerSign_Query = "server.sign";
        protected const string WSMethods_ServerTime_Query = "server.time";
        #endregion

        #region Client Options
        protected static ThodexSocketClientOptions defaultOptions = new ThodexSocketClientOptions();
        protected static ThodexSocketClientOptions DefaultOptions => defaultOptions.Copy();
        #endregion

        #region Constructor/Destructor
        /// <summary>
        /// Create a new instance of ThodexSocketClient with default options
        /// </summary>
        public ThodexSocketClient() : this(DefaultOptions)
        {
        }

        /// <summary>
        /// Create a new instance of ThodexSocketClient using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public ThodexSocketClient(ThodexSocketClientOptions options) : base("Thodex", options, options.ApiCredentials == null ? null : new ThodexAuthenticationProvider(options.ApiCredentials, ArrayParametersSerialization.Array))
        {
        }
        #endregion

        #region Common Methods
        /// <summary>
        /// Set the default options to be used when creating new socket clients
        /// </summary>
        /// <param name="options">The options to use for new clients</param>
        public static void SetDefaultOptions(ThodexSocketClientOptions options)
        {
            defaultOptions = options;
        }
        #endregion

        public virtual CallResult<ThodexSocketPingPong> Ping() => PingAsync().Result;
        public virtual async Task<CallResult<ThodexSocketPingPong>> PingAsync()
        {
            var pit = DateTime.UtcNow;
            var sw = Stopwatch.StartNew();
            var request = new ThodexSocketRequest(this.NextRequestId(), WSMethods_ServerPing_Query);
            var response = await Query<ThodexSocketQueryResponse<string>>(request, false).ConfigureAwait(true);
            var pot = DateTime.UtcNow;
            sw.Stop();

            var result = new ThodexSocketPingPong { PingTime = pit, PongTime = pot, Latency = sw.Elapsed, PongMessage = response.Data.Data };
            return new CallResult<ThodexSocketPingPong>(result, response.Error);
        }

        public virtual CallResult<DateTime> Time() => TimeAsync().Result;
        public virtual async Task<CallResult<DateTime>> TimeAsync()
        {
            var request = new ThodexSocketRequest(this.NextRequestId(), WSMethods_ServerTime_Query);
            var response = await Query<ThodexSocketQueryResponse<int>>(request, false).ConfigureAwait(true);

            return new CallResult<DateTime>(response.Data.Data.FromUnixTimeSeconds(), response.Error);
        }

        public virtual CallResult<UpdateSubscription> SubscribeToDeals(IEnumerable<string> symbols, Action<ThodexSocketDeal> onData) => SubscribeToDealsAsync(symbols, onData).Result;
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToDealsAsync(IEnumerable<string> symbols, Action<ThodexSocketDeal> onData)
        {
            // To List
            var symbolList = symbols.ToList();

            // Check Point
            if (symbolList.Count == 0)
                throw new ArgumentException("Symbols must contain 1 element at least");

            // Check Point
            if (symbolList.Count > 100)
                throw new ArgumentException("Symbols can contain maximum 100 elements");

            var internalHandler = new Action<ThodexSocketUpdateResponse>(data =>
            {
                if (data.Method == WSMethods_Deals_Update && data.Data.Count == 2)
                {
                    if (symbols.Contains((string)data.Data[0]))
                    {
                        var symbol = (string)data.Data[0];
                        var deals = JsonConvert.DeserializeObject<IEnumerable<ThodexDeal>>(data.Data[1].ToString());
                        foreach (var deal in deals)
                        {
                            onData(new ThodexSocketDeal(symbol, deal));
                        }
                    }
                }
            });

            var request = new ThodexSocketRequest(this.NextRequestId(), WSMethods_Deals_Subscribe, symbols);
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        public virtual CallResult<UpdateSubscription> SubscribeToOrderBook(string symbol, Action<ThodexSocketOrderBook> onData) => SubscribeToOrderBookAsync(symbol, onData).Result;
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookAsync(string symbol, Action<ThodexSocketOrderBook> onData)
        {
            var internalHandler = new Action<ThodexSocketUpdateResponse>(data =>
            {
                if (data.Method == WSMethods_Depth_Update && data.Data.Count == 3)
                {
                    if (symbol == (string)data.Data[2])
                    {
                        var orderbook = JsonConvert.DeserializeObject<ThodexOrderBook>(data.Data[1].ToString());
                        onData(new ThodexSocketOrderBook((string)data.Data[2], (bool)data.Data[0], orderbook));
                    }
                }
            });

            var request = new ThodexSocketRequest(this.NextRequestId(), WSMethods_Depth_Subscribe, symbol, 100, "0");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        public virtual CallResult<UpdateSubscription> SubscribeToKlines(string symbol, ThodexPeriod period, Action<ThodexSocketCandle> onData) => SubscribeToKlinesAsync(symbol, period, onData).Result;
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToKlinesAsync(string symbol, ThodexPeriod period, Action<ThodexSocketCandle> onData)
        {
            var internalHandler = new Action<ThodexSocketUpdateResponse>(data =>
            {
                if (data.Method == WSMethods_Kline_Update && data.Data.Count == 1)
                {
                    var json = data.Data[0].ToString();
                    if (json.StartsWith("[[") && json.EndsWith("]]"))
                    {
                        var klines = JsonConvert.DeserializeObject<IEnumerable<ThodexSocketCandle>>(json);
                        foreach (var kline in klines)
                            if (kline.Symbol.ToUpper() == symbol.ToUpper())
                                onData(kline);
                    }
                    else
                    {
                        var kline = JsonConvert.DeserializeObject<ThodexSocketCandle>(json);
                        if (kline.Symbol.ToUpper() == symbol.ToUpper())
                            onData(kline);
                    }
                }
            });

            var request = new ThodexSocketRequest(this.NextRequestId(), WSMethods_Kline_Subscribe, symbol, Convert.ToInt32(JsonConvert.SerializeObject(period, new PeriodConverter(false)))); ;
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        public virtual CallResult<UpdateSubscription> SubscribeToPrice(IEnumerable<string> symbols, Action<ThodexSocketPrice> onData) => SubscribeToPriceAsync(symbols, onData).Result;
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToPriceAsync(IEnumerable<string> symbols, Action<ThodexSocketPrice> onData)
        {
            // To List
            var symbolList = symbols.ToList();

            // Check Point
            if (symbolList.Count == 0)
                throw new ArgumentException("Symbols must contain 1 element at least");

            // Check Point
            if (symbolList.Count > 100)
                throw new ArgumentException("Symbols can contain maximum 100 elements");

            var internalHandler = new Action<ThodexSocketUpdateResponse>(data =>
            {
                if (data.Method == WSMethods_Price_Update && data.Data.Count == 2)
                {
                    if (symbols.Contains((string)data.Data[0]))
                    {
                        onData(new ThodexSocketPrice((string)data.Data[0], ((string)data.Data[1]).ToDecimal()));
                    }
                }
            });

            var request = new ThodexSocketRequest(this.NextRequestId(), WSMethods_Price_Subscribe, symbols);
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        public virtual CallResult<UpdateSubscription> SubscribeToState(IEnumerable<string> symbols, Action<ThodexSocketState> onData) => SubscribeToStateAsync(symbols, onData).Result;
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToStateAsync(IEnumerable<string> symbols, Action<ThodexSocketState> onData)
        {
            // To List
            var symbolList = symbols.ToList();

            // Check Point
            if (symbolList.Count == 0)
                throw new ArgumentException("Symbols must contain 1 element at least");

            // Check Point
            if (symbolList.Count > 100)
                throw new ArgumentException("Symbols can contain maximum 100 elements");

            var internalHandler = new Action<ThodexSocketUpdateResponse>(data =>
            {
                if (data.Method == WSMethods_State_Update && data.Data.Count == 2)
                {
                    if (symbols.Contains((string)data.Data[0]))
                    {
                        var symbol = (string)data.Data[0];
                        var status = JsonConvert.DeserializeObject<ThodexState>(data.Data[1].ToString());
                        onData(new ThodexSocketState(symbol, status));
                    }
                }
            });

            var request = new ThodexSocketRequest(this.NextRequestId(), WSMethods_State_Subscribe, symbols);
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        public virtual CallResult<UpdateSubscription> SubscribeToToday(IEnumerable<string> symbols, Action<ThodexSocketToday> onData) => SubscribeToTodayAsync(symbols, onData).Result;
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToTodayAsync(IEnumerable<string> symbols, Action<ThodexSocketToday> onData)
        {
            // To List
            var symbolList = symbols.ToList();

            // Check Point
            if (symbolList.Count == 0)
                throw new ArgumentException("Symbols must contain 1 element at least");

            // Check Point
            if (symbolList.Count > 100)
                throw new ArgumentException("Symbols can contain maximum 100 elements");

            var internalHandler = new Action<ThodexSocketUpdateResponse>(data =>
            {
                if (data.Method == WSMethods_Today_Update && data.Data.Count == 2)
                {
                    if (symbols.Contains((string)data.Data[0]))
                    {
                        var symbol = (string)data.Data[0];
                        var today = JsonConvert.DeserializeObject<ThodexToday>(data.Data[1].ToString());
                        onData(new ThodexSocketToday(symbol, today));
                    }
                }
            });

            var request = new ThodexSocketRequest(this.NextRequestId(), WSMethods_Today_Subscribe, symbols);
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        #region Core Methods
        protected long iterator = 0;
        protected virtual long NextRequestId()
        {
            return ++iterator;
        }

        protected override bool HandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
        {
            return this.ThodexHandleQueryResponse<T>(s, request, data, out callResult);
        }
        protected virtual bool ThodexHandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
        {
            callResult = new CallResult<T>(default, null);

            /** /
            // Check for Structure
            if (data["result"] == null || data["id"] == null)
            {
                return true;
            }
            */

            // Check Request & Response
            if (request is ThodexSocketRequest req)
            {
                if (data["id"] != null && (long?)data["id"] == req.RequestId)
                {
                    var desResult = Deserialize<T>(data, false);
                    callResult = new CallResult<T>(desResult.Data, null);
                    return true;
                }
            }

            return false;
        }

        protected override bool HandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object> callResult)
        {
            return this.ThodexHandleSubscriptionResponse(s, subscription, request, message, out callResult);
        }
        protected virtual bool ThodexHandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object> callResult)
        {
            var MSG = message.ToString();
            callResult = null;

            // Check for Success
            if (request is ThodexSocketRequest socRequest)
            {
                if (message["id"] != null && (long?)message["id"] == socRequest.RequestId)
                {
                    var resp = JsonConvert.DeserializeObject<ThodexSocketQueryResponse<ThodexSocketFeedback>>(message.ToString());
                    if (resp.Data != null && resp.Data.Status.ToLower() == "success")
                    {
                        callResult = new CallResult<object>(true, null);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        protected override bool MessageMatchesHandler(JToken data, object request)
        {
            return this.ThodexMessageMatchesHandler(data, request);
        }
        protected virtual bool ThodexMessageMatchesHandler(JToken data, object request)
        {
            if (request is ThodexSocketRequest hRequest)
            {
                if (data["method"] == null)
                    return false;

                // Get Method
                var method = (string)data["method"];

                if (request is ThodexSocketRequest req)
                {
                    // Asset Update
                    if (method == WSMethods_Asset_Update && req.Method == WSMethods_Asset_Subscribe)
                        return true;

                    // Deals Update
                    if (method == WSMethods_Deals_Update && req.Method == WSMethods_Deals_Subscribe)
                        return true;

                    // Depth Update
                    if (method == WSMethods_Depth_Update && req.Method == WSMethods_Depth_Subscribe)
                        return true;

                    // Kline Update
                    if (method == WSMethods_Kline_Update && req.Method == WSMethods_Kline_Subscribe)
                        return true;

                    // Order Update
                    if (method == WSMethods_Order_Update && req.Method == WSMethods_Order_Subscribe)
                        return true;

                    // Price Update
                    if (method == WSMethods_Price_Update && req.Method == WSMethods_Price_Subscribe)
                        return true;

                    // State Update
                    if (method == WSMethods_State_Update && req.Method == WSMethods_State_Subscribe)
                        return true;

                    // StopLoss Update
                    if (method == WSMethods_StopLoss_Update && req.Method == WSMethods_StopLoss_Subscribe)
                        return true;

                    // Today Update
                    if (method == WSMethods_Today_Update && req.Method == WSMethods_Today_Subscribe)
                        return true;
                }

            }

            return false;
        }

        protected override bool MessageMatchesHandler(JToken message, string identifier)
        {
            return this.ThodexMessageMatchesHandler(message, identifier);
        }
        protected virtual bool ThodexMessageMatchesHandler(JToken message, string identifier)
        {
            return true;
        }

        protected override async Task<bool> Unsubscribe(SocketConnection connection, SocketSubscription s)
        {
            return await this.ThodexUnsubscribe(connection, s);
        }
        protected virtual async Task<bool> ThodexUnsubscribe(SocketConnection connection, SocketSubscription s)
        {
            if (s == null || s.Request == null)
                return false;

            var id = this.NextRequestId();
            var request = new ThodexSocketRequest(id, ((ThodexSocketRequest)s.Request).Method.Replace(".subscribe", ".unsubscribe"), ((ThodexSocketRequest)s.Request).Params);
            await connection.SendAndWait(request, ResponseTimeout, data =>
            {
                if (data.Type != JTokenType.Object)
                    return false;

                if (data["id"] != null && (long?)data["id"] == id)
                {
                    var resp = JsonConvert.DeserializeObject<ThodexSocketQueryResponse<ThodexSocketFeedback>>(data.ToString());
                    return resp.Data.Status.ToLower() == "success";
                }

                return false;
            });

            return false;
        }

        protected override async Task<CallResult<bool>> AuthenticateSocket(SocketConnection s)
        {
            return await this.ThodexAuthenticateSocket(s);
        }
        protected virtual async Task<CallResult<bool>> ThodexAuthenticateSocket(SocketConnection s)
        {
            if (authProvider == null)
                return new CallResult<bool>(false, new NoApiCredentialsError());

            var ap = (ThodexAuthenticationProvider)authProvider;
            var key = ap.Credentials.Key.GetString();
            var secret = ap.Credentials.Secret.GetString();
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(secret))
                return new CallResult<bool>(false, new NoApiCredentialsError());

            var id = NextRequestId();
            var tonce = DateTime.UtcNow.ToUnixTimeSeconds();//.ToString();
            var signparams = new SortedDictionary<string, object>();
            signparams.Add("access_id", key);
            signparams.Add("tonce", tonce);
            var signstring = string.Join("&", signparams.Select(x => $"{x.Key}={WebUtility.UrlEncode(x.Value.ToString())}")) + $"&secret={secret}";
            var signature = ThodexAuthenticationProvider.SHA256Hash(signstring);

            var request = new ThodexSocketRequest(id, WSMethods_ServerSign_Query, key, signature, tonce);
            // var msg = JsonConvert.SerializeObject(request);
            var response = await Query<ThodexSocketQueryResponse<int>>(request, false).ConfigureAwait(true);
            var result = new CallResult<bool>(false, new ServerError("No response from server"));
            await s.SendAndWait(request, ResponseTimeout, data =>
            {
                if (data["id"] == null || (long?)data["id"] != id)
                    return false;

                if (data["error"] != null && data["error"]["code"] != null && data["error"]["message"] != null)
                {
                    var code = (int?)data["error"]["code"]; if (!code.HasValue) code = 0;
                    var message = (string)data["error"]["message"]; if (string.IsNullOrEmpty(message)) message = string.Empty;
                    log.Write(LogVerbosity.Warning, "Authorization failed: " + message);
                    result = new CallResult<bool>(false, new ServerError(code.Value, message));
                    return true;
                }

                log.Write(LogVerbosity.Debug, "Authorization completed");
                result = new CallResult<bool>(true, null);
                this.Authendicated = true;
                return true;
            });

            return result;
        }

        #endregion

    }
}
