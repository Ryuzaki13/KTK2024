using System.Windows;
using VisitRegistration.Database;
using VisitRegistration.View;

namespace VisitRegistration
{
    public partial class MainWindow : Window
    {
        private readonly TestEntities databaseConnection;

        public MainWindow()
        {
            InitializeComponent();

            databaseConnection = new TestEntities();
        }

        private void OnClickAuthorization(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow(databaseConnection);
            authorizationWindow.Show();
        }
    }
}
