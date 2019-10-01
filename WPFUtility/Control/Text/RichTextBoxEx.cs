
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
        public  void SetTextRange(string text)
        {
            this.Document.Blocks.Clear();
            this.Document.Blocks.Add(new Paragraph(new Run(text)));
        }

        public TextRange GetTextRange()
        {
            return new TextRange(this.Document.ContentStart,this.Document.ContentEnd);
        }
    }
}
