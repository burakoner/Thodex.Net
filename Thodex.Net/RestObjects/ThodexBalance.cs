using Newtonsoft.Json;
using System.Collections.Generic;
using Thodex.Net.Attributes;

namespace Thodex.Net.RestObjects
{
    [JsonConverter(typeof(TypedDataConverter<ThodexBalance>))]
    public class ThodexBalance
    {
        [TypedData]
        public Dictionary<string, ThodexBalanceEntry> Balances { get; set; }
    }

    public class ThodexBalanceEntry
    {
        [JsonProperty("available")]
        public decimal Available { get; set; }
        
        [JsonProperty("freeze")]
        public decimal Freeze { get; set; }
    }
}
