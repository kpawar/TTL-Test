using AddressProcessing.CSV.Interface;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressProcessing.CSV;
using System.IO;
using NSubstitute;
namespace AddressProcessing.Tests.CSV
{
    [TestFixture]
    public class CSVWriterTests
    {         
        private ICSVWriter _csvWriter;
        //should write a tab delimited line
        [TestCase("Leke", "Harrow", "747", "leke@leke.com")]
        public void Should_Write_Tab_Delimited_Line(params string[] values)
        {
            //Arrange
            using (_csvWriter = new CSVWriter())
            {
                //Act
                string tabbedLine = _csvWriter.CreateLine(values);
                string[] columns = tabbedLine.Split('\t');
                //Assert
                Assert.That(columns.Count() == 4);
                Assert.AreEqual(columns[0], "Leke");
                Assert.AreEqual(columns[1], "Harrow");
                Assert.AreEqual(columns[2], "747");
                Assert.AreEqual(columns[3], "leke@leke.com");

            }    
        }
        //should not attempt to write an empty line in a file
        [TestCase("")]
        public void Should_Not_Write_Tab_Delimited_Line(params string[] values)
        {
            //arrange
            using (_csvWriter = new CSVWriter())
            {
                //act
                string tabbedLine = _csvWriter.CreateLine(values);
                //assert
                Assert.IsEmpty(tabbedLine);                                
            }
        }        
    }
}
