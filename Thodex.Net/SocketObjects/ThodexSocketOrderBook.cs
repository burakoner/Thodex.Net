using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using Thodex.Net.RestObjects;

namespace Thodex.Net.SocketObjects
{
    public class ThodexSocketOrderBook : ThodexOrderBook
    {
        public string Symbol { get; set; }

        public bool IsPartial { get; set; }

        public ThodexSocketOrderBook()
        {

        }

        public ThodexSocketOrderBook(string symbol, bool flag, ThodexOrderBook orderbook)
        {
            this.Symbol = symbol;
            this.IsPartial = !flag;
            this.Asks = orderbook.Asks != null ? orderbook.Asks : new List<ThodexOrderBookEntry>();
            this.Bids = orderbook.Bids != null ? orderbook.Bids : new List<ThodexOrderBookEntry>();
        }
    }
}
