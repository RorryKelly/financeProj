using Backend.Assets.Domain;
using Backend.Assets.Interfaces;

namespace Backend.Assets.Infra;

public class AssetUpdater : IAssetUpdater
{
    HttpClient client;

    IPriceFeedResponseMapper priceFeedResponseMapper;

    public AssetUpdater(HttpClient httpClient, IPriceFeedResponseMapper mapper)
    {
        client = httpClient;
        priceFeedResponseMapper = mapper;
    }

    public async Task<decimal> Handle(Asset asset)
    {
        string source = asset.getSource();

        HttpResponseMessage response = await client.GetAsync(source);

        string resJson = await response.Content.ReadAsStringAsync();

        priceFeedResponseMapper.Map(resJson, asset);

        decimal? newPrice = asset.getPrice();

        if (!newPrice.HasValue)
        {
            return -1;
        }

        return newPrice.Value;
    }
}