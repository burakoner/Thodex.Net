using Newtonsoft.Json;

namespace Thodex.Net.RestObjects
{
    public class ThodexMarket
    {
        [JsonProperty("keyname")]
        public string Symbol { get; set; }
        
        [JsonProperty("stock_keyname")]
        public string StockCurrency { get; set; }
        
        [JsonProperty("money_keyname")]
        public string MoneyCurrency { get; set; }
        
        [JsonProperty("stock_fullname")]
        public string StockCurrencyFullName { get; set; }
        
        [JsonProperty("money_fullname")]
        public string MoneyCurrencyFullName { get; set; }
        
        [JsonProperty("stock_display")]
        public string StockCurrencyDisplayName { get; set; }
        
        [JsonProperty("money_display")]
        public string MoneyCurrencyDisplayName { get; set; }

        [JsonProperty("stock_prec")]
        public int StockCurrencyPrecision { get; set; }

        [JsonProperty("money_prec")]
        public int MoneyCurrencyPrecision { get; set; }
        
        [JsonProperty("min_amount")]
        public decimal MinimumAmount { get; set; }
        
        [JsonProperty("maintenance")]
        public string Maintenance { get; set; }

        [JsonProperty("maintenance_note")]
        public string MaintenanceNote { get; set; }
    }
}
