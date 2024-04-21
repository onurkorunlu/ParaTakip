using MongoDB.Bson;
using MongoDB.Driver;
using ParaTakip.Core;
using ParaTakip.Entities;
using System.Linq.Expressions;
using System.Security.Claims;

namespace ParaTakip.DataAccess.Base
{
    public abstract class BaseDataAcccess<TEntity> where TEntity : MongoBaseEntity, new()
    {
        private readonly MongoClient MongoClient;
        protected readonly IMongoDatabase MongoDatabase;
        protected IMongoCollection<TEntity> _collection;
        protected ApplicationContext Context;

        public BaseDataAcccess()
        {
            if (ConfigurationCache.Instance.TryGet("MongoDbConnectionString", out string connectionString))
            {
                MongoClient = new MongoClient(connectionString);
            }
            else
            {
                throw new AppException(ReturnMessages.MONGO_DB_CONN_NOT_FOUND_IN_CONFIGURATION);
            }

            if (ConfigurationCache.Instance.TryGet("MongoDbName", out string databaseName))
            {
                MongoDatabase = MongoClient.GetDatabase(databaseName);
            }
            else
            {
                throw new AppException(ReturnMessages.MONGO_DB_NAME_NOT_FOUND_IN_CONFIGURATION);
            }

            _collection = MongoDatabase.GetCollection<TEntity>(typeof(TEntity).Name.Trim());

            Context = (ApplicationContext)AppServiceProvider.Instance.Get<IApplicationContext>();
        }

        public List<TEntity> Find(FilterDefinition<TEntity> filterDefinition)
        {
            List<TEntity> result = new List<TEntity>();
            try
            {
                var data = _collection.Find(filterDefinition);
                if (data != null)
                    result = data.ToList();
            }
            catch (Exception ex)
            {
                throw new AppException(ReturnMessages.MONGO_DB_ERROR, ex);
            }

            return result;
        }

        public List<TEntity> GetAll()
        {
            List<TEntity> result = new List<TEntity>();
            try
            {
                var data = FilterBy(x => x.RecordStatus);
                if (data != null)
                    result = data.ToList();
            }
            catch (Exception ex)
            {
                throw new AppException(ReturnMessages.MONGO_DB_ERROR, ex);
            }

            return result;
        }

        public List<TEntity> GetByDateBtw(DateTime startDate, DateTime endDate)
        {
            List<TEntity> result = new List<TEntity>();
            try
            {
                var data = _collection.Find(x => x.RecordCreateDate > startDate & x.RecordCreateDate < endDate).ToList();
                if (data != null)
                    result = data.ToList();
            }
            catch (Exception ex)
            {
                throw new AppException(ReturnMessages.MONGO_DB_ERROR, ex);
            }

            return result;
        }

        public TEntity DeleteByRecordId(string recordId, string type = "object")
        {
            try
            {
                object objectId = null;
                if (type == "guid")
                    objectId = Guid.Parse(recordId);
                else
                    objectId = ObjectId.Parse(recordId);

                var item = this.GetByRecordId(recordId, type);

                if (item == null)
                {
                    throw new AppException(ReturnMessages.ITEM_NOT_FOUND);
                }

                item.RecordUpdateDate = DateTime.Now;
                item.RecordStatus = false;
                item.RecordUpdateUsername = GetUserName();
                this.ReplaceOne(item, recordId, type);
                return item;
            }
            catch (Exception ex)
            {
                throw new AppException(ReturnMessages.MONGO_DB_ERROR, ex, new { id = recordId });
            }
        }

        public void FactoryDeleteById(string recordId, string type = "object")
        {
            try
            {
                object objectId = null;
                if (type == "guid")
                    objectId = Guid.Parse(recordId);
                else
                    objectId = ObjectId.Parse(recordId);

                _collection.DeleteOne(filter => filter.RecordId == (ObjectId)objectId);
            }
            catch (Exception ex)
            {
                throw new AppException(ReturnMessages.MONGO_DB_ERROR, ex, new { id = recordId });
            }
        }

        public List<TEntity> FilterBy(Expression<Func<TEntity, bool>> filter)
        {
            var result = new List<TEntity>();
            try
            {
                var data = _collection.Find(filter).ToList();
                if (data != null)
                    result = data;
            }
            catch (Exception ex)
            {
                throw new AppException(ReturnMessages.MONGO_DB_ERROR, ex, new { filter = filter.ToString() });

            }
            return result;
        }

        public TEntity GetByRecordId(string recordId, string type = "object")
        {
            try
            {
                object objectId = null;
                if (type == "guid")
                    objectId = Guid.Parse(recordId);
                else
                    objectId = ObjectId.Parse(recordId);

                return _collection.Find(x => x.RecordId == (ObjectId)objectId && x.RecordStatus).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new AppException(ReturnMessages.MONGO_DB_ERROR, ex, new { id = recordId });
            }
        }

        public List<TEntity> InsertMany(ICollection<TEntity> entities)
        {
            var result = new List<TEntity>();
            try
            {
                entities.ToList().ForEach(x => x.RecordCreateDate = DateTime.Now);
                _collection.InsertMany(entities);
                result = entities.ToList();
            }
            catch (Exception ex)
            {
                throw new AppException(ReturnMessages.MONGO_DB_ERROR, ex, new { entities = entities });
            }
            return result;
        }

        public TEntity InsertOne(TEntity entity)
        {
            var result = new TEntity();
            try
            {
                entity.RecordCreateDate = DateTime.Now;
                entity.RecordStatus = true;
                entity.RecordCreateUsername = GetUserName();
                _collection.InsertOne(entity);
                result = entity;
            }
            catch (Exception ex)
            {
                throw new AppException(ReturnMessages.MONGO_DB_ERROR, ex, new { entity = entity });
            }
            return result;
        }

        public TEntity ReplaceOne(TEntity entity, string id, string type = "object")
        {
            var result = new TEntity();
            try
            {
                object objectId = null;
                if (type == "guid")
                    objectId = Guid.Parse(id);
                else
                    objectId = ObjectId.Parse(id);

                var filter = Builders<TEntity>.Filter.Eq("_id", objectId);
                entity.RecordUpdateDate = DateTime.Now;
                entity.RecordUpdateUsername = GetUserName();
                var updatedDocument = _collection.ReplaceOne(filter, entity);
                result = entity;
            }
            catch (Exception ex)
            {
                throw new AppException(ReturnMessages.MONGO_DB_ERROR, ex, new { entity = entity, id = id });
            }
            return result;
        }

        public string GetUserName()
        {
            if (Context != null && Context.Current != null && Context.Current.HttpContext != null && Context.Current.HttpContext.User != null && Context.Current.HttpContext.User.Identity != null && Context.Current.HttpContext.User.Identity.IsAuthenticated)
            {
                return Context.Current.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Name).Value;
            }

            return "BATCH";
        }
    }
}
