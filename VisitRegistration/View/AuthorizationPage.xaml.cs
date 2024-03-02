using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace VisitRegistration.View
{
    public partial class AuthorizationPage : Page
    {
        private readonly Database.TestEntities database;

        public AuthorizationPage(Database.TestEntities testEntities)
        {
            InitializeComponent();

            database = testEntities;
        }

        private void OnClickAuthorization(object sender, RoutedEventArgs e)
        {
            // 1. Считать введенные данные (телефон)
            string phone = tbPhone.Text.Trim();

            // 2. Проверить телефон на корректность.
            if (!Regex.IsMatch(phone, "^[0-9]{10}$"))
            {
                MessageBox.Show("Некорректный номер телефона");
                return;
            }

            // 3. Выяснить, кем является пользователь.

            // 3.1. Посмотреть таблицу Visitor
            var visitor = database.Visitors.Where(v => v.Phone == phone).FirstOrDefault();
            if (visitor != null)
            {
                // 4. Выполнить навигацию на страницу Visitor
                NavigationService.Navigate(new VisitorMainPage(database, visitor));
                return;
            }

            // 3.1. Постртреть таблицу Leading
            var leading = database.Leadings.Where(v => v.Phone == phone).FirstOrDefault();
            if (leading != null)
            {
                // 4. Выполнить навигацию на страницу Leading
                NavigationService.Navigate(new AdminMainPage(database));
                return;
            }
        }

        private bool validatePhone(string phone)
        {
            if (phone.Length != 10) return false;

            foreach (char c in phone)
            {
                if (c < 48 || c > 57) return false;
            }

            return true;
        }
    } 
}
