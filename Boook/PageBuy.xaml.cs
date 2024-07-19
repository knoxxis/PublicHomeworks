using Boook.ObjectsFromDatabase;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Boook
{
    /// <summary>
    /// Interaction logic for PageBuy.xaml
    /// </summary>
    public partial class PageBuy : Page
    {
        public static PageBuy Self;
        private int _quantity;
        private decimal _totalPrice;

        int iSBN;
        int customer_Id;
        int quantity;
        decimal total_Price;

        public PageBuy()
        {
            InitializeComponent();

            LoadBooks();
            LoadCustomers();

            _quantity = 0;
            txtQuantity.Text = _quantity.ToString();
            _totalPrice = 0.00m;
            txtTotalPrice.Text = "Invalid";

            Self = this;
        }

        private void LoadBooks()
        {
            // Load books from database and from search
            stkBookCard.Children.Clear();
            
            if (int.TryParse(txtSearchBook.Text, out int iSBNSearch))
            {
                List<Book> books = DataAccess.SearchBookByInt(iSBNSearch);
                for (int i = 0; i < books.Count; i++)
                {
                    UserControls.Item3 product = new UserControls.Item3();
                    product.ISBN = books[i].ISBN;
                    product.Title = books[i].Title;
                    product.Description = books[i].Description;
                    product.Price = books[i].Price;
                    stkBookCard.Children.Add(product);
                }
            }
            else if (txtSearchBook.Text != "")
            {
                List<Book> books = DataAccess.SearchBookByString(txtSearchBook.Text);
                for (int i = 0; i < books.Count; i++)
                {
                    UserControls.Item3 product = new UserControls.Item3();
                    product.ISBN = books[i].ISBN;
                    product.Title = books[i].Title;
                    product.Description = books[i].Description;
                    product.Price = books[i].Price;
                    stkBookCard.Children.Add(product);
                }
            }
            else
            {
                List<Book> books = DataAccess.GetAllBook();
                for (int i = 0; i < books.Count; i++)
                {
                    UserControls.Item3 product = new UserControls.Item3();
                    product.ISBN = books[i].ISBN;
                    product.Title = books[i].Title;
                    product.Description = books[i].Description;
                    product.Price = books[i].Price;
                    stkBookCard.Children.Add(product);
                }
            }
        }
        private void LoadCustomers()
        {
            stkCustCard.Children.Clear();

            if (int.TryParse(txtSearchCustomer.Text, out int idSearch))
            {
                List<Customer> customers = DataAccess.SearchCustomerByInt(idSearch);
                for (int i = 0; i < customers.Count; i++)
                {
                    UserControls.Item4 customer = new UserControls.Item4();
                    customer.Customer_Id = customers[i].Customer_Id;
                    customer.Customer_Name = customers[i].Customer_Name;
                    customer.Email = customers[i].Email;
                    customer.Address = customers[i].Address;
                    stkCustCard.Children.Add(customer);
                }
            }
            else if (txtSearchCustomer.Text != "")
            {
                List<Customer> customers = DataAccess.SearchCustomerByString(txtSearchCustomer.Text);
                for (int i = 0; i < customers.Count; i++)
                {
                    UserControls.Item4 customer = new UserControls.Item4();
                    customer.Customer_Id = customers[i].Customer_Id;
                    customer.Customer_Name = customers[i].Customer_Name;
                    customer.Email = customers[i].Email;
                    customer.Address = customers[i].Address;
                    stkCustCard.Children.Add(customer);
                }
            }
            else
            {
                List<Customer> customers = DataAccess.GetAllCustomers();
                for (int i = 0; i < customers.Count; i++)
                {
                    UserControls.Item4 customer = new UserControls.Item4();
                    customer.Customer_Id = customers[i].Customer_Id;
                    customer.Customer_Name = customers[i].Customer_Name;
                    customer.Email = customers[i].Email;
                    customer.Address = customers[i].Address;
                    stkCustCard.Children.Add(customer);
                }
            }
        }

        public void PriceCalulation()
        {
            _totalPrice = DataAccess.GetPrice(int.Parse(txtISBN.Text)) * _quantity;
            txtTotalPrice.Text = String.Format("{0:0.00}", _totalPrice);
        }

        private bool ValidateISBN()
        {
            // Check if the ISBN is valid
            if (!int.TryParse(txtISBN.Text, out int iSBN))
            {
                txtInvalid.Text = "The ISBN must be a number.";
                Console.Beep();
                return false;
            }
            if (!DataAccess.GetISBNs().Contains(iSBN))
            {
                txtInvalid.Text = "The ISBN is invalid.";
                Console.Beep();
                return false;
            }

            txtInvalid.Text = "";
            return true;
        }

        private bool ValidateData()
        {
            if (txtISBN.Text == "" || txtCustomer_Id.Text == "")
            {
                MessageBox.Show("Please fill in all the fields.");
                return false;
            }

            // Check if the ISBN is valid
            if (!int.TryParse(txtISBN.Text, out int iSBN))
            {
                txtInvalid.Text = "The ISBN must be a number.";
                Console.Beep();
                return false;
            }
            if (!DataAccess.GetISBNs().Contains(iSBN))
            {
                MessageBox.Show("The ISBN is invalid.");
                return false;
            }

            // Check if the customer id is valid
            if (!int.TryParse(txtCustomer_Id.Text, out int customer_Id))
            {
                MessageBox.Show("The Customer ID must be a number.");
                return false;
            }
            if (!DataAccess.GetCustomerIds().Contains(customer_Id))
            {
                MessageBox.Show("The Customer ID is invalid.");
                return false;
            }

            // Check if the quantity is not 0
            int quantity;
            if (_quantity == 0)
            {
                MessageBox.Show("The quantity must be greater than 0.");
                return false;
            }
            else
            {
                quantity = _quantity;
            }

            // Check if the total price is valid
            if (!decimal.TryParse(txtTotalPrice.Text, out decimal totalPrice))
            {
                MessageBox.Show("The total price is invalid.");
                return false;
            }

            this.iSBN = iSBN;
            this.customer_Id = customer_Id;
            this.quantity = quantity;
            this.total_Price = totalPrice;

            txtInvalid.Text = "";
            return true;
        }

        private void btnSearchBook_Click(object sender, RoutedEventArgs e)
        {
            LoadBooks();
            Console.Beep();
        }

        private void btnSearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            LoadCustomers();
            Console.Beep();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            txtQuantity.Text = (++_quantity).ToString();
            if (ValidateISBN()) PriceCalulation();
            else txtTotalPrice.Text = "Invalid";
        }

        private void minusBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_quantity > 0) txtQuantity.Text = (--_quantity).ToString();
            if (ValidateISBN()) PriceCalulation();
            else txtTotalPrice.Text = "Invalid";
        }

        private void DigitValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtQuantity_LostFocus(object sender, RoutedEventArgs e)
        {
            txtQuantity.Text= txtQuantity.Text.Trim();
            _quantity = int.Parse(txtQuantity.Text);
            if (ValidateISBN()) PriceCalulation();
            else txtTotalPrice.Text = "Invalid";
        }

        private void txtISBN_LostFocus(object sender, RoutedEventArgs e)
        {
            txtISBN.Text = txtISBN.Text.Trim();
            if (ValidateISBN()) PriceCalulation();
            else txtTotalPrice.Text = "Invalid";
        }

        private void txtCustomer_Id_LostFocus(object sender, RoutedEventArgs e)
        {
            txtCustomer_Id.Text = txtCustomer_Id.Text.Trim();
            txtInvalid.Text = "";
        }

        private void btnCheckout_Click(object sender, RoutedEventArgs e)
        {
            Console.Beep();
            if (ValidateData())
            {
                Transaction transaction = new Transaction();
                transaction.ISBN = iSBN;
                transaction.Customer_Id = customer_Id;
                transaction.Quantity = quantity;
                transaction.Total_Price = total_Price;
                DataAccess.AddTransaction(transaction);
                MessageBox.Show("Transaction successful.");

                txtISBN.Text = "";
                txtCustomer_Id.Text = "";
                txtQuantity.Text = "0";
                txtTotalPrice.Text = "Invalid";

                _quantity = 0;
                _totalPrice = 0.00m;
            }
        }
    }
}
