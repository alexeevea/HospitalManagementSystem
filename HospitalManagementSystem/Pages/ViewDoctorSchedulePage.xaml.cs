using System.Windows.Controls;

namespace HospitalManagementSystem.Pages
{
    public partial class ViewDoctorSchedulePage : Page
    {
        private DatabaseHelper dbHelper;

        public ViewDoctorSchedulePage()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            LoadAppointments();
        }

        private void LoadAppointments()
        {
            AppointmentsListView.ItemsSource = dbHelper.GetAppointments();
        }
    }
}