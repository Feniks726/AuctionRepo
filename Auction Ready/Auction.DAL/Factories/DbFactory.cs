using Auction.DAL.Repositories.Implementation;
using Auction.DAL.Repositories.Interface;
using Auction.DAL.UnitOfWorks;

namespace Auction.DAL.Factories
{
    public class DbFactory : IDbFactory
    {
        private IUnitOfWork _unitOfWork;

        public IUnitOfWork CreateUnitOfWork()
        {
            _unitOfWork = new UnitOfWork();
            return _unitOfWork;
        }

        #region Repositories

        public IUserRepository Users => new UserRepository(_unitOfWork);

        public IAuctionTemplateRepository AuctionTemplates => new AuctionTemplateRepository(_unitOfWork);

        public ILotRepository Lots => new LotRepository(_unitOfWork);

        public IAuctionRepository Auctions => new AuctionRepository(_unitOfWork);

        #endregion
    }
}
