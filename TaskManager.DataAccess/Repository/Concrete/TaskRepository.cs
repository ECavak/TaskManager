using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core;
using TaskManager.Core.Entity.Concrete;
using TaskManager.Core.Settings;
using TaskManager.DataAccess.Context;
using TaskManager.DataAccess.Repository.Abstract;
using TaskManager.Entities.Entities;

namespace TaskManager.DataAccess.Repository.Concrete
{
    public class TaskRepository<T> : ITaskRepository<T> where T : class, new()
    {
        private readonly MongoDbContext _context;
        private readonly IMongoCollection<T> _collection;
        public TaskRepository(IOptions<MongoSettings> settings)
        {
            _context = new MongoDbContext(settings);
            _collection = _context.GetCollection<T>();
        }
        /// <summary>
        /// Gelen Entity'i Database'e ekler.
        /// </summary>
        /// <param name="item">Model Alır</param>
        /// <returns></returns>
        public GetOneResult<T> Add(T entity, T userEntity)
        {
            var result = new GetOneResult<T>();
            try
            {
                _collection.InsertOne(entity);
                result.Entity = entity;
            }
            catch (Exception ex)
            {
                result.Message = $"FilterBy {ex.Message}";
                result.Success = false;
                result.Entity = null;
            }
            return result;
        }
        public GetOneResult<T> Add(T entity)
        {
            var result = new GetOneResult<T>();
            try
            {
                _collection.InsertOne(entity);
                result.Entity = entity;
            }
            catch (Exception ex)
            {
                result.Message = $"FilterBy {ex.Message}";
                result.Success = false;
                result.Entity = null;
            }
            return result;
        }
        /// <summary>
        /// Async Yapı! Gelen Entity'i Database'e ekler.
        /// </summary>
        /// <param name="item">Model Alır</param>
        /// <returns></returns>
        public async Task<GetOneResult<T>> AddAsync(T entity)
        {
            var result = new GetOneResult<T>();
            try
            {
                await _collection.InsertOneAsync(entity);
                result.Entity = entity;
            }
            catch (Exception ex)
            {
                result.Message = $"FilterBy {ex.Message}";
                result.Success = false;
                result.Entity = null;
            }
            return result;
        }
        public async Task<GetOneResult<T>> AddAsync(T entity, T entitys)
        {
            var result = new GetOneResult<T>();
            try
            {
                await _collection.InsertOneAsync(entity);
                result.Entity = entity;
            }
            catch (Exception ex)
            {
                result.Message = $"FilterBy {ex.Message}";
                result.Success = false;
                result.Entity = null;
            }
            return result;
        }

       
        /// <summary>
        /// Liste halinde modelleri database'e aktarır.
        /// </summary>
        /// <param name="items">Model alır</param>
        /// <returns></returns>
        public GetManyResult<T> AddMany(ICollection<T> entities)
        {
            var result = new GetManyResult<T>();
            try
            {
                _collection.InsertMany(entities);
                result.Result = entities;
            }
            catch (Exception ex)
            {
                result.Message = $"AddMany {ex.Message}";
                result.Success = false;
                result.Result = null;
            }
            return result;
        }


        /// <summary>
        /// Async Yapı! Liste halinde modelleri database'e aktarır.
        /// </summary>
        /// <param name="items">Model alır</param>
        /// <returns></returns>
        public async Task<GetManyResult<T>> AddManyAsync(ICollection<T> entities)
        {
            var result = new GetManyResult<T>();
            try
            {
                await _collection.InsertManyAsync(entities);
                result.Result = entities;
            }
            catch (Exception ex)
            {
                result.Message = $"AddMany {ex.Message}";
                result.Success = false;
                result.Result = null;
            }
            return result;
        }


        /// <summary>
        /// Ne var ne yok getirir
        /// </summary>
        /// <returns></returns>
        public GetManyResult<T> AsQueryable()
        {
            var result = new GetManyResult<T>();
            try
            {
                var data = _collection.AsQueryable().ToList();
                if (data != null)
                    result.Result = data;
            }
            catch (Exception ex)
            {
                result.Message = $"AsQueryable {ex.Message}";
                result.Success = false;
                result.Result = null;
            }
            return result;
        }
        /// <summary>
        /// Async yapı! .Tüm db getirir.
        /// </summary>
        /// <returns></returns>
        public async Task<GetManyResult<T>> AsQueryableAsync()
        {
            var result = new GetManyResult<T>();
            try
            {
                var data = await _collection.AsQueryable().ToListAsync();
                if (data != null)
                    result.Result = data;
            }
            catch (Exception ex)
            {
                result.Message = $"AsQueryable {ex.Message}";
                result.Success = false;
                result.Result = null;
            }
            return result;
        }

