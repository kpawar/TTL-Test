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
        private string Name = "leke";
        private string Address = "harrow";
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
        [Test]
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
        [Test]
        public void Should_Dispose_Resources_When_Closed()
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
        
        [Test]
        public void Should_Return_Columns_When_Line_Is_Read_Successfully()
        {
            //Arrange
            _csvReader = Substitute.For<ICSVReader>();
            _csvWriter = Substitute.For<ICSVWriter>();
            _csvReaderWriter = new CSVReaderWriter(_csvReader, _csvWriter);
            string name, address;
            _csvReader.Read(out name, out address)
                .Returns(x => { x[0] = Name; x[1] = Address; return true; });
            //Act
            _csvReaderWriter.Open(FilePath, CSVReaderWriter.Mode.Read);
            var result = _csvReaderWriter.Read(out name, out address);

            //Assert            
            Assert.True(result);
            Assert.AreEqual(name, Name);
            Assert.AreEqual(address, Address);
        }
        [Test]
        public void Should_Read_Line_Successfully_When_File_Is_Read()
        {
            //Arrange
            _csvReader = Substitute.For<ICSVReader>();
            _csvWriter = Substitute.For<ICSVWriter>();
            _csvReaderWriter = new CSVReaderWriter(_csvReader, _csvWriter);

            //Act
            _csvReaderWriter.Open(FilePath, CSVReaderWriter.Mode.Read);
            _csvReaderWriter.Read(Name, Address);

            //Assert
            _csvReader.Received().Read(Name,Address);
        }

        [Test]
        public void Should_Write_Line_When_Columns_Are_Written()
        {
            //Arrange
            _csvReader = Substitute.For<ICSVReader>();
            _csvWriter = Substitute.For<ICSVWriter>();
            _csvReaderWriter = new CSVReaderWriter(_csvReader, _csvWriter);

            //Act
            _csvReaderWriter.Open(FilePath, CSVReaderWriter.Mode.Write);
            string[] columns = new string[] { Name, Address };
            _csvReaderWriter.Write(columns);

            //Assert
            _csvWriter.Received().Write(columns);
        }
    }
}
