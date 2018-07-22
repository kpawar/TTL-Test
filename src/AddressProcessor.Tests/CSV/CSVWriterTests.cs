using AddressProcessing.CSV.Interface;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressProcessing.CSV;
using System.IO;

namespace AddressProcessing.Tests.CSV
{
    [TestFixture]
    public class CSVWriterTests
    {
        private readonly string TestContacts = "testContacts"; 
        private CSVWriter _csvWriter;
        
        [TestCase("Leke","Harrow","747","leke@leke.com")]
        public void Should_Write_Tab_Delimited_Line(params string[] values)
        {
            using (_csvWriter = new CSVWriter())
            {
                _csvWriter.Open(TestContacts);
                _csvWriter.Write(values);                
            }
            using (CSVReader reader = new CSVReader())
            {
                reader.Open(TestContacts);
                string name, address;
                bool canRead = reader.Read(out name, out address);
                Assert.AreEqual(name, "Leke");
                Assert.AreEqual(address, "Harrow");
                Assert.True(canRead);
            }
        }
        [TestCase("")]
        public void Should_Not_Write_Tab_Delimited_Line(params string[] values)
        {
            using (_csvWriter = new CSVWriter())
            {
                _csvWriter.Open(TestContacts);
                _csvWriter.Write(values);
            }
            using (CSVReader reader = new CSVReader())
            {
                reader.Open(TestContacts);
                string name, address;
                bool canRead = reader.Read(out name, out address);                
                Assert.False(canRead);
            }
        }
        [TearDown]
        public void CleanUp()
        {    
            File.Delete(TestContacts);
        }
    }
}
