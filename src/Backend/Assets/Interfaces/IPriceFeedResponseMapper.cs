using Backend.Assets.Domain;

namespace Backend.Assets.Interfaces;
public interface IPriceFeedResponseMapper
{
    public Asset Map(string body, Asset asset);
}