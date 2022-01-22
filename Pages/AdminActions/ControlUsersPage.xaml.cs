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
    public partial class ControlUsersPage : Page
    {
        public ControlUsersPage()
        {
            InitializeComponent();
            DG.ItemsSource = Core.DB.Users.ToList();
        }

        private void CreateBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.mainWindow.MainFrame.Navigate(new ActionWithUserPage(-1));
        }

        private void ChangeBTN_Click(object sender, RoutedEventArgs e)
        {
            if (DG.SelectedItem == null)
            {
                return;
            }

            int id = 0;

            while (true)
            {
                if (DG.SelectedItem == Core.DB.Users.Where(s => s.Id == id).FirstOrDefault())
                {
                    break;
                }
                id++;
            }

            Core.mainWindow.MainFrame.Navigate(new ActionWithUserPage(id));
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.mainWindow.MainFrame.Navigate(new AdminPage());
        }

        private void DeleteBTN_Click(object sender, RoutedEventArgs e)
        {
            if (DG.SelectedItem == null)
            {
                return;
            }

            int id = 0;

            while (true)
            {
                if (DG.SelectedItem == Core.DB.Users.Where(s => s.Id == id).FirstOrDefault())
                {
                    break;
                }
                id++;
            }

            Users deleteUser = Core.DB.Users.Where(s => s.Id == id).FirstOrDefault();
            Core.DB.Users.Remove(deleteUser);
            Core.DB.SaveChanges();
            DG.ItemsSource = Core.DB.Users.ToList();
        }
    }
}
