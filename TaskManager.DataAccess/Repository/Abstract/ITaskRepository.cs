using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskManager.Core;

namespace TaskManager.DataAccess.Repository.Abstract
{
    public interface ITaskRepository<T> where T : class, new()
    {
        GetManyResult<T> AsQueryable();
        Task<GetManyResult<T>> AsQueryableAsync();
        GetManyResult<T> FilterBy(Expression<Func<T, bool>> filter);
        Task<GetManyResult<T>> FilterByAsync(Expression<Func<T, bool>> filter);
        GetOneResult<T> GetById(string id);
        Task<GetOneResult<T>> GetByIdAsync(string id);
        GetOneResult<T> Add(T entity,T userEntity);
        GetOneResult<T> Add(T entity);
        Task<GetOneResult<T>> AddAsync(T entity);
        Task<GetOneResult<T>> AddAsync(T entity,T entitys);
        Task<GetManyResult<T>> AddManyAsync(ICollection<T> entities);
        GetOneResult<T> Update(T entity, string id);
        Task<GetOneResult<T>> UpdateAsync(T entity, string id);
        Task<GetOneResult<T>> UpdateAsync(IList<T> entity, T model, string id);
        GetOneResult<T> DeleteOne(Expression<Func<T, bool>> filter);
        Task<GetOneResult<T>> DeleteOneAsync(Expression<Func<T, bool>> filter);
    }
}