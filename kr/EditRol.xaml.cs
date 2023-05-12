using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для EditRol.xaml
    /// </summary>
    public partial class EditRol : Window
    {
        Users _client;
        Rol _window1;
        PraktikaEntities database;
        public EditRol(Users client, Rol window1)
        {
            InitializeComponent();
            _client = client;
            _window1 = window1;
            database = _window1.praktika;
            DataContext = _client;
        }
        private void ValidateImput()
        {
            //
            if (Ro.SelectedIndex == -1)
            {
                _client.Role = null;
            }
            else
            {
                _client.Role = Ro.SelectedIndex + 1;
            }
            //
            _client.FIO = String.IsNullOrWhiteSpace(log.Text) ? string.Empty : ((log.Text, @"\d+").Text);
            _client.Password = String.IsNullOrWhiteSpace(pas.Text) ? string.Empty : ((pas.Text, @"\d+").Text);
         
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            Ro.ItemsSource = database.Roules.ToList();
            Ro.SelectedIndex = _client.Role is null ? -1 : (int)_client.Role;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ValidateImput();
            database.SaveChanges();
            _window1.udat();
            Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
