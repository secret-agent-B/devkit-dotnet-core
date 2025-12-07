using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Devkit.Data;
using Devkit.Data.Interfaces;
using LiteDB;
using MongoDB.Driver;

namespace Devkit.Test
{
    public class LiteDbRepository : IRepository
    {
        private readonly LiteDatabase _db;

        public LiteDbRepository(LiteDatabase db)
        {
            _db = db;
            
            // Map MongoDB ObjectId to LiteDB string/ObjectId
            // Note: This modifies global mapper, might affect other things if running in parallel with different config
            BsonMapper.Global.RegisterType<MongoDB.Bson.ObjectId>(
                serialize: (oid) => new BsonValue(oid.ToString()),
                deserialize: (bson) => MongoDB.Bson.ObjectId.Parse(bson.AsString)
            );
        }

        public void Add<T>(T item) where T : new()
        {
            _db.GetCollection<T>().Insert(item);
        }

        public void Add<T>(IEnumerable<T> items) where T : new()
        {
            _db.GetCollection<T>().Insert(items);
        }

        public void AddRangeWithAudit<T>(IEnumerable<T> items) where T : DocumentBase, new()
        {
            foreach (var item in items)
            {
                item.CreatedOn = DateTime.UtcNow;
            }
            _db.GetCollection<T>().Insert(items);
        }

        public void AddWithAudit<T>(T item) where T : DocumentBase, new()
        {
            item.CreatedOn = DateTime.UtcNow;
            _db.GetCollection<T>().Insert(item);
        }

        public List<T> All<T>() where T : new()
        {
            return _db.GetCollection<T>().FindAll().ToList();
        }

        public bool CollectionExists<T>() where T : new()
        {
            return _db.CollectionExists(typeof(T).Name);
        }

        public void Delete<T>(Expression<Func<T, bool>> expression) where T : new()
        {
            _db.GetCollection<T>().DeleteMany(expression);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            throw new NotSupportedException("LiteDB repository does not support IMongoCollection.");
        }

        public List<T> GetMany<T>(Expression<Func<T, bool>> expression) where T : new()
        {
            return _db.GetCollection<T>().Find(expression).ToList();
        }

        public List<T> GetMany<T>(Expression<Func<T, bool>> expression, int page, int pageSize) where T : new()
        {
            return _db.GetCollection<T>().Find(expression, skip: (page - 1) * pageSize, limit: pageSize).ToList();
        }

        public T GetOneOrDefault<T>(Expression<Func<T, bool>> expression) where T : new()
        {
            return _db.GetCollection<T>().FindOne(expression);
        }

        public T Update<T>(Expression<Func<T, bool>> filter, Func<UpdateDefinitionBuilder<T>, UpdateDefinition<T>> updateFunction) where T : new()
        {
            throw new NotSupportedException("LiteDB repository does not support MongoDB UpdateDefinitions.");
        }

        public T UpdateWithAudit<T>(Expression<Func<T, bool>> filter, Func<UpdateDefinitionBuilder<T>, UpdateDefinition<T>> updateFunction) where T : DocumentBase, new()
        {
            throw new NotSupportedException("LiteDB repository does not support MongoDB UpdateDefinitions.");
        }
    }
}
