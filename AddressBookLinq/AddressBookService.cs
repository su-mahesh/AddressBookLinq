using System;
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
        public DataTable Type;
        public DataTable ContactType;

        public AddressBookService()
        {
            AddressBook = new DataTable("AddressBook");
            ContactType = new DataTable("ContactType");
            Type = new DataTable("Type");

            AddressBook.Columns.Add("FirstName", typeof(string));
            AddressBook.Columns.Add("LastName", typeof(string));
            AddressBook.Columns.Add("Address", typeof(string));
            AddressBook.Columns.Add("City", typeof(string));
            AddressBook.Columns.Add("State", typeof(string));
            AddressBook.Columns.Add("Zip", typeof(string));
            AddressBook.Columns.Add("PhoneNumber", typeof(string));
            AddressBook.Columns.Add("Email", typeof(string));
            AddressBook.Columns.Add("Name", typeof(string));
            AddressBook.PrimaryKey = new DataColumn[] { AddressBook.Columns["Name"] };
            AddressBookDB.Tables.Add(AddressBook);

            DataColumn dc = Type.Columns.Add("TypeID", typeof(int));
            dc.AutoIncrement = true;
            dc.AutoIncrementSeed = 1;
            dc.AutoIncrementStep = 1;
            Type.Columns.Add("Type", typeof(string));
            ContactType.Columns.Add("TypeID", typeof(int));
            ContactType.Columns.Add("Name", typeof(string));

            Type.Rows.Add(null, "Family");
            Type.Rows.Add(null, "Friends");
            Type.Rows.Add(null, "Profession");
            AddressBookDB.Tables.Add(Type);
            AddressBookDB.Tables.Add(ContactType);
            ForeignKeyConstraint foreignKeyOnContactTypeTypeID = new ForeignKeyConstraint(
                 "foreignKeyOnContactType_TypeID", Type.Columns["TypeID"], ContactType.Columns["TypeID"]);
            ForeignKeyConstraint foreignKeyOnContactTypeName = new ForeignKeyConstraint(
                 "foreignKeyOnContactType_Name", AddressBook.Columns["Name"], ContactType.Columns["Name"]);

            ContactType.Constraints.Add(foreignKeyOnContactTypeTypeID);
            ContactType.Constraints.Add(foreignKeyOnContactTypeName);
    }


        /// <summary>
        /// Edits the name of the contact using.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="Column">The column.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public DataRow EditContactUsingName(string name, string Column, string data)
        {
            DataRow row = AddressBook.AsEnumerable().Where(contact => contact.Field<string>("FirstName") + " " + contact.Field<string>("LastName") == name)
                .FirstOrDefault();
            row[Column] = data;
            return row;
        }
        /// <summary>
        /// Adds the contact.
        /// </summary>
        /// <param name="contact">The contact.</param>
        /// <returns></returns>
        public DataRow AddContact(Contact contact)
        {
            string Name = contact.FirstName + " " + contact.LastName;
            AddressBook.Rows.Add(contact.FirstName, contact.LastName,
                contact.Address, contact.City, contact.State, contact.Zip, contact.PhoneNumber, contact.Email, Name);
            int TypeID = Type.AsEnumerable().Where(type => type.Field<string>("Type").Equals(contact.Type)).Select(type => type.Field<int>("TypeID")).FirstOrDefault();
            ContactType.Rows.Add(TypeID, Name);
            return AddressBook.Rows[^1];
        }
        /// <summary>
        /// Deletes the contact.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public bool DeleteContact(string name)
        {
            DataRow row = AddressBook.AsEnumerable().Where(contact => contact.Field<string>("FirstName") + " " + contact.Field<string>("LastName") == name)
                .FirstOrDefault();
            row.Delete();
            return row.RowState.Equals(DataRowState.Detached);
        }
        /// <summary>
        /// Prints the table.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
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
        /// <summary>
        /// Gets the state of the count of persons in city or.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public int GetCountOfPersonsInCityOrState(string column, string param)
        {
            int count = 0;
            count = AddressBook.AsEnumerable().Where(contact => contact.Field<string>(column).Equals(param, StringComparison.OrdinalIgnoreCase)).Count();
            return count;
        }
        /// <summary>
        /// Retrieves the state of the persons from city or.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Gets the sorted address book by persons name in city.
        /// </summary>
        /// <param name="city">The city.</param>
        /// <returns></returns>
        public DataTable GetSortedAddressBookByPersonsNameInCity(string city)
        {
            return AddressBook.AsEnumerable().Where(contact => contact.Field<string>("City").Equals(city, StringComparison.OrdinalIgnoreCase))
                .OrderBy(contact => contact.Field<string>("FirstName") + " " + contact.Field<string>("LastName")).CopyToDataTable();
        }       
    }
}
