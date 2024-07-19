using Boook.ObjectsFromDatabase;
using Boook.UserControls;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Boook
{
    /// <summary>
    /// Interaction logic for PageCustomers.xaml
    /// </summary>
    public partial class PageCustomers : Page
    {
        // Me
        public static PageCustomers Self;

        // Enums
        enum Action
        {
            New,    // 0
            Edit,   // 1
            Delete  // 3
        }

        // Data members
        private int selectedAction;
        private int selectedCustomer;
        private int customer_Id;
        private string customer_Name;
        private string email;
        private string address;

        public int SelectedAction { get => selectedAction; set => selectedAction = value; }
        public int SelectedCustomer { get => selectedCustomer; set => selectedCustomer = value; }

        public PageCustomers()
        {
            InitializeComponent();

            txtId.IsEnabled = false;
            txtName.IsEnabled = false;
            txtEmail.IsEnabled = false;
            txtAddress.IsEnabled = false;

            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;

            LoadFromDatabase();

            Self = this;

            SelectedCustomer = -1;
            SelectedAction = -1;

            WhatAction();
        }

        private void LoadFromDatabase()
        {
            stkCust.Children.Clear();
            List<Customer> customers = DataAccess.GetAllCustomers();
            for (int i = 0; i < customers.Count; i++)
            {
                UserControls.Item2 cust = new UserControls.Item2();
                cust.Customer_Id = customers[i].Customer_Id;
                cust.Customer_Name = customers[i].Customer_Name;
                cust.Email = customers[i].Email;
                cust.Address = customers[i].Address;
                stkCust.Children.Add(cust);
            }
        }

        public void WhatAction()
        {
            switch (selectedAction)
            {
                case -1:
                    txtAction.Text = "Select an action.";
                    btnSave.Visibility = Visibility.Hidden;
                    elpFirst.Fill = new SolidColorBrush(Color.FromRgb(241, 196, 15));
                    elpSecond.Fill = new SolidColorBrush(Color.FromRgb(123, 122, 120));
                    elpThird.Fill = new SolidColorBrush(Color.FromRgb(123, 122, 120));
                    break;
                case 0:
                    txtAction.Text = "Press save to add customer.";
                    btnSave.Visibility = Visibility.Visible;
                    btnEdit.IsEnabled = false;
                    btnDelete.IsEnabled = false;
                    elpFirst.Fill = new SolidColorBrush(Color.FromRgb(123, 122, 120));
                    elpSecond.Fill = new SolidColorBrush(Color.FromRgb(241, 196, 15));
                    elpThird.Fill = new SolidColorBrush(Color.FromRgb(123, 122, 120));
                    break;
                case 1:
                    txtAction.Text = "Press save to confirm edits.";
                    btnSave.Visibility = Visibility.Visible;
                    elpFirst.Fill = new SolidColorBrush(Color.FromRgb(123, 122, 120));
                    elpSecond.Fill = new SolidColorBrush(Color.FromRgb(241, 196, 15));
                    elpThird.Fill = new SolidColorBrush(Color.FromRgb(123, 122, 120));
                    break;
                case 2:
                    txtAction.Text = "Press save to confirm deletion.";
                    btnSave.Visibility = Visibility.Visible;
                    elpFirst.Fill = new SolidColorBrush(Color.FromRgb(123, 122, 120));
                    elpSecond.Fill = new SolidColorBrush(Color.FromRgb(241, 196, 15));
                    elpThird.Fill = new SolidColorBrush(Color.FromRgb(123, 122, 120));
                    break;
            }
        }

        private bool ValidateData()
        {
            // Check if all fields are filled
            if (txtId.Text == "" || txtName.Text == "" || txtEmail.Text == "" || txtAddress.Text == "")
            {
                MessageBox.Show("Please fill all fields.");
                return false;
            }

            // Check if Id is a number and has less than two digits
            int id;
            if (int.TryParse(txtId.Text, out id))
            {
                if (id < 0 || id >= 100)
                {
                    MessageBox.Show("Id must be 2 digits and positive.");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Id must be a number.");
                return false;
            }

            // Check if Id is unique
            if (selectedAction == 0)
            {
                if (DataAccess.GetAllCustomers().Exists(c => c.Customer_Id == id))
                {
                    MessageBox.Show("Id must be unique.");
                    return false;
                }
            }

            // Check if name contains only letters and contains at least one space, is less than 100 characters
            string name;
            if (txtName.Text.Length > 100)
            {
                MessageBox.Show("Name must be less than 100 characters.");
                return false;
            }
            else
            {
                if (!txtName.Text.All(c => char.IsLetter(c) || c == ' '))
                {
                    MessageBox.Show("Name must contain only letters and spaces.");
                    return false;
                }
                else
                {
                    if (txtName.Text.Count(c => c == ' ') == 0)
                    {
                        MessageBox.Show("Name must contain at least one space.");
                        return false;
                    }
                    else
                    {
                        name = txtName.Text.Trim();
                    }
                }
            }

            // Check if address is less than 200 characters and contains 2 commas
            string address;
            if (txtAddress.Text.Length > 200)
            {
                MessageBox.Show("Address must be less than 200 characters.");
                return false;
            }
            else
            {
                if (txtAddress.Text.Count(c => c == ',') != 2)
                {
                    MessageBox.Show("Address must contain 2 commas.");
                    return false;
                }
                else
                {
                    address = txtAddress.Text.Trim();
                }
            }

            // Check if email is valid
            string email;
            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("Email must contain '@' and '.'.");
                return false;
            }
            else
            {
                if (txtEmail.Text.Length > 50)
                {
                    MessageBox.Show("Email must be less than 50 characters.");
                    return false;
                }
                else
                {
                    email = txtEmail.Text.Trim();
                }
            }

            this.customer_Id = id;
            this.customer_Name = name;
            this.address = address;
            this.email = email;

            return true;
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            selectedAction = 0;
            selectedCustomer = -1;
            WhatAction();
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            txtId.IsEnabled = true;
            txtName.IsEnabled = true;
            txtEmail.IsEnabled = true;
            txtAddress.IsEnabled = true;
            txtId.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            Console.Beep();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            selectedAction = 1;
            WhatAction();
            btnDelete.IsEnabled = true;
            txtId.IsEnabled = false;
            txtName.IsEnabled = true;
            txtEmail.IsEnabled = true;
            txtAddress.IsEnabled = true;
            Console.Beep();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Item2 item = stkCust.Children.OfType<Item2>().ToList().Find(i => i.Customer_Id == selectedCustomer);

            txtName.Text = item.Customer_Name;
            txtEmail.Text = item.Email;
            txtAddress.Text = item.Address;

            selectedAction = 2;
            WhatAction();
            btnEdit.IsEnabled = true;

            txtId.IsEnabled = false;
            txtName.IsEnabled = false;
            txtEmail.IsEnabled = false;
            txtAddress.IsEnabled = false;

            Console.Beep();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            elpFirst.Fill = new SolidColorBrush(Color.FromRgb(123, 122, 120));
            elpSecond.Fill = new SolidColorBrush(Color.FromRgb(123, 122, 120));
            elpThird.Fill = new SolidColorBrush(Color.FromRgb(241, 196, 15));

            switch (selectedAction)
            {
                case (int)Action.New:
                    if (ValidateData())
                    {
                        Customer customer = new Customer();
                        customer.Customer_Id = customer_Id;
                        customer.Customer_Name = customer_Name;
                        customer.Email = email;
                        customer.Address = address;

                        DataAccess.AddCustomer(customer);
                        SelectedCustomer = customer.Customer_Id;

                        btnEdit.IsEnabled = true;
                        btnDelete.IsEnabled = true;
                    }
                    else
                    {
                        elpFirst.Fill = new SolidColorBrush(Color.FromRgb(123, 122, 120));
                        elpSecond.Fill = new SolidColorBrush(Color.FromRgb(241, 196, 15));
                        elpThird.Fill = new SolidColorBrush(Color.FromRgb(123, 122, 120));
                        return;
                    }
                    break;
                case (int)Action.Edit:
                    if (ValidateData())
                    {
                        Customer customer = new Customer();
                        customer.Customer_Id = customer_Id;
                        customer.Customer_Name = customer_Name;
                        customer.Email = email;
                        customer.Address = address;

                        DataAccess.UpdateCustomer(customer);
                        SelectedCustomer = customer.Customer_Id;

                        btnEdit.IsEnabled = true;
                        btnDelete.IsEnabled = true;
                    }
                    else
                    {
                        elpFirst.Fill = new SolidColorBrush(Color.FromRgb(123, 122, 120));
                        elpSecond.Fill = new SolidColorBrush(Color.FromRgb(241, 196, 15));
                        elpThird.Fill = new SolidColorBrush(Color.FromRgb(123, 122, 120));
                        return;
                    }
                    break;
                case (int)Action.Delete:
                    if (MessageBox.Show("Are you sure you want to delete this customer?", 
                        "Delete Customer", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        DataAccess.DeleteCustomer(selectedCustomer);
                        txtId.Text = "";
                        txtName.Text = "";
                        txtEmail.Text = "";
                        txtAddress.Text = "";

                        SelectedCustomer = -1;

                        btnEdit.IsEnabled = false;
                        btnDelete.IsEnabled = false;
                    }
                    break;
            }

            LoadFromDatabase();

            txtId.IsEnabled = false;
            txtName.IsEnabled = false;
            txtEmail.IsEnabled = false;
            txtAddress.IsEnabled = false;

            SelectedAction = -1;

            WhatAction();
            Console.Beep();
        }

        private void DigitValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CapitalizeMe(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && !textBox.Text.Equals(""))
            {
                textBox.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(textBox.Text).Trim();
            }
        }

        private void AllLower(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && !textBox.Text.Equals(""))
            {
                textBox.Text = textBox.Text.ToLower();
            }
        }
    }
}
