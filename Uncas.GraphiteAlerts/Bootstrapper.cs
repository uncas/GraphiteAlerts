using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;

namespace Uncas.GraphiteAlerts
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            IUnityContainer container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
        }
    }
}