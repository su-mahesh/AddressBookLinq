using System.Data;
using AddressBookLinq;
using NUnit.Framework;

namespace NUnitTestProject
{
    public class AddressBookLinqTest
    {
        AddressBook addressBook;
           [SetUp]
        public void Setup()
        {
            addressBook = new AddressBook();
        }

        [Test]
        public void GivenTable_WhenChecked_ShouldRetunrTable()
        {
            DataTable result = addressBook.CreateAddressBookTable();
            Assert.AreEqual(result.TableName, "AddressBook");
        }
    }
}