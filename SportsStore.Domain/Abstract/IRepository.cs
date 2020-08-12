using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Abstract
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> GetAll();
        List<TEntity> GetAllBind();
        TEntity Add(TEntity entity);
        TEntity GetBy(params object[] Id);
        bool Update(TEntity entity);
        bool Remove(TEntity entity);
    }
}
