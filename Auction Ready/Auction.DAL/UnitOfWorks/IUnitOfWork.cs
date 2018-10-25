using System;

namespace Auction.DAL.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
