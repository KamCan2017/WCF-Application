using Domain.Services;
using System;
using System.Threading.Tasks;

namespace GettingStartedClient
{
    class Program
    {
        static void Main(string[] args)
        {
            SampleCalculationService();
            SampleWeatherService();

            Console.ReadLine();
        }

        private static void SampleCalculationService()
        {
            try
            {
                Console.WriteLine("-----CalculatorService-----");

                //var client = ProxyCreator.GetInstance<ServiceReference1.ICalculatorService>();
                //var value = client.Add(1, 2);

                var svc = ServiceClient<ICalculatorService>.GetService();
                var res = svc.Add(1, 2);
                //Step 1: Create an instance of the WCF proxy.  
                //CalculatorServiceClient client = new CalculatorServiceClient();

                // Step 2: Call the service operations.  
                // Call the Add service operation.  
                double value1 = 100.00D;
                double value2 = 15.99D;
                double result = ServiceClient<ICalculatorService>.Execute(o => o.Add(value1, value2));
                //client.Add(value1, value2);
                Console.WriteLine("Add({0};{1}) = {2}", value1, value2, result);

                // Call the Subtract service operation.  
                value1 = 145.00D;
                value2 = 76.54D;
                result = ServiceClient<ICalculatorService>.Execute(o => o.Subtract(value1, value2));
                //client.Subtract(value1, value2);
                Console.WriteLine("Subtract({0};{1}) = {2}", value1, value2, result);

                // Call the Multiply service operation.  
                value1 = 9.00D;
                value2 = 81.25D;
                result = ServiceClient<ICalculatorService>.Execute(o => o.Multiply(value1, value2));
                //client.Multiply(value1, value2);
                Console.WriteLine("Multiply({0};{1}) = {2}", value1, value2, result);

                // Call the Divide service operation.  
                value1 = 22.00D;
                value2 = 7.00D;
                result = ServiceClient<ICalculatorService>.Execute(o => o.Divide(value1, value2));
                //client.Divide(value1, value2);
                Console.WriteLine("Divide({0};{1}) = {2}", value1, value2, result);
                //Step 3: Closing the client gracefully closes the connection and cleans up resources.  
                //client.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private static void SampleWeatherService()
        {
            try
            {
                Console.WriteLine("-----WeatherService-----");

                var weather = ServiceClient<IWeatherService>.Execute(o => o.GetTemperature("Paris"));
                Console.WriteLine(weather);

                weather = ServiceClient<IWeatherService>.Execute(o => o.GetTemperature("Yaounde"));
                Console.WriteLine(weather);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}