using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using MaterialDesignThemes.Wpf.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
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
using static System.Net.Mime.MediaTypeNames;

namespace kr
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public PraktikaEntities praktika { get; set; }
        public List<Jurnal_Ucheta> jurnals;
        public Window1(string UserLogin, string Roles)
        {
            InitializeComponent();
            Name.Text += $"Имя: {UserLogin}";
            Role.Text += $"Роль: {Roles}";
            if (Roles == "Admin")
            {
                Image.ImageSource = new BitmapImage(new Uri("C:\\Users\\Grigoriy\\Desktop\\kr\\kr\\Resourse\\software-engineer.png", UriKind.Absolute));
            }
            else if (Roles == "Manager")
            {
                Image.ImageSource = new BitmapImage(new Uri("C:\\Users\\Grigoriy\\Desktop\\kr\\kr\\Resourse\\user.png", UriKind.Absolute));
            }
            else if (Roles == "User")
            {
                //Image.ImageSource = new BitmapImage(new Uri("D:\\RentalCarProject\\RentalCarProject1\\RentalCarProject1\\Resources\\user.png", UriKind.Absolute));
            }
            else
            {
                //Image.ImageSource = new BitmapImage(new Uri("D:\\RentalCarProject\\RentalCarProject1\\RentalCarProject1\\Resources\\unknown.png", UriKind.Absolute));
            }
          
            Name2.Text += $"Имя: {UserLogin}";
            Role2.Text += $"Роль: {Roles}";
            if (Roles == "Admin")
            {
                Image2.ImageSource = new BitmapImage(new Uri("C:\\Users\\Grigoriy\\Desktop\\kr\\kr\\Resourse\\software-engineer.png", UriKind.Absolute));
            }
            else if (Roles == "Manager")
            {
                Image2.ImageSource = new BitmapImage(new Uri("C:\\Users\\Grigoriy\\Desktop\\kr\\kr\\Resourse\\user.png", UriKind.Absolute));
            }
            else if (Roles == "User")
            {
                //Image.ImageSource = new BitmapImage(new Uri("D:\\RentalCarProject\\RentalCarProject1\\RentalCarProject1\\Resources\\user.png", UriKind.Absolute));
            }
            else
            {
                //Image.ImageSource = new BitmapImage(new Uri("D:\\RentalCarProject\\RentalCarProject1\\RentalCarProject1\\Resources\\unknown.png", UriKind.Absolute));
            }
            Name3.Text += $"Имя: {UserLogin}";
            Role3.Text += $"Роль: {Roles}";
            if (Roles == "Admin")
            {
                Image3.ImageSource = new BitmapImage(new Uri("C:\\Users\\Grigoriy\\Desktop\\kr\\kr\\Resourse\\software-engineer.png", UriKind.Absolute));
            }
            else if (Roles == "Manager")
            {
                Image3.ImageSource = new BitmapImage(new Uri("C:\\Users\\Grigoriy\\Desktop\\kr\\kr\\Resourse\\user.png", UriKind.Absolute));
            }
            ReadData();
            ReadData3();
            ReadData1();
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

        public void ReadData3()
        {
            praktika = new PraktikaEntities();
            Grida3.ItemsSource = praktika.VidUslug.ToList();
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
            SearchTextBox.Text = null;
            After.Text = null;
            Before.Text = null;
            ReadData();
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
       

        private void Addcl1(object sender, RoutedEventArgs e)
        {
            var Dob1 = new AddCli(this);
            Dob1.Show();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Grida.SelectAllCells();
            Grida.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, Grida);
            String resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            String result = (string)Clipboard.GetData(DataFormats.Text);
            Grida.UnselectAllCells();
            StreamWriter file = new StreamWriter(@"C:\Users\Grigoriy\Desktop\Test.xls", true, Encoding.GetEncoding(1251));
            file.WriteLine(result.Replace(',', ' '));
            file.Close();
            MessageBox.Show("Exporting DataGrid data to Excel file created");
            System.Diagnostics.Process.Start(@"C:\Users\Grigoriy\Desktop\Test.xls");
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            Grida.Columns[7].Visibility = Visibility.Hidden;
 
            System.Windows.Controls.PrintDialog Printdlg = new System.Windows.Controls.PrintDialog();
            if ((bool)Printdlg.ShowDialog().GetValueOrDefault())
            {
                Size pageSize = new Size(Printdlg.PrintableAreaWidth, Printdlg.PrintableAreaHeight);
                // sizing of the element.
                Grida.Measure(pageSize);
                Grida.Arrange(new Rect(0, 0, pageSize.Width, pageSize.Height));
                Printdlg.PrintVisual(Grida, Title);
            }
            Grida.Columns[7].Visibility = Visibility.Visible;
     
        }

        private void DeleteRecordButton_Click_1(object sender, RoutedEventArgs e)
        {

        }







        private void Edit3(object sender, RoutedEventArgs e)
        {
            var slec = (sender as Button).DataContext as VidUslug;
            EditUsl editing = new EditUsl(slec, this);
            editing.Show();
        }

        private void SearchTextBox_TextChanged21(object sender, TextChangedEventArgs e)
        {
            var input = (sender as TextBox).Text.ToLower();
            int resultCount = praktika.VidUslug.Count(x => x.Наименование_услуги.Contains(input));
            if (!(string.IsNullOrEmpty(input)))
            {
                Grida3.ItemsSource = praktika.VidUslug.Where(x => x.Наименование_услуги.Contains(input)).ToList();
            }
            else
            {
                ReadData3();
            }
        }

        private void POl(object sender, RoutedEventArgs e)
        {
            string roles = Role.Text;

            if (roles == "Роль: Admin")
            {
                var dob = new Rol(this);
                dob.Show();
            }
            else
            {
                MessageBox.Show("Недостаточно прав для совершения этой операции!");
            }
        }

        private void sot(object sender, RoutedEventArgs e)
        {
            string roles = Role.Text;

            if (roles == "Роль: Admin")
            {
                var dob = new Sotr(this);
                dob.Show();
            }
            else
            {
                MessageBox.Show("Недостаточно прав для совершения этой операции!");
            }
        }

        private void close(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void Addcl2(object sender, RoutedEventArgs e)
        {
            var Dob1 = new AddUsl(this);
            Dob1.Show();
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

        private void Addcl12(object sender, RoutedEventArgs e)
        {
            var Dob1 = new AddCli(this);
            Dob1.Show();
        }
    }
}
