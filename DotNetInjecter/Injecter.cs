using System;
using System.Diagnostics;
using System.Threading;
using DotNetInjectable;
using SampleBundleApi;
using SampleBundleApi.Injector;

namespace DotNetInjecter
{
    public class Injecter : Sample, IInjecter, IDisposable
    {
        private readonly ICommunicator communicator;
        private readonly IInterProcessHelper processHelper;
        readonly Process sampleProcess = new Process { StartInfo = { FileName = "SimpleWindowsApplication.exe" } };

        public Injecter(string sampleName, ICommunicator communicator, IInterProcessHelper processHelper)
            : base(sampleName)
        {
            this.communicator = communicator;
            this.processHelper = processHelper;

            AppDomain.CurrentDomain.ProcessExit += Injecter_Exited; 
        }

        void Injecter_Exited(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public override void Start()
        {
            this.communicator.Say("Starting injecter");

            this.communicator.Say("Started separate process");

            sampleProcess.Start();

            sampleProcess.WaitForInputIdle();

            Thread.Sleep(1000);

            this.processHelper.InjectService<ServiceLoader>(sampleProcess);
        }

        public void Dispose()
        {
            AppDomain.CurrentDomain.ProcessExit -= Injecter_Exited;

            try
            {
                this.sampleProcess.Kill();
            }
            catch (InvalidOperationException)
            {
                // ignore
            }
        }
    }
}
