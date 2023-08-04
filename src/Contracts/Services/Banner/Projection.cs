using Contracts.Abstractions;
using CouchDB.Driver.Types;

namespace Contracts.Services.Banner;

public sealed class BannerDetails : CouchDocument, IProjection
{
    public string Title { get; }
    public string ImagePath { get; }
    public int Order { get; }
    public string CallToAction { get; }
    public string Author { get; }
    public int Status { get; }
    public bool IsDeleted { get; }
    public long Version { get; }

    public BannerDetails(Guid bannerId, string title, string imagePath, int order, string callToAction, string author, int status, bool isDeleted, long version)
    {
        Id = bannerId.ToString();
        Title = title;
        ImagePath = imagePath;
        Order = order;
        CallToAction = callToAction;
        Author = author;
        Status = status;
        IsDeleted = isDeleted;
        Version = version;
    }

    public static implicit operator Protobuf.BannerDetails(BannerDetails bannerDetails)
        => new()
        {
            Id = bannerDetails.Id,
            BannerId = bannerDetails.Id,
            Title = bannerDetails.Title,
            ImagePath = bannerDetails.ImagePath,
            Order = bannerDetails.Order,
            CallToAction = bannerDetails.CallToAction,
            Author = bannerDetails.Author,
            Status = bannerDetails.Status
        };
}