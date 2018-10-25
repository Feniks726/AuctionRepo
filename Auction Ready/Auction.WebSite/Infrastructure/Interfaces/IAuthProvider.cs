using Auction.DAL.Data;
using Auction.WebSite.Models;

namespace Auction.WebSite.Infrastructure.Interfaces
{
    /// <summary>
    /// Represent authentification provider
    /// </summary>
    public interface IAuthProvider
    {
        /// <summary>
        /// Authenticate user by username and password
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password">User password</param>
        /// <param name="user">Current user</param>
        /// <returns>Authentication result</returns>
        bool Authenticate(string login, string password, out User user);

        /// <summary>
        /// Authenticate user by unique user identifier
        /// </summary>
        /// <param name="id">Unique user identifier</param>
        /// <param name="user"></param>
        /// <returns></returns>
        bool Authenticate(long id, out User user);

        /// <summary>
        /// Sign out user
        /// </summary>
        void SignOut();

        /// <summary>
        /// Checking e-mail registration
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        bool CheckByLogin(string email);

        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="model"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool CreateUser(RegistrationViewModel model, out string error);

        /// <summary>
        /// return current user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User GetUserById(long id);
    }
}