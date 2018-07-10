using System.ServiceModel;
using System.Threading.Tasks;

namespace Domain.Services
{
    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples")]
    public interface IWeatherService
    {
        [OperationContract]
        string GetTemperature(string city);
    }
}
