using System;
using System.Windows;
using System.Windows.Controls;
using SkillMapWPF.Models;
using SkillMapWPF.Presenters;

namespace SkillMapWPF.Views
{
    public partial class CreateCompanyPage : Page
    {
        public CreateCompanyPage()
        {
            InitializeComponent();
        }

        private void CreateCompany_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Валидация
                if (string.IsNullOrWhiteSpace(TxtCompanyName.Text) || TxtTaxNumber.Text.Length < 10)
                {
                    MessageBox.Show("Пожалуйста, заполните название и корректный ИНН.");
                    return;
                }

                string selectedType = (CbCompanyType.SelectedItem as ComboBoxItem)?.Content.ToString();

                Database db = new Database();
                //[cite_start]// Используем сессию для получения ID создателя 
                int companyId = db.CreateCompany(
                    TxtCompanyName.Text,
                    TxtTaxNumber.Text,
                    selectedType,
                    UserSession.UserId
                );

                if (companyId > 0)
                {
                    MessageBox.Show("Компания успешно зарегистрирована!");
                    NavigationService.GoBack();
                }
            }
            catch (Exception ex)
            {
                // Вывод ошибки из SQL (например, если ИНН уже существует)
                MessageBox.Show(ex.Message);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}