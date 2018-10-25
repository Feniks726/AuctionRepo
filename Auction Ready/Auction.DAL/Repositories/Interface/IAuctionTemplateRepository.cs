using System.Linq;
using Auction.DAL.Data;

namespace Auction.DAL.Repositories.Interface
{
    public interface IAuctionTemplateRepository : IRepository<AuctionTemplate>
    {
        IQueryable<AuctionTemplate> GetForUser(long userId, string filterByName);

        IQueryable<AuctionTemplate> GetActive(string filterByName);
    }
}
