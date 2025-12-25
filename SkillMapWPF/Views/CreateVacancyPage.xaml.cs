using SkillMapWPF.Presenters;
using SkillMapWPF.Models; // Убедитесь, что у вас есть доступ к UserSession
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SkillMapWPF.Views
{
    public partial class CreateVacancyPage : Page
    {
        public CreateVacancyPage()
        {
            InitializeComponent();
        }

        private void SaveVacancy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Database db = new Database();

                // 1. Получаем ID компании текущего пользователя
                int userCompanyId = db.GetCompanyIdByUserId(UserSession.UserId);

                if (userCompanyId <= 0)
                {
                    MessageBox.Show("Ошибка: У вас еще не зарегистрирована компания. Создайте профиль компании перед публикацией вакансий.");
                    return;
                }

                // 2. Сбор и валидация числовых данных
                if (!int.TryParse(TxtMinSalary.Text, out int minSalary) ||
                    !int.TryParse(TxtMaxSalary.Text, out int maxSalary) ||
                    !int.TryParse(TxtExperience.Text, out int exp))
                {
                    MessageBox.Show("Зарплата и опыт должны быть числами!");
                    return;
                }

                // 3. Получение текста из ComboBox
                string empType = (CbEmploymentType.SelectedItem as ComboBoxItem)?.Content.ToString();
                string schedule = (CbWorkSchedule.SelectedItem as ComboBoxItem)?.Content.ToString();
                string format = (CbWorkFormat.SelectedItem as ComboBoxItem)?.Content.ToString();

                if (string.IsNullOrEmpty(empType) || string.IsNullOrEmpty(schedule) || string.IsNullOrEmpty(format))
                {
                    MessageBox.Show("Выберите все параметры работы!");
                    return;
                }

                // 4. СОЗДАЕМ ОБЪЕКТ Vacancy (как того требует метод в Database.cs)
                Vacancy newVacancy = new Vacancy
                {
                    VacancyTitle = TxtTitle.Text,
                    VacancyDescription = TxtDescription.Text,
                    MinSalary = minSalary,
                    MaxSalary = maxSalary,
                    RequiredExperience = exp,
                    Location = TxtLocation.Text,
                    CompanyId = userCompanyId
                };

                // 5. ВЫЗОВ МЕТОДА (теперь аргументы совпадают)
                int vacancyId = db.CreateVacancy(newVacancy, empType, schedule, format);

                if (vacancyId > 0)
                {
                    MessageBox.Show("Вакансия успешно опубликована!");
                    NavigationService.GoBack();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
        // ЭТОТ МЕТОД ДОЛЖЕН БЫТЬ ОБЯЗАТЕЛЬНО, чтобы XAML его видел
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}