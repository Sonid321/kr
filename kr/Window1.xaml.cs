using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Spreadsheet;
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
        public List<Jurnal_Ucheta> jurnals;
        public Window1()
        {
            InitializeComponent();
            ReadData();
            ReadData1();
            ReadData2();
        }
        public void ReadData()
        {
            praktika = new PraktikaEntities();
            Grida.ItemsSource = praktika.Jurnal_Ucheta.ToList();
        }
        public void ReadData1()
        {
            praktika = new PraktikaEntities();
            Grida1.ItemsSource = praktika.Client.ToList();
        }
        public void ReadData2()
        {
            praktika = new PraktikaEntities();
            Grida2.ItemsSource = praktika.Sotrudniki.ToList();
        }
        private void Edit(object sender, RoutedEventArgs e)
        {
            var slec = (sender as Button).DataContext as Jurnal_Ucheta;
            Window2 editing = new Window2(slec, this);
            editing.Show();
        }
        private void Edit1(object sender, RoutedEventArgs e)
        {
            var slec = (sender as Button).DataContext as Client;
            EditCli editing = new EditCli(slec, this);
            editing.Show();
        }

        private void delClick(object sender, RoutedEventArgs e)
        {
            var selectedInvoice = ((sender as Button).DataContext as Jurnal_Ucheta);
            if (MessageBox.Show($"Вы уверены, что хотите удалить запись под номером {selectedInvoice.Код_записи}?",
                "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                praktika.Jurnal_Ucheta.Remove(selectedInvoice);
                praktika.SaveChanges();
                ReadData();
            }
        }
        private void Filtr1(object sender, RoutedEventArgs e)
        {
            Grida.ItemsSource = praktika.Jurnal_Ucheta.ToList();
        }

        private void Addcl(object sender, RoutedEventArgs e)
        {
            var Dob1 = new AddG(this);
            Dob1.Show();
        }


        private void DeleteRecordButton_Click(object sender, RoutedEventArgs e)
        {
            Grida.ItemsSource = praktika.Jurnal_Ucheta.ToList();
        }

    private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Before.SelectedDate != null && After.SelectedDate != null)
            {
                if (Before.SelectedDate > After.SelectedDate)
                {
                    var start = After.SelectedDate;
                    var end = Before.SelectedDate;
                    var filteredData = praktika.Jurnal_Ucheta
                        .Where(x => x.Дата_подписания >= start && x.Дата_подписания <= end)
                        .ToList();
                    Grida.ItemsSource = filteredData;
                }
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var input = (sender as TextBox).Text.ToLower();
            int resultCount = praktika.Jurnal_Ucheta.Count(x => x.Client.ФИО.Contains(input));
            if (!(string.IsNullOrEmpty(input)))
            {
                Grida.ItemsSource = praktika.Jurnal_Ucheta.Where(x => x.Client.ФИО.Contains(input)).ToList();
            }
            else
            {
                ReadData();
            }
        }
        private void SearchTextBox_TextChanged1(object sender, TextChangedEventArgs e)
        {
            var input = (sender as TextBox).Text.ToLower();
            int resultCount = praktika.Client.Count(x => x.ФИО.Contains(input));
            if (!(string.IsNullOrEmpty(input)))
            {
                Grida1.ItemsSource = praktika.Client.Where(x => x.ФИО.Contains(input)).ToList();
            }
            else
            {
                ReadData1();
            }
        }

        private void Addcl1(object sender, RoutedEventArgs e)
        {
            var Dob1 = new AddCli(this);
            Dob1.Show();
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
                ReadData2();
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
    }
}
