using Boook.ObjectsFromDatabase;
using Boook.UserControls;
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
using System.Text.RegularExpressions;
using System.Globalization;

namespace Boook
{
    /// <summary>
    /// Interaction logic for PageBooks.xaml
    /// </summary>
    public partial class PageBooks : Page
    {
        // Me
        public static PageBooks Self;

        // Enums
        enum Action
        {
            New,    // 0
            Edit,   // 1
            Delete  // 3
        }

        // Data members
        private int selectedAction;
        private int selectedISBN;
        private int iSBN;
        private string title;
        private string descr;
        private decimal price;

        public int SelectedAction { get => selectedAction; set => selectedAction = value; }
        public int SelectedISBN { get => selectedISBN; set => selectedISBN = value; }

        public PageBooks()
        {
            InitializeComponent();

            txtIsbn.IsEnabled = false;
            txtTitle.IsEnabled = false;
            txtDescr.IsEnabled = false;
            txtPrice.IsEnabled = false;

            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;

            LoadFromDatabase();

            Self = this;

            SelectedISBN = -1;
            SelectedAction = -1;

            WhatAction();
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
                    break;
                case 0:
                    txtAction.Text = "Press save to add book.";
                    btnSave.Visibility = Visibility.Visible;
                    btnEdit.IsEnabled = false;
                    btnDelete.IsEnabled = false;
                    elpFirst.Fill = new SolidColorBrush(Color.FromRgb(123, 122, 120));
                    elpSecond.Fill = new SolidColorBrush(Color.FromRgb(241, 196, 15));
                    break;
                case 1:
                    txtAction.Text = "Press save to confirm edits.";
                    btnSave.Visibility = Visibility.Visible;
                    elpFirst.Fill = new SolidColorBrush(Color.FromRgb(123, 122, 120));
                    elpSecond.Fill = new SolidColorBrush(Color.FromRgb(241, 196, 15));
                    break;
                case 2:
                    txtAction.Text = "Press save to confirm deletion.";
                    btnSave.Visibility = Visibility.Visible;
                    elpFirst.Fill = new SolidColorBrush(Color.FromRgb(123, 122, 120));
                    elpSecond.Fill = new SolidColorBrush(Color.FromRgb(241, 196, 15));
                    break;
            }
        }

        private void LoadFromDatabase()
        {
            stkBook.Children.Clear();
            List<Book> books = DataAccess.GetAllBook();
            for (int i = 0; i < books.Count; i++)
            {
                UserControls.Item1 product = new UserControls.Item1();
                product.ISBN = books[i].ISBN;
                product.Title = books[i].Title;
                product.Description = books[i].Description;
                product.Price = books[i].Price;
                stkBook.Children.Add(product);
            }
        }

