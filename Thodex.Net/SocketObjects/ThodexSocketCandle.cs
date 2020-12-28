using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Thodex.Net.SocketObjects
{
    [JsonConverter(typeof(ArrayConverter))]
    public class ThodexSocketCandle
    {
        [ArrayProperty(0)]
        [JsonConverter(typeof(TimestampSecondsConverter))]
        public DateTime UtcTime { get; set; }

        [ArrayProperty(1)]
        public decimal Open { get; set; }
        
        [ArrayProperty(2)]
        public decimal Close { get; set; }
        
        [ArrayProperty(3)]
        public decimal High { get; set; }
        
        [ArrayProperty(4)]
        public decimal Low { get; set; }
        
        [ArrayProperty(5)]
        public decimal StockVolume { get; set; }

        [ArrayProperty(6)]
        public decimal MoneyVolume { get; set; }
        
        [ArrayProperty(7)]
        public string Symbol { get; set; }

    }
}
