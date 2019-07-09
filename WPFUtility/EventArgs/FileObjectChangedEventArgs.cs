

namespace WPFUtility
{
    using System.Windows;

    public class FileObjectChangedEventArgs : RoutedEventArgs
    {
        public object FileObject;

        public FileObjectChangedEventArgs(RoutedEvent routedEvent, object fileObject) : base(routedEvent)
        {
            this.FileObject = fileObject;
        }
    }
}

