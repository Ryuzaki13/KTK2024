using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VisitRegistration.Database;

namespace VisitRegistration.View
{

    public partial class EditSectionWindow : Window
    {
        public EditSectionWindow(Section section, ObservableCollection<Leading> leadings)
        {
            InitializeComponent();

            DataContext = section;

            cbLeadings.SetBinding(ComboBox.ItemsSourceProperty, new Binding()
            {
                Source = leadings
            });
        }

        private void OnSave(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
