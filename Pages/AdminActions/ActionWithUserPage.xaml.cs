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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DefaultWPF.Resources;

namespace DefaultWPF.Pages
{
    public partial class ActionWithUserPage : Page
    {
        Users currentUser;
        bool isCreated = true;
        public ActionWithUserPage(int сhangeableUserId) //n > 0 - Изменение, n < 0 - Создание
        {
            InitializeComponent();

            RoleBox.ItemsSource = Core.DB.Roles.Select(r => r.RoleName).ToList();

            if (сhangeableUserId < 0)
            {
                isCreated = false;
                VerifiedBox.IsChecked = true;
                return;
            }

            currentUser = Core.DB.Users.Where(s => s.Id == сhangeableUserId).FirstOrDefault();

            if (currentUser == null)
            {
                MessageBox.Show("Акак?");
                return;
            }

            NameBox.Text = currentUser.Name;
            SurnameBox.Text = currentUser.Surname;
            LoginBox.Text = currentUser.Login;
            PasswordBox.Text = currentUser.Password;
            RoleBox.SelectedIndex = currentUser.RoleId;
            VerifiedBox.IsChecked = currentUser.Verified;
        }

        private void AcceptBTN_Click(object sender, RoutedEventArgs e)
        {
            if (isCreated)
            {
                Core.DB.Users.Remove(Core.DB.Users.Where(s => s.Id == currentUser.Id).FirstOrDefault());
                Core.DB.SaveChanges();
            }

            currentUser = new Users();
            currentUser.Name = NameBox.Text;
            currentUser.Surname = SurnameBox.Text;
            currentUser.Login = LoginBox.Text;
            currentUser.Password = PasswordBox.Text;
            currentUser.RoleId = RoleBox.SelectedIndex;
            currentUser.Verified = Convert.ToBoolean(VerifiedBox.IsChecked);

            Core.DB.Users.Add(currentUser);
            Core.DB.SaveChanges();

            Core.mainWindow.MainFrame.Navigate(new ControlUsersPage());
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.mainWindow.MainFrame.Navigate(new ControlUsersPage());
        }
    }
}
