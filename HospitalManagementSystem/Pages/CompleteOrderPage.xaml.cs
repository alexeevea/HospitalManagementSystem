using System;
using System.Windows;
using System.Windows.Controls;

namespace HospitalManagementSystem.Pages
{
    public partial class CompleteOrderPage : Page
    {
        private DatabaseHelper dbHelper;

        public CompleteOrderPage()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            LoadOrders();
        }

        private void LoadOrders()
        {
            OrderComboBox.ItemsSource = dbHelper.GetOrders();
        }

        private void CompleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (OrderComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите назначение.");
                return;
            }

            try
            {
                int orderId = (OrderComboBox.SelectedItem as Order).ID;
                dbHelper.CompleteOrder(orderId);
                MessageBox.Show("Назначение отмечено как выполненное!");
                LoadOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выполнении назначения: {ex.Message}");
            }
        }
    }
}