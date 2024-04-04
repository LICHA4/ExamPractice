using ExamPractice.Core;
using System.Linq;
using System.Windows;

namespace ExamPractice.ContentWindows
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void AuthorizationRequest(object sender, RoutedEventArgs e)
        {
            string login = LoginField.Text;
            string password = PasswordField.Password;
            var user = DBCon.DB.Users.Where(u => u.Login == login && u.Password == password).FirstOrDefault();
            

            if (user != null)
            {
                switch (user.User_Role)
                {
                    case 1: //Администратор
                        MainWindow admWin = new MainWindow(); 
                        admWin.Show();
                        Close();
                        break;
                    case 2: //Менеджер
                        ManagerWindow managerWindow = new ManagerWindow();
                        managerWindow.Show();
                        Close();
                        break;
                    case 3: //Техник
                        TechnicianWindow techWin = new TechnicianWindow();
                        techWin.Show();
                        Close();
                        break;
                }
            }
            else { MessageBox.Show("Такой пользователь не найден!","Ошибка доступа", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
