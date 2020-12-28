using Newtonsoft.Json;

namespace Thodex.Net.RestObjects
{
    public class ThodexToday
    {
        [JsonProperty("open")]
        public decimal Open { get; set; }
        
        [JsonProperty("low")]
        public decimal Low { get; set; }
        
        [JsonProperty("last")]
        public decimal Last { get; set; }
        
        [JsonProperty("high")]
        public decimal High { get; set; }
        
        [JsonProperty("volume")]
        public decimal Volume { get; set; }
        
        [JsonProperty("deal")]
        public decimal Deal { get; set; }
    }
}