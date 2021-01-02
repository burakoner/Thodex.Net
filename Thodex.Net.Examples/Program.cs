using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Thodex.Net.CoreObjects;

namespace Thodex.Net.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            // var api = new ThodexClient(new ThodexClientOptions { LogVerbosity = LogVerbosity.Debug });
            var api = new ThodexClient();
            api.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX");
            
            var server_time = api.ServerTime();
            var market_list = api.GetMarkets();
            var market_status = api.GetMarketStatus("BTCTRY");
            var market_summary = api.GetMarketSummary();
            var trade_history_01 = api.GetDealsHistory("BTCTRY");
            var trade_history_02 = api.GetDealsHistory("BTCTRY", 9550000);
            var order_book_01 = api.GetOrderBook("BTCTRY");
            var order_book_02 = api.GetOrderBook("BTCTRY", 20);

            var open_orders_01 = api.GetOpenOrders("BTCTRY");
            var open_orders_02 = api.GetOpenOrders("BTCTRY", 50);
            var open_orders_03 = api.GetOpenOrders("BTCTRY", 20, 5);
            var order_history_01 = api.GetOrderHistory("BTCTRY");
            var order_history_02 = api.GetOrderHistory("BTCTRY", 50);
            var order_history_03 = api.GetOrderHistory("BTCTRY", 50, 5);
            var buy_limit = api.BuyLimit("BTCTRY", 0.01m, 180376.99m);
            var buy_market = api.BuyMarket("BTCTRY", 0.01m);
            var sell_limit = api.BuyLimit("BTCTRY", 0.01m, 180376.99m);
            var sell_market = api.BuyMarket("BTCTRY", 0.01m);
            var cancel_order = api.CancelOrder("BTCTRY", order_id:1001);

            var balances_01 = api.GetBalances();
            var balances_02 = api.GetBalances(new List<string> { "BTC", "TRY" });


            var ws = new ThodexSocketClient();
            var ping = ws.Ping();
            var time = ws.Time();
            var symbols = new List<string> { "BTCTRY", "ETHTRY", "LTCTRY", "HOTTRY", "HOTUSDT", "DASHTRY", "LINKUSDT", "DOGETRY", "LINKTRY", "BATUSDT", "XRPTRY", "BATTRY", "XLMTRY", "BCHTRY", "EOSTRY", "XEMTRY", "BTGTRY", "ETCTRY", "USDTTRY", "TRXTRY", "BTTTRY", "ADATRY", "XMRTRY", "ZECTRY", "BTCUSDT", "ETHUSDT", "LTCUSDT", "DOGEUSDT", "XRPUSDT", "XLMUSDT", "BCHUSDT", "EOSUSDT", "ETCUSDT", "TRXUSDT", "ETHBTC", "TRXBTC", "XRPBTC", "LTCBTC", "BCHBTC", "XLMBTC" };
            
            var ws01 = ws.SubscribeToState(symbols, (data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Ticker Update >> {data.Symbol} >> O:{data.Open} H:{data.High} L:{data.Low} C:{data.Close} SV:{data.StockVolume} MV:{data.MoneyVolume}");
                }
            });
            // var uns = ws.Unsubscribe(ws02.Data);
            
            var ws02 = ws.SubscribeToDeals(symbols, (data) =>
             {
                 if (data != null)
                 {
                     Console.WriteLine($"Deal Update >> {data.Symbol} >> I:{data.Id} T:{data.UtcTime} S:{data.Side} P:{data.Price} A:{data.Amount}");
                 }
             });

            var ws03 = ws.SubscribeToOrderBook("BTCTRY", (data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Book Update >> {data.Symbol} >> Partial:{data.IsPartial} Bids:{data.Bids.Count()} Asks:{data.Asks.Count()}");
                }
            });

            var ws04 = ws.SubscribeToKlines("BTCTRY", Enums.ThodexPeriod.TwoHours, (data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Kline Update >> {data.Symbol} >> O:{data.Open} H:{data.High} L:{data.Low} C:{data.Close} SV:{data.StockVolume} MV:{data.MoneyVolume}");
                }
            });

            var ws05 = ws.SubscribeToPrice(symbols, (data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Price Update >> {data.Symbol} >> P:{data.Price}");
                }
            });

            var ws06 = ws.SubscribeToToday(symbols, (data) =>
            {
                if (data != null)
                {
                    Console.WriteLine($"Today Update >> {data.Symbol} >> O:{data.Open} H:{data.High} L:{data.Low} C:{data.Last} V:{data.Volume} D:{data.Deal}");
                }
            });

            // Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
