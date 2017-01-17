using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Web;
using Aurora.Core.Data;

namespace Aurora.SMS.Web.App_Start
{
 
    public class CurrentUserService : ICurrentUserService
    {
        public string GetCurrentUser()
        {
            if (HttpContext.Current==null && !HttpContext.Current.User.Identity.IsAuthenticated)
                return null;
            return HttpContext.Current.User.Identity.Name;
        }
    }

    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // NOTE: Register your types here
            container.RegisterType<ICurrentUserService, CurrentUserService>(new PerRequestLifetimeManager());
            container.RegisterType<DbFactory<Service.Data.SMSDb>, Service.Data.SMSDbFactory>(new PerRequestLifetimeManager());
            
            //Register two interfaces as one singleton
            container.RegisterType<UnitOfWork<Service.Data.SMSDb>, UnitOfWork<Service.Data.SMSDb>>(new PerRequestLifetimeManager());
            container.RegisterType<IUnitOfWork<Service.Data.SMSDb>, UnitOfWork<Service.Data.SMSDb>>(new PerRequestLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork<Service.Data.SMSDb>>(new PerRequestLifetimeManager());

            container.RegisterType<Service.ITemplateServices, Service.TemplateServices>();
            container.RegisterType<Service.ITemplateFieldServices, Service.TemplateFieldServices>();
            container.RegisterType<Service.IInsuranceServices, Service.InsuranceServices>();
            container.RegisterType<Service.ISMSServices, Service.SMSServices>();
            
        }
    }
}
