using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using Thodex.Net.Converters;
using Thodex.Net.Enums;

namespace Thodex.Net.RestObjects
{
    public class ThodexDeal
    {
        [JsonProperty("type"), JsonConverter(typeof(OrderSideConverter))]
        public ThodexOrderSide Side { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("time"), JsonConverter(typeof(TimestampSecondsConverter))]
        public DateTime UtcTime { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

    }
}
