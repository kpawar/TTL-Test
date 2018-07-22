using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressProcessing.CSV.Interface
{
    /* This interface will allow us to abstract the csvwriter implementation
     * and make it easier to write tests and swap implementations in future 
     * and make use of dependency injection
     */
    public interface ICSVWriter
    {
        void Open(string fileName);        
        void Write(params string[] columns);        
    }
}
