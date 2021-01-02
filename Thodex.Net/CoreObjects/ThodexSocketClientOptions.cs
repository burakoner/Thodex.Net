using CryptoExchange.Net.Objects;

namespace Thodex.Net.CoreObjects
{
    public class ThodexSocketClientOptions : SocketClientOptions
    {
        // public ThodexSocketClientOptions(): base("wss://wspub.thodex.com")
        public ThodexSocketClientOptions(): base("wss://ws.thodex.com")
        {
            SocketSubscriptionsCombineTarget = 1;
        }

        public ThodexSocketClientOptions Copy()
        {
            var copy = Copy<ThodexSocketClientOptions>();
            return copy;
        }
    }
}
