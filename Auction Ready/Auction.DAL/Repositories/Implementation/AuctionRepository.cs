using System;
using System.Linq;
using Auction.Common.Constant;
using Auction.DAL.Repositories.Interface;
using Auction.DAL.UnitOfWorks;

namespace Auction.DAL.Repositories.Implementation
{
    internal class AuctionRepository : BaseRepository<Data.Auction>, IAuctionRepository

    {
        #region Members

        #endregion

        #region Constructor

        public AuctionRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #endregion

        #region Notifications

        public IQueryable<LiderForLot> GetLiderForLot(Guid id, short direction)
        {
            decimal? liderRate = direction == DirectionOfLot.Increase
                ? DbContext.Auctions.Where(at => at.LotId == id).Max(at => at.Rate)
                : DbContext.Auctions.Where(at => at.LotId == id).Min(at => at.Rate);

            return DbContext.Auctions.Where(at => at.LotId == id && at.Rate == liderRate)
                .Select(
                    at =>
                        new LiderForLot
                        {
                            LiderRate = at.Rate,
                            LiderId = at.UserId,
                            Lider = DbContext.Users.FirstOrDefault(u => u.Id == at.UserId)
                        });
        }

        public long GetCountForLot(Guid id)
        {
            return DbContext.Auctions.Count(at => at.LotId==id);
        }

        #endregion
    }
}
