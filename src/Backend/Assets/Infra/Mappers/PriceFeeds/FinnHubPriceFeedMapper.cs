using System.Text.Json;
using Backend.Assets.Domain;
using Backend.Assets.Interfaces;

public class FinnHubPriceFeedMapper : IPriceFeedResponseMapper
{
    private class FinnHubPriceFeedPayload
    {
        //Current price
        public decimal c;
        //Change
        public decimal d;
        //Change Percent
        public decimal dp;
        //High price of the day
        public decimal h;
        //Low price of the day
        public decimal l;
        //Open price of the day
        public decimal o;
        //Previous close price
        public decimal pc;
    }

    public Asset Map(string body, Asset asset)
    {
        FinnHubPriceFeedPayload? priceFeed = JsonSerializer.Deserialize<FinnHubPriceFeedPayload>(body);
        PriceFeed newFeed = new PriceFeed();

        if (priceFeed == null)
        {
            throw new JsonException("Failed to deserialize Price Feed information");
        }

        newFeed.price = priceFeed.c;
        newFeed.change = priceFeed.d;
        asset.setPriceFeed(newFeed);

        return asset;
    }
}