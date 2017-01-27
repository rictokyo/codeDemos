using System;
using System.ComponentModel;
using System.Net.Security;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Threading;

namespace SampleBundleApi.Wcf
{
    public abstract class WcfService<T, T1> : WcfServiceBase, IErrorHandler
    {
        readonly ManualResetEvent mre = new ManualResetEvent(false);

        public override void Start(string endpointUrl, string pipeName)
        {
            var bgw = new BackgroundWorker();
            bgw.DoWork += bgw_DoWork;

            var uriData = new AddressData(endpointUrl, pipeName);
            bgw.RunWorkerAsync(uriData);
        }

        public override void Close()
        {
            this.mre.Set();
        }

        protected void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            ServiceHost host = null;

            try
            {
                var adressData = (AddressData)e.Argument;

                if (string.IsNullOrEmpty(adressData.BaseAddress) || string.IsNullOrEmpty(adressData.PipeName)) return;

                var baseAddresses = new Uri(adressData.BaseAddress);

                using (host = new ServiceHost(typeof(T), baseAddresses))
                {
                    var stb = new ServiceThrottlingBehavior
                    {
                        MaxConcurrentSessions = 99999999,
                        MaxConcurrentCalls = 99999999,
                        MaxConcurrentInstances = 99999999
                    };

                    host.Description.Behaviors.Add(stb);

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
                        CloseTimeout = TimeSpan.FromDays(1)
                    };

                    host.AddServiceEndpoint(typeof(T1), netNamedPipeBinding, adressData.PipeName);

                    host.Open();

                    this.mre.WaitOne();

                    host.Close();
                }
            }
            catch (InvalidOperationException exc)
            {
                Console.WriteLine(exc.Message);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            finally
            {
                if (host != null)
                {
                    ((IDisposable)host).Dispose();
                }
            }
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            Console.WriteLine("ProvideFault called.");
        }

        public bool HandleError(Exception error)
        {
            Console.WriteLine("HandleError called.");

            return true;
        }
    }
}