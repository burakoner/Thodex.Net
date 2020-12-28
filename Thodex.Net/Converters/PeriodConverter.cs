using CryptoExchange.Net.Converters;
using System.Collections.Generic;
using Thodex.Net.Enums;

namespace Thodex.Net.Converters
{
    internal class PeriodConverter : BaseConverter<ThodexPeriod>
    {
        public PeriodConverter() : this(true) { }
        public PeriodConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<ThodexPeriod, string>> Mapping => new List<KeyValuePair<ThodexPeriod, string>>
        {
            new KeyValuePair<ThodexPeriod, string>(ThodexPeriod.FiveMinutes, "300"),
            new KeyValuePair<ThodexPeriod, string>(ThodexPeriod.FifteenMinutes, "900"),
            new KeyValuePair<ThodexPeriod, string>(ThodexPeriod.ThirtyMinutes, "1800"),
            new KeyValuePair<ThodexPeriod, string>(ThodexPeriod.OneHour, "3600"),
            new KeyValuePair<ThodexPeriod, string>(ThodexPeriod.TwoHours, "7200"),
            new KeyValuePair<ThodexPeriod, string>(ThodexPeriod.TwelveHours, "43200"),
            new KeyValuePair<ThodexPeriod, string>(ThodexPeriod.OneDay, "86400"),
            new KeyValuePair<ThodexPeriod, string>(ThodexPeriod.OneWeek, "604800"),
        };
    }
}