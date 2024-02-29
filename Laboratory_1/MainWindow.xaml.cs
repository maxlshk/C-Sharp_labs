using System.Windows;
using KMA.ProgrammingInCSharp.Lab1.ViewModels;

namespace KMA.ProgrammingInCSharp.Lab1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}
