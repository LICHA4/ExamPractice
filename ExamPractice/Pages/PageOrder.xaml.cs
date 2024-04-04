using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ExamPractice.ContentWindows;
using ExamPractice.Core;
using System.Threading;
using System.Windows.Documents;

namespace ExamPractice.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageOrder.xaml
    /// </summary>
    public partial class PageOrder : Page
    {
        public PageOrder()
        {
            InitializeComponent();
            LoadOrder();

            ResetDatePickers.IsEnabled = false;
        }

        private void DateSort()
        {
            DateTime? begin = BeginDatePicker.SelectedDate;
            DateTime? end = EndDatePicker.SelectedDate;

            if (BeginDatePicker.SelectedDate == null || EndDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Выберите начало и конец сортировки!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                var dateSort = DBCon.DB.Order.Where(p => p.DateStart >= begin & p.DateStart <= end).ToList();
                OrderList.ItemsSource = dateSort;
                ResetDatePickers.IsEnabled = true;
            }
        }

        private void ResetDatePickersRequest(object sender, RoutedEventArgs e)
        {
            BeginDatePicker.SelectedDate = null;
            EndDatePicker.SelectedDate = null;
            LoadOrder();

            ResetDatePickers.IsEnabled = false;
        } //Очистка полей выбора даты и вывод списка по новой

        private void LoadOrder()
        {
            OrderList.ItemsSource = DBCon.DB.Order.ToList();
        } //Вывод списка заявок

        private void RemoveItemRequest(object sender, System.Windows.RoutedEventArgs e)
        {
            if (OrderList.SelectedItem == null)
            {
                MessageBox.Show("Заявка не выбрана", "Ошибка удаления!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Order item = OrderList.SelectedItem as Order;
            MessageBoxResult canRemove = MessageBox.Show("Удалить выбранную заявку?", "Удаление!", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            if (canRemove == MessageBoxResult.OK)
            {
                try
                {
                    DBCon.DB.Order.Remove(item);
                    DBCon.DB.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка удаления заявки: " + ex.Message, "Ошибка!", MessageBoxButton.OK);
                    return;
                }

                string device = string.Format("Заявка ({0}) {1} удалена", item.ID, item.DeviceModel);
                MessageBox.Show(device, "Успешно!", MessageBoxButton.OK);
                LoadOrder();
            }
        }

        private void AddOrderRequest(object sender, RoutedEventArgs e)
        {
            WindowCreateRequest window = new WindowCreateRequest(LoadOrder);
            window.Owner = Application.Current.MainWindow;
            window.ShowDialog();
        }

        private void EditOrderRequest(object sender, RoutedEventArgs e)
        {
            Order item = OrderList.SelectedItem as Order;

            if (item == null)
            {
                MessageBox.Show("Не выбрана заявка для редактирования!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            WindowEditRequest windowEditRequest = new WindowEditRequest(LoadOrder, item);

            windowEditRequest.Owner = Application.Current.MainWindow;
            windowEditRequest.ShowDialog();
        }

        private void DateSortRequest(object sender, RoutedEventArgs e)
        {
            DateSort();
        }

        private void EndDatePickerChanged(object sender, SelectionChangedEventArgs e)
        {
            ResetDatePickers.IsEnabled = true;
        }

        private void BeginDatePickerChanged(object sender, SelectionChangedEventArgs e)
        {
            ResetDatePickers.IsEnabled = true;
        }

        private void SearchFieldValueChanged(object sender, TextChangedEventArgs e)
        {
            DateTime? begin = BeginDatePicker.SelectedDate;
            DateTime? end = EndDatePicker.SelectedDate;

            var search = SearchField.Text;

            var orderFoundClientName = DBCon.DB.Order.Where
                (p => p.ClientName.Contains(search) ||
                      p.ClientPhone.Contains(search) ||
                      p.DeviceModel.Contains(search) ||
                      p.DeviceType.Contains(search) ||
                      p.Description.Contains(search) ||
                      p.Technicians.TechnicianFIO.Contains(search) ).ToList();

            if (orderFoundClientName != null)
            {
                OrderList.ItemsSource = orderFoundClientName;
            }
            else
            {
                MessageBox.Show("Заказ не найден", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void SortingListChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FilterOptionsList.SelectedIndex == 0)
            {
                var dateSort = DBCon.DB.Order.OrderBy(p => p.DateStart).ToList();
                OrderList.ItemsSource = dateSort;

            }
            else if (FilterOptionsList.SelectedIndex == 1)
            {
                var dateSort = DBCon.DB.Order.OrderByDescending(p => p.DateStart).ToList();
                OrderList.ItemsSource = dateSort;
            }
        }
    }
}
