using System;
using System.Collections.Generic;
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

        public AuthorizationWindow(TestEntities dbConn)
        {
            InitializeComponent();
            databaseConnection = dbConn;
        }

        private void OnSingIn(object sender, RoutedEventArgs e)
        {
            // 1 Получить текст из TextBox
            var phone = tbPhone.Text.Trim();

            // 2 Найти объект в базе с указанным текстом в свойстве Phone
            Leading leading = databaseConnection.Leadings
                .Where(l => l.Phone == phone)
                .FirstOrDefault();

            if (leading == null) {
                MessageBox.Show("Такого телефона нет в базы");
                return;
            }

            MessageBox.Show(string.Format("Здравствуйте, {0} {1}!", leading.FirstName, leading.Patronymic));

            // ...
        }
    }
}
