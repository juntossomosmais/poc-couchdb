using System.ComponentModel.DataAnnotations;

namespace WebAPI.DependencyInjection.Options;

public record BannerGrpcClientOptions
{
    [Required, Url] 
    public required string BaseAddress { get; init; }
}