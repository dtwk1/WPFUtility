
namespace WPFUtility.Contracts
{
    using System.Windows.Input;

    public interface  IHasCommand
    {
        ICommand Command { get; set; }
    }
}
