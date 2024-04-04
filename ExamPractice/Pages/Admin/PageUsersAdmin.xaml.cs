using ExamPractice.ContentWindows;
using ExamPractice.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

namespace ExamPractice.Pages.Admin
{
    public partial class PageUsersAdmin : Page
    {

        public PageUsersAdmin()
        {
            InitializeComponent();
            LoadUsers();;
        }

        public void LoadUsers()
        {
            List <Users> users = DBCon.DB.Users.ToList();
            UsersList.ItemsSource = users;
        }

        private void DeleteUserRequest(object sender, RoutedEventArgs e)
        {
            Users item = UsersList.SelectedItem as Users;

            if (item.User_Role == 1)
            {
                MessageBox.Show("Вы не можете удалить этого пользователя, так как пользователь является администратором!",
                    "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                string message = string.Format("Удалить пользователя {0}? Это действие нельзя будет отменить!", item.User_Name);
                if (MessageBox.Show(message, "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    DBCon.DB.Users.Remove(item);
                    DBCon.DB.SaveChanges();
                    string deleted = string.Format("Пользователь {0} успешно удален!", item.User_Name);
                    MessageBox.Show(deleted, "Успешно!", MessageBoxButton.OK);
                    LoadUsers();
                }
            }
        }

        private void AddNewUserRequest(object sender, RoutedEventArgs e)
        {
            NewUserWindow window = new NewUserWindow(LoadUsers);
            window.Owner = Application.Current.MainWindow;
            window.ShowDialog();
        }
    }
}
