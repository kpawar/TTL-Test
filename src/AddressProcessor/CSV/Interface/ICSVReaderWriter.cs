using static AddressProcessing.CSV.CSVReaderWriter;

namespace AddressProcessing.CSV.Interface
{
    public interface ICSVReaderWriter
    {
        void Open(string fileName, Mode mode);
        void Write(params string[] columns);
        bool Read(string column1, string column2);
        bool Read(out string column1, out string column2);
        void Close();
    }
}
