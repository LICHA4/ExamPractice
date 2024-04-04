using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExamPractice
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenPageOrder(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Pages.PageOrder());
        }

        private void OpenPageMaster(object sender, RoutedEventArgs e)
        {

        }

        private void OpenPageHardware(object sender, RoutedEventArgs e)
        {

        }

        private void OpenPageComments(object sender, RoutedEventArgs e)
        {

        }

        private void ExitApp(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OpenPageUsers(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Pages.Admin.PageUsersAdmin());
        }
    }
}
