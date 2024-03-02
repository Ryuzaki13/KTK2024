using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using VisitRegistration.Database;

namespace VisitRegistration.View
{
    public partial class CreateSectionWindow : Window
    {
        public CreateSectionWindow(Section section, ObservableCollection<Leading> leadings)
        {
            InitializeComponent();

            DataContext = section;
            cbLeadings.SetBinding(ComboBox.ItemsSourceProperty, new Binding()
            {
                Source = leadings
            });
        }

        private void OnCreate(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
