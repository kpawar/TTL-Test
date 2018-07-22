using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AddressProcessing.CSV.CSVReaderWriter;

namespace AddressProcessing.CSV.Interface
{
   interface ICSVReaderWriter
    {
        void Open(string fileName, Mode mode);
        void Write(params string[] columns);
        bool Read(string column1, string column2);
        bool Read(out string column1, out string column2);
        void Close();
    }
}
