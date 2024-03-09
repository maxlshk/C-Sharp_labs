using System.Windows;
using KMA.ProgrammingInCSharp.Lab1;
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
            this.Closing += MainWindow_Closing;

        }
        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DataContext is MainViewModel mainViewModel)
            {
                mainViewModel.Cleanup();
            }
        }
    }
}


