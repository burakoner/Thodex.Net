using Newtonsoft.Json;
using Thodex.Net.Converters;
using Thodex.Net.Enums;

namespace Thodex.Net.RestObjects
{
    public class ThodexState
    {
        [JsonProperty("period"), JsonConverter(typeof(PeriodConverter))]
        public ThodexPeriod Period { get; set; }
        
        [JsonProperty("open")]
        public decimal Open { get; set; }
        
        [JsonProperty("high")]
        public decimal High { get; set; }
        
        [JsonProperty("low")]
        public decimal Low { get; set; }
        
        [JsonProperty("close")]
        public decimal Close { get; set; }

        [JsonProperty("last")]
        public decimal Last { get; set; }
        
        [JsonProperty("volume")]
        public decimal StockVolume { get; set; }
        
        [JsonProperty("deal")]
        public decimal MoneyVolume { get; set; }
        
    }
}