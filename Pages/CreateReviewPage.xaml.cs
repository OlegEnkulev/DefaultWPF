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
    public partial class CreateReviewPage : Page
    {
        public CreateReviewPage()
        {
            InitializeComponent();

            UserBox.ItemsSource = Core.DB.Users.Where(u => u.Id != Core.currentUser.Id).Select(u => u.Surname + " " + u.Name).ToList();
        }

        private void AcceptBTN_Click(object sender, RoutedEventArgs e)
        {
            Users utoid = (Users)UserBox.SelectedItem;
            Reviews review = new Reviews() { UserFromId = Core.currentUser.Id, UserToId = utoid.Id, Review = ReviewBox.Text, Grade = GradeBox.SelectedIndex + 1 };
            Core.DB.Reviews.Add(review);
            Core.DB.SaveChanges();
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
