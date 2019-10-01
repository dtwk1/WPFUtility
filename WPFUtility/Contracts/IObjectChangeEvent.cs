using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUtility.Contracts
{
    using System.Windows;


    public interface IObjectChangeEvent
    {
        event RoutedObjectChangeEventHandler ObjectChange;
    }
   
}
