using System;
using System.Linq;
using Auction.DAL.Data;

namespace Auction.DAL.Repositories.Interface
{
    public interface IAuctionRepository: IRepository<Data.Auction>
    {
        IQueryable<LiderForLot> GetLiderForLot(Guid id, short direction);

        long GetCountForLot(Guid id);
    }

    public class LiderForLot
    {

        public decimal LiderRate { get; set; }

        public long LiderId { get; set; }

        public User Lider { get; set; }
    }
}
