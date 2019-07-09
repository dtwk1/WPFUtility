
//namespace WPFUtility
//{
//    using System;
//    using System.IO;
//    using System.Text;
//    using System.Windows;
//    using System.Windows.Controls;
//    using System.Windows.Documents;
//    using System.Windows.Input;

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
//            var text = ((parameter as FileObjectChangedEventArgs)?.FileObject as TextObject)?.Text;
//            if (text != null)
//            {
//                this.Editor.GetTextRange().Load(new MemoryStream(Encoding.ASCII.GetBytes(text)), DataFormats.Text);
//            }

//            var title = ((parameter as FileObjectChangedEventArgs)?.FileObject as TextObject)?.Title;
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
//}
