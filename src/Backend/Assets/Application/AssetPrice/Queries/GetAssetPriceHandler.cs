using Backend.Assets.Domain;
using Backend.Assets.Interfaces;

namespace Backend.Assets.Application.AssetPrice;
public class GetAssetPriceHandler
{
    private IAssetRepository _repository;
    private IAssetUpdater _assetUpdater;

    public GetAssetPriceHandler(IAssetRepository repository, IAssetUpdater assetUpdater)
    {
        _repository = repository;
        _assetUpdater = assetUpdater;
    }

    public async Task<decimal> HandleAsync(AssetPriceQuery query)
    {
        decimal? result;
        Asset asset = await _repository.GetAsset(query);
        result = asset.getPrice();

        if (!result.HasValue)
        {
            result = await _assetUpdater.Handle(asset);
        }

        return result.GetValueOrDefault(0m);
    }
}