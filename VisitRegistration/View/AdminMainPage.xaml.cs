using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using VisitRegistration.Database;

namespace VisitRegistration.View
{

    public partial class AdminMainPage : Page
    {
        private readonly TestEntities databaseConnection;

        public ObservableCollection<Leading> Leadings { get; set; }
        public ObservableCollection<Section> Sections { get; set; }

        public AdminMainPage(TestEntities testEntities)
        {
            InitializeComponent();

            databaseConnection = testEntities;

            Leadings = new ObservableCollection<Leading>(databaseConnection.Leadings);
            Sections = new ObservableCollection<Section>(databaseConnection.Sections);

            DataContext = this;
        }

        private void OnLeadingCreate(object sender, RoutedEventArgs e)
        {
            Leading leading = new Leading();

            CreateLeadingWindow leadingWindow = new CreateLeadingWindow(leading);

            var result = leadingWindow.ShowDialog();
            if (result == null || result.Value == false) return;

            Leadings.Add(leading);
            databaseConnection.Leadings.Add(leading);
            databaseConnection.SaveChanges();
        }

        private void OnLeadingEdit(object sender, RoutedEventArgs e)
        {
            var leading = lvLeadings.SelectedItem as Leading;
            if (leading == null) return;

            EditLeadingWindow leadingWindow = new EditLeadingWindow(leading);
            leadingWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            leadingWindow.Owner = MainWindow.Instance;

            var result = leadingWindow.ShowDialog();
            if (result == null || result.Value == false) return;

            databaseConnection.SaveChanges();
        }

        private void OnLeadingDelete(object sender, RoutedEventArgs e)
        {
            var leading = lvLeadings.SelectedItem as Leading;
            if (leading == null) return;

            if (leading.Sections.Count > 0)
            {
                MessageBox.Show("Нельзя удалить ведущего, которому уже назначены мероприятия");
                return;
            }

            var result = MessageBox.Show(
                "Вы уверены, что ходите удалить пользователя " + leading.ToString(),
                "Подтверждение удаления",
                MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result != MessageBoxResult.OK) return;

            Leadings.Remove(leading);
            databaseConnection.Leadings.Remove(leading);
            databaseConnection.SaveChanges();
        }

        private void OnSectionCreate(object sender, RoutedEventArgs e)
        {
            Section section = new Section()
            {
                StartTime = DateTime.Now
            };

            CreateSectionWindow leadingWindow = new CreateSectionWindow(section, Leadings);

            var result = leadingWindow.ShowDialog();
            if (result == null || result.Value == false) return;

            Sections.Add(section);
            databaseConnection.Sections.Add(section);
            databaseConnection.SaveChanges();
        }

        private void OnSectionEdit(object sender, RoutedEventArgs e)
        {
            var section = lvSections.SelectedItem as Section;
            if (section == null) return;

            EditSectionWindow sectionWindow = new EditSectionWindow(section, Leadings);
            sectionWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            sectionWindow.Owner = MainWindow.Instance;

            var result = sectionWindow.ShowDialog();
            if (result == null || result.Value == false) return;

            databaseConnection.SaveChanges();
        }

        private void OnSectionDelete(object sender, RoutedEventArgs e)
        {
            var section = lvSections.SelectedItem as Section;
            if (section == null) return;

            section.Visitors.Clear();

            var result = MessageBox.Show(
                "Вы уверены, что ходите удалить мероприятие " + section.Name,
                "Подтверждение удаления",
                MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result != MessageBoxResult.OK) return;

            Sections.Remove(section);
            databaseConnection.Sections.Remove(section);
            databaseConnection.SaveChanges();
        }
    }
}
