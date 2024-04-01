using System.Windows;
using KMA.ProgrammingInCSharp.Lab4.ViewModels;

namespace KMA.ProgrammingInCSharp.Lab4
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


