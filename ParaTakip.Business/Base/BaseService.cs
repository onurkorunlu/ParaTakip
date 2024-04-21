using ParaTakip.Core;
using ParaTakip.DataAccess.Base;
using ParaTakip.Entities;
using System.Linq.Expressions;

namespace ParaTakip.Business.Base
{
    public abstract class BaseService<TEntity, TDataAcess> : IBaseService<TEntity,TDataAcess> where TDataAcess: IBaseDataAccess<TEntity> where TEntity : MongoBaseEntity, new()
    {
        public virtual List<TEntity> GetAll()
        {
           return AppServiceProvider.Instance.Get<TDataAcess>().GetAll();
        }

        public virtual List<TEntity> GetByDateBtw(DateTime startDate, DateTime endDate)
        {
            return AppServiceProvider.Instance.Get<TDataAcess>().GetByDateBtw(startDate, endDate);
        }

        public virtual TEntity DeleteById(string id, string type = "object")
        {
           return AppServiceProvider.Instance.Get<TDataAcess>().DeleteByRecordId(id, type);
        }

        public virtual void FactoryDeleteById(string id)
        {
            AppServiceProvider.Instance.Get<TDataAcess>().FactoryDeleteById(id);
        }

        public virtual List<TEntity> FilterBy(Expression<Func<TEntity, bool>> filter)
        {
           return AppServiceProvider.Instance.Get<TDataAcess>().FilterBy(filter);
        }

        public virtual TEntity GetById(string id, string type = "object")
        {
           return AppServiceProvider.Instance.Get<TDataAcess>().GetByRecordId(id, type);
        }

        public virtual List<TEntity> CreateMany(ICollection<TEntity> entities)
        {
           return AppServiceProvider.Instance.Get<TDataAcess>().InsertMany(entities);
        }

        public virtual TEntity Create(TEntity entity)
        {
           return AppServiceProvider.Instance.Get<TDataAcess>().InsertOne(entity);
        }

        public virtual TEntity Update(TEntity entity)
        {
           return AppServiceProvider.Instance.Get<TDataAcess>().ReplaceOne(entity, entity.RecordId.ToString());
        }
    }
}
