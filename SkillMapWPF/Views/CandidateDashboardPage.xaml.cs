using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using SkillMapWPF.Presenters;

namespace SkillMapWPF.Views
{
    public partial class CandidateDashboardPage : Page
    {
        private int _currentPage = 0;
        private const int _pageSize = 5; // Сколько резюме на одной странице

        public CandidateDashboardPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                ResumeContainer.Children.Clear();
                Database db = new Database();

                // Получаем данные через наше представление
                DataTable dt = db.GetResumesPaged(_currentPage * _pageSize, _pageSize);

                if (dt.Rows.Count == 0 && _currentPage > 0)
                {
                    _currentPage--; // Возвращаемся, если данных больше нет
                    return;
                }

                foreach (DataRow row in dt.Rows)
                {
                    // Создаем красивую кнопку для каждого резюме
                    Button resumeBtn = new Button
                    {
                        Height = 60,
                        Margin = new Thickness(0, 0, 0, 10),
                        HorizontalContentAlignment = HorizontalAlignment.Left,
                        Padding = new Thickness(10),
                        // Сохраняем ID, чтобы потом открыть полное резюме
                        Tag = row["ResumeId"]
                    };

                    string specialty = row["Specialty"].ToString();
                    string salary = row["DesiredSalary"] != DBNull.Value ? row["DesiredSalary"].ToString() : "0";

                    resumeBtn.Content = $"{specialty} — Ожидаемая ЗП: {salary} руб.";
                    resumeBtn.Click += (s, e) => {
                        MessageBox.Show($"Открытие резюме ID: {((Button)s).Tag}");
                    };

                    ResumeContainer.Children.Add(resumeBtn);
                }

                PageNumber.Text = $"Страница {_currentPage + 1}";
                BtnPrev.IsEnabled = _currentPage > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки резюме: " + ex.Message);
            }
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            _currentPage++;
            LoadData();
        }

        private void PrevPage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 0)
            {
                _currentPage--;
                LoadData();
            }
        }
    }
}