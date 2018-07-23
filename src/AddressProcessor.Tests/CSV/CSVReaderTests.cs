using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressProcessing;
using AddressProcessing.CSV.Interface;
using AddressProcessing.CSV;

namespace AddressProcessing.Tests.CSV
{
    [TestFixture]
    public class CSVReaderTests
    {
        private readonly string ContactsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"test_data\contacts.csv");
        private readonly string NoContactsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"test_data\no-contacts.csv");
        private ICSVReader _csvReader;

        [Test]
        public void Should_Not_Throw_Exception_If_File_Exists()
        {
            //Arrange
            using (_csvReader = new CSVReader())
            {
                //Act & Assert
                Assert.DoesNotThrow(() => _csvReader.Open(ContactsFile));
            }
        }

        [Test]
        public void Should_Throw_Exception_If_File_Does_Not_Exist()
        {
            //Arrange
            using (_csvReader = new CSVReader())
            {
                //Act & Assert
                Assert.Throws(typeof(FileNotFoundException), () => _csvReader.Open("Not_Exists"));
            }
        }

        [Test]
        public void Should_Return_Columns_Read_When_Contact_Exists_In_File()
        {
            //Arrange
            using (_csvReader = new CSVReader())
            {
                //Act
                _csvReader.Open(ContactsFile);
                string name, address;
                bool canRead = _csvReader.Read(out name, out address);

                //Assert
                Assert.AreEqual(name, "Shelby Macias");
                Assert.AreEqual(address, "3027 Lorem St.|Kokomo|Hertfordshire|L9T 3D5|England");
                Assert.True(canRead);
            }
        }

        [Test]
        public void Should_Read_When_Contact_Exists_In_File()
        {
            //Arrange
            using (_csvReader = new CSVReader())
            {
                //Act
                _csvReader.Open(ContactsFile);
                string name = "";
                string address = "";
                bool canRead = _csvReader.Read(name, address);
                //Assert
                Assert.True(canRead);
            }
        }

        [Test]
        public void Should_Not_Return_Read_Contacts_If_There_Is_No_Contact()
        {
            //Arrange
            using (_csvReader = new CSVReader())
            {
                //Act
                _csvReader.Open(NoContactsFile);
                string name, address;
                bool canRead = _csvReader.Read(out name, out address);

                //Assert
                Assert.False(canRead);
                Assert.IsEmpty(name);
                Assert.IsEmpty(address);
            }
        }

        [Test]
        public void Should_Not_Read_Contacts_If_There_Is_No_Contact()
        {
            //Arrange
            using (_csvReader = new CSVReader())
            {
                //Act
                _csvReader.Open(NoContactsFile);
                string name = "";
                string address = "";
                bool canRead = _csvReader.Read(name, address);

                //Assert
                Assert.False(canRead);
            }
        }
    }
}
