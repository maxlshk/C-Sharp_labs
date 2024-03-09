using System.Windows;
using System.Windows.Controls;

namespace KMA.ProgrammingInCSharp.Lab2.Tools.Controls
{
    /// <summary>
    /// Interaction logic for MyTextBox.xaml
    /// </summary>
    public partial class MyTextBox : UserControl
    {
        public string Caption
        {
            get
            {
                return TbCaption.Text;
            }
            set
            {
                TbCaption.Text = value;
            }
        }

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register
        (
            "Text",
            typeof(string),
            typeof(MyTextBox),
            new PropertyMetadata(null)
        );

        public MyTextBox()
        {
            InitializeComponent();
        }
    }
}