using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddressProcessing.CSV.Interface;
using NUnit.Framework;
using NSubstitute;
using AddressProcessing.CSV;

namespace Csv.Tests
{
    [TestFixture]
    public class CSVReaderWriterTests
    {
        private ICSVReader _csvReader;
        private ICSVWriter _csvWriter;
        private ICSVReaderWriter _csvReaderWriter;
        private string FilePath = "file";
        [Test]
        public void Should_Open_Reader_When_Mode_Is_Read()
        {
            
            //Arrange
            _csvReader = Substitute.For<ICSVReader>();        
            _csvWriter = Substitute.For<ICSVWriter>();
            _csvReaderWriter = new CSVReaderWriter(_csvReader, _csvWriter);

            //Act
            _csvReaderWriter.Open(FilePath, CSVReaderWriter.Mode.Read);

            //Assert
            _csvReader.Received().Open(FilePath);
            _csvWriter.DidNotReceive().Open(FilePath);            
        }
        [TestCase]
        public void Should_Open_Writer_When_Mode_Is_Write()
        {
            //Arrange
            _csvReader = Substitute.For<ICSVReader>();
            _csvWriter = Substitute.For<ICSVWriter>();
            _csvReaderWriter = new CSVReaderWriter(_csvReader, _csvWriter);

            //Act
            _csvReaderWriter.Open(FilePath, CSVReaderWriter.Mode.Write);

            //Assert            
            _csvWriter.Received().Open(FilePath); 
            _csvReader.DidNotReceive().Open(FilePath);
        }
        [TestCase]
        public void Should_Close_Resources_When_Close_Is_Called()
        {
            //Arrange
            _csvReader = Substitute.For<ICSVReader>();
            _csvWriter = Substitute.For<ICSVWriter>();
            _csvReaderWriter = new CSVReaderWriter(_csvReader, _csvWriter);

            //Act
            _csvReaderWriter.Open(FilePath, CSVReaderWriter.Mode.Read);
            _csvReaderWriter.Close();

            //Assert
            _csvReader.Received().Dispose();
            _csvWriter.Received().Dispose();
        }
        
        public void Should_Return_Columns_Read()
        {

        }

        public void Should_Read_Columns()
        {

        }

        public void Should_Write_Columns()
        {

        }
    }
}
