using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KMA.ProgrammingInCSharp.Lab4.Tools.Controls
{
    public partial class FilterControl : UserControl
    {
        public static readonly DependencyProperty SelectedFilterPropertyProperty = 
            DependencyProperty.Register("SelectedFilterProperty", typeof(string), typeof(FilterControl));

        public string SelectedFilterProperty
        {
            get => (string)GetValue(SelectedFilterPropertyProperty);
            set => SetValue(SelectedFilterPropertyProperty, value);
        }

        public static readonly DependencyProperty FilterValueProperty = 
            DependencyProperty.Register("FilterValue", typeof(string), typeof(FilterControl), new PropertyMetadata(string.Empty));

        public string FilterValue
        {
            get => (string)GetValue(FilterValueProperty);
            set => SetValue(FilterValueProperty, value);
        }

        public static readonly DependencyProperty FilterCommandProperty = 
            DependencyProperty.Register("FilterCommand", typeof(ICommand), typeof(FilterControl));

        public ICommand FilterCommand
        {
            get => (ICommand)GetValue(FilterCommandProperty);
            set => SetValue(FilterCommandProperty, value);
        }

        public FilterControl()
        {
            InitializeComponent();
            FilterButton.Click += (s, e) => 
            {
                if(FilterCommand != null && FilterCommand.CanExecute(null))
                {
                    FilterCommand.Execute(null);
                }
            };
        }
    }
}