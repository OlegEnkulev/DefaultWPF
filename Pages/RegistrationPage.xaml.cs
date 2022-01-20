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
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();

            RoleBox.ItemsSource = Core.DB.Roles.Where(r => r.Id != 3).Select(r => r.RoleName).ToList();
        }

        private void RegistrationBTN_Click(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text.Length < 4 || PasswordBox.Text.Length < 4 || NameBox.Text.Length < 2 || SurnameBox.Text.Length < 3 || RoleBox.SelectedItem == null)
            {
                MessageBox.Show("Проверте правильность данных!");
                return;
            }

            if (Core.DB.Users.Where(u => LoginBox.Text == u.Login).FirstOrDefault() != null)
            {
                MessageBox.Show("Такой логин уже существует!");

                return;
            }

            Users newUser = new Users { Name = NameBox.Text, Surname = SurnameBox.Text, Login = LoginBox.Text, Password = PasswordBox.Text, RoleId = Core.DB.Roles.Where(r => r.RoleName == RoleBox.SelectedItem).FirstOrDefault().Id, Verified = false};

            Core.DB.Users.Add(newUser);
            Core.DB.SaveChanges();

            MessageBox.Show("Вы успешно зарегистрировались!");
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.mainWindow.MainFrame.Navigate(new MainPage());
        }
    }
}
