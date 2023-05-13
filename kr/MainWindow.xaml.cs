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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace kr
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        PraktikaEntities HS { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            HS= new PraktikaEntities();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Role roles = new Role();
            var login = login1.Text;
            var pass1 = pwt.Password;
            if (HS.User.Any(u => u.FIO == login && u.Password == pass1))
            {
                foreach (var client in HS.User)
                {
                    if (client.FIO == login)
                    {
                        if (client.Password == pass1)
                        {
                            var role = HS.Roles.Find(client.Role);
                            roles.UserLogin = login;
                            roles.UserRole = role.Role;
                            this.Visibility = Visibility.Collapsed;
                            Window1 administratorWindow = new Window1(roles.UserLogin, roles.UserRole);
                            administratorWindow.Show();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Проверьте вводимые данные");
            }
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void helpMe(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Обратитесь к системному администратору за помощью...");
        }
    }
}
