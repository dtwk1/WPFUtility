using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFUtility.Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string lorumIpsumText =
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed eros nunc, auctor a erat fringilla, rhoncus pretium dolor. Sed risus sem, rhoncus vel quam nec, viverra ultrices quam. Duis dapibus luctus egestas. Fusce lacinia ac metus pellentesque maximus. Maecenas sollicitudin lacus non lacus eleifend dictum. Mauris pulvinar porta turpis a accumsan. In dapibus congue turpis, vitae volutpat tellus imperdiet vitae. Aliquam erat volutpat. Quisque augue purus, lacinia non ipsum quis, gravida tempor lorem. Maecenas ullamcorper ultrices neque. In volutpat, ante posuere scelerisque cursus, massa nulla lobortis metus, in molestie tortor eros at magna.";
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            await this.Dispatcher.InvokeAsync(
                () =>
                    {
                        this.RichTextEditor.Command.Execute(new DocumentObject{Text= lorumIpsumText, Title = "Lorem Ipsum"});
                    });
        }
    }
}
