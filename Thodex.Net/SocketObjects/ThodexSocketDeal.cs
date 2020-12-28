using Newtonsoft.Json;
using Thodex.Net.Converters;
using Thodex.Net.Enums;
using Thodex.Net.RestObjects;

namespace Thodex.Net.SocketObjects
{
    public class ThodexSocketDeal : ThodexDeal
    {
        public string Symbol { get; set; }

        public ThodexSocketDeal() { }
        public ThodexSocketDeal(string symbol, ThodexDeal deal)
        {
            this.Symbol = symbol;
            this.Side = deal.Side;
            this.Id = deal.Id;
            this.UtcTime = deal.UtcTime;
            this.Price = deal.Price;
            this.Amount = deal.Amount;
        }
    }
}