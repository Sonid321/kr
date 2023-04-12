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
    /// Логика взаимодействия для AddSot.xaml
    /// </summary>
    public partial class AddSot : Window
    {
        Sotrudniki _sotrudniki;
        PraktikaEntities praktika;
        Window1 _window1;
        public AddSot(Window1 window1)
        {
            InitializeComponent();
            this._window1 = window1;
            _sotrudniki = new Sotrudniki();
            praktika = _window1.praktika;
            this.DataContext = _sotrudniki;
        }
        public AddSot(PraktikaEntities praktika, Sotrudniki sotrudniki)
        {
            InitializeComponent();
            this.praktika = praktika;
            this.DataContext = sotrudniki;
            this.Uslu.ItemsSource = praktika.Special.ToList();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Sotrudniki JC = new Sotrudniki();

            var sot = Uslu.SelectedItem as Special;
            JC.Код_специальности = sot.Код_спец;
            ////
            JC.ФИО = Convert.ToString(login_Copy4.Text);

            JC.Пол = Convert.ToString(login_Copy5.Text);

            JC.Телефон = Convert.ToDecimal(login_Copy7.Text);

            JC.Адрес = Convert.ToString(login_Copy.Text);

            JC.Документ = Convert.ToString(login_Copy6.Text);

            JC.Серия_Номер = Convert.ToDecimal(login_Copy1.Text);

            JC.Семейное_положение = Convert.ToString(login_Copy2.Text);

            JC.Квалификация = Convert.ToString(login_Copy3.Text);

            MessageBox.Show("Запись успешно добавлена!");
            try
            {
                praktika.Sotrudniki.Add(JC);
                praktika.SaveChanges();
                _window1.ReadData2();
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
            this.Uslu.ItemsSource = praktika.Special.ToList();
        }
    }
}
