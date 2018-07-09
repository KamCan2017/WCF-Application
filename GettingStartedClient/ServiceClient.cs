using System;
using System.ServiceModel;

namespace GettingStartedClient
{
    public static class ServiceClient<T> 
    {
        private static readonly ChannelFactory<T> ChannelFactory;

        private static readonly string EndpointAddress = "http://localhost:8733/Design_Time_Addresses/GettingStartedLib/CalculatorService/CalculatorService";
        static ServiceClient()
        {
            WSHttpBinding wSHttpBinding = new WSHttpBinding();
            EndpointAddress myEndpoint = new EndpointAddress(EndpointAddress);
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
}
