using Backend.Assets.Domain;

namespace UnitTests.Assets;

[TestFixture]
public class FinnHubPriceFeedMapperTests
{

    FinnHubPriceFeedMapper mapper;
    string payload;

    [SetUp]
    public void SetUp()
    {
        mapper = new FinnHubPriceFeedMapper();
    }

    [Test]
    public void AssetPriceShouldBeUpdatedWhenGivenCorrectPayload()
    {
        decimal startPrice = 30.00m;
        Asset asset = new Asset("test asset", "$TEST", startPrice, "test/src", DateTime.UtcNow);
        string payloadPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"Assets/Infra/Mappers/PriceFeeds/Payloads", "FinnHubPriceFeedPayload.txt");
        string payload = File.ReadAllText(payloadPath);

        mapper.Map(payload, asset);

        Assert.That(asset.getPrice() != startPrice);
    }
}