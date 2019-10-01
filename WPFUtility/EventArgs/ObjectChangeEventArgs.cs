

namespace WPFUtility
{
    using System.Windows;

    public class ObjectChangeEventArgs : RoutedEventArgs
    {
        public object Object;

        public ObjectChangeEventArgs(RoutedEvent routedEvent, object obj) : base(routedEvent)
        {
            this.Object = obj;
        }
    }
}

//public class ObjectChangeEventArgs : RoutedEventArgs
//{
//    //public string Property;

//    public object Value;

//    public ObjectChangeEventArgs(RoutedEvent routedEvent, object value) : base(routedEvent)
//    {
//        //this.Property = property;
//        this.Value = value;
//    }
//}