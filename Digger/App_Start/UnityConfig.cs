using Digger.Core.Enums;
using Digger.Core.Handlers;
using Digger.Core.Interfaces;
using Digger.Core.Services;
using Unity;

namespace Digger
{
    public static class UnityConfig
    {
        public static IUnityContainer RegisterComponents(IDiggerConfiguration config)
        {
            IUnityContainer container = new UnityContainer();

            container.RegisterInstance(config);
            container.RegisterType<IFileService, FileService>();
            container.RegisterType<BaseHandler, AllHandler>(DiggerActionType.All.ToString());
            container.RegisterType<BaseHandler, CsHandler>(DiggerActionType.Cs.ToString());
            container.RegisterType<BaseHandler, Reversed1Handler>(DiggerActionType.Reversed1.ToString());
            container.RegisterType<BaseHandler, Reversed2Handler>(DiggerActionType.Reversed2.ToString());

            return container; 
        }
    }
}
