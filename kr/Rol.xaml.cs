using DocumentFormat.OpenXml.Presentation;
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
    /// Логика взаимодействия для Rol.xaml
    /// </summary>
    public partial class Rol : Window
    {
        public PraktikaEntities praktika;
        public Rol( Window1 window1)
        {
            InitializeComponent();
            udat();
        }
        public void udat() 
        {
            praktika = new PraktikaEntities();
            Grida2.ItemsSource = praktika.Users.ToList();
        }
        private void Addcl2(object sender, RoutedEventArgs e)
        {
            var Dob1 = new AddRol(this);
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

        private void close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Edit2(object sender, RoutedEventArgs e)
        {
            var slec = (sender as Button).DataContext as Users;
            EditRol editing = new EditRol(slec, this);
            editing.Show();
        }

        private void onRepair_Checked(object sender, RoutedEventArgs e)
        {
            Grida2.ItemsSource = praktika.Users.Where(x => x.Role == 1).ToList();
        }
        private void breakDown_Checked(object sender, RoutedEventArgs e)
        {
            Grida2.ItemsSource = praktika.Users.Where(x => x.Role == 2).ToList();
        }
        private void clearFilter_Checked(object sender, RoutedEventArgs e)
        {
            Grida2.ItemsSource = praktika.Users.ToList();
        }
    }
}
