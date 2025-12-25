using SkillMapWPF.Presenters;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SkillMapWPF.Views
{
    public partial class VacanciesPage : Page
    {
        private int _page = 0;
        private const int _limit = 4; // 4 вакансии на экран

        public VacanciesPage()
        {
            InitializeComponent();
            LoadVacancies();
        }

        private void LoadVacancies()
        {
            try
            {
                VacanciesContainer.Children.Clear();
                Database db = new Database();
                DataTable dt = db.GetVacanciesPaged(_page * _limit, _limit);

                foreach (DataRow row in dt.Rows)
                {
                    // Создаем контейнер-карточку (Border)
                    Border card = new Border
                    {
                        Background = Brushes.White,
                        BorderBrush = Brushes.LightGray,
                        BorderThickness = new Thickness(1),
                        CornerRadius = new CornerRadius(8),
                        Padding = new Thickness(15),
                        Margin = new Thickness(0, 0, 0, 15)
                    };

                    StackPanel content = new StackPanel();

                    // Заголовок вакансии (из VacancyTitle)
                    content.Children.Add(new TextBlock
                    {
                        Text = row["VacancyTitle"].ToString(),
                        FontSize = 18,
                        FontWeight = FontWeights.Bold,
                        Foreground = new SolidColorBrush(Color.FromRgb(41, 128, 185))
                    });
                    // Используем WorkFormat, так как в View он назван именно так
                    string workFormat = row["WorkFormat"].ToString();
                    string location = row["Location"].ToString();
                    // Компания и Зарплата (из CompanyName и SalaryRange)
                    content.Children.Add(new TextBlock
                    {
                        Text = $"{row["CompanyName"]} • {workFormat} • {location}",
                        Margin = new Thickness(0, 5, 0, 5),
                        FontSize = 14,
                        Foreground = Brushes.DarkSlateGray
                    });

                    // Кнопка отклика
                    Button applyBtn = new Button
                    {
                        Content = "Откликнуться",
                        Width = 120,
                        HorizontalAlignment = HorizontalAlignment.Right,
                        Tag = row["VacancyId"]
                    };
                    applyBtn.Click += (s, e) => MessageBox.Show($"Вы откликнулись на вакансию ID: {((Button)s).Tag}");

                    content.Children.Add(applyBtn);
                    card.Child = content;
                    VacanciesContainer.Children.Add(card);
                }

                TxtPageInfo.Text = $"Страница {_page + 1}";
                BtnBack.IsEnabled = _page > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
        private void CreateResume_Click(object sender, RoutedEventArgs e)
        {
            // Переход на страницу создания резюме
            // Предполагается, что вы создадите класс CreateResumePage
            NavigationService.Navigate(new CreateResumePage());
        }
        private void Next_Click(object sender, RoutedEventArgs e) { _page++; LoadVacancies(); }
        private void Prev_Click(object sender, RoutedEventArgs e) { if (_page > 0) { _page--; LoadVacancies(); } }
    }
}