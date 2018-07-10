using Domain.Services;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace GettingStartedClient
{
    public static class ServiceClient<T> 
    {
        private static readonly ChannelFactory<T> ChannelFactory;
        static ServiceClient()
        {
            WSHttpBinding wSHttpBinding = new WSHttpBinding();
            EndpointAddress myEndpoint = new EndpointAddress(ServiceEndpoint.GetEndPointOfService<T>());
            ChannelFactory = new ChannelFactory<T>(wSHttpBinding, myEndpoint);
        }

        public static T GetService()
        {
            IClientChannel clientChannel = (IClientChannel)ChannelFactory.CreateChannel();
            return(T)clientChannel;
        }

        public static TResult Execute<TResult>(Func<T, TResult> action)
        {
            IClientChannel clientChannel = (IClientChannel)ChannelFactory.CreateChannel();
            TResult result;

            bool success = false;
            try
            {
                result = action((T)clientChannel);
                clientChannel.Close();
                success = true;
            }
            finally
            {
                if (!success)
                {
                    clientChannel.Abort();
                }
            }
            return result;
        }
    }


    public static class ServiceEndpoint
    {
        private static readonly Dictionary<Type, string> _mappingEndpoint = new Dictionary<Type, string>();


        static ServiceEndpoint()
        {
            _mappingEndpoint.Add(typeof(ICalculatorService), 
            "http://localhost:8733/Design_Time_Addresses/GettingStartedLib/CalculatorService/CalculatorService");
        }

        public static string GetEndPointOfService<T>()
        {
            if (_mappingEndpoint.TryGetValue(typeof(T), out string endPoint))
                return endPoint;

            return string.Empty;
        }
    }
}
