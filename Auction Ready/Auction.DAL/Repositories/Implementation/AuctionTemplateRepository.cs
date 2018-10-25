using System.Linq;
using Auction.DAL.Data;
using Auction.DAL.Repositories.Interface;
using Auction.DAL.UnitOfWorks;

namespace Auction.DAL.Repositories.Implementation
{
    internal class AuctionTemplateRepository : BaseRepository<AuctionTemplate>, IAuctionTemplateRepository
    {
        #region Members

        #endregion

        #region Constructor

        public AuctionTemplateRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #endregion
        
        #region Notifications

        public IQueryable<AuctionTemplate> GetForUser(long userId, string filterByName)
        {
            return
                DbContext.AuctionTemplates.Where(
                    at => at.AuthorID == userId && (filterByName == null || at.Name.Contains(filterByName)))
                    .OrderByDescending(at=>at.CreateDateTime)
                    .ThenBy(at=>at.Name);
        }

        public IQueryable<AuctionTemplate> GetActive(string filterByName)
        {
            return
                DbContext.AuctionTemplates.Where(
                    at => !at.IsStopped && (filterByName == null || at.Name.Contains(filterByName)))
                    .OrderByDescending(at => at.CreateDateTime)
                    .ThenBy(at => at.Name);
        }

        #endregion
    }
}
