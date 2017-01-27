using System;
using System.ServiceModel;

namespace SampleBundleApi.Wcf
{
    public delegate void ConnectedEventHandler();

    public abstract class WcfClientBase<T> : IWcfClient<T>, IDisposable
    {
        protected void PipeFactoryOpened(object sender, EventArgs e)
        {

        }

        protected void PipeFactoryClosed(object sender, EventArgs e)
        {
            var contextChannel = ((IContextChannel)sender);
            contextChannel.Faulted -= PipeFactoryClosed;
        }

        public abstract void Dispose();

        public abstract T GetChannel(string endPoint);

        public event ConnectedEventHandler Connected;
    }
}