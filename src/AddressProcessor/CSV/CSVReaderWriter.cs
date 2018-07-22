using AddressProcessing.CSV.Interface;
using System;
using System.IO;

namespace AddressProcessing.CSV
{
    /*
        2) Refactor this class into clean, elegant, rock-solid & well performing code, without over-engineering.
           Assume this code is in production and backwards compatibility must be maintained.
    */

        /* Include separate classes for reading and writing
         */
    public class CSVReaderWriter : IDisposable, ICSVReaderWriter
    {
        private CSVReader _csvReader;
        private CSVWriter _csvWriter;        

        /*can be moved to its own enum class but the assumption here
         * is since this should be backwards compatible, we will leave it in this class
        */
        [Flags]
        public enum Mode { Read = 1, Write = 2 };

        public void Open(string fileName, Mode mode)
        {
            if (mode == Mode.Read)
            {
                _csvReader = new CSVReader();
                _csvReader.Open(fileName);
            }
            else if (mode == Mode.Write)
            {
                _csvWriter = new CSVWriter();
                _csvWriter.Open(fileName);
            }
        }
        public void Write(params string[] columns)
        {
            _csvWriter.Write(columns);
        }

        public bool Read(string column1, string column2)
        {
            return _csvReader.Read(column1, column2);
        }

        public bool Read(out string column1, out string column2)
        {
            return _csvReader.Read(out column1, out column2);
        }

        //Implement and call Idisposable to prevent memory leaks
        public void Close()
        {
            Dispose();
        }
    
        public void Dispose()
        {
            if (_csvReader != null)
                _csvReader.Dispose();

            if (_csvWriter != null)
                _csvWriter.Dispose();
        }
    }
}
