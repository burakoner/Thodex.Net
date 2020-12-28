using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Thodex.Net.RestObjects
{
    public class ThodexServerTime
    {
        [JsonProperty("time"), JsonConverter(typeof(TimestampSecondsConverter))]
        public DateTime UtcTime { get; set; }
        
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
