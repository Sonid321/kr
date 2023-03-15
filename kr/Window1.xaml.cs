using MaterialDesignThemes.Wpf.Internal;
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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
       public PraktikaEntities praktika { get; set; }
        public Window1()
        {
            praktika = new PraktikaEntities();
            InitializeComponent();
            Grida.ItemsSource = praktika.Jurnal_Ucheta.ToList();
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            praktika = new PraktikaEntities();
            Jurnal_Ucheta JurUc = Grida.SelectedItem as Jurnal_Ucheta;
            Jurnal_Ucheta JurUc1 = praktika.Jurnal_Ucheta.Where(x => x.Код_записи == JurUc.Код_записи).Single();
            Window2 edit = new Window2(JurUc1.Код_записи, this);
            edit.Show();
        }

        private void delClick(object sender, RoutedEventArgs e)
        {
            var dea = Grida.SelectedItem as Jurnal_Ucheta;
            if (dea == null)
            {
                MessageBox.Show("Выберите строку", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            praktika.Jurnal_Ucheta.Remove(dea);
            praktika.SaveChanges();
            Grida.ItemsSource = praktika.Jurnal_Ucheta.ToList();
            MessageBox.Show("Данные удалены");
        }
        private void Filtr(object sender, RoutedEventArgs e)
        {
            DateTime? left = After.DisplayDate;
            DateTime? right = Before.DisplayDate;
            var list = praktika.Jurnal_Ucheta.Where(x => x.Дата_подписания > left && x.Дата_подписания < right).ToList();
            Grida.ItemsSource = null;
            Grida.ItemsSource = list;
        }

        private void Filtr1(object sender, RoutedEventArgs e)
        {
            //After.ClearValue(UidProperty);
            Grida.ItemsSource = praktika.Jurnal_Ucheta.ToList();
        }

        private void Addcl(object sender, RoutedEventArgs e)
        {
            var Dob1 = new AddG(praktika);
            Dob1.Show();
            Grida.ItemsSource = praktika.Jurnal_Ucheta.ToList();
            Grida.Items.Refresh();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var input = (sender as TextBox).Text.ToLower();
            if (!(string.IsNullOrEmpty(input)))
            {
                int resultCount = praktika.Jurnal_Ucheta.Count(x => x.Client.ФИО.Contains(input));
                Grida.ItemsSource = praktika.Jurnal_Ucheta.Where(x => x.Client.ФИО.Contains(input)).ToList();
            }
            else
            {
                Grida.ItemsSource = praktika.Jurnal_Ucheta.ToList();
            }
        }


    }
}
