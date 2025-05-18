using System;
using System.Windows;
using System.Windows.Controls;

namespace HospitalManagementSystem.Pages
{
    public partial class AddMedicalNotePage : Page
    {
        private DatabaseHelper dbHelper;

        public AddMedicalNotePage()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            LoadPatients();
        }

        private void LoadPatients()
        {
            PatientComboBox.ItemsSource = dbHelper.GetPatients();
        }

        private void SaveNote_Click(object sender, RoutedEventArgs e)
        {
            if (PatientComboBox.SelectedItem == null || string.IsNullOrEmpty(NoteTextBox.Text))
            {
                MessageBox.Show("Заполните все поля.");
                return;
            }

            try
            {
                int patientId = (PatientComboBox.SelectedItem as Patient).ID;
                string noteText = NoteTextBox.Text.Trim();

                dbHelper.AddNote(patientId, noteText);
                MessageBox.Show("Заметка сохранена!");
                NoteTextBox.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении заметки: {ex.Message}");
            }
        }
    }
}