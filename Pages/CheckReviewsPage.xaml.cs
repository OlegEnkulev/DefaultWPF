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
    public partial class CheckReviewsPage : Page
    {
        public CheckReviewsPage()
        {
            InitializeComponent();

            DG.ItemsSource = Core.DB.Reviews.Where(r => r.UserToId == Core.currentUser.Id).ToList();
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            switch (Core.currentUser.RoleId)
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
