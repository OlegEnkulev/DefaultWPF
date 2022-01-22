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
    public partial class EmployerPage : Page
    {
        public EmployerPage()
        {
            InitializeComponent();

            NameLabel.Content = Core.currentUser.Surname + " " + Core.currentUser.Name;
            GradeLabel.Content = "Ваша оценка: " + Core.currentUser.Grade;

            DG.ItemsSource = Core.DB.Internships.Where(i => i.EmployerId == Core.currentUser.Id).ToList();
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
                if (DG.SelectedItem == Core.DB.Internships.Where(s => s.Id == id).FirstOrDefault())
                {
                    break;
                }
                id++;
            }

            Internships deleteInternship = Core.DB.Internships.Where(s => s.Id == id).FirstOrDefault();
            Core.DB.Internships.Remove(deleteInternship);
            Core.DB.SaveChanges();
            DG.ItemsSource = Core.DB.Internships.ToList();
        }

        private void EditBTN_Click(object sender, RoutedEventArgs e)
        {
            if (DG.SelectedItem == null)
            {
                return;
            }

            int id = 0;

            while (true)
            {
                if (DG.SelectedItem == Core.DB.Internships.Where(s => s.Id == id).FirstOrDefault())
                {
                    break;
                }
                id++;
            }

            Core.mainWindow.MainFrame.Navigate(new ActionWithInternshipPage(id));
        }

        private void CreateBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.mainWindow.MainFrame.Navigate(new ActionWithInternshipPage(-1));
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.currentUser = null;
            Core.mainWindow.MainFrame.Navigate(new MainPage());
        }

        private void DeleteProfileBTN_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить профиль?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Core.DB.Users.Remove(Core.currentUser);
            }
        }
    }
}
