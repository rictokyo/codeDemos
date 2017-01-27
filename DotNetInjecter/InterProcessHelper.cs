using System.Diagnostics;
using DotNetInjectable;
using SampleBundleApi;
using SampleBundleApi.Injector;
using SampleBundleApi.Wcf;

namespace DotNetInjecter
{
    public class InterProcessHelper : IInterProcessHelper
    {
        private readonly IInjectorWrapper wrapper;
        private readonly ICommunicator communicator;
        private IHelloWorldService service;

        public InterProcessHelper(IInjectorWrapper wrapper, ICommunicator communicator)
        {
            this.wrapper = wrapper;
            this.communicator = communicator;
        }

        public void InjectService<T>(string processName)
        {
            if (this.wrapper == null)
            {
                this.communicator.Say("error: no injector wrapper defined...");

                return;
            }

            var injected = this.wrapper.Inject(processName, typeof (T).Assembly.Location, typeof (T).FullName, "Start");

            if (injected)
            {
                var sampleWcfClient = new WcfClient<IHelloWorldService>();
                this.service = sampleWcfClient.GetChannel("net.pipe://localhost/B/PipeService");

                this.service.Say("Owned");
            }
            else
            {
                this.communicator.Say("error: failed to inject service");
            }
        }

        public void InjectService<T>(Process sampleProcess)
        {
            InjectService<T>(sampleProcess.ProcessName);
        }
    }
}