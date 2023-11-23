using NetAdmin.Domain.Contexts;
using NetAdmin.Domain.DbMaps.Dependency;

namespace NetAdmin.Application.Repositories;

/// <summary>
///     默认仓储
/// </summary>
public sealed class DefaultRepository<TEntity>(
    IFreeSql          fSql       //
  , UnitOfWorkManager uowManger  //
  , ContextUserToken  userToken) //
    : DefaultRepository<TEntity, long>(fSql, uowManger)
    where TEntity : EntityBase
{
    /// <summary>
    ///     当前上下文关联的用户
    /// </summary>
    public ContextUserToken UserToken => userToken;
}