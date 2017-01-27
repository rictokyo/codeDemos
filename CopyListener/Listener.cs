﻿using System;
using System.Threading;
using System.Windows;
using SampleBundleApi;

namespace CopyListener
{
    class Listener : Sample, IListener, IDisposable
    {
        private readonly ICommunicator communicator;
        private ClipboardMonitor clipboardMonitor;
        private bool startWhenReady;

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
                    var clipboardWindow = new ClipboardWindow();
                    clipboardWindow.Show();
                    this.clipboardMonitor = clipboardWindow.FindResource("ClipWatch") as ClipboardMonitor;

                    if (this.startWhenReady)
                    {
                        StartListening();
                    }

                    System.Windows.Threading.Dispatcher.Run();
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
                this.clipboardMonitor.Close();
            }
        }

        public void Dispose()
        {
            StopListening();
        }

        public override void Start()
        {
            StartListening();
        }
    }
}
