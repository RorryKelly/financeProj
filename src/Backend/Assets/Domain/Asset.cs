namespace Backend.Assets.Domain;
public class Asset
{
    private string _assetId;

    private readonly string _symbol;

    private PriceFeed _price;

    private string _source;

    private DateTime _lastUpdated;

    public Asset(string assetId, string symbol, decimal price, string source, DateTime lastUpdated)
    {
        _assetId = assetId;
        _symbol = symbol;
        _price = new PriceFeed();
        _price.price = price;
        _source = source;
        _lastUpdated = lastUpdated;
    }

    public Asset(string assetId, string symbol, PriceFeed price, string source, DateTime lastUpdated)
    {
        _assetId = assetId;
        _symbol = symbol;
        _price = price;
        _source = source;
        _lastUpdated = lastUpdated;
    }

    public decimal? getPrice()
    {
        if (_lastUpdated.CompareTo(DateTime.UtcNow.AddMinutes(-5)) <= 0)
        {
            return null;
        }
        return _price.price;
    }

    public PriceFeed? getPriceFeed()
    {
        return _price;
    }


    public void setPriceFeed(PriceFeed feed)
    {
        _price = feed;
        _lastUpdated = DateTime.UtcNow;
    }

    public string getSource()
    {
        return _source;
    }
}