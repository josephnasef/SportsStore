using Moq;
using Ninject;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Infrastructure.Concrete;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernal;
        public NinjectDependencyResolver(IKernel kernelPram)
        {
            this.kernal = kernelPram;
            AddBindings();
        }

        private void AddBindings()
        {

            kernal.Bind<IRepository<Product>>()
                .To<Repository<Product>>();

            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };

            kernal.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);
            kernal.Bind<IAuthProvider>().To<FormsAuthProvider>();
        }

        public object GetService(Type serviceType)
        {
            return kernal.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernal.GetAll(serviceType);
        }
        private static void RegisterServices(IKernel kernel)
        {
            System.Web.Mvc.DependencyResolver.SetResolver(new SportsStore.WebUI.Infrastructure.NinjectDependencyResolver(kernel));
        }
    }
}