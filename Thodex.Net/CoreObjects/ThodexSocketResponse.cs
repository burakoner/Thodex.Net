using CryptoExchange.Net.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Thodex.Net.CoreObjects
{
    public class ThodexSocketQueryResponse<T>
    {
        [JsonProperty("id")]
        internal long RequestId { get; set; }

        [JsonProperty("error")]
        internal object Error { get; set; }

        [JsonProperty("result")]
        internal T Data { get; set; }
    }
    
    public class ThodexSocketUpdateResponse
    {
        [JsonProperty("id")]
        internal long? RequestId { get; set; }

        [JsonProperty("method")]
        internal string Method { get; set; }

        [JsonProperty("params")]
        internal List<object> Data { get; set; }
    }

    public class ThodexSocketFeedback
    {
        [JsonProperty("status")]
        internal string Status { get; set; }
    }
}
