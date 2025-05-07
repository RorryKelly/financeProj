using Backend.Assets.Domain;
using Backend.Assets.Infra;
namespace UnitTests.Assets;

[TestFixture]
public class AssetUpdaterTests
{

    AssetUpdaterHandler assetUpdater;



    [SetUp]
    public void SetUp()
    {
        assetUpdater = new AssetUpdaterHandler();
    }

    [Test]
    public async Task AssetShouldReturnNewlyUpdatedPriceAsync()
    {
        decimal startPrice = 30.00m;
        decimal updatedPrice = 40.00m;
        Asset asset = new Asset("test asset", "$TEST", startPrice, "test/src", DateTime.UtcNow);

        decimal result = await assetUpdater.Handle(asset);

        Assert.That(updatedPrice.Equals(result));
    }
}