using Grand_Converter.ViewModels;
using System.Windows;

namespace Grand_Converter
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}
