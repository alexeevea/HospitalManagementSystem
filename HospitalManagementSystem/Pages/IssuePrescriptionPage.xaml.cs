using System;
using System.Windows;
using System.Windows.Controls;

namespace HospitalManagementSystem.Pages
{
    public partial class IssuePrescriptionPage : Page
    {
        private DatabaseHelper dbHelper;
        private int doctorId;

        public IssuePrescriptionPage()
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

        private void SavePrescription_Click(object sender, RoutedEventArgs e)
        {
            if (AppointmentComboBox.SelectedItem == null || string.IsNullOrEmpty(PrescriptionTextBox.Text))
            {
                MessageBox.Show("Заполните все поля.");
                return;
            }

            try
            {
                int appointmentId = (AppointmentComboBox.SelectedItem as Appointment).ID;
                string prescriptionText = PrescriptionTextBox.Text.Trim();

                dbHelper.AddPrescription(appointmentId, prescriptionText);
                MessageBox.Show("Рецепт выписан!");
                PrescriptionTextBox.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выписке рецепта: {ex.Message}");
            }
        }
    }
}