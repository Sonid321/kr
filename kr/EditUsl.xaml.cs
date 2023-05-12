using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для EditUsl.xaml
    /// </summary>
    public partial class EditUsl : Window
    {
        VidUslug _client;
        Window1 _window1;
        PraktikaEntities database;
        public EditUsl(VidUslug vidUslug, Window1 window1)
        {
            InitializeComponent();
            _client = vidUslug;
            _window1 = window1;
            database = _window1.praktika;
            DataContext = _client;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Group.ItemsSource = database.Gruppa.ToList();
            Group.SelectedIndex = _client.Код_группы is null ? -1 : (int)_client.Код_группы -1;
        }

        private void ValidateImput()
        {
            if (Group.SelectedIndex == -1)
            {
                _client.Код_группы = null;
            }
            else
            {
                _client.Код_группы = Group.SelectedIndex + 1;
            }
            //
            _client.Наименование_услуги = String.IsNullOrWhiteSpace(login_Copy4.Text) ? string.Empty : ((login_Copy4.Text, @"\d+").Text);
            _client.Объем_работ = String.IsNullOrWhiteSpace(Time.Text) ? string.Empty : ((Time.Text, @"\d+").Text);
            _client.Базовая_стоимость = String.IsNullOrWhiteSpace(Stoim.Text) ? 0 : decimal.Parse(Regex.Match(Stoim.Text, @"\d+").Value);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ValidateImput();
            database.SaveChanges();
            _window1.ReadData3();
            Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
