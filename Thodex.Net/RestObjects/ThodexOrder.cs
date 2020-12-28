using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Thodex.Net.Converters;
using Thodex.Net.Enums;

namespace Thodex.Net.RestObjects
{
    public class ThodexOrders
    {
        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("records")]
        public IEnumerable<ThodexOrder> Orders { get; set; }
    }

    public class ThodexOrder
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("market")]
        public string Symbol { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("type"), JsonConverter(typeof(OrderTypeConverter))]
        public ThodexOrderType Type { get; set; }

        [JsonProperty("side"), JsonConverter(typeof(OrderSideConverter))]
        public ThodexOrderSide Side { get; set; }

        [JsonProperty("ctime"), JsonConverter(typeof(TimestampSecondsConverter))]
        public DateTime CreateTimeUtc { get; set; }

        [JsonProperty("mtime"), JsonConverter(typeof(TimestampSecondsConverter))]
        public DateTime? ModifyTimeUtc { get; set; }

        [JsonProperty("price")]
        public decimal? Price { get; set; }

        [JsonProperty("amount")]
        public decimal? Amount { get; set; }

        [JsonProperty("taker_fee")]
        public decimal TakerFee { get; set; }

        [JsonProperty("maker_fee")]
        public decimal MakerFee { get; set; }

        [JsonProperty("left")]
        public decimal? Left { get; set; }

        [JsonProperty("deal_stock")]
        public decimal? DealStock { get; set; }

        [JsonProperty("deal_money")]
        public decimal? DealMoney { get; set; }

        [JsonProperty("deal_fee")]
        public decimal? DealFee { get; set; }
    }
}
