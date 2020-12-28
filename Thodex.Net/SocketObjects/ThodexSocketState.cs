using Newtonsoft.Json;
using Thodex.Net.Converters;
using Thodex.Net.Enums;
using Thodex.Net.RestObjects;

namespace Thodex.Net.SocketObjects
{
    public class ThodexSocketState : ThodexState
    {
        public string Symbol { get; set; }

        public ThodexSocketState() { }
        public ThodexSocketState(string symbol, ThodexState state)
        {
            this.Symbol = symbol;
            this.Period = state.Period;
            this.Open = state.Open;
            this.High = state.High;
            this.Low = state.Low;
            this.Close = state.Close;
            this.Last = state.Last;
            this.StockVolume = state.StockVolume;
            this.MoneyVolume = state.MoneyVolume;
        }
    }
}