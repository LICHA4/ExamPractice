using ExamPractice.Core;
using System.Linq;
using System.Windows;
using System;
using ExamPractice.Pages;

namespace ExamPractice.ContentWindows
{
    /// <summary>
    /// Логика взаимодействия для WindowCreateRequest.xaml
    /// </summary>
    public partial class WindowCreateRequest : Window
    {
        Action LoadOrderGlobal;

        public WindowCreateRequest(Action loadOrderGlobal)
        {
            InitializeComponent();
            LoadTechniciansToList();
            LoadOrderGlobal = loadOrderGlobal;
        }

        public void LoadTechniciansToList()
        {
            TechniciansList.ItemsSource = DBCon.DB.Technicians.ToList();
        }

        private void AddOrderConfirm(object sender, RoutedEventArgs e)
        {
            Order newOrder = new Order
            {
                DeviceType = DeviceTypeField.Text,
                OrderStatus = 1,
                DeviceModel = DeviceModelField.Text,
                Description = DescriptionField.Text,
                Technician = (TechniciansList.SelectedItem as Technicians).ID_Technician,
                ClientPhone = ClientPhoneField.Text,
                DateStart = System.DateTime.Today,
                ClientName = ClientNameField.Text,
            };

            
            try
            {
                DBCon.DB.Order.Add(newOrder);
                DBCon.DB.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось добавить заявку! Ошибка: " + ex.Message, "Ошибка добавления!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult notified = MessageBox.Show("Заявка успешно добавлена", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);

            if (notified == MessageBoxResult.OK)
            {
                LoadOrderGlobal();
                this.Close();
            }
        }
    }
}
