using SkillMapWPF.Models;
using SkillMapWPF.Presenters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SkillMapWPF.Views
{
    /// <summary>
    /// Логика взаимодействия для CreateResumePage.xaml
    /// </summary>
    public partial class CreateResumePage : Page
    {
        public CreateResumePage()
        {
            InitializeComponent();
        }
        private void SaveResume_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Простая валидация числовых полей
                if (!int.TryParse(TxtExperience.Text, out int exp) || !int.TryParse(TxtSalary.Text, out int salary))
                {
                    MessageBox.Show("Опыт и зарплата должны быть числами!");
                    return;
                }

                Database db = new Database();
                // Используем UserId из нашей статической сессии
                int resumeId = db.CreateResume(
                    UserSession.UserId,
                    TxtSpecialty.Text,
                    TxtDescription.Text,
                    exp,
                    salary
                );

                if (resumeId > 0)
                {
                    MessageBox.Show("Резюме успешно создано!");
                    NavigationService.GoBack(); // Возвращаемся на страницу вакансий
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
