using CryptoExchange.Net.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Thodex.Net.CoreObjects
{
    public class ThodexSocketQueryResponse<T>
    {
        [JsonProperty("id")]
        public long RequestId { get; set; }

        [JsonProperty("error")]
        public object Error { get; set; }

        [JsonProperty("result")]
        public T Data { get; set; }
    }
    
    public class ThodexSocketUpdateResponse
    {
        [JsonProperty("id")]
        public long? RequestId { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("params")]
        public List<object> Data { get; set; }
    }

    public class ThodexSocketFeedback
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
