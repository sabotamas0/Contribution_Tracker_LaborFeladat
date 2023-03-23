using ContributionTracker.Interfaces;
using ContributionTracker.Models;
using System.Text.Json;

namespace ContributionTracker.InterfaceImplementations
{
    public class JsonDataProvider : ITransactionProvider
    {

        private readonly string _filename;

        public JsonDataProvider(string filename)
        {
            _filename = filename;
        }

        public IList<Transaction> Read()
        {
            throw new NotImplementedException();
        }
    }
}
