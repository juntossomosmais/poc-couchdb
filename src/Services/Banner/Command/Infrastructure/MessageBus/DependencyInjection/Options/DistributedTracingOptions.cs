using System.ComponentModel.DataAnnotations;

namespace Infrastructure.MessageBus.DependencyInjection.Options;

public record DistributedTracingOptions
{
    [Required] 
    public string ServiceName { get; init; }
    
    [Required] 
    public string Host { get; init; }
    
    [Required] 
    public int Port { get; init; }
}