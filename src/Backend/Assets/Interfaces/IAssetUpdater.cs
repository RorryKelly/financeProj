using Backend.Assets.Domain;

public interface IAssetUpdater
{
    public Task<decimal> Handle(Asset asset);
}