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
    /// Логика взаимодействия для AddUsl.xaml
    /// </summary>
    public partial class AddUsl : Window
    {
        Window1 _window1;
        VidUslug _vidUsl;
        PraktikaEntities praktika;
        public AddUsl( Window1 window1 )
        {
            InitializeComponent();
            this._window1 = window1;
            _vidUsl = new VidUslug();
            praktika = _window1.praktika;
            this.DataContext = _vidUsl;
        }
        public AddUsl(PraktikaEntities praktika, VidUslug vidUslug)
        {
            InitializeComponent();
            this.praktika = praktika;
            this.DataContext = vidUslug;
            Group.ItemsSource = praktika.VidUslug.ToList();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Group.ItemsSource = praktika.Gruppa.ToList();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            VidUslug VD = new VidUslug();
            var group = Group.SelectedItem as Gruppa;
            VD.Код_группы = group.Код_группы;
            VD.Наименование_услуги = Convert.ToString(login_Copy4.Text);
            VD.Объем_работ = Convert.ToString(Time.Text);
            VD.Базовая_стоимость = Convert.ToDecimal(Stoim.Text);
            MessageBox.Show("Запись успешно добавлена!");
            try
            {
                praktika.VidUslug.Add(VD);
                praktika.SaveChanges();
                _window1.ReadData3();
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
    }
}
