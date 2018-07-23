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
    /*System.IO.Abstractions can be used to abstract out the file creation
    /*behaviour in order to write more tests but for now 
    /* but we don't want to overengineer for stage 1
     * */
    public class CSVWriter : ICSVWriter, IDisposable
    {
        private StreamWriter _streamWriter;

        public void Open(string fileName)
        {
            //include try catch to handle more specific exceptions associated with the System.IO library
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
            //create a separate method to allow us to test the line creation function
            string line = CreateLine(columns);
            if (!string.IsNullOrEmpty(line))
                WriteLine(line);
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

        /// <summary>
        /// Creates a line entry
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public string CreateLine(params string[] columns)
        {
            //this allows us to test this functionality
            return string.Join("\t", columns);
        }
    }
}
