using Backend.Assets.Domain;
namespace UnitTests.Assets;

[TestFixture]
public class AssetTests
{
    [Test]
    public void AssetPriceShouldOnlyBeActiveForAMaximumOf5Minutes()
    {
        decimal actualPrice = 30.00m;
        Asset validAsset = new Asset("validAsset", "$TEST", actualPrice, "test/src", DateTime.UtcNow);
        decimal? validPrice = validAsset.getPrice();

        Asset invalidAsset = new Asset("invalidAsset", "$TEST", actualPrice, "test/src", DateTime.UtcNow.AddMinutes(-10));
        decimal? invalidPrice = invalidAsset.getPrice();

        Assert.That(validPrice == actualPrice, "asset should return the correct price.");
        Assert.That(actualPrice != invalidPrice, "asset should not return price, after invalidation.");
    }
}