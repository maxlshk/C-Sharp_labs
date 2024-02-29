using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
