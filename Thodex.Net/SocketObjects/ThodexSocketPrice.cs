using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Thodex.Net.SocketObjects
{
    [JsonConverter(typeof(ArrayConverter))]
    public class ThodexSocketPrice
    {
        [ArrayProperty(0)]
        public string Symbol { get; set; }
        
        [ArrayProperty(1)]
        public decimal Price { get; set; }

        public ThodexSocketPrice() { }
        public ThodexSocketPrice(string symbol, decimal price)
        {
            this.Symbol = symbol;
            this.Price = price;
        }
    }
}
