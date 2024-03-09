using System.Windows;
using System.Windows.Controls;

namespace KMA.ProgrammingInCSharp.Lab1.Tools.Controls
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

        public DateTime Date
        {
            get
            {
                return (DateTime)GetValue(DateProperty);
            }
            set
            {
                SetValue(DateProperty, value);
            }
        }

        public static readonly DependencyProperty DateProperty = DependencyProperty.Register
        (
            "Date",
            typeof(DateTime),
            typeof(BirthdayPicker),
            new PropertyMetadata(null)
        );

        public BirthdayPicker()
        {
            InitializeComponent();
        }
    }
}
