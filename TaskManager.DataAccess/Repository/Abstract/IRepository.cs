using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core;
using TaskManager.Core.Entity.Concrete;
using TaskManager.Entities.Entities;

namespace TaskManager.DataAccess.Repository.Abstract
{
    public interface IRepository<T> where  T : class, new()
    {
        GetManyResult<T> AsQueryable();
        Task<GetManyResult<T>> AsQueryableAsync();
        GetManyResult<T> FilterBy(Expression<Func<T, bool>> filter);
        Task<GetManyResult<T>> FilterByAsync(Expression<Func<T, bool>> filter);
        GetOneResult<T> GetById(string id);
        Task<GetOneResult<T>> GetByIdAsync(string id);
        GetOneResult<T> Add(T entity);
        Task<GetOneResult<T>> AddAsync(T entity);
        GetManyResult<T> AddMany(ICollection<T> entities);
        Task<GetManyResult<T>> AddManyAsync(ICollection<T> entities);
        Task<GetManyResult<Tasks>> AddManyAsync(ICollection<Tasks> entities);
        GetOneResult<T> Update(T entity,string id);
        Task<GetOneResult<T>> UpdateAsync(T entity,string id);
        GetOneResult<T> DeleteOne(Expression<Func<T, bool>> filter);
        Task<GetOneResult<T>> DeleteOneAsync(Expression<Func<T, bool>> filter);
        //GetOneResult<T> DeleteById(string id);
        //Task<GetOneResult<T>> DeleteByIdAsync(string id);
        //void DeleteMany(Expression<Func<T, bool>> filter);
        //Task<GetManyResult<T>> DeleteManyAsync(Expression<Func<T, bool>> filter);
    }
}
