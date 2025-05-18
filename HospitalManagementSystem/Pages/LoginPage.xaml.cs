using System;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace HospitalManagementSystem.Pages
{
    public partial class LoginPage : Page
    {
        private DatabaseHelper dbHelper;
        private MainWindow mainWindow;

        public LoginPage(MainWindow mainWindow)
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            this.mainWindow = mainWindow;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string hashedPassword = HashPassword(password);

            string query = "SELECT Role FROM Users WHERE Username = @Username AND Password = @Password";
            using (SQLiteConnection connection = dbHelper.GetConnection())
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", hashedPassword);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        string role = result.ToString();
                        mainWindow.SetAuthenticated(true, role); // Передаем роль пользователя  
                        switch (role)
                        {
                            case "Registrar":
                                NavigationService.Navigate(new RegistrarHomePage());
                                break;
                            case "Doctor":
                                NavigationService.Navigate(new DoctorHomePage());
                                break;
                            case "Nurse":
                                NavigationService.Navigate(new NurseHomePage());
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль.");
                    }
                }
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage(mainWindow));
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