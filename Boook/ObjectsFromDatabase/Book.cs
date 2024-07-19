using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boook.ObjectsFromDatabase
{
    public class Book
    {
        // Data members
        int iSBN;
        string title;
        string description;
        decimal price;
        string source;

        // Mutator and accessor methods
        public int ISBN { get => iSBN; set => iSBN = value; }
        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }
        public decimal Price { get => price; set => price = value; }
        public string Source { get => source; set => source = value; }
    }
}
