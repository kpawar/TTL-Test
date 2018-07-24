using System;

namespace AddressProcessing.CSV.Interface
{
    /* This interface will allow us to abstract the csvwriter implementation
     * and make it easier to write tests and swap implementations in future 
     * and make use of dependency injection
     */
    public interface ICSVWriter : IDisposable
    {
        void Open(string fileName);        
        void Write(params string[] columns);
        string CreateLine(params string[] columns);
    }
}
