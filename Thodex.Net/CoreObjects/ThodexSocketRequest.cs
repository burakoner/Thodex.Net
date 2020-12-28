using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Thodex.Net.CoreObjects
{
    internal class ThodexSocketRequest
    {
        [JsonProperty("id")]
        public long RequestId { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("params")]
        public List<object> Params { get; set; }

        public ThodexSocketRequest(long id, string method, params object[] args)
        {
            this.RequestId = id;
            this.Method = method;
            this.Params = args.ToList();
        }

        public ThodexSocketRequest(long id, string method, List<object> args)
        {
            this.RequestId = id;
            this.Method = method;
            this.Params = args.ToList();
        }

        public ThodexSocketRequest(long id, string method, IEnumerable<object> args)
        {
            this.RequestId = id;
            this.Method = method;
            this.Params = args.ToList();
        }
    }
}
