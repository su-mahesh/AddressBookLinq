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
            Contact contact = new Contact("Sam", "Sher", "Shivajinagr", "Pune", "Mah", "111 222", "91 2837373737", "sam@g.com");
            Contact contact1 = new Contact("Maj", "Sin", "vile", "mumbai", "Mah", "111 222", "91 2837373737", "maj@g.com");
            Contact contact2 = new Contact("Sim", "Ran", "patiala", "patiala", "Punjab", "111 222", "91 2837373737", "sim@g.com");
            Contact contact3 = new Contact("M", "K", "vanas", "Pune", "Mah", "111 222", "91 2837373737", "mk@g.com");

            addressBook.AddContact(contact);
            addressBook.AddContact(contact1);
            addressBook.AddContact(contact2);
            addressBook.AddContact(contact3);
        }

        [Test]
        public void GivenTable_WhenChecked_ShouldRetunrTable()
        {
            Assert.AreEqual(addressBook.AddressBook.TableName, "AddressBook");
        }

        [Test]
        public void GivenContact_WhenAdded_ShouldReturnRow()
        {            
            Contact contact = new Contact("Sam", "Sher", "Shivajinagr", "Pune",  "Mah", "111 222", "91 2837373737", "sam@g.com");
            DataRow result = addressBook.AddContact(contact);
            DataRow row = addressBook.AddressBook.NewRow();
            row["FirstName"] = "Sam";
            row["LastName"] = "Sher";
            row["Address"] = "Shivajinagr";
            row["City"] = "Pune";
            row["State"] = "Mah";
            row["Zip"] = "111 222";
            row["PhoneNumber"] = "91 2837373737";
            row["Email"] = "sam@g.com";

            Assert.AreEqual(row["Zip"], result["Zip"]);
        }

        [Test]
        public void GivenTable_EditedUsingName_ShouldReturnDataRow()
        {
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

        [Test]
        public void GivenTable_WhenDeletedContact_ShouldReturnTrue()
        {
            bool result = addressBook.DeleteContact("Maj Sin");
            Assert.IsTrue(result);
        }

        [Test]
        public void GivenTable_WhenRetrievePersonsBelongToCityOrState_ShouldReturnDataTable()
        {
            DataRow row = addressBook.AddressBook.NewRow();
            row["FirstName"] = "Sam";
            row["LastName"] = "Sher";
            row["Address"] = "kondhwa";
            row["City"] = "Pune";
            row["State"] = "State";
            row["Zip"] = "111 222";
            row["PhoneNumber"] = "91 2837373737";
            row["Email"] = "sam@g.com";
            DataTable table = addressBook.RetrievePersonsFromCityOrState("City", "Pune");
            Assert.AreEqual(row["City"], table.Rows[0]["City"]);
        }

        [Test]
        public void GivenTable_WhenQueriedSizeOfAddressBookByCityOrState_ShouldReturnExpected()
        {
            int result = addressBook.GetCountOfPersonsInCityOrState("City", "pune");
            Assert.AreEqual(2, result);
        }

        [Test]
        public void GivenTable_WhenRetrieveSortedAddressBookInCity_ShouldReturnExpected()
        {
            DataTable result = addressBook.GetSortedAddressBookByPersonsNameInCity("pune");
            Assert.AreEqual(result.Rows[0]["FirstName"], "M");
        }
    }
}