using System.Windows.Controls;

namespace KMA.ProgrammingInCSharp.Lab4.Tools.Controls
{
    /// <summary>
    /// Interaction logic for BirthdayPicker.xaml
    /// </summary>
    public partial class BirthdayPicker : UserControl
    {

        public string Caption
        {
            get
            {
                return DpCaption.Text;
            }
            set
            {
                DpCaption.Text = value;
            }
        }

        public BirthdayPicker()
        {
            InitializeComponent();
        }
    }
}
