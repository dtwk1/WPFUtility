
namespace WPFUtility
{
    using System.Windows.Controls;
    using System.Windows.Documents;

    public interface ITextRange
    {
        TextRange GetTextRange();

    }
    public class RichTextBoxEx : RichTextBox, ITextRange
    {
        public TextRange GetTextRange() => new TextRange((this).Document.ContentStart, (this).Document.ContentEnd);

    }
}
