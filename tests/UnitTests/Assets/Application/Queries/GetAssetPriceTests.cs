using Backend.Assets.Application.AssetPrice;
using Backend.Assets.Domain;
using Backend.Assets.Interfaces;
using Moq;

namespace UnitTests.Assets;

[TestFixture]
public class GetAssetPriceTests
{
    private GetAssetPriceHandler _getAssetPriceHandler;
    private Mock<IAssetRepository> _repository;
    private Mock<IAssetUpdater> _assetUpdater;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<IAssetRepository>();
        _assetUpdater = new Mock<IAssetUpdater>();
        _getAssetPriceHandler = new GetAssetPriceHandler(_repository.Object, _assetUpdater.Object);
    }

    [Test]
    public async Task AssetPriceQueryShouldReturnPriceAsync()
    {
        string symbol = "$TEST";
        decimal expectedResult = 30.00m;
        Asset asset = new Asset("validAsset", symbol, expectedResult, "test/src", DateTime.UtcNow);
        AssetPriceQuery apq = new AssetPriceQuery { Symbol = symbol };
        _repository
            .Setup(r => r.GetAsset(apq))
            .ReturnsAsync(asset);

        decimal actualResult = await _getAssetPriceHandler.HandleAsync(apq);

        Assert.That(expectedResult == actualResult, $"getAssetPriceHandler should return the expected price of {expectedResult}");
    }

    [Test]
    public async Task AssetPriceShouldBeUpdatedWhenInvalidated()
    {
        string symbol = "$TEST";
        decimal expectedResult = 30.00m;
        Asset asset = new Asset("validAsset", symbol, expectedResult, "test/src", DateTime.UtcNow.AddHours(-3));
        AssetPriceQuery apq = new AssetPriceQuery { Symbol = symbol };
        _repository
            .Setup(r => r.GetAsset(apq))
            .ReturnsAsync(asset);
        _assetUpdater
            .Setup(a => a.Handle(asset))
            .ReturnsAsync(0m);

        decimal actualResult = await _getAssetPriceHandler.HandleAsync(apq);

        _assetUpdater
            .Verify(mock => mock.Handle(asset), Times.Once);
        Assert.That(expectedResult != actualResult);
    }
}