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
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ExitBTN_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void RegistrationBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.mainWindow.MainFrame.Navigate(new RegistrationPage());
        }

        private void EntranceBTN_Click(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text.Length < 4 || PasswordBox.Password.Length < 4) 
            {

                return;
            }

            if(Core.DB.Users.Where(u => u.Login == LoginBox.Text).FirstOrDefault() == null)
            {


                return;
            }

            Users user = Core.DB.Users.Where(u => u.Login == LoginBox.Text).FirstOrDefault();

            if(user.Password != PasswordBox.Password)
            {


                return;
            }

            if (user.Verified == false)
            {
                MessageBox.Show("ТЫ ДАЖЕ НЕ ГРАЖДАНИН!");
                return;
            }

            Core.currentUser = user;

            switch (user.RoleId)
            {
                case 0:
                    Core.mainWindow.MainFrame.Navigate(new StudentPage());
                    break;
                case 1:
                    Core.mainWindow.MainFrame.Navigate(new EmployerPage());
                    break;
                case 2:
                    Core.mainWindow.MainFrame.Navigate(new EdOrgPage());
                    break;
                case 3:
                    Core.mainWindow.MainFrame.Navigate(new AdminPage());
                    break;
            }
        }
    }
}
