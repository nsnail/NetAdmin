using NetAdmin.Domain.Contexts;

namespace NetAdmin.Application.Repositories;

/// <summary>
///     基础仓储
/// </summary>
public sealed class BasicRepository<TEntity, TPrimary>(
    IFreeSql          fSql
  , UnitOfWorkManager uowManger
  , ContextUserToken  userToken) : DefaultRepository<TEntity, TPrimary>(fSql, uowManger)
    where TEntity : EntityBase<TPrimary> //
    where TPrimary : IEquatable<TPrimary>
{
    /// <summary>
    ///     当前上下文关联的用户令牌
    /// </summary>
    public ContextUserToken UserToken => userToken;
}