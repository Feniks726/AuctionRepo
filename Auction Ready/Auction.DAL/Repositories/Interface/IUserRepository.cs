using Auction.DAL.Data;

namespace Auction.DAL.Repositories.Interface
{
    public interface IUserRepository: IRepository<User>
    {
        /// <summary>
        /// Get user by login and password
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        User GetByLoginAndPassword(string login, string password);

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User GetById(long id);

        /// <summary>
        /// Checking login existence 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        bool CheckByLogin(string login);
    }
}
