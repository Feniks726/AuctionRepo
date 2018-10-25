using Auction.DAL.Data;

namespace Auction.DAL.UnitOfWorks
{
    internal class UnitOfWork : AuctionDBContainer, IUnitOfWork
    {
        public void Commit()
        {
            SaveChanges();
        }
    }
}
