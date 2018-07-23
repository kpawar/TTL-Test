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
    //implement ICSVReader interface for abstraction and TDD
    public class CSVReader : ICSVReader, IDisposable
    {
        private StreamReader _streamReader = null;
        private readonly char[] _separator = { '\t' };
        public void Open(string fileName)
        {
            //include try catch to handle more specific exceptions associated with the System.IO library
            try
            {
                _streamReader = File.OpenText(fileName);
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
        public bool Read(string name, string address)
        {
            string line;
            //optimized the while check 
            while (((line = _streamReader.ReadLine()) != null)
                && !string.IsNullOrEmpty(line))
            {
                string[] columns = line.Split(_separator);
                if (columns.Length == 4)
                {
                    name = columns[0];
                    address = columns[1];
                    return true;
                }
                else
                    return false;

            }
            return false;
        }
        public bool Read(out string name, out string address)
        {
            name = address = "";
            string line;
            //optimized the while check 
            while (((line = _streamReader.ReadLine()) != null)
                && !string.IsNullOrEmpty(line))
            {
                string[] columns = line.Split(_separator);
                if (columns.Length == 4)
                {
                    name = columns[0];
                    address = columns[1];
                    return true;
                }
                else
                    return false;

            }
            return false;

        }

        public void Dispose()
        {
            if (_streamReader != null)
                _streamReader.Dispose();
        }
    }
}
