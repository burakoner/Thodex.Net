using CryptoExchange.Net.Objects;

namespace Thodex.Net.CoreObjects
{
    public class ThodexClientOptions: RestClientOptions
    {
        public ThodexClientOptions():base("https://api.thodex.com")
        {
        }

        public ThodexClientOptions Copy()
        {
            return Copy<ThodexClientOptions>();
        }
    }
}
