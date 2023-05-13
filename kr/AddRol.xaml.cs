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

namespace kr
{
    /// <summary>
    /// Логика взаимодействия для AddRol.xaml
    /// </summary>
    public partial class AddRol : Window
    {
        User _client;
        PraktikaEntities praktika;
        Rol _window1;
        public AddRol(Rol window1)
        {
            InitializeComponent(); 
            this._window1 = window1;
            _client = new User();
            praktika = _window1.praktika;
            this.DataContext = _client;
        }


        public AddRol(PraktikaEntities praktika, User client)
        {
            InitializeComponent();
            this.praktika = praktika;
            this.DataContext = client;
            this.Ro.ItemsSource = praktika.Roles.ToList();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            User JC = new User();
            var sot = Ro.SelectedItem as Roles;
            JC.Role = sot.ID_Roles;
            JC.FIO = Convert.ToString(log.Text);
            JC.Password = Convert.ToString(pas.Text);
            MessageBox.Show("Запись успешно добавлена!");
            try
            {
                praktika.User.Add(JC);
                praktika.SaveChanges();
                _window1.udat();
                Hide();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            this.Ro.ItemsSource = praktika.Roles.ToList();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
