using SkillMapWPF.Models;
using SkillMapWPF.Presenters; // Для доступа к классу Database
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SkillMapWPF.Views
{
    public partial class LoginRegisterPage : Page
    {
        public LoginRegisterPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = LoginEmail.Text;
            string password = LoginPassword.Text;

            Database db = new Database();
            DataRow userRow = db.LoginAndGetProfile(email, password);

            if (userRow != null)
            {
                // Сохраняем данные в сессию
                UserSession.UserId = (int)userRow["UserID"];
                UserSession.FirstName = userRow["FirstName"].ToString();
                UserSession.RoleId = (int)userRow["RoleId"];

                MessageBox.Show($"Добро пожаловать, {UserSession.FirstName}!");

                // Логика перенаправления по ролям
                if (UserSession.RoleId == 2) // Соискатель / Аналитик
                {
                    NavigationService.Navigate(new VacanciesPage());
                }
                else if (UserSession.RoleId == 5) //HR
                {
                    MessageBox.Show("Вход в панель HR");
                    NavigationService.Navigate(new CandidateDashboardPage());
                }
                else if (UserSession.RoleId == 1) // Администратор
                {
                    MessageBox.Show("Вход в панель администратора");
                }
            }
            else
            {
                MessageBox.Show("Неверный Email или пароль");
            }
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем выбранный код роли (Tag из выбранного ComboBoxItem)
                string selectedRoleCode = (RoleComboBox.SelectedValue as string) ?? "SEEKER";

                Database db = new Database();
                int newId = db.RegisterUser(
                    RegisterFirstName.Text,
                    RegisterLastName.Text,
                    RegisterEmail.Text,
                    RegisterPhone.Text,
                    RegisterPassWord.Text, // Рекомендуется хэшировать перед отправкой
                    selectedRoleCode       // Передаем динамически выбранную роль
                );

                if (newId > 0)
                {
                    MessageBox.Show($"Регистрация успешна! Роль: {selectedRoleCode}. Ваш ID: {newId}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка регистрации: " + ex.Message);
            }
        }
    }
}