using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HospitalManagementSystem.Pages
{
    public partial class ViewOrdersPage : Page
    {
        private DatabaseHelper dbHelper;

        public ViewOrdersPage()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            LoadOrders();
        }

        private void LoadOrders()
        {
            OrdersListView.ItemsSource = dbHelper.GetOrders();
        }
    }

    public class BooleanToStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? "Выполнено" : "Не выполнено";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}