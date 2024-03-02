using System.Windows;
using VisitRegistration.Database;
using VisitRegistration.View;

namespace VisitRegistration
{
    public partial class MainWindow : Window
    {
        public static MainWindow Instance;

        private readonly TestEntities databaseConnection;

        public MainWindow()
        {
            InitializeComponent();

            Instance = this;

            databaseConnection = new TestEntities();

            AppFrame.Navigate(new AuthorizationPage(databaseConnection));
        }
    }
}
