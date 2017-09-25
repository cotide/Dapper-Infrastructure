using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BH.Framework.Utility;
using Castle.Windsor;

namespace BH.Framework.Extensions.Castle
{
    /// <summary>
    /// Windsor IoC 工厂类
    /// </summary>
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IWindsorContainer _container;

        /// <summary>
        /// 初始化 Windsor IoC 工厂
        /// </summary>
        /// <param name="container">当前容器对象</param>
        public WindsorControllerFactory(IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            Guard.IsNotNull(
                container,
                "container", 
                "Windsor IoC 初始化错误");

            this._container = container;
        }


        public override void ReleaseController(IController controller)
        {
            var disposable = controller as IDisposable;

            if (disposable != null)
            {
                disposable.Dispose();
            }

            this._container.Release(controller);
        }


        protected override IController GetControllerInstance(RequestContext context, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404,
                    string.Format("Windsor IoC 错误: The controller for path '{0}' could not be found or it does not implement IController.",
                    context.HttpContext.Request.Path));
            } 
            return (IController)this._container.Resolve(controllerType);
        }
    }
}