        private bool ValidateData()
        {
            // Check if all fields are filled
            if (txtIsbn.Text == "" || txtTitle.Text == "" || txtDescr.Text == "" || txtPrice.Text == "")
            {
                MessageBox.Show("Please fill in all fields.");
                return false;
            }

            // Check if ISBN is a number and has 7 digits
            if (int.TryParse(txtIsbn.Text, out int iSBN))
            {
                if (iSBN < 1000000)
                {
                    MessageBox.Show("ISBN must be 7 digits.");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("ISBN must be a number.");
                return false;
            }

            // Check if ISBN is unique
            if (selectedAction == 0)
            { 
                if (DataAccess.GetAllBook().Exists(b => b.ISBN == iSBN))
                {
                    MessageBox.Show("ISBN must be unique.");
                    return false;
                }
            }

            // Check if title has the correct length
            string title;
            if (txtTitle.Text.Length < 1 || txtTitle.Text.Length > 50)
            {
                MessageBox.Show("Title must be between 1 and 50 characters.");
                return false;
            }
            else
            {
                title = txtTitle.Text.Trim();
            }

            // Check if description has the correct length
            string descr;
            if (txtDescr.Text.Length < 1 || txtDescr.Text.Length > 10)
            {
                MessageBox.Show("Description must be between 1 and 10 characters.");
                return false;
            }
            else
            {
                descr = txtDescr.Text.Trim();
            }

            // Check if price is a number, is positive and has under 2 decimal places
            if (decimal.TryParse(txtPrice.Text, out decimal price))
            {
                if (price < 0)
                {
                    MessageBox.Show("Price must be a positive number.");
                    return false;
                }

                if (txtPrice.Text.Contains("."))
                {
                    if (txtPrice.Text.Split('.')[1].Length > 2)
                    {
                        MessageBox.Show("Price can only have 2 decimal places.");
                        return false;
                    }
                }
            }
            else
            {
                MessageBox.Show("Price must be a number.");
                return false;
            }

            this.iSBN = iSBN;
            this.title = title;
            this.descr = descr;
            this.price = price;

            return true;
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            selectedAction = 0;
            selectedISBN = -1;
            WhatAction();
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            txtIsbn.IsEnabled = true;
            txtTitle.IsEnabled = true;
            txtDescr.IsEnabled = true;
            txtPrice.IsEnabled = true;
            txtIsbn.Text = "";
            txtTitle.Text = "";
            txtDescr.Text = "";
            txtPrice.Text = "";
            Console.Beep();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            selectedAction = 1;
            WhatAction();
            btnDelete.IsEnabled = true;
            txtIsbn.IsEnabled = false;
            txtTitle.IsEnabled = true;
            txtDescr.IsEnabled = true;
            txtPrice.IsEnabled = true;
            Console.Beep();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Item1 item = stkBook.Children.OfType<Item1>().ToList().Find(i => i.ISBN == selectedISBN);

            txtTitle.Text = item.Title;
            txtDescr.Text = item.Description;
            txtPrice.Text = item.Price.ToString();

            selectedAction = 2;
            WhatAction();
            btnEdit.IsEnabled = true;

            txtIsbn.IsEnabled = false;
            txtTitle.IsEnabled = false;
            txtDescr.IsEnabled = false;
            txtPrice.IsEnabled = false;
            Console.Beep();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            switch (selectedAction)
            {
                case (int)Action.New:
                    if (ValidateData())
                    {
                        Book book = new Book();
                        book.ISBN = iSBN;
                        book.Title = title;
                        book.Description = descr;
                        book.Price = price;
                        book.Source = "new";
                        DataAccess.AddBook(book);
                        SelectedISBN = book.ISBN;

                        btnEdit.IsEnabled = true;
                        btnDelete.IsEnabled = true;
                    }
                    else
                    {
                        return;
                    }
                    break;
                case (int)Action.Edit:
                    if (ValidateData())
                    {
                        Book book = new Book();
                        book.ISBN = selectedISBN;
                        book.Title = title;
                        book.Description = descr;
                        book.Price = price;
                        book.Source = "edit";
                        DataAccess.UpdateBook(book);
                        SelectedISBN = book.ISBN;

                        btnEdit.IsEnabled = true;
                        btnDelete.IsEnabled = true;
                    }
                    else
                    {
                        return;
                    }
                    break;
                case (int)Action.Delete:
                    if (MessageBox.Show("Are you sure you want to delete this book?",
                        "Delete Book", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        DataAccess.DeleteBook(selectedISBN);
                        txtIsbn.Text = "";
                        txtTitle.Text = "";
                        txtDescr.Text = "";
                        txtPrice.Text = "";

                        SelectedISBN = -1;

                        btnEdit.IsEnabled = false;
                        btnDelete.IsEnabled = false;
                    }
                    break;
            }

            LoadFromDatabase();

            txtIsbn.IsEnabled = false;
            txtTitle.IsEnabled = false;
            txtDescr.IsEnabled = false;
            txtPrice.IsEnabled = false;

            SelectedAction = -1;

            WhatAction();
            Console.Beep();
        }

        private void DigitValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string fullText = textBox.Text.Insert(textBox.CaretIndex, e.Text);
            Regex regex = new Regex("[^0-9.]+"); // Allow digits and dot
            bool containsMoreThanOneDot = fullText.Count(f => f == '.') > 1;
            e.Handled = regex.IsMatch(e.Text) || containsMoreThanOneDot;
        }

        private void CapitalizeMe(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && !textBox.Text.Equals(""))
            {
                textBox.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(textBox.Text).Trim();
            }
        }
    }
}
