using System.Data;
using AddressBookLinq;
using NUnit.Framework;

namespace NUnitTestProject
{
    public class AddressBookLinqTest
    {
        AddressBookService addressBook;
           [SetUp]
        public void Setup()
        {
            addressBook = new AddressBookService();
            addressBook.CreateAddressBookTable();
        }

        [Test]
        public void GivenTable_WhenChecked_ShouldRetunrTable()
        {
            Assert.AreEqual(addressBook.AddressBook.TableName, "AddressBook");
        }

        [Test]
        public void GivenContact_WhenAdded_ShouldReturnRow()
        {
            
            Contact contact = new Contact("Sam", "Sher", "Shivajinagr", "Pune",  "State", "111 222", "91 2837373737", "sam@g.com");
            DataRow result = addressBook.AddContact(contact);
            DataRow row = addressBook.AddressBook.NewRow();
            row["FirstName"] = "Sam";
            row["LastName"] = "Sher";
            row["Address"] = "Shivajinagr";
            row["City"] = "Pune";
            row["State"] = "State";
            row["Zip"] = "111 222";
            row["PhoneNumber"] = "91 2837373737";
            row["Email"] = "sam@g.com";

            Assert.AreEqual(row["Zip"], result["Zip"]);
        }

        [Test]
        public void GivenTable_EditedUsingName_ShouldReturnDataRow()
        {
            Contact contact = new Contact("Sam", "Sher", "Shivajinagr", "Pune", "State", "111 222", "91 2837373737", "sam@g.com");
            addressBook.AddContact(contact);
            DataRow result = addressBook.EditContactUsingName("Sam Sher", "Address", "kondhwa");
            DataRow row = addressBook.AddressBook.NewRow();
            row["FirstName"] = "Sam";
            row["LastName"] = "Sher";
            row["Address"] = "kondhwa";
            row["City"] = "Pune";
            row["State"] = "State";
            row["Zip"] = "111 222";
            row["PhoneNumber"] = "91 2837373737";
            row["Email"] = "sam@g.com";
            
            Assert.AreEqual(row[2], result[2]);
        }

    }
}