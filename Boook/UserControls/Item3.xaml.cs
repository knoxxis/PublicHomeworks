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
    /// Interaction logic for Item3.xaml
    /// </summary>
    public partial class Item3 : UserControl
    {
        public Item3()
        {
            InitializeComponent();
        }

        public int ISBN
        {
            get { return (int)GetValue(ISBNProperty); }
            set { SetValue(ISBNProperty, value); }
        }

        public static readonly DependencyProperty ISBNProperty = DependencyProperty.Register("ISBN", typeof(int), typeof(Item3));


        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(Item3));


        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(Item3));

        public decimal Price
        {
            get { return (decimal)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }

        public static readonly DependencyProperty PriceProperty = DependencyProperty.Register("Price", typeof(decimal), typeof(Item3));

        private void btnSelectBook_Click(object sender, RoutedEventArgs e)
        {
            PageBuy.Self.txtISBN.Text = ISBN.ToString();
            PageBuy.Self.PriceCalulation();
            PageBuy.Self.txtInvalid.Text = "";
            Console.Beep();
        }
    }
}
