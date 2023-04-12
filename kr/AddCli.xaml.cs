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
    /// Логика взаимодействия для AddCli.xaml
    /// </summary>
    public partial class AddCli : Window
    {
        Client _client;
        PraktikaEntities praktika;
        Window1 _window1;
        public AddCli(Window1 window1)
        {
            InitializeComponent();
            this._window1 = window1;
            _client = new Client();
            praktika = _window1.praktika;
            this.DataContext = _client;
        }


        public AddCli(PraktikaEntities praktika, Client client)
        {
            InitializeComponent();
            this.praktika = praktika;
            this.DataContext = client;
            this.Uslu.ItemsSource = praktika.Raion.ToList();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Client JC = new Client();
            var sot = Uslu.SelectedItem as Raion;
            JC.Код_района = sot.Код_района;
            JC.Адрес = Convert.ToString(login_Copy.Text);
            JC.ФИО = Convert.ToString(login_Copy6.Text);
            JC.Наименование_предприятия = Convert.ToString(login_Copy4.Text);
            JC.Телефон = Convert.ToDecimal(login_Copy1.Text);
            JC.Должность = Convert.ToString(login_Copy2.Text);
            JC.Код_плательщика = Convert.ToInt32(login_Copy3.Text);
            MessageBox.Show("Запись успешно добавлена!");
            try
            {
                praktika.Client.Add(JC);
                praktika.SaveChanges();
                _window1.ReadData1();
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
            this.Uslu.ItemsSource = praktika.Raion.ToList();
        }
    }
}
