using System.Data.Entity.Migrations;
using System.Linq;
using Auction.DAL.Data;
using Auction.DAL.UnitOfWorks;

namespace Auction.DAL.Repositories.Implementation
{
    /// <summary>
    /// Base repository for all repositories
    /// </summary>
    internal abstract class BaseRepository<T> where T : class, new()
    {
        #region Members

        protected AuctionDBContainer DbContext { get; private set; }
      
        #endregion

        #region Constructor

        protected BaseRepository(IUnitOfWork unitOfWork)
        {
            DbContext = (AuctionDBContainer)unitOfWork;
        }

        #endregion

        #region Notifications

        public virtual IQueryable<T> GetAll()
        {
            return DbContext.Set<T>();
        }

        public virtual T GetById(object id)
        {
            return DbContext.Set<T>().Find(id);
        }

        public virtual void Save(T entity)
        {
            DbContext.Set<T>().AddOrUpdate(entity);
            DbContext.SaveChanges();
        }

        public virtual T Add(T entity)
        {
            return DbContext.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            DbContext.Set<T>().Remove(entity);
            DbContext.SaveChanges();
        }

        #endregion
    }
}
