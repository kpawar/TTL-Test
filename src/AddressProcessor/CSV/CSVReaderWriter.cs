using AddressProcessing.CSV.Interface;
using System;

namespace AddressProcessing.CSV
{
    /*
        2) Refactor this class into clean, elegant, rock-solid & well performing code, without over-engineering.
           Assume this code is in production and backwards compatibility must be maintained.
    */

    /* Include separate classes for reading and writing
     * In future revisions, the csvreader and csvwriter
     * will be moved into separate class files
     */
    public class CSVReaderWriter : IDisposable, ICSVReaderWriter
    {
        //separate out the read and write responsibilities
        private ICSVReader _csvReader;
        private ICSVWriter _csvWriter;

        public CSVReaderWriter()
        {
            //for backward compatibility initialize with a concrete implementation
            _csvReader = new CSVReader();
            _csvWriter = new CSVWriter();
        }

        public CSVReaderWriter(ICSVReader csvReader, ICSVWriter csvWriter)
        {
            //going forward this allows for changing implementations, mocking and dependency injection
            _csvReader = csvReader;
            _csvWriter = csvWriter;
        }
        /*can be moved to its own enum class but the assumption here
         * is since this should be backwards compatible, we will leave it in this class
        */
        [Flags]
        public enum Mode { Read = 1, Write = 2 };
        //Keep the open method for backward compatibility
        public void Open(string fileName, Mode mode)
        {
            if (mode == Mode.Read)
            {
                _csvReader.Open(fileName);
            }
            else if (mode == Mode.Write)
            {
                _csvWriter.Open(fileName);
            }
        }
        //Keep the write method for backward compatibility
        public void Write(params string[] columns)
        {
            _csvWriter.Write(columns);
        }

        //Keep the read method for backward compatibility
        public bool Read(string column1, string column2)
        {
            return _csvReader.Read(column1, column2);
        }
        //Keep the read method for backward compatibility
        public bool Read(out string column1, out string column2)
        {
            return _csvReader.Read(out column1, out column2);
        }

        /*Keep the close method for backward compatibility
         * it will call the Idisposable dispose method
         * */
        public void Close()
        {
            Dispose();
        }
        //implement idisposable
        public void Dispose()
        {
            if (_csvReader != null)
                _csvReader.Dispose();

            if (_csvWriter != null)
                _csvWriter.Dispose();
        }
    }
}
