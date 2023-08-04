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
        
        group.MapPut("/{bannerId:guid}/activate", ([AsParameters] Commands.ActivateBanner activateBanner)
            => ApplicationApi.SendCommandAsync(activateBanner));

        group.MapPut("/{bannerId:guid}/deactivate", ([AsParameters] Commands.DeactivateBanner deactivateBanner)
            => ApplicationApi.SendCommandAsync(deactivateBanner));
        
        return builder;
    }
}