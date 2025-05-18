using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace HospitalManagementSystem.Pages
{
    public partial class PatientsPage : Page
    {
        private DatabaseHelper dbHelper;
        private bool isUpdatingDays = false;

        public PatientsPage()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            InitializeDateComboBoxes();
            LoadPatients();
        }

        private void InitializeDateComboBoxes()
        {
            try
            {
                YearComboBox.Items.Clear();
                for (int year = 2025; year >= 1850; year--)
                {
                    YearComboBox.Items.Add(year.ToString("D4"));
                }
                YearComboBox.SelectedIndex = 0;

                MonthComboBox.Items.Clear();
                for (int month = 1; month <= 12; month++)
                {
                    MonthComboBox.Items.Add(month.ToString("D2"));
                }
                MonthComboBox.SelectedIndex = 0;

                UpdateDaysComboBox();

                if (DayComboBox.Items.Count == 0 || MonthComboBox.Items.Count == 0 || YearComboBox.Items.Count == 0)
                {
                    MessageBox.Show("Ошибка: один из списков дат пуст!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при инициализации дат: {ex.Message}");
            }
        }

        private void UpdateDaysComboBox()
        {
            if (isUpdatingDays) return;

            isUpdatingDays = true;
            try
            {
                DayComboBox.Items.Clear();
                int daysInMonth = GetDaysInMonth();
                for (int day = 1; day <= daysInMonth; day++)
                {
                    DayComboBox.Items.Add(day.ToString("D2"));
                }
                DayComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении дней: {ex.Message}");
            }
            finally
            {
                isUpdatingDays = false;
            }
        }

        private int GetDaysInMonth()
        {
            if (!int.TryParse(YearComboBox.SelectedItem?.ToString(), out int year) ||
                !int.TryParse(MonthComboBox.SelectedItem?.ToString(), out int month))
            {
                return 31;
            }

            if (month == 2)
            {
                bool isLeapYear = (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
                return isLeapYear ? 29 : 28;
            }

            int[] daysInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            return daysInMonth[month - 1];
        }

        private void DateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender == MonthComboBox || sender == YearComboBox)
            {
                UpdateDaysComboBox();
            }
        }

        private void ContactInfoTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string phone = ContactInfoTextBox.Text;
            if (string.IsNullOrEmpty(phone))
            {
                ContactErrorTextBlock.Visibility = Visibility.Collapsed;
                return;
            }

            if (Regex.IsMatch(phone, @"^\+\d{9,14}$"))
            {
                ContactErrorTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                ContactErrorTextBlock.Text = "Номер должен начинаться с '+' и содержать 10–15 цифр";
                ContactErrorTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void SavePatient_Click(object sender, RoutedEventArgs e)
        {
            string firstName = FirstNameTextBox.Text.Trim();
            string lastName = LastNameTextBox.Text.Trim();
            string patronymic = PatronymicTextBox.Text.Trim();
            string contactInfo = ContactInfoTextBox.Text.Trim();

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                MessageBox.Show("Имя и фамилия обязательны для заполнения.");
                return;
            }

            string fullName = $"{lastName} {firstName}";
            if (!string.IsNullOrEmpty(patronymic))
            {
                fullName += $" {patronymic}";
            }

            if (YearComboBox.SelectedItem == null || MonthComboBox.SelectedItem == null || DayComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите дату рождения.");
                return;
            }
            string birthDate = $"{YearComboBox.SelectedItem}-{MonthComboBox.SelectedItem}-{DayComboBox.SelectedItem}";

            if (!Regex.IsMatch(contactInfo, @"^\+\d{9,14}$"))
            {
                MessageBox.Show("Укажите корректный номер телефона (например, +79012345678).");
                return;
            }

            dbHelper.AddPatient(fullName, birthDate, contactInfo);
            MessageBox.Show("Пациент успешно добавлен!");
            ClearInputs();
            LoadPatients();
        }

        private void DeletePatient_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag != null)
            {
                int patientId = Convert.ToInt32(button.Tag);
                MessageBoxResult result = MessageBox.Show(
                    "Вы уверены, что хотите удалить этого пациента?",
                    "Подтверждение удаления",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        dbHelper.DeletePatient(patientId);
                        MessageBox.Show("Пациент успешно удалён!");
                        LoadPatients();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении пациента: {ex.Message}");
                    }
                }
            }
        }

        private void ClearInputs()
        {
            FirstNameTextBox.Text = "";
            LastNameTextBox.Text = "";
            PatronymicTextBox.Text = "";
            ContactInfoTextBox.Text = "";
            YearComboBox.SelectedIndex = 0;
            MonthComboBox.SelectedIndex = 0;
            UpdateDaysComboBox();
        }

        private void LoadPatients()
        {
            PatientsListView.ItemsSource = dbHelper.GetPatients();
        }
    }
}