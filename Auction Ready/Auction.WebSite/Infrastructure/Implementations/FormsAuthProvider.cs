using System;
using System.Web.Security;
using Auction.Common.Utilities.Interfaces;
using Auction.DAL.Data;
using Auction.DAL.Factories;
using Auction.WebSite.Infrastructure.Interfaces;
using Auction.WebSite.Models;

namespace Auction.WebSite.Infrastructure.Implementations
{
    public class FormsAuthProvider : IAuthProvider
    {
        protected readonly IDbFactory DbFactory;
        private readonly ICryptoProvider _cryptoProvider;

        public FormsAuthProvider
            (
                IDbFactory dbFactory,
                ICryptoProvider cryptoProvider
            )
        {
            DbFactory = dbFactory;
            _cryptoProvider = cryptoProvider;
        }

        public bool Authenticate(string username, string password, out User user)
        {
            using (DbFactory.CreateUnitOfWork())
            {
                user = DbFactory.Users.GetByLoginAndPassword(username, _cryptoProvider.ComputeHash(password));
                var result = user != null;
                if (result)
                    FormsAuthentication.SetAuthCookie(user.Id.ToString(), false);

                return result;
            }
        }

        public bool Authenticate(long id, out User user)
        {
            using (DbFactory.CreateUnitOfWork())
            {
                user = DbFactory.Users.GetById(id);
            }
            return user != null;
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        public bool CheckByLogin(string username)
        {
            using (DbFactory.CreateUnitOfWork())
            {
                return DbFactory.Users.CheckByLogin(username);
            }
        }

        public bool CreateUser(RegistrationViewModel model, out string error)
        {
            var res = false;
            error = null;
            try
            {
                using (DbFactory.CreateUnitOfWork())
                {
                    var newUser = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.NewEmail,
                        Password = _cryptoProvider.ComputeHash(model.NewPassword),
                        IsActive = true
                    };

                    DbFactory.Users.Save(newUser);
                }
            }
            catch (Exception e)
            {
                res = true;
                error = e.Message;
            }
            return res;
        }

        public User GetUserById(long id)
        {
            using (DbFactory.CreateUnitOfWork())
            {
                return DbFactory.Users.GetById(id);
            }
        }
    }
}