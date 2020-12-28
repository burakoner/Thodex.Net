using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Thodex.Net.RestObjects
{
    public class ThodexOrderBook
    {
        [JsonProperty("asks")]
        public IEnumerable<ThodexOrderBookEntry> Asks { get; set; }

        [JsonProperty("bids")]
        public IEnumerable<ThodexOrderBookEntry> Bids { get; set; }
    }

    [JsonConverter(typeof(ArrayConverter))]
    public class ThodexOrderBookEntry : ISymbolOrderBookEntry
    {
        /// <summary>
        /// The price for this entry
        /// </summary>
        [ArrayProperty(0)]
        public decimal Price { get; set; }
        /// <summary>
        /// The quantity for this entry
        /// </summary>
        [ArrayProperty(1)]
        public decimal Quantity { get; set; }

    }
}
