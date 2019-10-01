
namespace WPFUtility
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using System.Reactive.Threading.Tasks;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Threading;


    public class OpenCommand : ICommand
    {
        public ISubject<object> Changes = new Subject<object>();

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;
        //{
        //    return true;
        //}

        public OpenCommand(TextBox textBox, RichTextBoxEx richTextBox, IObservable<string> textChanges, IObservable<string> titleChanges)
        {
            var ddd = Changes.Where(_ => _.GetType() != typeof(DocumentObject))
                .Select(_ => new { properties = _.GetType().GetProperties(), obj = _ }).CombineLatest(
                    textChanges,
                    titleChanges,
                    async (a, b, c) => await Task.Run(
                                           () =>
                                               {
                                                   string text = (string)a.properties.SingleOrDefault(v => v.Name == b)?.GetValue(a.obj);
                                                   string title = (string)a.properties.SingleOrDefault(v => v.Name == c)?.GetValue(a.obj);
                                                   return Tuple.Create(text, title);
                                               }).ToObservable()).SelectMany(_ => _);
            //.ObserveOnDispatcher()
            //.Subscribe(
            //    async _ => await richTextBox.Dispatcher.InvokeAsync(async () =>
            //        {
            //            var xx = await _;
            //        richTextBox.SetTextRange(xx.text);/*.Load(new MemoryStream(Encoding.ASCII.GetBytes(xx.text)), DataFormats.Text);*/
            //        textBox.Text = xx.title;
            //    },DispatcherPriority.Background));

            var eee = Changes.Where(_ => _.GetType() == typeof(DocumentObject))
                 .Select(_ =>
                     {
                         DocumentObject documentObject = _ as DocumentObject;
                         return Tuple.Create((string)documentObject.Text, documentObject.Title);
                     });

            ddd.Merge<Tuple<string, string>>(eee)

            .ObserveOnDispatcher()
            .Subscribe(
                 async _ => 
                     await richTextBox.Dispatcher.InvokeAsync(async () =>
                    {
                        richTextBox.SetTextRange((string)_.Item1);/*GetTextRange().Load(new MemoryStream(Encoding.ASCII.GetBytes((string)xx.Text)), DataFormats.Text);*/
                        textBox.Text = _.Item2;
                    }, DispatcherPriority.Background));

            //var text = ((parameter as FileObjectChangedEventArgs)?.Obj as TextObject)?.Text;
            //if (text != null)
            //{
            //    this.Editor.GetTextRange().Load(new MemoryStream(Encoding.ASCII.GetBytes(text)), DataFormats.Text);
            //}

            //var title = ((parameter as FileObjectChangedEventArgs)?.Obj as TextObject)?.Title;
            //if (title != null)
            //{
            //    (this.TitleEditor as TextBox).Text = title;
            //}
        }

        public void Execute(object parameter)
        {
            this.Changes.OnNext(parameter);
        }
    }




    //    //using ButtonsControl;

    //    //using Contracts;

    //    //using OpenSaveTextWpf;
    //    //using OpenSaveTextWpf.Control;

    //    public class OpenCommand : ICommand
    //    {
    //        private RichTextBox richTextBox;


    //        //public object Editor
    //        //{
    //        //    get { return (object)GetValue(EditorProperty); }
    //        //    set { SetValue(EditorProperty, value); }
    //        //}

    //        //public static readonly DependencyProperty EditorProperty = DependencyProperty.Register("Editor", typeof(object), typeof(OpenCommand), new PropertyMetadata(null));

    //        //public object TitleEditor
    //        //{
    //        //    get { return (object)GetValue(TitleEditorProperty); }
    //        //    set { SetValue(TitleEditorProperty, value); }
    //        //}

    //        //public static readonly DependencyProperty TitleEditorProperty = DependencyProperty.Register("TitleEditor", typeof(object), typeof(OpenCommand), new PropertyMetadata(null));


    //        //public OpenCommand(RichTextBox richTextBox)
    //        //{
    //        //    this.richTextBox = richTextBox;
    //        //}

    //        public OpenCommand(ITextRange editor, IText titleEditor)
    //        {
    //            this.Editor = editor;
    //            this.TitleEditor = titleEditor;
    //        }

    //        public ITextRange Editor { get; }
    //        public IText TitleEditor { get; }

    //        public event EventHandler CanExecuteChanged;

    //        public bool CanExecute(object parameter)
    //        {
    //            return true;
    //        }

    //        public void Execute(object parameter)
    //        {
    //            var text = ((parameter as FileObjectChangedEventArgs)?.Obj as TextObject)?.Text;
    //            if (text != null)
    //            {
    //                this.Editor.GetTextRange().Load(new MemoryStream(Encoding.ASCII.GetBytes(text)), DataFormats.Text);
    //            }

    //            var title = ((parameter as FileObjectChangedEventArgs)?.Obj as TextObject)?.Title;
    //            if (title != null)
    //            {
    //                (this.TitleEditor as TextBox).Text = title;
    //            }
    //        }


    //        //// Create a custom routed event by first registering a RoutedEventID
    //        //// This event uses the bubbling routing strategy
    //        //public static readonly RoutedEvent FileObjectChangedEvent = EventManager.RegisterRoutedEvent("FileObjectChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(OpenSaveControl));

    //        //// Provide CLR accessors for the event
    //        //public event RoutedEventHandler FileObjectChanged
    //        //{
    //        //    add => this.AddHandler(FileObjectChangedEvent, Value);
    //        //    remove => this.RemoveHandler(FileObjectChangedEvent, Value);
    //        //}

    //        //// This method raises the FileObjectChanged event
    //        //void RaiseFileObjectChangedEvent()
    //        //{

    //        //}
    //    }

}
