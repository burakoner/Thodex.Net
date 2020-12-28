using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Thodex.Net.Converters;
using Thodex.Net.Enums;

namespace Thodex.Net.RestObjects
{
    public class ThodexOrdersHistory
    {
        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("records")]
        public IEnumerable<ThodexOrderHistory> Orders { get; set; }
    }

    public class ThodexOrderHistory
    {
        [JsonProperty("market")]
        public string Symbol { get; set; }

        [JsonProperty("time"), JsonConverter(typeof(TimestampSecondsConverter))]
        public DateTime UtcTime { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("side"), JsonConverter(typeof(OrderSideConverter))]
        public ThodexOrderSide Side { get; set; }
        
        [JsonProperty("role"), JsonConverter(typeof(TraderRoleConverter))]
        public ThodexTraderRole Role { get; set; }

        [JsonProperty("price")]
        public decimal? Price { get; set; }

        [JsonProperty("amount")]
        public decimal? Amount { get; set; }

        [JsonProperty("deal")]
        public decimal? Deal { get; set; }

        [JsonProperty("fee")]
        public decimal? Fee { get; set; }

        [JsonProperty("deal_order_id")]
        public long? DealId { get; set; }
    }
}
