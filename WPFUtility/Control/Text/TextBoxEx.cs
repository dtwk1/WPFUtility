


//namespace WPFUtility
//{
//    using System.Windows.Controls;

//    public interface IText
//    {
//        string Text { get; set; }
//    }


//    public class TextBoxEx : TextBox, IText
//    {
//        public TextBoxEx()
//        {
//            //Mask = "(000) 000-0000";
//            //ValueDataType = typeof(string);

//        }

//        //public string Text { get; set; }
//    }
//}


namespace WPFUtility
{
    using System;
    using System.Collections.Generic;
    using System.Reactive.Subjects;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Animation;

    public class TextBlockEx : Control
    {
        private TextBlock TextBlock;


        static TextBlockEx()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBlockEx), new FrameworkPropertyMetadata(typeof(TextBlockEx)));
        }

        public override void OnApplyTemplate()
        {
            this.TextBlock = this.GetTemplateChild("TextBlock1") as TextBlock;
            this.sb = this.TextBlock.Resources["Storyboard"] as Storyboard;

            foreach (var action in actions)
            {
                action.Invoke();
            }
        }

        private List<Action> actions = new List<Action>();

        private Storyboard sb;

        public TextBlockEx()
        {
            this.TextChanges
                .Subscribe(this.Change);
        }



        private void Change(string Text)
        {
            this.Dispatcher.Invoke(
                () =>
                    {
                    Action a = this.BeginStoryBoard;

                    if (this.TextBlock != null)
                    {
                        a.Invoke();
                        this.TextBlock.Text = Text;
                    }
                    else
                    {
                        this.actions.Add(a);
                        this.actions.Add(() => this.TextBlock.Text = Text);
                    }
                });
        }

        private void BeginStoryBoard()
        {
            sb.Begin();
        }

        #region Properties

        private ISubject<string> TextChanges = new Subject<string>();

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(TextBlockEx), new PropertyMetadata("", TextChanged));



        private static void TextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as TextBlockEx).TextChanges.OnNext((string)e.NewValue);

        }


        #endregion
    }
}
