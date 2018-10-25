using Auction.DAL.Repositories.Interface;
using Auction.DAL.UnitOfWorks;

namespace Auction.DAL.Factories
{
    public interface IDbFactory
    {
        /// <summary>
        /// Create unit of work
        /// </summary>
        /// <returns></returns>
        IUnitOfWork CreateUnitOfWork();

        #region Repositories

        /// <summary>
        /// Get user repository
        /// </summary>
        IUserRepository Users { get; }

        /// <summary>
        /// Get auctiontemplate repository
        /// </summary>
        IAuctionTemplateRepository AuctionTemplates { get; }

        /// <summary>
        /// Get lot repository
        /// </summary>
        ILotRepository Lots { get; }

        /// <summary>
        /// Get auction repository
        /// </summary>
        IAuctionRepository Auctions { get; }
        #endregion
    }
}
