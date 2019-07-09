using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicData;

namespace WPFUtility.Demo
{
    using System.Collections.ObjectModel;
    using DynamicData.Binding;

    public class PathViewModel
    {
        private ReadOnlyObservableCollection<string> collection;

        public IReadOnlyCollection<string> Collection => this.collection;

        public PathViewModel()
        {
            PathObservable.PathChanges.Subscribe(_ => 
                    { });
            PathObservable.PathChanges.ToObservableChangeSet(_ => _).Bind(out collection).Subscribe();
        }
    }
}
