using Backend.Assets.Domain;

namespace Backend.Assets.Infra;

public class AssetUpdaterHandler : IAssetUpdater
{
    HttpClient client = new HttpClient();

    public async Task<decimal> Handle(Asset asset)
    {
        string source = asset.getSource();

        HttpResponseMessage response = await client.GetAsync(source);

        return 0.00m;
    }
}