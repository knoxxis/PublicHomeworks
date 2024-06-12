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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Money
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            double salary, spending, wanted;
            int day;

            salary = double.Parse(txtboxSalaryPerDay.Text);
            spending = double.Parse(txtboxSpendingPerDay.Text);
            wanted = double.Parse(txtboxWantedAmount.Text);

            day = (int)Math.Ceiling(wanted / (salary - spending));
            txtboxDay.Text = day.ToString();
        }
    }
}
