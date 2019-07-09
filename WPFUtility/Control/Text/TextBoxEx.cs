


namespace WPFUtility
{
    using System.Windows.Controls;

    public interface IText
    {
        string Text { get; set; }
    }


    public class TextBoxEx : TextBox, IText
    {
        public TextBoxEx()
        {
            //Mask = "(000) 000-0000";
            //ValueDataType = typeof(string);

        }

        //public string Text { get; set; }
    }
}
