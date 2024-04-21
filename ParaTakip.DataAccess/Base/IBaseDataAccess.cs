using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ParaTakip.DataAccess.Base
{
    public interface IBaseDataAccess<TEntity> where TEntity : class, new()
    {
        List<TEntity> Find(FilterDefinition<TEntity> filterDefinition);
        List<TEntity> GetAll();
        List<TEntity> GetByDateBtw(DateTime startDate, DateTime endDate);
        List<TEntity> FilterBy(Expression<Func<TEntity, bool>> filter);
        TEntity GetByRecordId(string id, string type = "object");
        TEntity InsertOne(TEntity entity);
        List<TEntity> InsertMany(ICollection<TEntity> entities);
        TEntity ReplaceOne(TEntity entity, string id, string type = "object");
        TEntity DeleteByRecordId(string id, string type = "object");
        void FactoryDeleteById(string recordId, string type = "object");

    }
}
