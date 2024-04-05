using System.Windows;
using System.Windows.Controls;

namespace KMA.ProgrammingInCSharp.Lab4.Tools.Controls;

public partial class SortControl : UserControl
{
    public SortControl()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty SelectedValueProperty = DependencyProperty.Register(
        "SelectedValue", typeof(string), typeof(SortControl), new PropertyMetadata(default(string)));

    public string SelectedValue
    {
        get { return (string)GetValue(SelectedValueProperty); }
        set { SetValue(SelectedValueProperty, value); }
    }
}
