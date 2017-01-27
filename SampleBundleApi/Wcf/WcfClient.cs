using System;
using System.Net.Security;
using System.ServiceModel;

namespace SampleBundleApi.Wcf
{
    public class WcfClient<T> : WcfClientBase<T>
    {
        private readonly ChannelFactory<T> pipeFactory;

        public WcfClient()
        {
            var namedPipeTransportSecurity = new NamedPipeTransportSecurity
            {
                ProtectionLevel = ProtectionLevel.None
            };

            var netNamedPipeSecurity = new NetNamedPipeSecurity
            {
                Mode = NetNamedPipeSecurityMode.None,
                Transport = namedPipeTransportSecurity
            };

            var netNamedPipeBinding = new NetNamedPipeBinding
            {
                MaxConnections = 9999,
                TransactionProtocol = TransactionProtocol.OleTransactions,
                Security = netNamedPipeSecurity,
                OpenTimeout = TimeSpan.FromDays(1),
                SendTimeout = TimeSpan.FromDays(1),
                ReceiveTimeout = TimeSpan.FromDays(1),
                TransferMode = TransferMode.Streamed,
                CloseTimeout = TimeSpan.FromDays(1),
            };

            this.pipeFactory = new ChannelFactory<T>(netNamedPipeBinding);

            this.pipeFactory.Closed += PipeFactoryClosed;
            this.pipeFactory.Faulted += PipeFactoryClosed;
            this.pipeFactory.Opened += PipeFactoryOpened;
        }

        public override T GetChannel(string endPoint)
        {
            var endpointAddress = new EndpointAddress(endPoint);

            var channel = this.pipeFactory.CreateChannel(endpointAddress);

            var contextChannel = ((IContextChannel)channel);

            if (contextChannel != null)
            {
                contextChannel.OperationTimeout = TimeSpan.FromDays(1);
            }

            return channel;
        }

        public override void Dispose()
        {
            this.pipeFactory.Closed -= PipeFactoryClosed;
            this.pipeFactory.Faulted -= PipeFactoryClosed;
            this.pipeFactory.Opened -= PipeFactoryOpened;
        }
    }
}