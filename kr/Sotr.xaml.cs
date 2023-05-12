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
    /// Логика взаимодействия для Sotr.xaml
    /// </summary>
    public partial class Sotr : Window
    {
        public PraktikaEntities praktika;
        public Sotr( Window1 window1)
        {
            InitializeComponent();
            ReadData1();
        }
        public void ReadData1()
        {
            praktika = new PraktikaEntities();
            Grida2.ItemsSource = praktika.Sotrudniki.ToList();
        }
        private void DeleteRecordButton_Click_2(object sender, RoutedEventArgs e)
        {
            var selectedInvoice = ((sender as Button).DataContext as Sotrudniki);
            if (MessageBox.Show($"Вы уверены, что хотите удалить запись под номером {selectedInvoice.КодCотрудника}?",
                "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                praktika.Sotrudniki.Remove(selectedInvoice);
                praktika.SaveChanges();
                ReadData1();
            }
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private bool IsMaximized = false;
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1250;
                    this.Height = 720;

                    IsMaximized = false;

                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                    IsMaximized = true;
                }
            }
        }
        private void Addcl2(object sender, RoutedEventArgs e)
        {
            var Dob1 = new AddSot(this);
            Dob1.Show();
        }
        private void Edit2(object sender, RoutedEventArgs e)
        {
            var slec = (sender as Button).DataContext as Sotrudniki;
            EditSot editing = new EditSot(slec, this);
            editing.Show();
        }
        private void SearchTextBox_TextChanged2(object sender, TextChangedEventArgs e)
        {
            var input = (sender as TextBox).Text.ToLower();
            int resultCount = praktika.Sotrudniki.Count(x => x.ФИО.Contains(input));
            if (!(string.IsNullOrEmpty(input)))
            {
                Grida2.ItemsSource = praktika.Sotrudniki.Where(x => x.ФИО.Contains(input)).ToList();
            }
            else
            {
                ReadData1();
            }
        }

        private void close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
