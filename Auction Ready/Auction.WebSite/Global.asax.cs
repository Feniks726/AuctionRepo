using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Auction.WebSite
{
    public class MvcApplication : HttpApplication
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        protected void Application_Start()
        {
            Logger.Info("Application Start");

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public override void Init()
        {
            Logger.Info("Application Init");
            base.Init();
        }

        public override void Dispose()
        {
            Logger.Info("Application Dispose");
            base.Dispose();
        }

        protected void Application_Error()
        {
            Logger.Info("Application Error");
        }


        protected void Application_End()
        {
            Logger.Info("Application End");
        }

        protected void Application_BeginRequest()
        {
            Logger.Info("Application BeginRequest");
        }

        protected void Application_EndRequest()
        {
            Logger.Info("Application EndRequest");
        }

        protected void Application_PreRequestHandlerExecute()
        {
            Logger.Info("Application PreRequestHandlerExecute");
        }

        protected void Application_PostRequestHandlerExecute()
        {
            Logger.Info("Application PreRequestHandlerExecute");
        }

        protected void Application_PreSendRequestHeaders()
        {
            Logger.Info("Application PreSendRequestHeaders");
        }

        protected void Application_PreSendContent()
        {
            Logger.Info("Application PreSendContent");
        }

        protected void Application_AcquireRequestState()
        {
            Logger.Info("Application AcquireRequestState");
        }

        protected void Application_ReleaseRequestState()
        {
            Logger.Info("Application ReleaseRequestState");
        }

        protected void Application_ResolveRequestCache()
        {
            Logger.Info("Application ResolveRequestCache");
        }

        protected void Application_UpdateRequestCache()
        {
            Logger.Info("Application UpdateRequestCache");
        }

        protected void Application_AuthenticateRequest()
        {
            Logger.Info("Application AuthenticateRequest");
        }

        protected void Application_AuthorizeRequest()
        {
            Logger.Info("Application AuthorizeRequest");
        }

        protected void Session_Start()
        {
            Logger.Info("Session Start");
        }

        protected void Session_End()
        {
            Logger.Info("Session End");
        }
    }
}
