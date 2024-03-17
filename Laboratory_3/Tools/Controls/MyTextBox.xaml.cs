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
        private bool _isPlaceholderActive;

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
            get
            {
                return PlaceholderTextBlock.Text;
            }
            set
            {
                PlaceholderTextBlock.Text = value;
            }
        }
        #endregion

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register
        (
            "Text",
            typeof(string),
            typeof(MyTextBox),
            new PropertyMetadata(null)
        );

        private void MyTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            SetPlaceholderVisibility();
        }

        private void TbValue_GotFocus(object sender, RoutedEventArgs e)
        {
            SetPlaceholderVisibility();
        }

        private void TbValue_LostFocus(object sender, RoutedEventArgs e)
        {
            SetPlaceholderVisibility();
        }

        private void SetPlaceholderVisibility()
        {
            if (string.IsNullOrEmpty(TbValue.Text) && !TbValue.IsFocused)
            {
                PlaceholderTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                PlaceholderTextBlock.Visibility = Visibility.Collapsed;
            }
        }

        public MyTextBox()
        {
            InitializeComponent();
            this.Loaded += MyTextBox_Loaded;
            TbValue.GotFocus += TbValue_GotFocus;
            TbValue.LostFocus += TbValue_LostFocus;
            this.Unloaded += Cleanup;
        }
        public void Cleanup(object sender, RoutedEventArgs e)
        {
            TbValue.GotFocus -= TbValue_GotFocus;
            TbValue.LostFocus -= TbValue_LostFocus;
            TbValue.Loaded -= MyTextBox_Loaded;
        }

    }
}