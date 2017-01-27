using System.ServiceModel;

namespace SampleBundleApi.Wcf
{
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface IWcfService
    {
        [OperationContract(IsOneWay = true)]
        void Start(string endpointUrl, string pipeName);
    }
}