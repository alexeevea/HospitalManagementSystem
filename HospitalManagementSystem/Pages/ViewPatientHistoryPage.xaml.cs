using System.Windows;
using System.Windows.Controls;

namespace HospitalManagementSystem.Pages
{
    public partial class ViewPatientHistoryPage : Page
    {
        private DatabaseHelper dbHelper;

        public ViewPatientHistoryPage()
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
                MedicalRecordsListView.ItemsSource = dbHelper.GetMedicalRecordsByPatient(patientId);
                NotesListView.ItemsSource = dbHelper.GetNotesByPatient(patientId);
            }
            else
            {
                MedicalRecordsListView.ItemsSource = null;
                NotesListView.ItemsSource = null;
            }
        }
    }
}