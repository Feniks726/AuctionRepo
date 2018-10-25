using System.Web.Mvc;
using Auction.DAL.Factories;

namespace Auction.WebSite.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly IDbFactory DbFactory;
        protected BaseController(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
        }
    }
}