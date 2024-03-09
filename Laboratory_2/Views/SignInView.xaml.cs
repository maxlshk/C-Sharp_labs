using System.Windows.Controls;
using KMA.ProgrammingInCSharp.Lab2.ViewModels;

namespace KMA.ProgrammingInCSharp.Lab2.Views
{
    /// <summary>
    /// Interaction logic for SignInControl.xaml
    /// </summary>
    public partial class SignInView : UserControl
    {
        //private readonly SignInViewModel _viewModel;
        public SignInView()
        {
            InitializeComponent();
            //DataContext = _viewModel = new SignInViewModel();
        }
    }
}
