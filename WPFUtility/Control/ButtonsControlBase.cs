using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Windows;
using System.Windows.Controls;


namespace WPFUtility
{
    using System.Reactive.Linq;

    public abstract class ButtonsBase : Control
    {
        //private Button[] buttons;
        private ISubject<object> serviceSubject = new Subject<object>();
        private ISubject<Dictionary<string,Button>> buttonsSubject = new Subject<Dictionary<string, Button>>();

        static ButtonsBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ButtonsBase), new FrameworkPropertyMetadata(typeof(ButtonsBase)));
        }

        public ButtonsBase()
        {
            this.buttonsSubject.WithLatestFrom(this.serviceSubject, (a,b)=>new {b=a,a=b})
                .Subscribe(_ => this.PropertyChange(_.a, _.b));

        }

        public override void OnApplyTemplate()
        {
            this.buttonsSubject.OnNext( GetControlsOfType<Button>(this.GetTemplateChild("ItemsControl")).ToDictionary(_=>_.Name,_=>_));

        }

        protected abstract void PropertyChange(object service, Dictionary<string, Button> dictionary);
    

        public object Service
        {
            get { return (object)GetValue(ServiceProperty); }
            set { SetValue(ServiceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Service.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ServiceProperty = DependencyProperty.Register(
            "Service",
            typeof(object),
            typeof(ButtonsBase),
            new PropertyMetadata(
                null,
                new Helper<ButtonsBase>(_ => _.serviceSubject).Changed));


        public static IEnumerable<T> GetControlsOfType<T>(DependencyObject root)
            where T : Control
        {
            var t = root as T;
            if (t != null)
                yield return t;

            foreach (Control c in LogicalTreeHelper.GetChildren(root))
                foreach (var i in GetControlsOfType<T>(c))
                    yield return i;
        }

    }





    public class Helper<R> where R : DependencyObject
    {
        private Func<R, ISubject<object>> subject;


        public void Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            this.subject((R)d).OnNext(e.NewValue);
        }

        public Helper(Func<R, ISubject<object>> subject)
        {
            this.subject = subject;
        }
    }

    public class Helper<T,R> where R : DependencyObject
    {
        private Func<R, ISubject<T>> subject;


        public void Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            this.subject((R)d).OnNext((T)e.NewValue);
        }

        public Helper(Func<R, ISubject<T>> subject)
        {
            this.subject = subject;
        }


    }
}

