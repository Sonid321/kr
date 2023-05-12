using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для EditCli.xaml
    /// </summary>
    public partial class EditCli : Window
    {
        Client _client;
        Window1 _window1;
        PraktikaEntities database;
        public EditCli(Client client, Window1 window1)
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
            if (Uslu.SelectedIndex == -1)
            {
                _client.Код_района = null;
            }
            else
            {
                _client.Код_района = Uslu.SelectedIndex + 1;
            }
            //
            _client.Наименование_предприятия = String.IsNullOrWhiteSpace(login_Copy4.Text) ? string.Empty : ((login_Copy.Text, @"\d+").Text);
            _client.Адрес = String.IsNullOrWhiteSpace(login_Copy.Text) ? string.Empty : ((login_Copy.Text, @"\d+").Text);
            _client.ФИО = String.IsNullOrWhiteSpace(login_Copy6.Text) ? string.Empty : ((login_Copy6.Text, @"\d+").Text);
            _client.Телефон = String.IsNullOrEmpty(login_Copy1.Text) ? 0 : decimal.Parse((login_Copy1.Text, @"\d+").Text) ;
            _client.Должность = String.IsNullOrWhiteSpace(login_Copy2.Text) ? string.Empty : ((login_Copy2.Text, @"\d+").Text);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ValidateImput();
            database.SaveChanges();
            _window1.ReadData1();
            Hide();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Uslu.ItemsSource = database.Raion.ToList();
            Uslu.SelectedIndex = _client.Код_района is null ? -1 : (int)_client.Код_района;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
