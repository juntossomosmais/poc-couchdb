using System.Linq.Expressions;
using Application.Abstractions;
using Contracts.Abstractions;
using CouchDB.Driver;
using CouchDB.Driver.Extensions;
using CouchDB.Driver.Types;
using Infrastructure.Projections.DependencyInjection.Options;
using Microsoft.Extensions.Options;

namespace Infrastructure.Projections;

public class ProjectionGateway<TProjection> : IProjectionGateway<TProjection>
    where TProjection : CouchDocument, IProjection
{
    private readonly CouchDbOptions _options;

    public ProjectionGateway(IOptions<CouchDbOptions> options)
    {
        _options = options.Value;
    }
    
    public Task<TProjection?> GetAsync(string id, CancellationToken cancellationToken)
        => FindAsync(projection => projection.Id == id, cancellationToken);

    public async Task<TProjection?> FindAsync(Expression<Func<TProjection, bool>> predicate, CancellationToken cancellationToken)
    {
        var database = await GetDatabaseAsync(cancellationToken);
        
        return await database.AsQueryable().Where(predicate).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TProjection> AddOrUpdateAsync(TProjection replacement, CancellationToken cancellationToken)
    {
        var database = await GetDatabaseAsync(cancellationToken);
        
        return await database.AddOrUpdateAsync(replacement, false, cancellationToken);
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        var database = await GetDatabaseAsync(cancellationToken);

        var document = await GetAsync(id, cancellationToken: cancellationToken);

        if (document is null)
            return;

        await database.RemoveAsync(document, false, cancellationToken);
    }

    private async Task<ICouchDatabase<TProjection>> GetDatabaseAsync(CancellationToken cancellationToken)
    {
        var client = new CouchClient(builder =>
        {
            builder
                .UseEndpoint(_options.ServerAddress)
                .UseBasicAuthentication(_options.Username, _options.Password);
        });

        return await client.GetOrCreateDatabaseAsync<TProjection>(cancellationToken: cancellationToken);
    }
}