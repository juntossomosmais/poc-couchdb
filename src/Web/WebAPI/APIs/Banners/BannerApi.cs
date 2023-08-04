using Asp.Versioning.Builder;
using WebAPI.Abstractions;

namespace WebAPI.APIs.Banners;


public static class IdentityApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/banners/";
    
    public static IVersionedEndpointRouteBuilder MapBannerApiV1(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapPost("/create", ([AsParameters] Commands.CreateBanner create)
            => ApplicationApi.SendCommandAsync(create));
        
        group.MapDelete("/{bannerId:guid}", ([AsParameters] Commands.DeleteBanner delete)
            => ApplicationApi.SendCommandAsync(delete));
        
        return builder;
    }
}