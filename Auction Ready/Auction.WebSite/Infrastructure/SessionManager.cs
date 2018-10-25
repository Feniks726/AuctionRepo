using System.Web;
using Auction.DAL.Data;

namespace Auction.WebSite.Infrastructure
{
    /// <summary>
    /// Represent session state manager. Provide methods for working with session variables
    /// </summary>
    public static class SessionManager
    {
        private const string UserIdKey = "UserID";
        private const string UserNameKey = "UserNameKey";


        public static void SetSessionStateVariables(HttpSessionStateBase state, User user)
        {
            state[UserIdKey] = user.Id;
            state[UserNameKey] = user.FirstName + ' ' + user.LastName;
        }

        public static void ClearSession(HttpSessionStateBase state)
        {
            state.Abandon();
        }

        public static string GetUserName(this HttpSessionStateBase state)
        {
            return state[UserNameKey] as string;
        }

        public static long GetUserId(this HttpSessionStateBase state)
        {
            return (long)state[UserIdKey];
        }
    }
}