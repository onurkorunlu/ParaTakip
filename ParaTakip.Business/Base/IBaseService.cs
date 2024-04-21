using System.Linq.Expressions;

namespace ParaTakip.Business.Base
{
    public interface IBaseService<TEntity,TDataAccess> 
    {
        List<TEntity> GetAll();
        List<TEntity> GetByDateBtw(DateTime startDate, DateTime endDate);
        List<TEntity> FilterBy(Expression<Func<TEntity, bool>> filter);
        TEntity GetById(string id, string type = "object");
        TEntity Create(TEntity entity);
        List<TEntity> CreateMany(ICollection<TEntity> entities);
        TEntity Update(TEntity entity);
        TEntity DeleteById(string id, string type = "object");
        void FactoryDeleteById(string id);
    }
}
