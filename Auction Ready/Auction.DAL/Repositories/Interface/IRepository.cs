using System.Linq;

namespace Auction.DAL.Repositories.Interface
{
    public interface IRepository<T> where T : class, new()
    {
        IQueryable<T> GetAll();

        T GetById(object id);

        void Save(T entity);

        T Add(T entity);

        void Delete(T entity);
    }
}
