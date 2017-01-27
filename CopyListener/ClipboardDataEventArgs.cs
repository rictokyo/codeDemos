using System.Windows;

namespace CopyListener
{
    public class ClipboardDataEventArgs : RoutedEventArgs
    {
        public IDataObject Data { get; set; }
        public ClipboardDataEventArgs(RoutedEvent routedEvent, IDataObject data)
            : base(routedEvent)
        {
            this.Data = data;
        }
    }
}