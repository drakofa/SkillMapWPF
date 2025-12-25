using SkillMapWPF.Presenters; // Для доступа к классу Database
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
            string password = LoginPassword.Text; // Если используете PasswordBox, то .Password

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("поля пустуют");
                return;
            }

            Database db = new Database();
            int userId = db.Login(email, password);

            if (userId > 0)
            {
                MessageBox.Show("Успешный вход!");
                // Сохраняем в сессию (если создали класс UserSession)
                // UserSession.UserId = userId;
                NavigationService.Navigate(new VacanciesPage());
                // Переход на страницу личного кабинета
                // NavigationService.Navigate(new HomePage());
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
                Database db = new Database();
                string password = RegisterPassWord.Text;

              //  [cite_start]// Используем вашу хранимую процедуру AddNewUser [cite: 1]
                int newId = db.RegisterUser(
                    RegisterFirstName.Text,
                    RegisterLastName.Text,
                    RegisterEmail.Text,
                    RegisterPhone.Text,
                    password,
                    "CANDIDATE"
                );

                if (newId > 0)
                {
                    MessageBox.Show($"Регистрация успешна! Ваш ID: {newId}");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Ошибка регистрации: " + ex.Message);
            }
        }
    }
}