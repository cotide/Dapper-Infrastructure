using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace BH.Framework.Extensions.Castle
{
    /// <summary>
    /// Windsor IoC 扩展类
    /// </summary>
    public static class WindsorExtensions
    {
        /// <summary>
        ///     Searches for the first interface found associated with the 
        ///     <see cref = "ServiceDescriptor" /> which is not generic and which 
        ///     is found in the specified namespace.
        /// </summary>
        public static BasedOnDescriptor FirstNonGenericCoreInterface(
            this ServiceDescriptor descriptor, string interfaceNamespace)
        {
            return descriptor.Select(
                delegate(Type type, Type[] baseType)
                {
                    var interfaces =
                        type.GetInterfaces().Where(
                            t => t.IsGenericType == false && t.Namespace.StartsWith(interfaceNamespace));

                    if (interfaces.Any())
                    {
                        return new[] { interfaces.ElementAt(0) };
                    }

                    return null;
                });
        }

        public static IWindsorContainer RegisterController<T>(this IWindsorContainer container) where T : IController
        {
            container.RegisterControllers(typeof(T));
            return container;
        }

        public static IWindsorContainer RegisterControllers(this IWindsorContainer container, params Type[] controllerTypes)
        {
            foreach (var type in controllerTypes.Where(ControllerExtensions.IsController))
            {
                container.Register(Component.For(type).Named(type.FullName.ToLower()).LifeStyle.Is(LifestyleType.Transient));
            }

            return container;
        }

        public static IWindsorContainer RegisterControllers(this IWindsorContainer container, params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                container.RegisterControllers(assembly.GetExportedTypes());
            }
            return container;
        }
    }
}
