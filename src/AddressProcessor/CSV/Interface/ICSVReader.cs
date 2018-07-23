using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;   

namespace AddressProcessing.CSV.Interface
{
    /* This interface will allow us to abstract the csvreader implementation
     * and make it easier to write tests and swap implementations in future 
     * and make use of dependency injection
     */
    public interface ICSVReader : IDisposable
    {
        void Open(string fileName);
        bool Read(string column1, string column2);
        bool Read(out string column1, out string column2);                
    }
}
