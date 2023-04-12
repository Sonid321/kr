using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
    /// Логика взаимодействия для AddG.xaml
    /// </summary>
    public partial class AddG : Window
    {
        Jurnal_Ucheta _jurnal_Ucheta;
        PraktikaEntities praktika;
        Window1 _window1;
        public AddG(Window1 window1)
        {
            InitializeComponent();
            this._window1 = window1;
            _jurnal_Ucheta = new Jurnal_Ucheta();
            praktika = _window1.praktika;
            this.DataContext = _jurnal_Ucheta;

        }
        public AddG(PraktikaEntities praktika, Jurnal_Ucheta jurnal_Ucheta)
        {
            InitializeComponent();
            this.praktika = praktika;
            this.DataContext = jurnal_Ucheta;
            this.Clien.ItemsSource = praktika.Jurnal_Ucheta.ToList();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Jurnal_Ucheta JC = new Jurnal_Ucheta();
            var sot = Sotr1.SelectedItem as Sotrudniki;
            JC.Код_сотрудника = sot.КодCотрудника;
            var cli = Clien.SelectedItem as Client;
            JC.Код_клиента = cli.КодKлиента;
            var Ulug = Uslu.SelectedItem as VidUslug;
            JC.Код_услуги= Ulug.КодУслуги;
            JC.Номер_договора = Convert.ToInt32 (login_Copy.Text);
            JC.Комер_акта_накладной_= Convert.ToInt32(login_Copy1.Text);
            JC.Стоимость = Convert.ToInt32(login_Copy2.Text);
            JC.Код_плательщика = Convert.ToInt32(login_Copy3.Text);
            JC.Дата_подписания = DateTime.Parse(Convert.ToString(DataPic));
            MessageBox.Show("Запись успешно добавлена!");
            try
            {
                praktika.Jurnal_Ucheta.Add(JC);
                praktika.SaveChanges();
                _window1.ReadData();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Clien.ItemsSource = praktika.Client.ToList();
            this.Sotr1.ItemsSource = praktika.Sotrudniki.ToList();
            this.Uslu.ItemsSource = praktika.VidUslug.ToList();
        }
    }
}
