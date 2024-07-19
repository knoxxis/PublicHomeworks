using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Boook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Data Members


        public MainWindow()
        {
            InitializeComponent();
            DataAccess.InitializeDatabase();
            InitializeControls();
        }

        private void InitializeControls()
        {
            btnCustomer.IsEnabled = false;
            btnBook.IsEnabled = false;
            btnBuy.IsEnabled = false;
            btnTran.IsEnabled = false;
            btnLogout.IsEnabled = false;
            Main.Content = new PageLogin();
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btnCustomer_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PageCustomers();
            Console.Beep();
        }

        private void btnBook_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PageBooks();
            Console.Beep();
        }

        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PageBuy();
            Console.Beep();
        }

        private void btnTran_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PageTrans();
            Console.Beep();
        }

        private void txtLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtLogin.Text.Equals("1234"))
            {
                btnCustomer.IsEnabled = true;
                btnBook.IsEnabled = true;
                btnBuy.IsEnabled = true;
                btnTran.IsEnabled = true;

                PageLogin.Self.txtNav.Text = "Welcome User.\nSelect a menu to start.";
                PageLogin.Self.imgArr.Visibility = Visibility.Hidden;

                txtLogin.IsEnabled = false;
                btnLogout.IsEnabled = true;

                Console.Beep();
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            txtLogin.Text = "";
            btnCustomer.IsEnabled = false;
            btnBook.IsEnabled = false;
            btnBuy.IsEnabled = false;
            btnTran.IsEnabled = false;
            Main.Content = new PageLogin();
            btnLogout.IsEnabled = false;
            txtLogin.IsEnabled = true;
            Console.Beep();
        }
    }
}
