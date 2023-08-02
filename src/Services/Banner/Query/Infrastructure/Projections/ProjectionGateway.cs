using System.Linq.Expressions;
using Application.Abstractions;
using Contracts.Abstractions;
using CouchDB.Driver;
using CouchDB.Driver.Extensions;
using CouchDB.Driver.Types;
using Infrastructure.Projections.Abstractions;

namespace Infrastructure.Projections;

public class ProjectionGateway<TProjection> : IProjectionGateway<TProjection>
    where TProjection : CouchDocument, IProjection
{
    private readonly ICouchDatabase<TProjection> _database;

    public ProjectionGateway(ICouchDbContext<TProjection> context)
    {
        _database = context.GetDatabase();
    }

    public Task<TProjection?> FindAsync(Expression<Func<TProjection, bool>> predicate, CancellationToken cancellationToken)
        => _database.AsQueryable().Where(predicate).FirstOrDefaultAsync(cancellationToken)!;

    public async ValueTask ReplaceInsertAsync(TProjection replacement, CancellationToken cancellationToken)
        => await _database.AddOrUpdateAsync(replacement, false, cancellationToken);
}