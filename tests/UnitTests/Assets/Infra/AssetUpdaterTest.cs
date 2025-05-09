using System.Net;
using System.Net.Http.Json;
using Backend.Assets.Domain;
using Backend.Assets.Infra;
using Backend.Assets.Interfaces;
using Moq;
using Moq.Protected;
namespace UnitTests.Assets;

[TestFixture]
public class AssetUpdaterTests
{

    private AssetUpdater assetUpdater;
    private Mock<IPriceFeedResponseMapper> mockPriceFeedMapper;
    private Mock<HttpMessageHandler> handlerMock;
    private HttpClient httpClient;

    [SetUp]
    public void SetUp()
    {
        handlerMock = new Mock<HttpMessageHandler>();
        httpClient = new HttpClient(handlerMock.Object);
        mockPriceFeedMapper = new Mock<IPriceFeedResponseMapper>();
        assetUpdater = new AssetUpdater(httpClient, mockPriceFeedMapper.Object);
    }

    [TearDown]
    public void TearDown()
    {
        httpClient.Dispose();
    }

    [Test]
    public async Task AssetShouldReturnNewlyUpdatedPriceAsync()
    {
        decimal startPrice = 30.00m;
        decimal updatedPrice = 197.49m;
        var asset = new Asset("test asset", "$TEST", startPrice, "https://fake.url/pricefeed", DateTime.UtcNow);

        var fakeResponse = "fake JSON response";

        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(fakeResponse)
        };

        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get &&
                    req.RequestUri.ToString() == asset.getSource()),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(responseMessage);

        decimal result = await assetUpdater.Handle(asset);

        mockPriceFeedMapper.Verify(mock => mock.Map(It.IsAny<string>(), asset));
    }
}