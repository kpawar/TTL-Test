using AddressProcessing.CSV.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressProcessing.CSV
{
    //implement Idispoable to prevent memory leakage
    //implement ICSVWriter interface for abstraction and TDD
    public class CSVWriter : ICSVWriter, IDisposable
    {
        private StreamWriter _streamWriter;

        public void Open(string fileName)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(fileName);
                _streamWriter = fileInfo.CreateText();
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                throw fileNotFoundException;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void Write(params string[] columns)
        {
            //string.join is more efficient and cleaner
            WriteLine(string.Join("\t", columns));
        }

        private void WriteLine(string line)
        {
            _streamWriter.WriteLine(line);
        }
        //Implement Idisposable to prevent memory leakage
        public void Dispose()
        {
            if (_streamWriter != null)
                _streamWriter.Dispose();
        }

    }
}
