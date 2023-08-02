using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Projections.DependencyInjection.Options;

public record CouchDbOptions
{
    [Required] 
    public string ServerAddress { get; init; }
    
    [Required] 
    public string Username { get; init; }
    
    [Required] 
    public string Password { get; init; }
}