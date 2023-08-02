using System.Linq.Expressions;
using Contracts.Abstractions;

namespace Application.Abstractions;

public interface IProjectionGateway<TProjection>
    where TProjection : IProjection
{
    Task<TProjection?> FindAsync(Expression<Func<TProjection, bool>> predicate, CancellationToken cancellationToken);
    ValueTask ReplaceInsertAsync(TProjection replacement, CancellationToken cancellationToken);
}