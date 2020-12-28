using Newtonsoft.Json;

namespace Thodex.Net.CoreObjects
{
    internal class ThodexApiResponse<T>
    {
        [JsonProperty("error")]
        internal ThodexApiError Error { get; set; }
        
        [JsonProperty("result")]
        public T Data { get; set; }
    }

    internal class ThodexApiError
    {
        [JsonProperty("code")]
        internal int ErrorCode { get; set; }

        [JsonProperty("message")]
        internal string ErrorMessage { get; set; } = "";
    }
}
