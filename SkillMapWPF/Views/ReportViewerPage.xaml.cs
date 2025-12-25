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
    /// Логика взаимодействия для ReportViewerPage.xaml
    /// </summary>
    public partial class ReportViewerPage : Page
    {
        public ReportViewerPage()
        {
            InitializeComponent();
        }
        private string _procedureName;

        public ReportViewerPage(string procedureName)
        {
            InitializeComponent();
            _procedureName = procedureName;
            SetFriendlyTitle(procedureName);
            LoadReportData();
        }

        private void SetFriendlyTitle(string proc)
        {
            // Делаем красивые названия для заголовков
            TxtReportTitle.Text = proc switch
            {
                "GetTop5PopularVacancies" => "Топ-20 популярных вакансий",
                "GetHighestSalaryVacancies" => "Топ-10 вакансий с самой высокой ЗП",
                "GetResumeBySpecialtyAnalysis" => "Статистика по специальностям резюме",
                "GetApplicationAnalysis" => "Статистика по откликам",
                "GetUserActivityStats" => "Активность пользователей",
                _ => "Аналитический отчет"
            };
        }

        private void LoadReportData()
        {
            try
            {
                Database db = new Database();
                // Используем наш универсальный метод из предыдущего шага
                DataTable dt = db.ExecuteReportStoredProcedure(_procedureName);

                if (dt != null)
                {
                    DgReport.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки данных: " + ex.Message);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
