
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
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        Jurnal_Ucheta _Jurnal_Ucheta;
        Window1 _window1;
        PraktikaEntities database;
        public Window2(Jurnal_Ucheta jurnal_Ucheta, Window1 window1)
        {
            InitializeComponent();
            _Jurnal_Ucheta = jurnal_Ucheta;
            _window1 = window1;
            database = _window1.praktika;
            DataContext = _Jurnal_Ucheta;
        }
        private void ValidateImput()
        {
            ///////
            if (Uslu.SelectedIndex == -1)
            {
                _Jurnal_Ucheta.Код_услуги = null;
            }
            else
            {
                _Jurnal_Ucheta.Код_услуги = Uslu.SelectedIndex + 1;
            }
            /////
            if (klient.SelectedIndex == -1)
            {
                _Jurnal_Ucheta.Код_клиента = null;
            }
            else
            {
                _Jurnal_Ucheta.Код_клиента = klient.SelectedIndex + 1;
            }
            ///////
            if (Sotr1.SelectedIndex == -1)
            {
                _Jurnal_Ucheta.Код_сотрудника = null;
            }
            else
            {
                _Jurnal_Ucheta.Код_сотрудника = Sotr1.SelectedIndex + 1;
            }
            _Jurnal_Ucheta.Дата_подписания = String.IsNullOrEmpty(DataPic.Text) ? DateTime.Now : DateTime.Parse(DataPic.Text);
            _Jurnal_Ucheta.Номер_договора = String.IsNullOrEmpty(login_Copy.Text) ? 0 : int.Parse(Regex.Match(login_Copy.Text, @"\d+").Value);
            _Jurnal_Ucheta.Комер_акта_накладной_ = String.IsNullOrEmpty(login_Copy1.Text) ? 0 : int.Parse(Regex.Match(login_Copy1.Text, @"\d+").Value);
            _Jurnal_Ucheta.Стоимость = String.IsNullOrEmpty(login_Copy2.Text) ? 0 : int.Parse(Regex.Match(login_Copy2.Text, @"\d+").Value);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ValidateImput();
            database.SaveChanges();
            _window1.ReadData();
            Hide();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Sotr1.ItemsSource = database.Sotrudniki.ToList();
            Sotr1.SelectedIndex = _Jurnal_Ucheta.Код_сотрудника is null ? -1 : (int)_Jurnal_Ucheta.Код_сотрудника - 1;

            klient.ItemsSource = database.Client.ToList();
            klient.SelectedIndex = _Jurnal_Ucheta.Код_клиента is null ? -1 : (int)_Jurnal_Ucheta.Код_клиента - 1;

            Uslu.ItemsSource = database.VidUslug.ToList();
            Uslu.SelectedIndex = _Jurnal_Ucheta.Код_услуги is null ? -1 : (int)_Jurnal_Ucheta.Код_услуги - 1;
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
