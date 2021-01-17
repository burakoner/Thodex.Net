using CryptoExchange.Net.Converters;
using System.Collections.Generic;
using Thodex.Net.Enums;

namespace Thodex.Net.Converters
{
    public class OrderSideConverter : BaseConverter<ThodexOrderSide>
    {
        public OrderSideConverter() : this(true) { }
        public OrderSideConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<ThodexOrderSide, string>> Mapping => new List<KeyValuePair<ThodexOrderSide, string>>
        {
            new KeyValuePair<ThodexOrderSide, string>(ThodexOrderSide.Buy, "buy"),
            new KeyValuePair<ThodexOrderSide, string>(ThodexOrderSide.Sell, "sell"),

            new KeyValuePair<ThodexOrderSide, string>(ThodexOrderSide.Buy, "2"),
            new KeyValuePair<ThodexOrderSide, string>(ThodexOrderSide.Sell, "1"),
        };
    }
}