using System.Linq.Expressions;
using Contracts.Abstractions;

namespace Application.Abstractions;

public interface IProjectionGateway<TProjection>
    where TProjection : IProjection
{
    Task<TProjection?> GetAsync(string id, CancellationToken cancellationToken);
    Task<TProjection?> FindAsync(Expression<Func<TProjection, bool>> predicate, CancellationToken cancellationToken);
    Task<TProjection> ReplaceInsertAsync(TProjection replacement, CancellationToken cancellationToken);
    Task DeleteAsync(string id, CancellationToken cancellationToken);
}