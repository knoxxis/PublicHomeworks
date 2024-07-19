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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Boook.UserControls
{
    /// <summary>
    /// Interaction logic for Item4.xaml
    /// </summary>
    public partial class Item4 : UserControl
    {
        public Item4()
        {
            InitializeComponent();
        }

        public int Customer_Id
        {
            get { return (int)GetValue(Customer_IdProperty); }
            set { SetValue(Customer_IdProperty, value); }
        }

        public static readonly DependencyProperty Customer_IdProperty = DependencyProperty.Register("Customer_Id", typeof(int), typeof(Item4));


        public string Customer_Name
        {
            get { return (string)GetValue(Customer_NameProperty); }
            set { SetValue(Customer_NameProperty, value); }
        }

        public static readonly DependencyProperty Customer_NameProperty = DependencyProperty.Register("Customer_Name", typeof(string), typeof(Item4));


        public string Email
        {
            get { return (string)GetValue(EmailProperty); }
            set { SetValue(EmailProperty, value); }
        }

        public static readonly DependencyProperty EmailProperty = DependencyProperty.Register("Email", typeof(string), typeof(Item4));

        public string Address
        {
            get { return (string)GetValue(AddressProperty); }
            set { SetValue(AddressProperty, value); }
        }

        public static readonly DependencyProperty AddressProperty = DependencyProperty.Register("Address", typeof(string), typeof(Item4));

        private void btnSelectCust_Click(object sender, RoutedEventArgs e)
        {
            PageBuy.Self.txtCustomer_Id.Text = Customer_Id.ToString();
            Console.Beep();
        }
    }
}
