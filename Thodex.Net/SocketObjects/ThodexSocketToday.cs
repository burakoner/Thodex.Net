using Newtonsoft.Json;
using Thodex.Net.Converters;
using Thodex.Net.Enums;
using Thodex.Net.RestObjects;

namespace Thodex.Net.SocketObjects
{
    public class ThodexSocketToday : ThodexToday
    {
        public string Symbol { get; set; }

        public ThodexSocketToday() { }
        public ThodexSocketToday(string symbol, ThodexToday today)
        {
            this.Symbol = symbol;
            this.Open = today.Open;
            this.Low = today.Low;
            this.Last = today.Last;
            this.High = today.High;
            this.Volume = today.Volume;
            this.Deal = today.Deal;
        }
    }
}