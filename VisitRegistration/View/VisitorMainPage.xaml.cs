using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using VisitRegistration.Database;

namespace VisitRegistration.View
{

    public partial class VisitorMainPage : Page
    {
        private readonly TestEntities databaseConnection;
        private readonly Visitor currentUser;
        public ObservableCollection<Section> Sections { get; set; }

        public VisitorMainPage(TestEntities testEntities, Visitor user)
        {
            InitializeComponent();

            databaseConnection = testEntities;
            currentUser = user;

            Sections = new ObservableCollection<Section>(databaseConnection.Sections);

            DataContext = this;
        }

        private void OnSectionEnter(object sender, RoutedEventArgs e)
        {
            Section selectedSection = lvSections.SelectedItem as Section;
            if (selectedSection == null) return;

            if (selectedSection.Visitors.Contains(currentUser))
            {
                MessageBox.Show("Вы уже записанны на выбранное мероприятие");
                return;
            }

            selectedSection.Visitors.Add(currentUser);
            databaseConnection.SaveChanges();

            lvSections.SelectedItem = null;
        }

        private void OnSectionExit(object sender, RoutedEventArgs e)
        {
            Section selectedSection = lvSections.SelectedItem as Section;
            if (selectedSection == null) return;

            if (!selectedSection.Visitors.Contains(currentUser))
            {
                MessageBox.Show("Вы не записанны на выбранное мероприятие");
                return;
            }

            selectedSection.Visitors.Remove(currentUser);
            databaseConnection.SaveChanges();

            lvSections.SelectedItem = null;
        }

        private void OnSectionSelection(object sender, SelectionChangedEventArgs e)
        {
            Section selectedSection = lvSections.SelectedItem as Section;

            btnEnter.IsEnabled = false;
            btnExit.IsEnabled = false;

            if (selectedSection != null)
            {
                btnExit.IsEnabled = selectedSection.Visitors.Contains(currentUser);
                btnEnter.IsEnabled = !btnExit.IsEnabled;
            }
        }
    }
}
