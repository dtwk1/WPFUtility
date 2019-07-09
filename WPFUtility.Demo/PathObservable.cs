using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUtility.Demo
{
    using System.Windows;
    using WPFUtility;
    using System.Reactive.Linq;

    static class PathObservable
    {
        public static IObservable<string> PathChanges { get; }

        static PathObservable()
        {
            PathChanges = ((App)Application.Current)
                .OnAnyPropertyChanges()
                .Where(_ => _.Item1 == nameof(App.Path))
                .Select(_ => _.Item2.Path);
        }
    }
}
