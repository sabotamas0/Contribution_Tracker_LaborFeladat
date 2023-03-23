using ContributionTracker.Models;

namespace ContributionTracker.Interfaces
{
    public interface ITransactionWriter
    {
        void Write(Transaction transaction);
    }
}
