using ExamPractice.Core;
using System;
using System.Linq;
using System.Windows;

namespace ExamPractice.ContentWindows
{
    /// <summary>
    /// Логика взаимодействия для WindowEditRequest.xaml
    /// </summary>
    public partial class WindowEditRequest : Window
    {
        Order OrderGlobal;

        Action LoadOrderGlobal;

        public WindowEditRequest(Action loadOrder, Order Order)
        {
            InitializeComponent();

            LoadOrderGlobal = loadOrder;

            OrderGlobal = Order;

            BindingRequest.DataContext = Order;

            TechniciansList.SelectedItem = Order.Technician;
            OrderStatusesList.SelectedItem = Order.OrderStatus;

            LoadTechniciansToList();
            LoadOrderStatusesToList();

            DeviceModelField.Text = Order.DeviceModel;
            DeviceTypeField.Text = Order.DeviceType;
            DescriptionField.Text = Order.Description;
            ClientNameField.Text = Order.ClientName;
            ClientPhoneField.Text = Order.ClientPhone;

            TechniciansList.SelectedItem = Order.Technicians;
            OrderStatusesList.SelectedItem = Order.OrderStatuses;
        }

        private void LoadTechniciansToList()
        {
            TechniciansList.ItemsSource = DBCon.DB.Technicians.ToList();
        }

        private void LoadOrderStatusesToList()
        {
            OrderStatusesList.ItemsSource = DBCon.DB.OrderStatuses.ToList();
        }

        private void EditOrderConfirm(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(DeviceTypeField.Text))
            {
                MessageBox.Show("Вы не указали тип устройства", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                DeviceTypeField.Focus();
                return;
            }

            if (string.IsNullOrEmpty(DeviceModelField.Text))
            {
                MessageBox.Show("Вы не указали модель устройства", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                DeviceModelField.Focus();
                return;
            }

            if (string.IsNullOrEmpty(DescriptionField.Text))
            {
                MessageBox.Show("Вы не ввели описание проблемы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                DescriptionField.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ClientNameField.Text))
            {
                MessageBox.Show("Вы не указали Имя клиента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                ClientNameField.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ClientPhoneField.Text))
            {
                MessageBox.Show("Вы не указали номер телефон клиента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                ClientPhoneField.Focus();
                return;
            }

            if (string.IsNullOrEmpty(TechniciansList.Text))
            {
                MessageBox.Show("Вы не указали Техника", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                TechniciansList.Focus();
                return;
            }

            if (string.IsNullOrEmpty(OrderStatusesList.Text))
            {
                MessageBox.Show("Вы не указали статус заявки", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                OrderStatusesList.Focus();
                return;
            }

            OrderGlobal.DeviceModel = DeviceModelField.Text;
            OrderGlobal.DeviceType = DeviceTypeField.Text;
            OrderGlobal.Description = DescriptionField.Text;
            OrderGlobal.ClientName = ClientNameField.Text;
            OrderGlobal.ClientPhone = ClientPhoneField.Text;
            OrderGlobal.Technician = (TechniciansList.SelectedItem as Technicians).ID_Technician;
            OrderGlobal.OrderStatus = (OrderStatusesList.SelectedItem as OrderStatuses).ID_Status;

            DBCon.DB.SaveChanges();
            MessageBox.Show("Заявка успешно изменена", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);

            LoadOrderGlobal();
            Close();
        } //Подтверждение редактирования
    }
}
