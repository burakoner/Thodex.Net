using CryptoExchange.Net.Converters;
using System.Collections.Generic;
using Thodex.Net.Enums;

namespace Thodex.Net.Converters
{
    internal class TraderRoleConverter : BaseConverter<ThodexTraderRole>
    {
        public TraderRoleConverter() : this(true) { }
        public TraderRoleConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<ThodexTraderRole, string>> Mapping => new List<KeyValuePair<ThodexTraderRole, string>>
        {
            new KeyValuePair<ThodexTraderRole, string>(ThodexTraderRole.Maker, "maker"),
            new KeyValuePair<ThodexTraderRole, string>(ThodexTraderRole.Taker, "taker"),

            new KeyValuePair<ThodexTraderRole, string>(ThodexTraderRole.Maker, "1"),
            new KeyValuePair<ThodexTraderRole, string>(ThodexTraderRole.Taker, "2"),
        };
    }
}