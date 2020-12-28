using Newtonsoft.Json;

namespace Thodex.Net.RestObjects
{
    public class ThodexSummary
    {
        [JsonProperty("name")]
        public string Symbol { get; set; }
        
        [JsonProperty("ask_count")]
        public int AskCount { get; set; }
        
        [JsonProperty("ask_amount")]
        public decimal AskAmount { get; set; }
        
        [JsonProperty("bid_count")]
        public int BidCount { get; set; }

        [JsonProperty("bid_amount")]
        public decimal BidAmount { get; set; }
    }
}