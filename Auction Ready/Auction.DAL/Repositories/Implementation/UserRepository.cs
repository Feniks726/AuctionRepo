using System.Linq;
using Auction.DAL.Data;
using Auction.DAL.Repositories.Interface;
using Auction.DAL.UnitOfWorks;

namespace Auction.DAL.Repositories.Implementation
{
    internal class UserRepository : BaseRepository<User>, IUserRepository
    {
        #region Members

        #endregion

        #region Constructor

        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #endregion

        #region Notifications

        public User GetByLoginAndPassword(string login, string password)
        {
            return DbContext.Users.SingleOrDefault(u => u.Email == login && u.Password == password);
        }

        public User GetById(long id)
        {
            return DbContext.Users.SingleOrDefault(u => u.Id == id);
        }

        public bool CheckByLogin(string login)
        {
            return DbContext.Users.Any(u => u.Email == login);
        }

        public override void Save(User user)
        {
            base.Save(user);
        }

        #endregion
    }
}
