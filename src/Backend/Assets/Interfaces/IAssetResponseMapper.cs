using Backend.Assets.Domain;

namespace Backend.Assets.Interfaces;
public interface IAssetResponseMapper
{
    public Asset MapAssetFromHtmlResponse(HttpResponse httpResponse, string source);
}