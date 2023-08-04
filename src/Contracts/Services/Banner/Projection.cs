using Contracts.Abstractions;
using CouchDB.Driver.Types;

namespace Contracts.Services.Banner;

public sealed class BannerDetails : CouchDocument, IProjection
{
    public string Title { get; set; }
    public string ImagePath { get; set;}
    public int Order { get; set;}
    public string CallToAction { get; set;}
    public string Author { get; set;}
    public int Status { get; set;}
    public bool IsDeleted { get; set;}
    public long Version { get; set;}

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