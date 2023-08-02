using CouchDB.Driver;
using CouchDB.Driver.Options;
using CouchDB.Driver.Types;
using Infrastructure.Projections.DependencyInjection.Options;
using Microsoft.Extensions.Options;

namespace Infrastructure.Projections.Abstractions;

public class CouchDbContext<TProjection> : CouchContext, ICouchDbContext<TProjection>
    where TProjection : CouchDocument
{
    private readonly CouchDbOptions _options;
    
    protected CouchDbContext(CouchOptions couchOptions, IOptions<CouchDbOptions> options)
        : base(couchOptions)
    {
        _options = options.Value;
    }

    public ICouchDatabase<TProjection> GetDatabase()
    {
        var client = new CouchClient(builder =>
        {
            builder.UseEndpoint(_options.ServerAddress)
                .UseBasicAuthentication(_options.Username, _options.Password)
                .EnsureDatabaseExists();
        });
        
        return client.GetDatabase<TProjection>(typeof(TProjection).Name);
    }
}