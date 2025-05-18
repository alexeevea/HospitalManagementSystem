using System;
using System.Windows;
using System.Windows.Controls;

namespace HospitalManagementSystem.Pages
{
    public partial class ScheduleAppointmentPage : Page
    {
        private DatabaseHelper dbHelper;

        public ScheduleAppointmentPage()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            LoadPatients();
            LoadDoctors();
        }

        private void LoadPatients()
        {
            PatientComboBox.ItemsSource = dbHelper.GetPatients();
        }

        private void LoadDoctors()
        {
            DoctorComboBox.ItemsSource = dbHelper.GetDoctors();
        }

        private void SaveAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (PatientComboBox.SelectedItem == null || DoctorComboBox.SelectedItem == null || DateDatePicker.SelectedDate == null || string.IsNullOrEmpty(TimeTextBox.Text))
            {
                MessageBox.Show("Заполните все поля.");
                return;
            }

            try
            {
                int patientId = (PatientComboBox.SelectedItem as Patient).ID;
                int doctorId = (DoctorComboBox.SelectedItem as Doctor).ID;
                DateTime dateTime = DateDatePicker.SelectedDate.Value.Date + TimeSpan.Parse(TimeTextBox.Text);
                string dateTimeString = dateTime.ToString("yyyy-MM-dd HH:mm");

                dbHelper.AddAppointment(patientId, doctorId, dateTimeString);
                MessageBox.Show("Приём успешно записан!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при записи на приём: {ex.Message}");
            }
        }
    }
}