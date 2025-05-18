using System;
using System.Windows;
using System.Windows.Controls;
using HospitalManagementSystem.Pages;

namespace HospitalManagementSystem
{
    public partial class MainWindow : Window
    {
        private bool isAuthenticated = false;
        private string userRole;

        public MainWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized; // Полноэкранный режим
            NavigationListBox.IsEnabled = false;
            MainFrame.Navigate(new LoginPage(this));
        }

        private void NavigationListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NavigationListBox.SelectedItem == null) return;

            string selectedItem = (NavigationListBox.SelectedItem as ListBoxItem).Content.ToString();
            switch (userRole)
            {
                case "Registrar":
                    switch (selectedItem)
                    {
                        case "Главная":
                            MainFrame.Navigate(new HomePage());
                            break;
                        case "Пациенты":
                            MainFrame.Navigate(new PatientsPage());
                            break;
                        case "Запись на приём":
                            MainFrame.Navigate(new ScheduleAppointmentPage());
                            break;
                        case "Редактировать приём":
                            MainFrame.Navigate(new EditAppointmentPage());
                            break;
                        case "Расписание врачей":
                            MainFrame.Navigate(new ViewDoctorSchedulePage());
                            break;
                        case "Выход":
                            isAuthenticated = false;
                            userRole = null;
                            NavigationListBox.IsEnabled = false;
                            MainFrame.Navigate(new LoginPage(this));
                            break;
                    }
                    break;
                case "Doctor":
                    switch (selectedItem)
                    {
                        case "Главная":
                            MainFrame.Navigate(new HomePage());
                            break;
                        case "Мои приёмы":
                            MainFrame.Navigate(new ViewOwnAppointmentsPage());
                            break;
                        case "История болезни":
                            MainFrame.Navigate(new RecordMedicalHistoryPage());
                            break;
                        case "Назначить обследование":
                            MainFrame.Navigate(new OrderExaminationPage());
                            break;
                        case "Выписать рецепт":
                            MainFrame.Navigate(new IssuePrescriptionPage());
                            break;
                        case "Результаты обследований":
                            MainFrame.Navigate(new ViewExaminationResultsPage());
                            break;
                        case "Выход":
                            isAuthenticated = false;
                            userRole = null;
                            NavigationListBox.IsEnabled = false;
                            MainFrame.Navigate(new LoginPage(this));
                            break;
                    }
                    break;
                case "Nurse":
                    switch (selectedItem)
                    {
                        case "Главная":
                            MainFrame.Navigate(new HomePage());
                            break;
                        case "Назначения":
                            MainFrame.Navigate(new ViewOrdersPage());
                            break;
                        case "Выполнить назначение":
                            MainFrame.Navigate(new CompleteOrderPage());
                            break;
                        case "Добавить заметку":
                            MainFrame.Navigate(new AddMedicalNotePage());
                            break;
                        case "История пациента":
                            MainFrame.Navigate(new ViewPatientHistoryPage());
                            break;
                        case "Выход":
                            isAuthenticated = false;
                            userRole = null;
                            NavigationListBox.IsEnabled = false;
                            MainFrame.Navigate(new LoginPage(this));
                            break;
                    }
                    break;
            }
            NavigationListBox.SelectedItem = null;
        }

        public void SetAuthenticated(bool authenticated, string role)
        {
            isAuthenticated = authenticated;
            userRole = role;
            NavigationListBox.IsEnabled = authenticated;
            if (authenticated)
            {
                UpdateNavigationItems();
            }
        }

        private void UpdateNavigationItems()
        {
            NavigationListBox.Items.Clear();
            NavigationListBox.Items.Add(new ListBoxItem { Content = "Главная" });
            if (userRole == "Registrar")
            {
                NavigationListBox.Items.Add(new ListBoxItem { Content = "Пациенты" });
                NavigationListBox.Items.Add(new ListBoxItem { Content = "Запись на приём" });
                NavigationListBox.Items.Add(new ListBoxItem { Content = "Редактировать приём" });
                NavigationListBox.Items.Add(new ListBoxItem { Content = "Расписание врачей" });
            }
            else if (userRole == "Doctor")
            {
                NavigationListBox.Items.Add(new ListBoxItem { Content = "Мои приёмы" });
                NavigationListBox.Items.Add(new ListBoxItem { Content = "История болезни" });
                NavigationListBox.Items.Add(new ListBoxItem { Content = "Назначить обследование" });
                NavigationListBox.Items.Add(new ListBoxItem { Content = "Выписать рецепт" });
                NavigationListBox.Items.Add(new ListBoxItem { Content = "Результаты обследований" });
            }
            else if (userRole == "Nurse")
            {
                NavigationListBox.Items.Add(new ListBoxItem { Content = "Назначения" });
                NavigationListBox.Items.Add(new ListBoxItem { Content = "Выполнить назначение" });
                NavigationListBox.Items.Add(new ListBoxItem { Content = "Добавить заметку" });
                NavigationListBox.Items.Add(new ListBoxItem { Content = "История пациента" });
            }
            NavigationListBox.Items.Add(new ListBoxItem { Content = "Выход" });
        }
    }
}