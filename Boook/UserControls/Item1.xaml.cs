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
    /// Interaction logic for Item1.xaml
    /// </summary>
    public partial class Item1 : UserControl
    {
        public Item1()
        {
            InitializeComponent();
        }

        public int ISBN
        {
            get { return (int)GetValue(ISBNProperty); }
            set { SetValue(ISBNProperty, value); }
        }

        public static readonly DependencyProperty ISBNProperty = DependencyProperty.Register("ISBN", typeof(int), typeof(Item1));


        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(Item1));


        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(Item1));

        public decimal Price
        {
            get { return (decimal)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }

        public static readonly DependencyProperty PriceProperty = DependencyProperty.Register("Price", typeof(decimal), typeof(Item1));

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            PageBooks.Self.SelectedISBN = ISBN;
            PageBooks.Self.SelectedAction = -1;

            PageBooks.Self.txtIsbn.Text = ISBN.ToString();
            PageBooks.Self.txtTitle.Text = Title;
            PageBooks.Self.txtDescr.Text = Description;
            PageBooks.Self.txtPrice.Text = Price.ToString();

            PageBooks.Self.txtIsbn.IsEnabled = false;
            PageBooks.Self.txtTitle.IsEnabled = false;
            PageBooks.Self.txtDescr.IsEnabled = false;
            PageBooks.Self.txtPrice.IsEnabled = false;

            PageBooks.Self.btnEdit.IsEnabled = true;
            PageBooks.Self.btnDelete.IsEnabled = true;

            PageBooks.Self.WhatAction();

            Console.Beep();
        }
    }
}
