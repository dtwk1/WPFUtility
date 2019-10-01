

namespace WPFUtility
{
    using System;
    using System.Reactive.Subjects;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Input;

    using WPFUtility.Contracts;

    //public interface IOpenCommand
    //{
    //    ICommand OpenCommand { get; set; }
    //}

    //public interface IObjectChange
    //{
    //    event RoutedObjectChangeEventHandler PropertyChange;
    //}




    //public delegate void RoutedObjectChangeEventHandler(object sender, ObjectChangeEventArgs e);



    //public interface IObjectChangeOpen : IObjectChange/*, IOpenCommand*/
    //{


    //}



    public class RichTextEditor : System.Windows.Controls.Control, IObjectChangeEventCommand
    {

        private RichTextBoxEx RichTextBox;
        private TextBox TitleBox;

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(RichTextEditor), new PropertyMetadata(null));



        public override void OnApplyTemplate()
        {
            this.RichTextBox = this.GetTemplateChild(nameof(RichTextBoxEx)) as RichTextBoxEx;
            this.TitleBox = this.GetTemplateChild("TitleBox") as TextBox;
            //this.OpenCommand = new OpenCommand(this.RichTextBox, this.TitleBox);
            this.RichTextBox.TextChanged += RichTextBox_TextChanged;
            this.TitleBox.TextChanged += TitleBox_TextChanged;
            this.Command = new OpenCommand(TitleBox, this.RichTextBox, textChanges, titleChanges);
        }

        string title=string.Empty;
        private void TitleBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            title = this.TitleBox.Text;
            this.RaiseObjectChangeEvent(new DocumentObject{Title = title, Text =text });
        }

        private string text = string.Empty;
        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var doc = this.RichTextBox.Document;
            TextRange range = new TextRange(doc.ContentStart, doc.ContentEnd);
            text = range.Text;
            this.RaiseObjectChangeEvent(new DocumentObject { Title = title, Text = text });
        }

        static RichTextEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RichTextEditor), new FrameworkPropertyMetadata(typeof(RichTextEditor)));
        }

        public RichTextEditor()
        {
            //this.MakeUpdateCommand();
  
        }



        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(RichTextEditor), new PropertyMetadata("Title", TitleChanged));

        ISubject<string> titleChanges=new Subject<string>();
        private static void TitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as RichTextEditor).titleChanges.OnNext((string)e.NewValue);
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(RichTextEditor), new PropertyMetadata("Text",TextChanged));

        ISubject<string> textChanges = new Subject<string>();

        private static void TextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as RichTextEditor). textChanges.OnNext((string)e.NewValue);
        }

        //protected abstract void MakeUpdateCommand();

        //public event Action<object> Event;



        // Create a custom routed event by first registering a RoutedEventID
        // This event uses the bubbling routing strategy
        public static readonly RoutedEvent ObjectChangeEvent = EventManager.RegisterRoutedEvent("ObjectChange", RoutingStrategy.Bubble, typeof(RoutedObjectChangeEventHandler), typeof(RichTextEditor));


        // Provide CLR accessors for the event
        public event RoutedObjectChangeEventHandler ObjectChange
        {
            add => this.AddHandler(ObjectChangeEvent, value);
            remove => this.RemoveHandler(ObjectChangeEvent, value);
        }

        // This method raises the TextObjectChange event
        void RaiseObjectChangeEvent(object value)
        {
            if (value.ToString() != "\r\n")
            {
                RoutedEventArgs newEventArgs = new ObjectChangeEventArgs(
                    RichTextEditor.ObjectChangeEvent,
                    value);
                this.RaiseEvent(newEventArgs);
            }
        }
    }
}

