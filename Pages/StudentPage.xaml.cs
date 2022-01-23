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
    public partial class StudentPage : Page
    {
        public StudentPage()
        {
            InitializeComponent();

            NameLabel.Content = Core.currentUser.Surname + " " + Core.currentUser.Name;
            GradeLabel.Content = "Ваша оценка: " + Core.currentUser.Grade;

            DG.ItemsSource = Core.DB.Internships.Where(i => i.StudentId == 12).ToList();
        }

        private void DeleteProfileBTN_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Вы действительно хотите удалить профиль?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Core.DB.Users.Remove(Core.currentUser);
            }
        }

        private void OtklickBTN_Click(object sender, RoutedEventArgs e)
        {
            if (DG.SelectedItem == null)
            {
                return;
            }

            Internships newIntenship = (Internships)DG.SelectedItem;
            newIntenship.DateStart = DateTime.Now;
            newIntenship.StudentId = Core.currentUser.Id;

            Core.DB.SaveChanges();
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.currentUser = null;
            Core.mainWindow.MainFrame.Navigate(new MainPage());
        }

        private void CreateReviewBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.mainWindow.MainFrame.Navigate(new CreateReviewPage());
        }

        private void CheckReviewsBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.mainWindow.MainFrame.Navigate(new CheckReviewsPage());
        }
    }
}
