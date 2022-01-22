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
    public partial class ActionWithInternshipPage : Page
    {
        Internships currentInternship;
        bool isCreated = true;
        public ActionWithInternshipPage(int сhangeableInternshipId) //n > 0 - Изменение, n < 0 - Создание
        {
            InitializeComponent();

            StudentBox.ItemsSource = Core.DB.Users.Where(u => u.RoleId == 0).Select(u => u.Surname + " " + u.Name).ToList();

            EmployerBox.ItemsSource = Core.DB.Users.Where(u => u.RoleId == 1).Select(u => u.Surname + " " + u.Name).ToList();

            if (сhangeableInternshipId < 0)
            {
                isCreated = false;
                return;
            }

            currentInternship = Core.DB.Internships.Where(s => s.Id == сhangeableInternshipId).FirstOrDefault();

            if (currentInternship == null)
            {
                MessageBox.Show("Акак?");
                return;
            }

            Users stud = Core.DB.Users.Where(u => u.Id == currentInternship.StudentId).FirstOrDefault();
            StudentBox.SelectedItem = stud.Surname + " " + stud.Name;

            Users emp = Core.DB.Users.Where(u => u.Id == currentInternship.EmployerId).FirstOrDefault();
            EmployerBox.SelectedItem = emp.Surname + " " + emp.Name;

            DateStartDP.SelectedDate = currentInternship.DateStart;
            DateStopDP.SelectedDate = currentInternship.DateStop;
        }

        private void AcceptBTN_Click(object sender, RoutedEventArgs e)
        {
            if (!isCreated)
            {
                currentInternship = new Internships();
            }

            currentInternship.StudentId = Core.DB.Users.Where(u => u.Surname + " " + u.Name == StudentBox.SelectedItem.ToString()).FirstOrDefault().Id;
            currentInternship.EmployerId = Core.DB.Users.Where(u => u.Surname + " " + u.Name == EmployerBox.SelectedItem.ToString()).FirstOrDefault().Id;

            currentInternship.DateStart = DateStartDP.SelectedDate;
            currentInternship.DateStop = DateStopDP.SelectedDate;

            if (!isCreated)
            {
                Core.DB.Internships.Add(currentInternship);
            }

            Core.DB.SaveChanges();

            Core.mainWindow.MainFrame.Navigate(new ControlInternshipsPage());
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.mainWindow.MainFrame.Navigate(new ControlInternshipsPage());
        }
    }
}
