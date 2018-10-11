using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

using Odigo.Business.Interfaces;
using Odigo.Data;
using Odigo.Business;
using Odigo.Utility.Interfaces;
using Odigo.Utility;
using Odigo.Model.Model;
using Odigo.Business.Factories;
using Odigo.Web.Controllers;
using Odigo.Notification;
using Odigo.Notification.Interfaces;

namespace Odigo.Web.App_Start
{
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

            string basePath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;

            // TODO: Register your types here
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());

            //container.RegisterType<Microsoft.AspNet.Identity.IUserStore<Models.ApplicationUser>, Microsoft.AspNet.Identity.EntityFramework.UserStore<Models.ApplicationUser>>();

            container.RegisterType<ILogger, Logger>();
            container.RegisterType<IRepository, Repository>();
            container.RegisterType<IImageManager, ImageManager>();
            container.RegisterType<IFileManager, FileManager>(new InjectionConstructor(basePath));
            container.RegisterType<ISubscriptionFactory, SubscriptionFactory>();
            container.RegisterType<IRegistrationFactory, RegistrationFactory>();
            container.RegisterType<ISubscription, TeacherSubscriptionService>();
            container.RegisterType<IRegistration, EmployerRegistrationService>();
            container.RegisterType<IPersonEditorService, PersonEditorService>();

            container.RegisterType<INews, NewsService>();
            container.RegisterType<IQuickLinker, QuickLinkService>();
            container.RegisterType<IModelAggregator<TeacherEducationalQualification>, TeacherQualificationAggregator>();
            container.RegisterType<IModelAggregator<TeacherStudentCategory>, TeacherChildCategoryAggregator>();
            container.RegisterType<IModelAggregator<TeacherEmploymentHistory>, TeacherExperienceAggregator>();
            container.RegisterType<ITeacherFinder, TeacherFinderService>();
            container.RegisterType<IGateService, GateService>();
            container.RegisterType<IService, ServiceChargeEngine>();
            container.RegisterType<IPaymentService, PaymentService >();
            container.RegisterType<IRequestService, RequestService>();

            container.RegisterType<Models.HomeViewModel>(new ContainerControlledLifetimeManager());

            container.RegisterType<INotificationProvider<Email, bool>, EmailEngine>(new InjectionConstructor("localhost:1462", "admin@nitware.com"));
            container.RegisterType<INotificationProvider<Sms, NexmoResponse>, SmsEngine>(new InjectionConstructor("004272ab", "a2cc23a0", "https://rest.nexmo.com/sms/json"));

            //container.RegisterType<INotificationProvider<Sms, NexmoResponse>, SmsEngine>(new InjectionConstructor("0cd0f2f5", "f7dbe54b", "http://rest.nexmo.com/sms/json"));

            



        }



    }
}
