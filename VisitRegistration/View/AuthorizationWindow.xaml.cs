using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using VisitRegistration.Database;

namespace VisitRegistration.View
{
    public partial class AuthorizationWindow : Window
    {
        private readonly TestEntities databaseConnection;

        public ObservableCollection<Leading> Leadings { get; set; }

        public AuthorizationWindow(TestEntities dbConn)
        {
            InitializeComponent();
            databaseConnection = dbConn;

            Leadings = new ObservableCollection<Leading>(databaseConnection.Leadings);

            DataContext = this;
        }

        private void OnCreate(object sender, RoutedEventArgs e)
        {
            Database.Section section = new Database.Section();

            CreateLeadingWindow leadingWindow = new CreateLeadingWindow(section, Leadings);

            var result = leadingWindow.ShowDialog();
            if (result == null || result.Value == false) return;

            //Leadings.Add(leading);
            databaseConnection.Sections.Add(section);
            databaseConnection.SaveChanges();
        }

        private void OnEdit(object sender, RoutedEventArgs e)
        {
            var leading = lvLeadings.SelectedItem as Leading;
            if (leading == null) return;

            EditLeadingWindow leadingWindow = new EditLeadingWindow(leading);
            leadingWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            leadingWindow.Owner = this;

            var result = leadingWindow.ShowDialog();
            if (result == null || result.Value == false) return;

            databaseConnection.SaveChanges();
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            var leading = lvLeadings.SelectedItem as Leading;
            if (leading == null) return;

            var result = MessageBox.Show(
                "Вы уверены, что ходите удалить пользователя " + leading.ToString(),
                "Подтверждение удаления",
                MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result != MessageBoxResult.OK) return;

            Leadings.Remove(leading);
            databaseConnection.Leadings.Remove(leading);
            databaseConnection.SaveChanges();
        }
    }
}
