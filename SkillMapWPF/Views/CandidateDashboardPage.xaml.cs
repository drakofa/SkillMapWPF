using SkillMapWPF.Presenters;
using System;
using System.Collections.Generic;
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

namespace SkillMapWPF.Views
{
    /// <summary>
    /// Логика взаимодействия для CandidateDashboardPage.xaml
    /// </summary>
    public partial class CandidateDashboardPage : Page
    {
        

        private int currentPage = 0;
        private const int ItemsPerPage = 5;

        public CandidateDashboardPage()
        {
            InitializeComponent();
            LoadResumes();
        }

        private void LoadResumes()
        {
            ResumeContainer.Children.Clear();
            Database db = new Database();

            // Запрос на получение резюме (можно добавить OFFSET/FETCH для пагинации в SQL)
            string query = "SELECT ResumeId, Specialty FROM Resume";
            DataTable dt = db.GetData(query);

            foreach (DataRow row in dt.Rows)
            {
                Button btn = new Button
                {
                    Content = row["Specialty"].ToString(),
                    Tag = row["ResumeId"], // Сохраняем ID внутри кнопки
                    Height = 40,
                    Margin = new Thickness(0, 5, 0, 5)
                };
                btn.Click += ResumeButton_Click;
                ResumeContainer.Children.Add(btn);
            }
        }

        private void ResumeButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            int resumeId = (int)clickedButton.Tag;
            MessageBox.Show($"Открываем резюме с ID: {resumeId}");
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            currentPage++;
            LoadResumes(); // Здесь должна быть логика смещения (OFFSET) в SQL
        }

        private void PrevPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 0) currentPage--;
            LoadResumes();
        }

    }
}
