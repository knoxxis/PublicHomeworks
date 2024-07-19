using Boook.ObjectsFromDatabase;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Boook
{
    /// <summary>
    /// Interaction logic for PageTrans.xaml
    /// </summary>
    public partial class PageTrans : Page
    {
        int selectedTrans_id;

        public PageTrans()
        {
            InitializeComponent();

            selectedTrans_id = -1;

            dtgTransactions.ItemsSource = DataAccess.GetTransactions().DefaultView;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Console.Beep();
            if (selectedTrans_id > 0)
            {
                if (MessageBox.Show("Are you sure you want to delete this transaction?",
                            "Delete Transaction", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    DataAccess.DeleteTransaction(selectedTrans_id);
                    dtgTransactions.ItemsSource = DataAccess.GetTransactions().DefaultView;

                    selectedTrans_id = -1;
                    txtTrans_Id.Text = "";

                    Console.Beep();
                }
            }
            else MessageBox.Show("Please select a transaction to delete.");
        }

        private void dtgTransactions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgTransactions.SelectedItem != null)
            {
                // Cast the SelectedItem to DataRowView
                var selectedRowView = dtgTransactions.SelectedItem as DataRowView;
                if (selectedRowView != null)
                {
                    // Access the "Trans_Id" column value from the DataRow
                    selectedTrans_id = Convert.ToInt32(selectedRowView["Trans_Id"]);
                    txtTrans_Id.Text = selectedTrans_id.ToString();
                }
            }
        }
    }
}
