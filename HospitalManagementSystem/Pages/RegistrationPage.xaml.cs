using System;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace HospitalManagementSystem.Pages
{
    public partial class RegistrationPage : Page
    {
        private DatabaseHelper dbHelper;
        private MainWindow mainWindow;

        public RegistrationPage(MainWindow mainWindow = null)
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            this.mainWindow = mainWindow;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string role = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            string hashedPassword = HashPassword(password);

            string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
            using (SQLiteConnection connection = dbHelper.GetConnection())
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(checkQuery, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    if (count > 0)
                    {
                        MessageBox.Show("Логин уже существует.");
                        return;
                    }
                }

                string insertQuery = "INSERT INTO Users (Username, Password, Role) VALUES (@Username, @Password, @Role)";
                using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", hashedPassword);
                    command.Parameters.AddWithValue("@Role", role);
                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Регистрация успешна. Пожалуйста, войдите.");
            NavigationService.Navigate(new LoginPage(mainWindow));
        }

        private void BackToLoginButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage(mainWindow));
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}