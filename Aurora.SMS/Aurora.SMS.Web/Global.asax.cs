using Aurora.Insurance.Services;
using Aurora.SMS.Web.App_Start;
using Aurora.SMS.Web.Areas.Api;
using AutoMapper;
using FluentValidation.Mvc;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Aurora.SMS.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Automatic migration

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Service.Data.SMSDb, Aurora.SMS.Web.Migrations.Configuration>());
            var dbMigrator = new DbMigrator(new Aurora.SMS.Web.Migrations.Configuration());
            if (dbMigrator.GetPendingMigrations().Any())
            {
                dbMigrator.Update();
            }

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<InsuranceDb, Aurora.Insurance.Services.Migrations.Configuration>());
            var dbMigrator2 = new DbMigrator(new Aurora.Insurance.Services.Migrations.Configuration());
            if (dbMigrator2.GetPendingMigrations().Any())
            {
                dbMigrator2.Update();
            }

            // Register Web API routing support before anything else

            GlobalConfiguration.Configure(WebApiConfig.Register);


            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperWebProfile>();
                cfg.AddProfile<AutoMapperApiProfile>();
            });



            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FluentValidationModelValidatorProvider.Configure();
        }
    }
}
