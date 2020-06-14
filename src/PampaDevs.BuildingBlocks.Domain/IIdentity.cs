namespace PampaDevs.BuildingBlocks.Domain
{
    public interface IIdentity<TId>
    {
        TId Id { get; }
    }
}
