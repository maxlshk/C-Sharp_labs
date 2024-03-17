using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace KMA.ProgrammingInCSharp.Lab3.Tools.Controls
{
    /// <summary>
    /// Interaction logic for MyTextBox.xaml
    /// </summary>
    public partial class MyTextBox : UserControl
    {
        #region Properties
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

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }
        #endregion

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register
        (
            "Text",
            typeof(string),
            typeof(MyTextBox),
            new PropertyMetadata(null)
        );

        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register
        (
            "Placeholder",
            typeof(string),
            typeof(MyTextBox),
            new PropertyMetadata("")
        );

        private void MyTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            SetPlaceholder();
        }

        private void TbValue_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TbValue.Text == Placeholder)
            {
                TbValue.Text = "";
                TbValue.Foreground = Brushes.Black; // Reset to default text color
            }
        }

        private void TbValue_LostFocus(object sender, RoutedEventArgs e)
        {
            SetPlaceholder();
        }

        private void SetPlaceholder()
        {
            if (string.IsNullOrEmpty(TbValue.Text))
            {
                TbValue.Text = Placeholder;
                TbValue.Foreground = Brushes.Gray; // Placeholder text color
            }
        }

        public MyTextBox()
        {
            InitializeComponent();
            this.Loaded += MyTextBox_Loaded;
            TbValue.GotFocus += TbValue_GotFocus;
            TbValue.LostFocus += TbValue_LostFocus;
        }
    }
}