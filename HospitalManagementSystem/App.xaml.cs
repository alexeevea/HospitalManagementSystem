using System.Data.SQLite;
using System.Windows;

namespace HospitalManagementSystem
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            string connectionString = "Data Source=hospital.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Таблица Users
                string createUsersTable = @"
                    CREATE TABLE IF NOT EXISTS Users (
                        ID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Username TEXT NOT NULL UNIQUE,
                        Password TEXT NOT NULL,
                        Role TEXT NOT NULL
                    )";

                // Таблица Patients
                string createPatientsTable = @"
                    CREATE TABLE IF NOT EXISTS Patients (
                        ID INTEGER PRIMARY KEY AUTOINCREMENT,
                        FullName TEXT NOT NULL,
                        BirthDate TEXT NOT NULL,
                        ContactInfo TEXT NOT NULL
                    )";

                // Таблица Doctors
                string createDoctorsTable = @"
                    CREATE TABLE IF NOT EXISTS Doctors (
                        ID INTEGER PRIMARY KEY AUTOINCREMENT,
                        FullName TEXT NOT NULL,
                        Specialty TEXT NOT NULL
                    )";

                // Таблица Nurses
                string createNursesTable = @"
                    CREATE TABLE IF NOT EXISTS Nurses (
                        ID INTEGER PRIMARY KEY AUTOINCREMENT,
                        FullName TEXT NOT NULL
                    )";

                // Таблица Appointments
                string createAppointmentsTable = @"
                    CREATE TABLE IF NOT EXISTS Appointments (
                        ID INTEGER PRIMARY KEY AUTOINCREMENT,
                        PatientID INTEGER NOT NULL,
                        DoctorID INTEGER NOT NULL,
                        DateTime TEXT NOT NULL,
                        FOREIGN KEY (PatientID) REFERENCES Patients(ID),
                        FOREIGN KEY (DoctorID) REFERENCES Doctors(ID)
                    )";

                // Таблица MedicalRecords
                string createMedicalRecordsTable = @"
                    CREATE TABLE IF NOT EXISTS MedicalRecords (
                        ID INTEGER PRIMARY KEY AUTOINCREMENT,
                        AppointmentID INTEGER NOT NULL,
                        Description TEXT NOT NULL,
                        FOREIGN KEY (AppointmentID) REFERENCES Appointments(ID)
                    )";

                // Таблица Orders
                string createOrdersTable = @"
                    CREATE TABLE IF NOT EXISTS Orders (
                        ID INTEGER PRIMARY KEY AUTOINCREMENT,
                        AppointmentID INTEGER NOT NULL,
                        ExaminationType TEXT NOT NULL,
                        IsCompleted INTEGER NOT NULL DEFAULT 0,
                        FOREIGN KEY (AppointmentID) REFERENCES Appointments(ID)
                    )";

                // Таблица Prescriptions
                string createPrescriptionsTable = @"
                    CREATE TABLE IF NOT EXISTS Prescriptions (
                        ID INTEGER PRIMARY KEY AUTOINCREMENT,
                        AppointmentID INTEGER NOT NULL,
                        PrescriptionText TEXT NOT NULL,
                        FOREIGN KEY (AppointmentID) REFERENCES Appointments(ID)
                    )";

                // Таблица Notes
                string createNotesTable = @"
                    CREATE TABLE IF NOT EXISTS Notes (
                        ID INTEGER PRIMARY KEY AUTOINCREMENT,
                        PatientID INTEGER NOT NULL,
                        NoteText TEXT NOT NULL,
                        FOREIGN KEY (PatientID) REFERENCES Patients(ID)
                    )";

                // Таблица Results
                string createResultsTable = @"
                    CREATE TABLE IF NOT EXISTS Results (
                        ID INTEGER PRIMARY KEY AUTOINCREMENT,
                        PatientID INTEGER NOT NULL,
                        OrderID INTEGER NOT NULL,
                        ResultText TEXT NOT NULL,
                        FOREIGN KEY (PatientID) REFERENCES Patients(ID),
                        FOREIGN KEY (OrderID) REFERENCES Orders(ID)
                    )";

                // Создание таблиц
                string[] createTableQueries = {
                    createUsersTable, createPatientsTable, createDoctorsTable, createNursesTable,
                    createAppointmentsTable, createMedicalRecordsTable, createOrdersTable,
                    createPrescriptionsTable, createNotesTable, createResultsTable
                };

                foreach (var query in createTableQueries)
                {
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}