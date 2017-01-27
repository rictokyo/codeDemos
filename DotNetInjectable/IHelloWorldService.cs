using System.ServiceModel;
using SampleBundleApi.Wcf;

namespace DotNetInjectable
{
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface IHelloWorldService : IWcfService
    {
        [OperationContract(IsOneWay = true)]
        void Say(string message);
    }
}
