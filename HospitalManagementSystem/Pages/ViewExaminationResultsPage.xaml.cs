using System.Windows;
using System.Windows.Controls;

namespace HospitalManagementSystem.Pages
{
    public partial class ViewExaminationResultsPage : Page
    {
        private DatabaseHelper dbHelper;

        public ViewExaminationResultsPage()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            LoadPatients();
        }

        private void LoadPatients()
        {
            PatientComboBox.ItemsSource = dbHelper.GetPatients();
        }

        private void PatientComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PatientComboBox.SelectedItem != null)
            {
                int patientId = (PatientComboBox.SelectedItem as Patient).ID;
                ResultsListView.ItemsSource = dbHelper.GetResultsByPatient(patientId);
            }
            else
            {
                ResultsListView.ItemsSource = null;
            }
        }
    }
}