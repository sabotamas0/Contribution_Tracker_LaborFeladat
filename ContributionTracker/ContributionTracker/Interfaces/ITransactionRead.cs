using ContributionTracker.Models;

namespace ContributionTracker.Interfaces
{
    public interface ITransactionRead
    {
        List<Transaction> Read();
    }
}
