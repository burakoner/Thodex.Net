using CryptoExchange.Net.Converters;
using System.Collections.Generic;
using Thodex.Net.Enums;

namespace Thodex.Net.Converters
{
    internal class OrderTypeConverter : BaseConverter<ThodexOrderType>
    {
        public OrderTypeConverter() : this(true) { }
        public OrderTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<ThodexOrderType, string>> Mapping => new List<KeyValuePair<ThodexOrderType, string>>
        {
            new KeyValuePair<ThodexOrderType, string>(ThodexOrderType.Limit, "limit"),
            new KeyValuePair<ThodexOrderType, string>(ThodexOrderType.Market, "market"),

            new KeyValuePair<ThodexOrderType, string>(ThodexOrderType.Limit, "1"),
            new KeyValuePair<ThodexOrderType, string>(ThodexOrderType.Market, "2"),
        };
    }
}