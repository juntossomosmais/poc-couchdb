using CouchDB.Driver;
using CouchDB.Driver.Types;

namespace Infrastructure.Projections.Abstractions;

public interface ICouchDbContext<TProjection>
    where TProjection : CouchDocument
{
    ICouchDatabase<TProjection> GetDatabase();
}