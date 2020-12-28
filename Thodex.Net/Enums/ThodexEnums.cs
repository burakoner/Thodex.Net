namespace Thodex.Net.Enums
{
    public enum ThodexPeriod
    {
        FiveMinutes,
        FifteenMinutes,
        ThirtyMinutes,
        OneHour,
        TwoHours,
        TwelveHours,
        OneDay,
        OneWeek,
    }

    public enum ThodexOrderSide
    {
        Buy,
        Sell,
    }

    public enum ThodexOrderType
    {
        Market,
        Limit,
    }

    public enum ThodexTraderRole
    {
        Maker,
        Taker,
    }
}
