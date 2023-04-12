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
    /// Логика взаимодействия для EditSot.xaml
    /// </summary>
    public partial class EditSot : Window
    {
        Sotrudniki _sotrudniki;
        Window1 _window1;
        PraktikaEntities database;
        public EditSot(Sotrudniki sotrudniki, Window1 window1)
        {
            InitializeComponent();
            _sotrudniki = sotrudniki;
            _window1 = window1;
            database = _window1.praktika;
            DataContext = _sotrudniki;
        }

        private void ValidateImput()
        {
            ///////
            if (Uslu.SelectedIndex == -1)
            {
                _sotrudniki.Код_специальности = null;
            }
            else
            {
                _sotrudniki.Код_специальности = Uslu.SelectedIndex + 1;
            }
            /////           
          
            _sotrudniki.ФИО = String.IsNullOrWhiteSpace(login_Copy4.Text) ? string.Empty : ((login_Copy4.Text, @"\d+").Text);
            _sotrudniki.Пол = String.IsNullOrWhiteSpace(login_Copy5.Text) ? string.Empty : ((login_Copy5.Text, @"\d+").Text);
            _sotrudniki.Телефон = String.IsNullOrEmpty(login_Copy7.Text) ? 0 : int.Parse(Regex.Match(login_Copy7.Text, @"\d+").Value);
            _sotrudniki.Адрес = String.IsNullOrWhiteSpace(login_Copy.Text) ? string.Empty : ((login_Copy.Text, @"\d+").Text);
            _sotrudniki.Документ = String.IsNullOrWhiteSpace(login_Copy6.Text) ? string.Empty : ((login_Copy6.Text, @"\d+").Text);
            _sotrudniki.Серия_Номер= String.IsNullOrWhiteSpace(login_Copy1.Text) ? 0 : decimal.Parse(Regex.Match(login_Copy1.Text, @"\d+").Value);         
            _sotrudniki.Семейное_положение = String.IsNullOrWhiteSpace(login_Copy2.Text) ? string.Empty : (login_Copy2.Text, @"\d+").Text;
            _sotrudniki.Квалификация = String.IsNullOrWhiteSpace(login_Copy3.Text) ? string.Empty : (login_Copy3.Text, @"\d+").Text;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ValidateImput();
            database.SaveChanges();
            _window1.ReadData2();
            Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            Uslu.ItemsSource = database.Special.ToList();
            Uslu.SelectedIndex = _sotrudniki.Код_специальности is null ? -1 : (int)_sotrudniki.Код_специальности;
        }
    }
}
