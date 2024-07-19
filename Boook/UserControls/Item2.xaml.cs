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
    /// Interaction logic for Item2.xaml
    /// </summary>
    public partial class Item2 : UserControl
    {
        public Item2()
        {
            InitializeComponent();
        }

        public int Customer_Id
        {
            get { return (int)GetValue(Customer_IdProperty); }
            set { SetValue(Customer_IdProperty, value); }
        }

        public static readonly DependencyProperty Customer_IdProperty = DependencyProperty.Register("Customer_Id", typeof(int), typeof(Item2));


        public string Customer_Name
        {
            get { return (string)GetValue(Customer_NameProperty); }
            set { SetValue(Customer_NameProperty, value); }
        }

        public static readonly DependencyProperty Customer_NameProperty = DependencyProperty.Register("Customer_Name", typeof(string), typeof(Item2));


        public string Email
        {
            get { return (string)GetValue(EmailProperty); }
            set { SetValue(EmailProperty, value); }
        }

        public static readonly DependencyProperty EmailProperty = DependencyProperty.Register("Email", typeof(string), typeof(Item2));

        public string Address
        {
            get { return (string)GetValue(AddressProperty); }
            set { SetValue(AddressProperty, value); }
        }

        public static readonly DependencyProperty AddressProperty = DependencyProperty.Register("Address", typeof(string), typeof(Item2));

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            PageCustomers.Self.SelectedCustomer = Customer_Id;
            PageCustomers.Self.SelectedAction = -1;

            PageCustomers.Self.txtId.Text = Customer_Id.ToString();
            PageCustomers.Self.txtName.Text = Customer_Name;
            PageCustomers.Self.txtEmail.Text = Email;
            PageCustomers.Self.txtAddress.Text = Address;

            PageCustomers.Self.txtId.IsEnabled = false;
            PageCustomers.Self.txtName.IsEnabled = false;
            PageCustomers.Self.txtEmail.IsEnabled = false;
            PageCustomers.Self.txtAddress.IsEnabled = false;

            PageCustomers.Self.btnEdit.IsEnabled = true;
            PageCustomers.Self.btnDelete.IsEnabled = true;

            PageCustomers.Self.WhatAction();

            Console.Beep();
        }
    }
}
