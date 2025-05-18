using System;
using System.Windows;
using System.Windows.Controls;

namespace HospitalManagementSystem.Pages
{
    public partial class EditAppointmentPage : Page
    {
        private DatabaseHelper dbHelper;
        private Appointment selectedAppointment;

        public EditAppointmentPage()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            LoadAppointments();
            LoadPatients();
            LoadDoctors();
        }

        private void LoadAppointments()
        {
            AppointmentComboBox.ItemsSource = dbHelper.GetAppointments();
            AppointmentComboBox.DisplayMemberPath = "DateTime";
            AppointmentComboBox.SelectedValuePath = "ID";
        }

        private void LoadPatients()
        {
            PatientComboBox.ItemsSource = dbHelper.GetPatients();
        }

        private void LoadDoctors()
        {
            DoctorComboBox.ItemsSource = dbHelper.GetDoctors();
        }

        private void AppointmentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AppointmentComboBox.SelectedItem != null)
            {
                selectedAppointment = (Appointment)AppointmentComboBox.SelectedItem;
                PatientComboBox.SelectedValue = selectedAppointment.PatientID;
                DoctorComboBox.SelectedValue = selectedAppointment.DoctorID;
                DateTime dateTime = DateTime.Parse(selectedAppointment.DateTime);
                DateDatePicker.SelectedDate = dateTime.Date;
                TimeTextBox.Text = dateTime.ToString("HH:mm");
            }
        }

        private void SaveAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (selectedAppointment == null || PatientComboBox.SelectedItem == null || DoctorComboBox.SelectedItem == null || DateDatePicker.SelectedDate == null || string.IsNullOrEmpty(TimeTextBox.Text))
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

                dbHelper.UpdateAppointment(selectedAppointment.ID, patientId, doctorId, dateTimeString);
                MessageBox.Show("Приём успешно обновлён!");
                LoadAppointments();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении приёма: {ex.Message}");
            }
        }

        private void DeleteAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (selectedAppointment == null)
            {
                MessageBox.Show("Выберите приём для удаления.");
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Вы уверены, что хотите удалить этот приём?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    dbHelper.DeleteAppointment(selectedAppointment.ID);
                    MessageBox.Show("Приём успешно удалён!");
                    LoadAppointments();
                    selectedAppointment = null;
                    PatientComboBox.SelectedItem = null;
                    DoctorComboBox.SelectedItem = null;
                    DateDatePicker.SelectedDate = null;
                    TimeTextBox.Text = "HH:mm";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении приёма: {ex.Message}");
                }
            }
        }
    }
}