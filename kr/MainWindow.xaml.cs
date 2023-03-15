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

        private void CheckBox_Checked (object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox.IsChecked.Value)
            {
                text.Text = pwt.Password; // скопируем в TextBox из PasswordBox
                text.Visibility = Visibility.Visible; // TextBox - отобразить
                pwt.Visibility = Visibility.Hidden; // PasswordBox - скрыть
            }
            else
            {
                pwt.Password = text.Text; // скопируем в PasswordBox из TextBox 
                text.Visibility = Visibility.Hidden; // TextBox - скрыть
                pwt.Visibility = Visibility.Visible; // PasswordBox - отобразить
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                var userAuth = HS.Users.FirstOrDefault(x => x.Login == login.Text
                && x.Password == pwt.Password || x.Password == text.Text);
                if (userAuth == null)
                {
                    MessageBox.Show("Неверный логин или пароль попробуйте снова", "Ошибка авторизации",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    switch (userAuth.Role)
                    {
                        case 1:
                            MessageBox.Show("Добрый день" + " " + userAuth.FIO);
                            Window1 f1 = new Window1();
                            f1.Show();
                            Close();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex.Message.ToString());
            }
        }
    }
}
