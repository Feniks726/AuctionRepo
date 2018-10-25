using System.Web.Mvc;
using Auction.Common.Constant.Messages;
using Auction.DAL.Data;
using Auction.DAL.Factories;
using Auction.WebSite.Infrastructure;
using Auction.WebSite.Infrastructure.Interfaces;
using Auction.WebSite.Models;
using NLog;

namespace Auction.WebSite.Controllers
{
    public class AccountController : BaseController
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IAuthProvider _authProvider;

        public AccountController
            (
                IDbFactory dbFactory,
                IAuthProvider authProvider)
            : base(dbFactory)
        {
            _authProvider = authProvider;
        }

        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            User user;
            if (_authProvider.Authenticate(model.Email, model.Password, out user))
            {
                if (!user.IsActive)
                {
                    Logger.Error(Messages.LogsConstant.TheUserBlocked.Format(user.FullName));
                    ModelState.AddModelError("", Messages.MessagesConstant.TheUserBlocked.Format(user.FullName));

                    return View();

                }
                SessionManager.SetSessionStateVariables(Session, user);
                Logger.Debug(Messages.LogsConstant.AuthorizationSuccessful.Format(user.FullName));
                return Redirect(returnUrl ?? Url.Action("ActiveAuctions", "Auctions"));
            }

            Logger.Error(Messages.LogsConstant.AuthorizationFailed);
            ModelState.AddModelError("", Messages.MessagesConstant.TheUsernameOrPasswordIsIncorrect.ToString());

            return View();
        }

        public ActionResult Exit()
        {
            _authProvider.SignOut();
            SessionManager.ClearSession(Session);
            Logger.Debug(Messages.LogsConstant.OutUser.Format(Session.GetUserName()));
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(RegistrationViewModel model, string returnUrl)
        {
            if (_authProvider.CheckByLogin(model.NewEmail))
            {
                Logger.Error(Messages.LogsConstant.TheEmailAlreadyPresent.Format(model.NewEmail));
                ModelState.AddModelError("", Messages.MessagesConstant.TheEmailAlreadyPresent.ToString());
                return View();
            }

            string error;
            if (!_authProvider.CreateUser(model, out error))
            {
                Logger.Debug(Messages.LogsConstant.TheUserCreated.Format(model.FullName));
                return RedirectToAction("Login", "Account");
            }
            Logger.Debug(Messages.LogsConstant.TheUserCreatedMistake.Format(model.FullName, error));
            ModelState.AddModelError("",
                Messages.MessagesConstant.TheUserCreatedMistake.Format(model.FullName, error));

            return View();
           
        }

        public ActionResult ProfileCurrentUser()
        {
            var currentuser = GetCurrentUser();
            return View(currentuser);
        }

        public ProfileViewModel GetCurrentUser()
        {
            var user = _authProvider.GetUserById(Session.GetUserId());
            var currentuser = new ProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
            return currentuser;
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }
    }
}
