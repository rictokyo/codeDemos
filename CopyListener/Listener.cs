using System;
using System.Threading;
using System.Windows;
using SampleBundleApi;
using System.Windows.Threading;

namespace CopyListener
{
    class Listener : Sample, IListener
    {
        private readonly ICommunicator communicator;
        private ClipboardMonitor clipboardMonitor;
        private bool startWhenReady;
        private readonly Dispatcher myDispatcher = Dispatcher.CurrentDispatcher;

        public Listener(string sampleName, ICommunicator communicator)
            : base(sampleName)
        {
            this.communicator = communicator;

            Initialise();
        }

        private void Initialise()
        {
            StartClipBoardWindowOnBackgroundThread();
        }

        private void StartClipBoardWindowOnBackgroundThread()
        {
            var clipboardWindowDisplayThread = new Thread(() =>
            {
                try
                {
                    myDispatcher.BeginInvoke((Action)(() => {

                        var clipboardWindow = new ClipboardWindow();
                        clipboardWindow.Show();
                        this.clipboardMonitor = clipboardWindow.FindResource("ClipWatch") as ClipboardMonitor;

                        if (this.startWhenReady)
                        {
                            StartListening();
                        }
                        
                        Dispatcher.Run();
                    }));
                    
                }
                catch (Exception exc)
                {
                    var message = string.Format("Failed to open clipboard window\n{0}\n{1}", exc.Message, exc.StackTrace);
                    this.communicator.Say(message);
                }
            });

            clipboardWindowDisplayThread.SetApartmentState(ApartmentState.STA);
            clipboardWindowDisplayThread.IsBackground = true;
            clipboardWindowDisplayThread.Start();
        }

        private void clipboardMonitor_ClipboardData(object sender, RoutedEventArgs e)
        {
            var monitor = sender as ClipboardMonitor;

            if (monitor != null && !string.IsNullOrEmpty(monitor.ClipboardText))
            {
                this.communicator.Say(monitor.ClipboardText);
            }
        }

        public void StartListening()
        {
            if (this.clipboardMonitor == null)
            {
                this.startWhenReady = true;

                return;
            }

            clipboardMonitor.ClipboardData -= clipboardMonitor_ClipboardData;
            clipboardMonitor.ClipboardData += clipboardMonitor_ClipboardData;

            var message = string.Format("Started {0}", this.SampleName);

            this.communicator.Say(message);
        }

        public void StopListening()
        {
            if (this.clipboardMonitor != null)
            {
                clipboardMonitor.ClipboardData -= clipboardMonitor_ClipboardData;
                myDispatcher.BeginInvoke((Action)(() => this.clipboardMonitor.Close()));
            }
        }

        public override void Dispose()
        {
            StopListening();
        }

        public override void Start()
        {
            StartListening();
        }
    }
}
