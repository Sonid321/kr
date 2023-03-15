
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
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        PraktikaEntities praktika;
        public Window2(int Код_записи, Window1 window1)
        {
            praktika = new PraktikaEntities();
            InitializeComponent();
            var eA = praktika.Jurnal_Ucheta.Where(x => x.Код_записи == Код_записи).FirstOrDefault();
            DataContext = eA;
            Grida.ItemsSource = praktika.Sotrudniki.ToList();
            Grida1.ItemsSource = praktika.Client.ToList();
            Grida2.ItemsSource = praktika.VidUslug.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {       
            praktika.SaveChanges();           
            Close();
        }
    }
}
