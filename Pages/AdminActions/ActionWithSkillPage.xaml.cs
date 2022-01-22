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
    public partial class ActionWithSkillPage : Page
    {
        List<int> selectedUsers = new List<int> { };
        Skills currentSkill;
        bool isCreated = true;
        public ActionWithSkillPage(int сhangeableSkillId) //n > 0 - Изменение, n < 0 - Создание
        {
            InitializeComponent();

            DG.ItemsSource = Core.DB.Users.Where(u => u.RoleId == 0).ToList();

            selectedUsers = Core.DB.StudentSkills.Where(s => s.SkillId == сhangeableSkillId).Select(u => u.StudentId).ToList();

            DG.Focus();
            DG.SelectedItems.Add(selectedUsers);
            DG.Focus();

            if (сhangeableSkillId < 0)
            {
                isCreated = false;
                return;
            }

            currentSkill = Core.DB.Skills.Where(s => s.Id == сhangeableSkillId).FirstOrDefault();

            if (currentSkill == null)
            {
                MessageBox.Show("Акак?");
                return;
            }

            SkillBox.Text = currentSkill.Skill;
        }

        private void AcceptBTN_Click(object sender, RoutedEventArgs e)
        {
            if (!isCreated)
            {
                currentSkill = new Skills();
            }

            currentSkill.Skill = SkillBox.Text;

            Core.DB.SaveChanges();

            List<StudentSkills> dss = Core.DB.StudentSkills.Where(s => s.SkillId == currentSkill.Id).ToList();
            Core.DB.StudentSkills.RemoveRange(dss);

            selectedUsers = DG.SelectedItems.Cast<Users>().Select(u => u.Id).ToList();

            for(int i = 0;i < selectedUsers.Count(); i++)
            {
                Core.DB.StudentSkills.Add(new StudentSkills { SkillId = currentSkill.Id, StudentId = selectedUsers[i] });
            }
            Core.DB.SaveChanges();

            Core.mainWindow.MainFrame.Navigate(new ControlSkillsPage());
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.mainWindow.MainFrame.Navigate(new ControlSkillsPage());
        }
    }
}
