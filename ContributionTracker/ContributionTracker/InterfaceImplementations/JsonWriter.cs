using ContributionTracker.Interfaces;
using ContributionTracker.Models;

namespace ContributionTracker.InterfaceImplementations
{
    public class JsonWriter : ITransactionWriter
    {
        private readonly string _filename;

        public JsonWriter(string filename)
        {
            _filename = filename;
        }

        public void Write(Transaction transaction)
        {
            
        }
    }
}
