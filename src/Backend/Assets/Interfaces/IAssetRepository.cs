using Backend.Assets.Domain;

namespace Backend.Assets.Interfaces;

public interface IAssetRepository
{
    Task<Asset> GetAsset(AssetPriceQuery assetSymbol);
}