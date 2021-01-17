using Newtonsoft.Json;

namespace Thodex.Net.CoreObjects
{
    public class ThodexApiResponse<T>
    {
        [JsonProperty("error")]
        public ThodexApiError Error { get; set; }
        
        [JsonProperty("result")]
        public T Data { get; set; }
    }

    public class ThodexApiError
    {
        [JsonProperty("code")]
        public int ErrorCode { get; set; }

        [JsonProperty("message")]
        public string ErrorMessage { get; set; } = "";
    }
}
