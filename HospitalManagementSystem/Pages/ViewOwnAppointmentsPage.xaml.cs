using System.Windows.Controls;

namespace HospitalManagementSystem.Pages
{
    public partial class ViewOwnAppointmentsPage : Page
    {
        private DatabaseHelper dbHelper;
        private int doctorId; // Предполагается, что ID врача передаётся при авторизации

        public ViewOwnAppointmentsPage()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            doctorId = 1; // Замените на реальный ID врача после авторизации
            LoadAppointments();
        }

        private void LoadAppointments()
        {
            AppointmentsListView.ItemsSource = dbHelper.GetAppointmentsByDoctor(doctorId);
        }
    }
}