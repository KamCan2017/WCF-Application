using Autofac;
using GettingStartedClient.ServiceReference1;
using System;

namespace GettingStartedClient
{
    public static class ProxyCreator
    {
        private static readonly IContainer _container;
        static ProxyCreator()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<CalculatorServiceClient>()
            .Named<ICalculatorService>(typeof(ICalculatorService).Name);
            _container = builder.Build();
        }

        public static T GetInstance<T>()
        {
            var type = typeof(T);
            if (_container.IsRegisteredWithName<T>(type.Name))
              return _container.ResolveNamed<T>(type.Name);

            throw new Exception(typeof(T).Name + " is not registered");
        }
    }
}
