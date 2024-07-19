using Boook.ObjectsFromDatabase;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Boook
{
    internal class DataAccess
    {
        internal static void InitializeDatabase()
        {
            string dbpath = "BookShopdb";
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                //var dropTable = new SqliteCommand("DROP TABLE Transactions; DROP TABLE Books;", db);
                //dropTable.ExecuteNonQuery();

                string tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Books (" +
                    "ISBN INTEGER PRIMARY KEY, " +
                    "Title NVARCHAR(50) NULL, " +
                    "Description NVARCHAR(10) NULL, " +
                    "Price NUMERIC DEFAULT 0, " +
                    "Source NVARCHAR(2) NULL)";
                var createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();

                tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Customers (" +
                    "Customer_Id INTEGER PRIMARY KEY, " +
                    "Customer_Name NVARCHAR(100) NULL, " +
                    "Address NVARCHAR(200) NULL, " +
                    "Email NVARCHAR(50) NULL)";
                createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();

                tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Transactions (" +
                    "Trans_Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "ISBN INTEGER, " +
                    "Customer_Id INTEGER, " +
                    "Quantity INTEGER, " +
                    "Total_Price NUMERIC, " +
                    "FOREIGN KEY (ISBN) REFERENCES Books (ISBN), " +
                    "FOREIGN KEY (Customer_Id) REFERENCES Customers (Customer_Id))";
                createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();

                //var insertCommand = new SqliteCommand();
                //insertCommand.Connection = db;

                //insertCommand.CommandText = "INSERT INTO Books VALUES (567897, 'C# Programming', 'Red', 29.99, 'p1'); " +
                //    "INSERT INTO Books VALUES (6543210, 'Java Programming', 'Blue', 39.99, 'p2'); " +
                //    "INSERT INTO Books VALUES (2131415, 'Python Programming', 'Green', 49.99, 'p3'); " +
                //    "INSERT INTO Books VALUES (7181920, 'JavaScript Programming', 'Yellow', 19.99, 'p4'); " +
                //    "" +
                //    "INSERT INTO Customers VALUES (1, 'Alice Smith', '123 Main St, Anytown, Thailand', 'alice@example.com'); " +
                //    "INSERT INTO Customers VALUES (2, 'Bob Jones', '456 Maple Ave, Anytown, Thailand', 'bob@example.com'); " +
                //    "INSERT INTO Customers VALUES (3, 'Carol White', '789 Oak St, Anytown, Thailand', 'carol@example.com'); " +
                //    "INSERT INTO Customers VALUES (4, 'David Brown', '101 Pine St, Anytown, Thailand', 'david@example.com'); " +
                //    "INSERT INTO Customers VALUES (5, 'Eva Green', '202 Cedar St, Anytown, Thailand', 'eva@example.com'); " +
                //    "INSERT INTO Customers VALUES (6, 'Frank Black', '303 Birch St, Anytown, Thailand', 'frank@example.com'); " +
                //    "INSERT INTO Customers VALUES (7, 'Grace Blue', '404 Willow St, Anytown, Thailand', 'grace@example.com'); " +
                //    "INSERT INTO Customers VALUES (8, 'Henry Yellow', '505 Walnut St, Anytown, Thailand', 'henry@example.com'); " +
                //    "INSERT INTO Customers VALUES (9, 'Ivy Purple', '606 Poplar St, Anytown, Thailand', 'ivy@example.com'); " +
                //    "INSERT INTO Customers VALUES (10, 'Jack Pink', '707 Elm St, Anytown, Thailand', 'jack@example.com'); " +
                //    "" +
                //    "INSERT INTO Transactions (ISBN, Customer_Id, Quantity, Total_Price) VALUES (567897, 1, 1, 29.99); " +
                //    "INSERT INTO Transactions (ISBN, Customer_Id, Quantity, Total_Price) VALUES (6543210, 2, 2, 79.98); " +
                //    "INSERT INTO Transactions (ISBN, Customer_Id, Quantity, Total_Price) VALUES (2131415, 3, 1, 49.99); " +
                //    "INSERT INTO Transactions (ISBN, Customer_Id, Quantity, Total_Price) VALUES (7181920, 4, 3, 59.97); " +
                //    "INSERT INTO Transactions (ISBN, Customer_Id, Quantity, Total_Price) VALUES (567897, 5, 2, 59.98);";
                //insertCommand.ExecuteReader();

                db.Close();
            }
        }
        internal static List<string> GetData(string fieldName, string tableName)
        {
            var entries = new List<string>();
            string dbpath = "BookShopdb";
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var selectCommand = new SqliteCommand
                    ("SELECT @fieldName from @tableName", db);
                selectCommand.Parameters.AddWithValue("@fieldName", fieldName);
                selectCommand.Parameters.AddWithValue("@tableName", tableName);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    entries.Add(query.GetString(0));
                }
                db.Close();
            }

            return entries;
        }

        // Methods for Book Page
        internal static List<Book> GetAllBook()
        {
            var entries = new List<Book>();
            string dbpath = "BookShopdb";
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var selectCommand = new SqliteCommand
                    ("SELECT ISBN, Title, Description, Price, Source FROM Books", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    entries.Add(new Book
                    {
                        ISBN = query.GetInt32(0),
                        Title = query.GetString(1),
                        Description = query.GetString(2),
                        Price = query.GetDecimal(3),
                        Source = query.GetString(4)
                    });
                }
                db.Close();
            }

            return entries;
        }
        internal static void AddBook(Book book)
        {
            string dbpath = "BookShopdb";
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.Transaction = db.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                try
                {
                    insertCommand.CommandText = "INSERT INTO Books VALUES (@ISBN, @Title, @Description, @Price, @Source)";
                    insertCommand.Parameters.AddWithValue("@ISBN", book.ISBN);
                    insertCommand.Parameters.AddWithValue("@Title", book.Title);
                    insertCommand.Parameters.AddWithValue("@Description", book.Description);
                    insertCommand.Parameters.AddWithValue("@Price", book.Price);
                    insertCommand.Parameters.AddWithValue("@Source", book.Source);
                    insertCommand.ExecuteReader();
                    insertCommand.Transaction.Commit();
                    Console.WriteLine("Transaction committed");
                }
                catch (Exception ex)
                {
                    insertCommand.Transaction.Rollback();
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine("Transaction rolled back");
                    throw ex;
                }
                finally
                {
                    insertCommand.Transaction.Dispose();
                    insertCommand.Dispose();
                }
                
                db.Close();
            }
        }
        internal static void UpdateBook(Book book)
        {
            string dbpath = "BookShopdb";
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var updateCommand = new SqliteCommand();
                updateCommand.Connection = db;
                updateCommand.Transaction = db.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                try
                {
                    updateCommand.CommandText = "UPDATE Books " +
                        "SET ISBN = @ISBN, Title = @Title, Description = @Description, Price = @Price, Source = @Source " +
                        "WHERE ISBN = @ISBN";
                    updateCommand.Parameters.AddWithValue("@ISBN", book.ISBN);
                    updateCommand.Parameters.AddWithValue("@Title", book.Title);
                    updateCommand.Parameters.AddWithValue("@Description", book.Description);
                    updateCommand.Parameters.AddWithValue("@Price", book.Price);
                    updateCommand.Parameters.AddWithValue("@Source", book.Source);
                    updateCommand.ExecuteReader();
                    updateCommand.Transaction.Commit();
                    Console.WriteLine("Transaction committed");
                }
                catch (Exception ex)
                {
                    updateCommand.Transaction.Rollback();
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine("Transaction rolled back");
                    throw ex;
                }
                finally
                {
                    updateCommand.Transaction.Dispose();
                    updateCommand.Dispose();
                }

                db.Close();
            }
        }
        internal static void DeleteBook(int ISBN)
        {
            string dbpath = "BookShopdb";
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var deleteCommand = new SqliteCommand();
                deleteCommand.Connection = db;
                deleteCommand.Transaction = db.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                try
                {
                    deleteCommand.CommandText = "DELETE FROM Books WHERE ISBN = @ISBN";
                    deleteCommand.Parameters.AddWithValue("@ISBN", ISBN);
                    deleteCommand.ExecuteReader();
                    deleteCommand.Transaction.Commit();
                    Console.WriteLine("Transaction committed");
                }
                catch (Exception ex)
                {
                    deleteCommand.Transaction.Rollback();
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine("Transaction rolled back");
                    throw ex;
                }
                finally
                {
                    deleteCommand.Transaction.Dispose();
                    deleteCommand.Dispose();
                }

                db.Close();
            }
        }

        // Methods for Customer Page
        internal static List<Customer> GetAllCustomers()
        {
            var entries = new List<Customer>();
            string dbpath = "BookShopdb";

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var selectCommand = new SqliteCommand
                    ("SELECT Customer_Id, Customer_Name, Address, Email FROM Customers", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    entries.Add(new Customer
                    {
                        Customer_Id = query.GetInt32(0),
                        Customer_Name = query.GetString(1),
                        Address = query.GetString(2),
                        Email = query.GetString(3),
                    });
                }
                db.Close();
            }

            return entries;
        }
        internal static void AddCustomer(Customer customer)
        {
            string dbpath = "BookShopdb";
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.Transaction = db.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                try
                {
                    insertCommand.CommandText = "INSERT INTO Customers VALUES (@Customer_Id, @Customer_Name, @Address, @Email)";
                    insertCommand.Parameters.AddWithValue("@Customer_Id", customer.Customer_Id);
                    insertCommand.Parameters.AddWithValue("@Customer_Name", customer.Customer_Name);
                    insertCommand.Parameters.AddWithValue("@Address", customer.Address);
                    insertCommand.Parameters.AddWithValue("@Email", customer.Email);
                    insertCommand.ExecuteReader();
                    insertCommand.Transaction.Commit();
                    Console.WriteLine("Transaction committed");
                }
                catch (Exception ex)
                {
                    insertCommand.Transaction.Rollback();
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine("Transaction rolled back");
                    throw ex;
                }
                finally
                {
                    insertCommand.Transaction.Dispose();
                    insertCommand.Dispose();
                }
                db.Close();
            }
        }
        internal static void UpdateCustomer(Customer customer)
        {
            string dbpath = "BookShopdb";
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var updateCommand = new SqliteCommand();
                updateCommand.Connection = db;
                updateCommand.Transaction = db.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                try
                {
                    updateCommand.CommandText = "UPDATE Customers " +
                        "SET Customer_Id = @Customer_Id, Customer_Name = @Customer_Name, Address = @Address, Email = @Email " +
                        "WHERE Customer_Id = @Customer_Id";
                    updateCommand.Parameters.AddWithValue("@Customer_Id", customer.Customer_Id);
                    updateCommand.Parameters.AddWithValue("@Customer_Name", customer.Customer_Name);
                    updateCommand.Parameters.AddWithValue("@Address", customer.Address);
                    updateCommand.Parameters.AddWithValue("@Email", customer.Email);
                    updateCommand.ExecuteReader();
                    updateCommand.Transaction.Commit();
                    Console.WriteLine("Transaction committed");
                }
                catch (Exception ex)
                {
                    updateCommand.Transaction.Rollback();
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine("Transaction rolled back");
                    throw ex;
                }
                finally
                {
                    updateCommand.Transaction.Dispose();
                    updateCommand.Dispose();
                }

                db.Close();
            }
        }
        internal static void DeleteCustomer(int Customer_Id)
        {
            string dbpath = "BookShopdb";
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var deleteCommand = new SqliteCommand();
                deleteCommand.Connection = db;
                deleteCommand.Transaction = db.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                try
                {
                    deleteCommand.CommandText = "DELETE FROM Customers WHERE Customer_Id = @Customer_Id";
                    deleteCommand.Parameters.AddWithValue("@Customer_Id", Customer_Id);
                    deleteCommand.ExecuteNonQuery();
                    deleteCommand.Transaction.Commit();
                    Console.WriteLine("Transaction committed.");
                }
                catch (Exception ex)
                {
                    deleteCommand.Transaction.Rollback();
                    Console.WriteLine("Error: " + ex.ToString());
                    Console.WriteLine("Transaction rolled back.");
                    throw;
                }
                finally
                {
                    deleteCommand.Transaction.Dispose();
                    deleteCommand.Dispose();
                }

                db.Close();
            }
        }

        // Methods for Buy Page
        internal static List<Book> SearchBookByString(string search)
        {
            var entries = new List<Book>();
            string dbpath = "BookShopdb";
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var selectCommand = new SqliteCommand
                    ("SELECT ISBN, Title, Description, Price, Source FROM Books WHERE Title LIKE @search OR Description LIKE @search", db);
                selectCommand.Parameters.AddWithValue("@search", "%" + search + "%");

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    entries.Add(new Book
                    {
                        ISBN = query.GetInt32(0),
                        Title = query.GetString(1),
                        Description = query.GetString(2),
                        Price = query.GetDecimal(3),
                        Source = query.GetString(4)
                    });
                }
                db.Close();
            }

            return entries;
        }
        internal static List<Book> SearchBookByInt(int search)
        {
            var entries = new List<Book>();
            string dbpath = "BookShopdb";
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var selectCommand = new SqliteCommand
                    ("SELECT ISBN, Title, Description, Price, Source FROM Books WHERE ISBN = @search", db);
                selectCommand.Parameters.AddWithValue("@search", search);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    entries.Add(new Book
                    {
                        ISBN = query.GetInt32(0),
                        Title = query.GetString(1),
                        Description = query.GetString(2),
                        Price = query.GetDecimal(3),
                        Source = query.GetString(4)
                    });
                }
                db.Close();
            }

            return entries;
        }
        internal static List<Customer> SearchCustomerByString(string search)
        {
            var entries = new List<Customer>();
            string dbpath = "BookShopdb";
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var selectCommand = new SqliteCommand
                    ("SELECT Customer_Id, Customer_Name, Address, Email FROM Customers WHERE Customer_Name LIKE @search OR Address LIKE @search OR Email LIKE @search", db);
                selectCommand.Parameters.AddWithValue("@search", "%" + search + "%");

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    entries.Add(new Customer
                    {
                        Customer_Id = query.GetInt32(0),
                        Customer_Name = query.GetString(1),
                        Address = query.GetString(2),
                        Email = query.GetString(3)
                    });
                }
                db.Close();
            }

            return entries;
        }
        internal static List<Customer> SearchCustomerByInt(int search)
        { 
            var entries = new List<Customer>();
            string dbpath = "BookShopdb";
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var selectCommand = new SqliteCommand
                    ("SELECT Customer_Id, Customer_Name, Address, Email FROM Customers WHERE Customer_Id = @search", db);
                selectCommand.Parameters.AddWithValue("@search", search);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    entries.Add(new Customer
                    {
                        Customer_Id = query.GetInt32(0),
                        Customer_Name = query.GetString(1),
                        Address = query.GetString(2),
                        Email = query.GetString(3)
                    });
                }
                db.Close();
            }
            return entries;
        }
        internal static decimal GetPrice(int iSBN)
        {
            decimal price;
            string dbpath = "BookShopdb";
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var selectCommand = new SqliteCommand
                    ("SELECT Price from Books WHERE ISBN = @ISBN", db);
                selectCommand.Parameters.AddWithValue("@ISBN", iSBN);

                price = Convert.ToDecimal(selectCommand.ExecuteScalar());

                db.Close();
            }

            return price;
        }
        internal static void AddTransaction(Transaction transaction)
        {
            string dbpath = "BookShopdb";
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.Transaction = db.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                try
                {
                    insertCommand.CommandText = "INSERT INTO Transactions (ISBN, Customer_Id, Quantity, Total_Price) VALUES (@ISBN, @Customer_Id, @Quantity, @Total_Price)";
                    insertCommand.Parameters.AddWithValue("@ISBN", transaction.ISBN);
                    insertCommand.Parameters.AddWithValue("@Customer_Id", transaction.Customer_Id);
                    insertCommand.Parameters.AddWithValue("@Quantity", transaction.Quantity);
                    insertCommand.Parameters.AddWithValue("@Total_Price", transaction.Total_Price);
                    insertCommand.ExecuteReader();
                    insertCommand.Transaction.Commit();
                    Console.WriteLine("Transaction committed");
                }
                catch (Exception ex)
                {
                    insertCommand.Transaction.Rollback();
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine("Transaction rolled back");
                    throw ex;
                }
                finally
                {
                    insertCommand.Transaction.Dispose();
                    insertCommand.Dispose();
                }
                db.Close();
            }
        }
        internal static List<int> GetISBNs()
        {
            var entries = new List<int>();
            string dbpath = "BookShopdb";
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var selectCommand = new SqliteCommand
                    ("SELECT ISBN FROM Books", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    entries.Add(query.GetInt32(0));
                }
                db.Close();
            }

            return entries;
        }
        internal static List<int> GetCustomerIds()
        {
            var entries = new List<int>();
            string dbpath = "BookShopdb";
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var selectCommand = new SqliteCommand
                    ("SELECT Customer_Id FROM Customers", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    entries.Add(query.GetInt32(0));
                }
                db.Close();
            }

            return entries;
        }

        // Methods for Transaction Page
        internal static void DeleteTransaction(int transId)
        {
            string dbpath = "BookShopdb";
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var deleteCommand = new SqliteCommand();
                deleteCommand.Connection = db;
                deleteCommand.Transaction = db.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                try
                {
                    deleteCommand.CommandText = "DELETE FROM Transactions WHERE Trans_Id = @Trans_Id";
                    deleteCommand.Parameters.AddWithValue("@Trans_Id", transId);
                    deleteCommand.ExecuteNonQuery();
                    deleteCommand.Transaction.Commit();
                    Console.WriteLine("Transaction committed.");
                }
                catch (Exception ex)
                {
                    deleteCommand.Transaction.Rollback();
                    Console.WriteLine("Error: " + ex.ToString());
                    Console.WriteLine("Transaction rolled back.");
                    throw;
                }
                finally
                {
                    deleteCommand.Transaction.Dispose();
                    deleteCommand.Dispose();
                }
                db.Close();
            }
        }
        internal static DataTable GetTransactions()
        {
            DataTable dt = new DataTable();
            string dbpath = "BookShopdb";
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var selectCommand = new SqliteCommand
                    ("SELECT Trans_Id, ISBN, Customer_Id, Quantity, Total_Price FROM Transactions", db);

                SqliteDataReader query = selectCommand.ExecuteReader();
                dt.Load(query);
                db.Close();
            }
            return dt;
        }
    }
}
