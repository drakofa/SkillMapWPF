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
    /// Логика взаимодействия для AnalystDashboardPage.xaml
    /// </summary>
    public partial class AnalystDashboardPage : Page
    {
        public AnalystDashboardPage()
        {
            InitializeComponent();
        }
        private void TopPopular_Click(object sender, RoutedEventArgs e)
        {
            // Передаем название процедуры для Топ-20 популярных
            NavigationService.Navigate(new ReportViewerPage("GetTop5PopularVacancies"));
        }

        private void HighSalary_Click(object sender, RoutedEventArgs e)
        {
            // Для Топ-10 по зарплате
            NavigationService.Navigate(new ReportViewerPage("GetHighestSalaryVacancies"));
        }

        private void SpecialtyAnalysis_Click(object sender, RoutedEventArgs e)
        {
            // Анализ по специальностям
            NavigationService.Navigate(new ReportViewerPage("GetResumeBySpecialtyAnalysis"));
        }

        private void ApplicationAnalysis_Click(object sender, RoutedEventArgs e)
        {
            // Анализ откликов
            NavigationService.Navigate(new ReportViewerPage("GetApplicationAnalysis"));
        }

        private void UserStats_Click(object sender, RoutedEventArgs e)
        {
            // Статистика пользователей
            NavigationService.Navigate(new ReportViewerPage("GetUserActivityStats"));
        }
    }
}
