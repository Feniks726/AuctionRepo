using System;
using System.Web;
using Auction.Common.Utilities.Implementations;
using Auction.Common.Utilities.Interfaces;
using Auction.DAL.Factories;
using Auction.WebSite;
using Auction.WebSite.Infrastructure.Implementations;
using Auction.WebSite.Infrastructure.Interfaces;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Syntax;
using Ninject.Web.Common;
using WebActivatorEx;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: ApplicationShutdownMethod(typeof(NinjectWebCommon), "Stop")]

namespace Auction.WebSite
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IBindingRoot kernel)
        {
            kernel.Bind<IDbFactory>().To<DbFactory>().InSingletonScope();
            kernel.Bind<IAuthProvider>().To<FormsAuthProvider>().InSingletonScope();
            kernel.Bind<ICryptoProvider>().To<Sha256CryptoProvider>().InSingletonScope();
            kernel.Bind<IAuctionProvider>().To<AuctionProvider>().InSingletonScope();
        }
    }
}