using System;
using System.Windows;
using System.Windows.Controls;

namespace HospitalManagementSystem.Pages
{
    public partial class OrderExaminationPage : Page
    {
        private DatabaseHelper dbHelper;
        private int doctorId;

        public OrderExaminationPage()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            doctorId = 1;
            LoadAppointments();
        }

        private void LoadAppointments()
        {
            AppointmentComboBox.ItemsSource = dbHelper.GetAppointmentsByDoctor(doctorId);
        }

        private void SaveOrder_Click(object sender, RoutedEventArgs e)
        {
            if (AppointmentComboBox.SelectedItem == null || ExaminationTypeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Заполните все поля.");
                return;
            }

            try
            {
                int appointmentId = (AppointmentComboBox.SelectedItem as Appointment).ID;
                string examinationType = (ExaminationTypeComboBox.SelectedItem as ComboBoxItem).Content.ToString();

                dbHelper.AddOrder(appointmentId, examinationType);
                MessageBox.Show("Обследование назначено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при назначении обследования: {ex.Message}");
            }
        }
    }
}