using ContributionTracker.Models;

namespace ContributionTracker.Interfaces
{
    public interface ITransactionProvider
    {
        IList<Transaction> Read();
    }
}
