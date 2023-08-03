using Asp.Versioning.Builder;
using WebAPI.Abstractions;

namespace WebAPI.APIs.Banners;


public static class IdentityApi
{
    private const string BaseUrl = "/api/v{version:apiVersion}/banners/";
    
    public static IVersionedEndpointRouteBuilder MapBannerApiV1(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(BaseUrl).HasApiVersion(1);

        group.MapPost("/create", ([AsParameters] Commands.Create registerUser)
            => ApplicationApi.SendCommandAsync(registerUser));
        
        return builder;
    }
}