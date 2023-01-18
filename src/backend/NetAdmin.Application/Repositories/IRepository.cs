using System.Linq.Expressions;
using FreeSql;
using NetAdmin.DataContract.Context;

namespace NetAdmin.Application.Repositories;

/// <summary>
///     基础仓储接口
/// </summary>
public interface IRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : EntityBase
{
    /// <summary>
    ///     当前上下文关联的用户
    /// </summary>
    ContextUser User { get; }

    /// <summary>
    ///     递归删除
    /// </summary>
    /// <param name="exp">exp</param>
    /// <param name="disableGlobalFilterNames">禁用全局过滤器名</param>
    Task<bool> DeleteRecursiveAsync(Expression<Func<TEntity, bool>> exp, params string[] disableGlobalFilterNames);

    /// <summary>
    ///     获得Dto
    /// </summary>
    /// <param name="id">主键</param>
    Task<TDto> GetAsync<TDto>(long id);

    /// <summary>
    ///     根据条件获取Dto
    /// </summary>
    Task<TDto> GetAsync<TDto>(Expression<Func<TEntity, bool>> exp);

    /// <summary>
    ///     根据条件获取实体
    /// </summary>
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp);
}