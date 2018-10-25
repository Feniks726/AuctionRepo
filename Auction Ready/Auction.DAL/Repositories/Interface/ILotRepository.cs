using System;
using System.Collections.Generic;
using Auction.DAL.Data;

namespace Auction.DAL.Repositories.Interface
{
    public interface ILotRepository : IRepository<Lot>
    {
        List<Lot> GetForAuctionTemplate(Guid? id, string filterLotByContext);

        int GetCountAuctionTemplate(Guid? id);

        List<Lot> GetActiveForAuction(Guid id, string filterLotByContext);
    }
}
