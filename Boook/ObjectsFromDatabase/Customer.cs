using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boook.ObjectsFromDatabase
{
    internal class Customer
    {
        int customer_Id;
        string customer_Name;
        string address;
        string email;

        public int Customer_Id { get => customer_Id; set => customer_Id = value; }
        public string Customer_Name { get => customer_Name; set => customer_Name = value; }
        public string Address { get => address; set => address = value; }
        public string Email { get => email; set => email = value; }
    }
}
