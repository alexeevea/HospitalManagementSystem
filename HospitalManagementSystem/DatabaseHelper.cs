using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using OfficeOpenXml;

namespace HospitalManagementSystem
{
    public class DatabaseHelper
    {
        private readonly string connectionString = "Data Source=hospital.db;Version=3;";

        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }

        public void AddPatient(string fullName, string birthDate, string contactInfo)
        {
            string query = "INSERT INTO Patients (FullName, BirthDate, ContactInfo) VALUES (@FullName, @BirthDate, @ContactInfo)";
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FullName", fullName);
                    command.Parameters.AddWithValue("@BirthDate", birthDate);
                    command.Parameters.AddWithValue("@ContactInfo", contactInfo);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeletePatient(int id)
        {
            string query = "DELETE FROM Patients WHERE ID = @ID";
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Patient> GetPatients()
        {
            List<Patient> patients = new List<Patient>();
            string query = "SELECT * FROM Patients";
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            patients.Add(new Patient
                            {
                                ID = reader.GetInt32(0),
                                FullName = reader.GetString(1),
                                BirthDate = reader.GetString(2),
                                ContactInfo = reader.GetString(3)
                            });
                        }
                    }
                }
            }
            return patients;
        }

        public List<Doctor> GetDoctors()
        {
            List<Doctor> doctors = new List<Doctor>();
            string query = "SELECT * FROM Doctors";
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            doctors.Add(new Doctor
                            {
                                ID = reader.GetInt32(0),
                                FullName = reader.GetString(1),
                                Specialty = reader.GetString(2)
                            });
                        }
                    }
                }
            }
            return doctors;
        }

        public List<Nurse> GetNurses()
        {
            List<Nurse> nurses = new List<Nurse>();
            string query = "SELECT * FROM Nurses";
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            nurses.Add(new Nurse
                            {
                                ID = reader.GetInt32(0),
                                FullName = reader.GetString(1)
                            });
                        }
                    }
                }
            }
            return nurses;
        }

        public void AddAppointment(int patientId, int doctorId, string dateTime)
        {
            string query = "INSERT INTO Appointments (PatientID, DoctorID, DateTime) VALUES (@PatientID, @DoctorID, @DateTime)";
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PatientID", patientId);
                    command.Parameters.AddWithValue("@DoctorID", doctorId);
                    command.Parameters.AddWithValue("@DateTime", dateTime);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateAppointment(int id, int patientId, int doctorId, string dateTime)
        {
            string query = "UPDATE Appointments SET PatientID = @PatientID, DoctorID = @DoctorID, DateTime = @DateTime WHERE ID = @ID";
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@PatientID", patientId);
                    command.Parameters.AddWithValue("@DoctorID", doctorId);
                    command.Parameters.AddWithValue("@DateTime", dateTime);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteAppointment(int id)
        {
            string query = "DELETE FROM Appointments WHERE ID = @ID";
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Appointment> GetAppointments()
        {
            List<Appointment> appointments = new List<Appointment>();
            string query = "SELECT * FROM Appointments";
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            appointments.Add(new Appointment
                            {
                                ID = reader.GetInt32(0),
                                PatientID = reader.GetInt32(1),
                                DoctorID = reader.GetInt32(2),
                                DateTime = reader.GetString(3)
                            });
                        }
                    }
                }
            }
            return appointments;
        }

        public List<Appointment> GetAppointmentsByDoctor(int doctorId)
        {
            List<Appointment> appointments = new List<Appointment>();
            string query = "SELECT * FROM Appointments WHERE DoctorID = @DoctorID";
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DoctorID", doctorId);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            appointments.Add(new Appointment
                            {
                                ID = reader.GetInt32(0),
                                PatientID = reader.GetInt32(1),
                                DoctorID = reader.GetInt32(2),
                                DateTime = reader.GetString(3)
                            });
                        }
                    }
                }
            }
            return appointments;
        }

        public void AddMedicalRecord(int appointmentId, string description)
        {
            string query = "INSERT INTO MedicalRecords (AppointmentID, Description) VALUES (@AppointmentID, @Description)";
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AppointmentID", appointmentId);
                    command.Parameters.AddWithValue("@Description", description);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddOrder(int appointmentId, string examinationType)
        {
            string query = "INSERT INTO Orders (AppointmentID, ExaminationType, IsCompleted) VALUES (@AppointmentID, @ExaminationType, 0)";
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AppointmentID", appointmentId);
                    command.Parameters.AddWithValue("@ExaminationType", examinationType);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void CompleteOrder(int orderId)
        {
            string query = "UPDATE Orders SET IsCompleted = 1 WHERE ID = @ID";
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", orderId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();
            string query = "SELECT * FROM Orders";
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orders.Add(new Order
                            {
                                ID = reader.GetInt32(0),
                                AppointmentID = reader.GetInt32(1),
                                ExaminationType = reader.GetString(2),
                                IsCompleted = reader.GetInt32(3) == 1
                            });
                        }
                    }
                }
            }
            return orders;
        }

        public void AddPrescription(int appointmentId, string prescriptionText)
        {
            string query = "INSERT INTO Prescriptions (AppointmentID, PrescriptionText) VALUES (@AppointmentID, @PrescriptionText)";
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AppointmentID", appointmentId);
                    command.Parameters.AddWithValue("@PrescriptionText", prescriptionText);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddNote(int patientId, string noteText)
        {
            string query = "INSERT INTO Notes (PatientID, NoteText) VALUES (@PatientID, @NoteText)";
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PatientID", patientId);
                    command.Parameters.AddWithValue("@NoteText", noteText);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Note> GetNotesByPatient(int patientId)
        {
            List<Note> notes = new List<Note>();
            string query = "SELECT * FROM Notes WHERE PatientID = @PatientID";
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PatientID", patientId);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            notes.Add(new Note
                            {
                                ID = reader.GetInt32(0),
                                PatientID = reader.GetInt32(1),
                                NoteText = reader.GetString(2)
                            });
                        }
                    }
                }
            }
            return notes;
        }

        public List<MedicalRecord> GetMedicalRecordsByPatient(int patientId)
        {
            List<MedicalRecord> records = new List<MedicalRecord>();
            string query = @"
                SELECT mr.ID, mr.AppointmentID, mr.Description 
                FROM MedicalRecords mr 
                JOIN Appointments a ON mr.AppointmentID = a.ID 
                WHERE a.PatientID = @PatientID";
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PatientID", patientId);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            records.Add(new MedicalRecord
                            {
                                ID = reader.GetInt32(0),
                                AppointmentID = reader.GetInt32(1),
                                Description = reader.GetString(2)
                            });
                        }
                    }
                }
            }
            return records;
        }

        public List<Result> GetResultsByPatient(int patientId)
        {
            List<Result> results = new List<Result>();
            string query = "SELECT * FROM Results WHERE PatientID = @PatientID";
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PatientID", patientId);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(new Result
                            {
                                ID = reader.GetInt32(0),
                                PatientID = reader.GetInt32(1),
                                OrderID = reader.GetInt32(2),
                                ResultText = reader.GetString(3)
                            });
                        }
                    }
                }
            }
            return results;
        }

        public void ImportFromExcel(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                // Импорт Patients
                var patientsSheet = package.Workbook.Worksheets["Пациенты"];
                for (int row = 2; row <= patientsSheet.Dimension.Rows; row++)
                {
                    string fullName = patientsSheet.Cells[row, 2].Text;
                    string birthDate = patientsSheet.Cells[row, 3].Text;
                    string contactInfo = patientsSheet.Cells[row, 4].Text;
                    AddPatient(fullName, birthDate, contactInfo);
                }

                // Импорт Doctors
                var doctorsSheet = package.Workbook.Worksheets["Врачи"];
                for (int row = 2; row <= doctorsSheet.Dimension.Rows; row++)
                {
                    string fullName = doctorsSheet.Cells[row, 2].Text;
                    string specialty = doctorsSheet.Cells[row, 3].Text;
                    string query = "INSERT INTO Doctors (FullName, Specialty) VALUES (@FullName, @Specialty)";
                    using (SQLiteConnection connection = GetConnection())
                    {
                        connection.Open();
                        using (SQLiteCommand command = new SQLiteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@FullName", fullName);
                            command.Parameters.AddWithValue("@Specialty", specialty);
                            command.ExecuteNonQuery();
                        }
                    }
                }

                // Импорт Nurses
                var nursesSheet = package.Workbook.Worksheets["Медсестры"];
                for (int row = 2; row <= nursesSheet.Dimension.Rows; row++)
                {
                    string fullName = nursesSheet.Cells[row, 2].Text;
                    string query = "INSERT INTO Nurses (FullName) VALUES (@FullName)";
                    using (SQLiteConnection connection = GetConnection())
                    {
                        connection.Open();
                        using (SQLiteCommand command = new SQLiteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@FullName", fullName);
                            command.ExecuteNonQuery();
                        }
                    }
                }

                // Импорт Appointments
                var appointmentsSheet = package.Workbook.Worksheets["Приёмы"];
                for (int row = 2; row <= appointmentsSheet.Dimension.Rows; row++)
                {
                    int patientId = int.Parse(appointmentsSheet.Cells[row, 2].Text);
                    int doctorId = int.Parse(appointmentsSheet.Cells[row, 3].Text);
                    string dateTime = appointmentsSheet.Cells[row, 4].Text;
                    AddAppointment(patientId, doctorId, dateTime);
                }

                // Импорт MedicalRecords
                var medicalRecordsSheet = package.Workbook.Worksheets["Истории_болезни"];
                for (int row = 2; row <= medicalRecordsSheet.Dimension.Rows; row++)
                {
                    int appointmentId = int.Parse(medicalRecordsSheet.Cells[row, 2].Text);
                    string description = medicalRecordsSheet.Cells[row, 3].Text;
                    AddMedicalRecord(appointmentId, description);
                }

                // Импорт Orders
                var ordersSheet = package.Workbook.Worksheets["Назначения"];
                for (int row = 2; row <= ordersSheet.Dimension.Rows; row++)
                {
                    int appointmentId = int.Parse(ordersSheet.Cells[row, 2].Text);
                    string examinationType = ordersSheet.Cells[row, 3].Text;
                    AddOrder(appointmentId, examinationType);
                }

                // Импорт Prescriptions
                var prescriptionsSheet = package.Workbook.Worksheets["Рецепты"];
                for (int row = 2; row <= prescriptionsSheet.Dimension.Rows; row++)
                {
                    int appointmentId = int.Parse(prescriptionsSheet.Cells[row, 2].Text);
                    string prescriptionText = prescriptionsSheet.Cells[row, 3].Text;
                    AddPrescription(appointmentId, prescriptionText);
                }

                // Импорт Notes
                var notesSheet = package.Workbook.Worksheets["Заметки"];
                for (int row = 2; row <= notesSheet.Dimension.Rows; row++)
                {
                    int patientId = int.Parse(notesSheet.Cells[row, 2].Text);
                    string noteText = notesSheet.Cells[row, 3].Text;
                    AddNote(patientId, noteText);
                }
            }
        }
    }

    public class Patient
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string BirthDate { get; set; }
        public string ContactInfo { get; set; }
    }

    public class Doctor
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Specialty { get; set; }
    }

    public class Nurse
    {
        public int ID { get; set; }
        public string FullName { get; set; }
    }

    public class Appointment
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public string DateTime { get; set; }
    }

    public class MedicalRecord
    {
        public int ID { get; set; }
        public int AppointmentID { get; set; }
        public string Description { get; set; }
    }

    public class Order
    {
        public int ID { get; set; }
        public int AppointmentID { get; set; }
        public string ExaminationType { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class Prescription
    {
        public int ID { get; set; }
        public int AppointmentID { get; set; }
        public string PrescriptionText { get; set; }
    }

    public class Note
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public string NoteText { get; set; }
    }

    public class Result
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public int OrderID { get; set; }
        public string ResultText { get; set; }
    }
}