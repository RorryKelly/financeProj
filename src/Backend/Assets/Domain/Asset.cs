namespace Backend.Assets.Domain;
public class Asset
{
    private string _assetId;

    private readonly string _symbol;

    private decimal _price;

    private string _source;

    private DateTime _lastUpdated;

    public Asset(string assetId, string symbol, decimal price, string source, DateTime lastUpdated)
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
        return _price;
    }

    public string getSource()
    {
        return _source;
    }
}