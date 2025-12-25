using Microsoft.Win32;
using SkillMapWPF.Presenters;
using System.Data;
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

namespace SkillMapWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new SkillMapWPF.Views.LoginRegisterPage());
        }

        //private void LoginButton_Click(object sender, RoutedEventArgs e)
        //{
        //    string email = LoginEmail.Text;
        //    string password = LoginPassword.Text;

        //    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) 
        //    {
        //        MessageBox.Show("поля пустуют");
        //        return;
        //    }
        //    Database db = new Database();
        //    int useerID = db.Login(email, password);
        //    if (useerID > 0)
        //    {
        //        MessageBox.Show("Успешный вход!");
        //        // Здесь можно открыть главное окно приложения и передать туда userId
        //        // MainWindow main = new MainWindow(userId);
        //        // main.Show();
        //        // this.Close();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Неверный Email или пароль");
        //    }
        //}

        //private void RegisterButton_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        Database db = new Database();

        //        // В реальном приложении здесь должен быть хеш от пароля! но не сегодня)))
        //        string password = RegisterPassWord.Text;

        //        int newId = db.RegisterUser(
        //            RegisterFirstName.Text,
        //            RegisterLastName.Text,
        //            RegisterEmail.Text,
        //            RegisterPhone.Text,
        //            password,
        //            "CANDIDATE" // Передаем код роли для поиска ID внутри SQL
        //        );

        //        if (newId > 0)
        //        {
        //            MessageBox.Show($"Регистрация успешна! Ваш ID: {newId}");
        //            // Переходим к авторизации или в личный кабинет
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Ошибка регистрации: " + ex.Message);
        //    }
        //}



        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        Database db = new Database();
        //        DataTable users = db.GetUsers();

        //        if (users == null || users.Rows.Count == 0)
        //        {
        //            tb.Text = "Пользователей нет в базе";
        //            return;
        //        }

        //        // Берем первую строку
        //        DataRow row = users.Rows[0];

        //        // Безопасное извлечение данных (проверка на null из БД)
        //        string fName = row["FirstName"] != DBNull.Value ? row["FirstName"].ToString() : "Имя не указано";
        //        string lName = row["LastName"] != DBNull.Value ? row["LastName"].ToString() : "Фамилия не указана";

        //        // Выводим текст в TextBlock
        //        tb.Text = $"Случайный пользователь: {fName} {lName}";
        //    }
        //    catch (Exception ex)
        //    {
        //        // Если база недоступна или запрос неверный, вы увидите причину здесь
        //        tb.Text = "Ошибка при загрузке данных";
        //        MessageBox.Show(ex.Message, "Ошибка БД", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

    }
}