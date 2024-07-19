using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boook.ObjectsFromDatabase
{
    internal class Transaction
    {
        int trans_Id;
        int iSBN;
        int customer_Id;
        int quantity;
        decimal total_Price;

        public int Trans_Id { get => trans_Id; set => trans_Id = value; }
        public int ISBN { get => iSBN; set => iSBN = value; }
        public int Customer_Id { get => customer_Id; set => customer_Id = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public decimal Total_Price { get => total_Price; set => total_Price = value; }
    }
}
