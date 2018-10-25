using System;
using System.Collections.Generic;
using System.Linq;
using Auction.Common.Constant;
using Auction.DAL.Data;
using Auction.DAL.Repositories.Interface;
using Auction.DAL.UnitOfWorks;

namespace Auction.DAL.Repositories.Implementation
{
    internal class LotRepository : BaseRepository<Lot>, ILotRepository

    {
        #region Members

        #endregion

        #region Constructor

        public LotRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #endregion

        #region Notifications

        public List<Lot> GetForAuctionTemplate(Guid? id, string filterLotByContext)
        {
            return
                new List<Lot>(DbContext.Lots.Where(
                    lot => lot.AuctionTemplateId == id && (filterLotByContext == null || lot.Context.Contains(filterLotByContext)))
                    .OrderByDescending(at => at.CreateDateTime)
                    .ThenBy(at => at.Context));
        }

        public int GetCountAuctionTemplate(Guid? id)
        {
            return DbContext.Lots
                    .Count(lot => lot.AuctionTemplateId == id);
        }

        public List<Lot> GetActiveForAuction(Guid id, string filterLotByContext)
        {
            var startedDateTime = DbContext.AuctionTemplates.FirstOrDefault(at => at.Id == id)?.StartedDateTime;
            var lots = new List<Lot>(DbContext.Lots.Where(
                     lot =>
                         lot.AuctionTemplateId == id &&
                          (filterLotByContext == null || lot.Context.Contains(filterLotByContext))));

            return new List<Lot>(lots.Where(lot => lot.Duration == null || startedDateTime.HasValue &&
                                              (startedDateTime.Value.ToOADate() + lot.Duration.Value.ToOADate() -
                                               DateTimeConstant.MinDate.ToOADate() >
                                               DateTime.Now.ToOADate()))
                .OrderByDescending(lot => lot.CreateDateTime)
                .ThenBy(lot => lot.Context));
        }

        #endregion
    }
}