        public GetOneResult<T> DeleteOne(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<GetOneResult<T>> DeleteOneAsync(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sorguya göre filtreleme yapar
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public GetManyResult<T> FilterBy(Expression<Func<T, bool>> filter)
        {
            var result = new GetManyResult<T>();
            try
            {
                var data = _collection.Find(filter).ToList();
                if (data != null)
                    result.Result = data;
            }
            catch (Exception ex)
            {
                result.Message = $"FilterBy {ex.Message}";
                result.Success = false;
                result.Result = null;
            }
            return result;
        }
        /// <summary>
        /// Async yapı! Sorguya göre filtreleme yapar
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<GetManyResult<T>> FilterByAsync(Expression<Func<T, bool>> filter)
        {
            var result = new GetManyResult<T>();
            try
            {
                var data = await _collection.Find(filter).ToListAsync();
                if (data != null)
                    result.Result = data;
            }
            catch (Exception ex)
            {
                result.Message = $"FilterBy {ex.Message}";
                result.Success = false;
                result.Result = null;
            }
            return result;
        }
        /// <summary>
        /// Gelen ID'ye göre o ID'ye ait Entity'i döndürür.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GetOneResult<T> GetById(string id)
        {
            var result = new GetOneResult<T>();
            try
            {
                var objectId = ObjectId.Parse(id);
                var filter = Builders<T>.Filter.Eq("_id", objectId);
                var data = _collection.Find(filter).FirstOrDefault();
                if (data != null)
                    result.Entity = data;
            }
            catch (Exception ex)
            {
                result.Message = $"FilterBy {ex.Message}";
                result.Success = false;
                result.Entity = null;
            }
            return result;
        }
        /// <summary>
        /// Async Yapı! Gelen ID'ye göre o ID'ye ait Entity'i döndürür.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GetOneResult<T>> GetByIdAsync(string id)
        {
            var result = new GetOneResult<T>();
            try
            {
                var objectId = ObjectId.Parse(id);
                var filter = Builders<T>.Filter.Eq("_id", objectId);
                var data = await _collection.Find(filter).FirstOrDefaultAsync();
                if (data != null)
                    result.Entity = data;
            }
            catch (Exception ex)
            {
                result.Message = $"FilterBy {ex.Message}";
                result.Success = false;
                result.Entity = null;
            }
            return result;
        }

        public GetOneResult<T> Update(T entity, string id)
        {
            var result = new GetOneResult<T>();
            try
            {
                var objectId = ObjectId.Parse(id);
                var filter = Builders<T>.Filter.Eq("_id", objectId);
                var update = _collection.ReplaceOne(filter, entity);
                result.Entity = entity;
            }
            catch (Exception ex)
            {
                result.Message = $"Update {ex.Message}";
                result.Success = false;
                result.Entity = null;
            }
            return result;
        }

        public async Task<GetOneResult<T>> UpdateAsync(T entity, string id)
        {
            var result = new GetOneResult<T>();
            try
            {
                var objectId = ObjectId.Parse(id);
                var filter = Builders<T>.Filter.Eq("_id", objectId);
                var update = await _collection.ReplaceOneAsync(filter, entity);
                result.Entity = entity;
            }
            catch (Exception ex)
            {
                result.Message = $"Update {ex.Message}";
                result.Success = false;
                result.Entity = null;
            }
            return result;
        }
        public async Task<GetOneResult<T>> UpdateAsync(IList<T> entity, T model,string id)
        {
            var result = new GetOneResult<T>();
            try
            {
                var objectId = ObjectId.Parse(id);
                var filter = Builders<T>.Filter.Eq("_id", objectId);
                var update = await _collection.ReplaceOneAsync(filter, model);
                result.Entity = model;
            }
            catch (Exception ex)
            {
                result.Message = $"Update {ex.Message}";
                result.Success = false;
                result.Entity = null;
            }
            return result;
        }
    }
}
