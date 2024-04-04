using ExamPractice.Core;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace ExamPractice.ContentWindows
{
    /// <summary>
    /// Логика взаимодействия для NewUserWindow.xaml
    /// </summary>
    public partial class NewUserWindow : Window
    {
        Action LoadUsersGlobal;

        public NewUserWindow(Action loadUsersGlobal)
        {
            InitializeComponent();

            UserRolesList.ItemsSource = DBCon.DB.Roles.ToList();
        }

        private void AddUserRequest(object sender, RoutedEventArgs e)
        {
            Users newUser = new Users();
            newUser.Login = LoginField.Text;
            newUser.Password = PasswordField.Password;
            newUser.User_Name = NameField.Text;
            newUser.User_Role = (UserRolesList.SelectedItem as Roles).ID_Role;

            DBCon.DB.Users.Add(newUser);
            DBCon.DB.SaveChanges();
            MessageBox.Show("Пользователь успешно добавлен!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadUsersGlobal();
        }
    }
}
