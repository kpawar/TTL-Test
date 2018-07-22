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
            try
            {
                string line;

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
            catch (Exception exception)
            {
                throw exception;
            }            
        }
        public bool Read(out string name, out string address)
        {
            try
            {
                name = address = "";
                string line;

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
            catch (Exception exception)
            {
                throw exception;
            }            
        }

        public void Dispose()
        {
            if (_streamReader != null)
                _streamReader.Dispose();            
        }        
    }
}
