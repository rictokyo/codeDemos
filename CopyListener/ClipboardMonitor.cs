﻿using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace CopyListener
{
    /// <summary>
    /// copied and slightly adjusted from www.radsoftware.com.au/articles/clipboardmonitor.aspx
    /// </summary>
    public class ClipboardMonitor : Window
    {
        private HwndSource source = null;
        private IntPtr nextClipboardViewer;
        private IntPtr handle
        {
            get
            {
                return new WindowInteropHelper(this).Handle;
            }
        }

        public ClipboardMonitor()
        {
            //We have to "show" the window in order to obtain hwnd to process WndProc messages in WPF
            this.Top = -10;
            this.Left = -10;
            this.Width = 0;
            this.Height = 0;
            this.WindowStyle = WindowStyle.None;
            this.ShowInTaskbar = false;
            this.ShowActivated = false;
            this.Show();
            this.Hide();
        }

        #region "Dependency properties"
        public static readonly DependencyProperty ClipboardContainsImageProperty =
            DependencyProperty.Register(
                "ClipboardContainsImage",
                typeof(bool),
                typeof(Window),
                new FrameworkPropertyMetadata(false));
        public static readonly DependencyProperty ClipboardContainsTextProperty =
            DependencyProperty.Register(
                "ClipboardContainsText",
                typeof(bool),
                typeof(Window),
                new FrameworkPropertyMetadata(false));
        public static readonly DependencyProperty ClipboardTextProperty =
            DependencyProperty.Register(
                "ClipboardText",
                typeof(string),
                typeof(Window),
                new FrameworkPropertyMetadata(string.Empty));
        public static readonly DependencyProperty ClipboardImageProperty =
            DependencyProperty.Register(
                "ClipboardImage",
                typeof(BitmapSource),
                typeof(Window),
                new FrameworkPropertyMetadata(null));

        public bool ClipboardContainsImage
        {
            get { return (bool)GetValue(ClipboardMonitor.ClipboardContainsImageProperty); }
            set { SetValue(ClipboardMonitor.ClipboardContainsImageProperty, value); }
        }
        public bool ClipboardContainsText
        {
            get { return (bool)GetValue(ClipboardMonitor.ClipboardContainsTextProperty); }
            set { SetValue(ClipboardMonitor.ClipboardContainsTextProperty, value); }
        }
        public string ClipboardText
        {
            get { return (string)GetValue(ClipboardMonitor.ClipboardTextProperty); }
            set { SetValue(ClipboardMonitor.ClipboardTextProperty, value); }
        }
        public BitmapSource ClipboardImage
        {
            get { return (BitmapSource)GetValue(ClipboardMonitor.ClipboardImageProperty); }
            set { SetValue(ClipboardMonitor.ClipboardImageProperty, value); }
        }

        #endregion

        #region "Routed Events"
        public static readonly RoutedEvent ClipboardDataEvent = EventManager.RegisterRoutedEvent("ClipboardData", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Window));
        public event RoutedEventHandler ClipboardData
        {
            add { AddHandler(ClipboardMonitor.ClipboardDataEvent, value); }
            remove { RemoveHandler(ClipboardMonitor.ClipboardDataEvent, value); }
        }
        protected virtual void OnRaiseClipboardData(ClipboardDataEventArgs e)
        {
            RaiseEvent(e);
        }
        #endregion

        #region "Win32 API"
        private const int WM_DRAWCLIPBOARD = 0x308;
        private const int WM_CHANGECBCHAIN = 0x030D;
        [DllImport("User32.dll")]
        private static extern int SetClipboardViewer(int hWndNewViewer);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);
        #endregion

        #region "overrides"
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            this.nextClipboardViewer = (IntPtr)SetClipboardViewer((int)this.handle);
            this.source = PresentationSource.FromVisual(this) as HwndSource;
            this.source.AddHook(WndProc);
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            ChangeClipboardChain(this.handle, this.nextClipboardViewer);
            if (null != this.source)
                this.source.RemoveHook(WndProc);
        }
        #endregion

        #region "Clipboard data"
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_DRAWCLIPBOARD:
                    clipboardData();
                    SendMessage(this.nextClipboardViewer, msg, wParam, lParam);
                    break;
                case WM_CHANGECBCHAIN:
                    if (wParam == this.nextClipboardViewer)
                        this.nextClipboardViewer = lParam;
                    else
                        SendMessage(this.nextClipboardViewer, msg, wParam, lParam);
                    break;
            }
            return IntPtr.Zero;
        }
        private void clipboardData()
        {
            System.Windows.IDataObject iData = System.Windows.Clipboard.GetDataObject();
            this.ClipboardContainsImage = iData.GetDataPresent(DataFormats.Bitmap);
            this.ClipboardContainsText = iData.GetDataPresent(DataFormats.Text);
            this.ClipboardImage = this.ClipboardContainsImage ? iData.GetData(DataFormats.Bitmap) as BitmapSource : null;
            this.ClipboardText = this.ClipboardContainsText ? iData.GetData(DataFormats.Text) as string : string.Empty;
            OnRaiseClipboardData(new ClipboardDataEventArgs(ClipboardMonitor.ClipboardDataEvent, iData));
        }
        #endregion
    }
}