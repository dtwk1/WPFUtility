

namespace WPFUtility
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Input;

    //public interface IOpenCommand
    //{
    //    ICommand OpenCommand { get; set; }
    //}

    public interface IObjectChange
    {
        event RoutedObjectChangeEventHandler ObjectChange;
    }

    public class ObjectChangeEventArgs : RoutedEventArgs
    {
        public string Property;

        public string Value;

        public ObjectChangeEventArgs(RoutedEvent routedEvent, string property, string value) : base(routedEvent)
        {
            this.Property = property;
            this.Value = value;
        }
    }


    public delegate void RoutedObjectChangeEventHandler(object sender, ObjectChangeEventArgs e);



    public interface IObjectChangeOpen : IObjectChange/*, IOpenCommand*/
    {


    }



    public class RichTextEditor : System.Windows.Controls.Control, IObjectChangeOpen
    {

        private RichTextBoxEx RichTextBox;
        private TextBoxEx TitleBox;

        //public ICommand OpenCommand
        //{
        //    get { return (ICommand)GetValue(OpenCommandProperty); }
        //    set { SetValue(OpenCommandProperty, value); }
        //}

        //public static readonly DependencyProperty OpenCommandProperty = DependencyProperty.Register("OpenCommand", typeof(ICommand), typeof(RichTextEditor), new PropertyMetadata(null));



        public override void OnApplyTemplate()
        {
            this.RichTextBox = this.GetTemplateChild(nameof(RichTextBoxEx)) as RichTextBoxEx;
            this.TitleBox = this.GetTemplateChild("TitleBox") as TextBoxEx;
            //this.OpenCommand = new OpenCommand(this.RichTextBox, this.TitleBox);
            this.RichTextBox.TextChanged += RichTextBox_TextChanged;
            this.TitleBox.TextChanged += TitleBox_TextChanged;
        }

        private void TitleBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = this.TitleBox.Text;
            this.RaiseTextObjectChangeEvent("Title", text);
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var doc = this.RichTextBox.Document;
            TextRange range = new TextRange(doc.ContentStart, doc.ContentEnd);
            string text = range.Text;
            this.RaiseTextObjectChangeEvent("Text", text);
        }

        static RichTextEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RichTextEditor), new FrameworkPropertyMetadata(typeof(RichTextEditor)));
        }

        public RichTextEditor()
        {
            //this.MakeUpdateCommand();

        }
        //protected abstract void MakeUpdateCommand();



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
        void RaiseTextObjectChangeEvent(string type, string value)
        {
            if (value != "\r\n")
            {
                RoutedEventArgs newEventArgs = new ObjectChangeEventArgs(
                    RichTextEditor.ObjectChangeEvent,
                    type,
                    value);
                this.RaiseEvent(newEventArgs);
            }
        }
    }
}

