using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace AddressBookLinq
{
    public class AddressBookService
    {
        private DataSet AddressBookDB = new DataSet("AddressBookService");
        public DataTable AddressBook;
        public DataTable CreateAddressBookTable()
        {
            AddressBook = new DataTable("AddressBook");
            DataColumn column;
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "FirstName";
            AddressBook.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "LastName";
            AddressBook.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Address";
            AddressBook.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "City";
            AddressBook.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "State";
            AddressBook.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Zip";
            AddressBook.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "PhoneNumber";
            AddressBook.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Email";
            AddressBook.Columns.Add(column);
            AddressBookDB.Tables.Add(AddressBook);
            return AddressBook;
        }

        public DataRow EditContactUsingName(string name, string Column, string data)
        {
            DataRow row = AddressBook.AsEnumerable().Where(contact => contact.Field<string>("FirstName") + " " + contact.Field<string>("LastName") == name)
                .FirstOrDefault();
            row[Column] = data;
            return row;
        }

        public DataRow AddContact(Contact contact)
        {
            AddressBook.Rows.Add(contact.FirstName, contact.LastName,
                contact.Address, contact.City, contact.State, contact.Zip, contact.PhoneNumber, contact.Email);
            return AddressBook.Rows[AddressBook.Rows.Count-1];
        }

        public bool DeleteContact(string name)
        {
            DataRow row = AddressBook.AsEnumerable().Where(contact => contact.Field<string>("FirstName") + " " + contact.Field<string>("LastName") == name)
                .FirstOrDefault();
            row.Delete();
            return row.RowState.Equals(DataRowState.Detached);
        }

        public void printTable(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                foreach (DataColumn column in dataTable.Columns)
                {
                    Console.WriteLine(column.ColumnName.PadRight(10) + " : " + row[column] + " ");
                }
                Console.WriteLine();
            }
        }

        static void Main()
        {
            Console.WriteLine("Hello World!");
        }
    }
}
