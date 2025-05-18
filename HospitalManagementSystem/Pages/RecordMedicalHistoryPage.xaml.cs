using System;
using System.Windows;
using System.Windows.Controls;

namespace HospitalManagementSystem.Pages
{
    public partial class RecordMedicalHistoryPage : Page
    {
        private DatabaseHelper dbHelper;
        private int doctorId; // Предполагается, что ID врача передаётся при авторизации

        public RecordMedicalHistoryPage()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            doctorId = 1; // Замените на реальный ID врача после авторизации
            LoadAppointments();
        }

        private void LoadAppointments()
        {
            AppointmentComboBox.ItemsSource = dbHelper.GetAppointmentsByDoctor(doctorId);
        }

        private void SaveMedicalRecord_Click(object sender, RoutedEventArgs e)
        {
            if (AppointmentComboBox.SelectedItem == null || string.IsNullOrEmpty(DescriptionTextBox.Text))
            {
                MessageBox.Show("Заполните все поля.");
                return;
            }

            try
            {
                int appointmentId = (AppointmentComboBox.SelectedItem as Appointment).ID;
                string description = DescriptionTextBox.Text.Trim();

                dbHelper.AddMedicalRecord(appointmentId, description);
                MessageBox.Show("История болезни сохранена!");
                DescriptionTextBox.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении истории болезни: {ex.Message}");
            }
        }
    }
}