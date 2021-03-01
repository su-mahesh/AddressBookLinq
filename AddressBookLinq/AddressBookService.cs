﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace AddressBookLinq
{
    public class AddressBookService
    {
        private readonly DataSet AddressBookDB = new DataSet("AddressBookService");
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

        public void PrintTable(DataTable dataTable)
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

        public int GetCountOfPersonsInCityOrState(string column, string param)
        {
            int count = 0;
            count = AddressBook.AsEnumerable().Where(contact => contact.Field<string>(column).Equals(param, StringComparison.OrdinalIgnoreCase)).Count();
            return count;
        }

        public DataTable RetrievePersonsFromCityOrState(string field, string param)
        {
            DataTable table;
            try
            {
                var rows = AddressBook.AsEnumerable().Where(contact => contact.Field<string>(field) == param);
                table = rows.Any() ? rows.CopyToDataTable() : null;
            }
            catch (Exception)
            {

                throw;
            }           
            return table;
        }

        public DataTable GetSortedAddressBookByPersonsNameInCity(string city)
        {
            return AddressBook.AsEnumerable().Where(contact => contact.Field<string>("City").Equals(city, StringComparison.OrdinalIgnoreCase))
                .OrderBy(contact => (contact.Field<string>("FirstName") + " " + contact.Field<string>("LastName"))).CopyToDataTable();
        }

        static void Main()
        {
            Console.WriteLine("Hello World!");
        }
    }
}