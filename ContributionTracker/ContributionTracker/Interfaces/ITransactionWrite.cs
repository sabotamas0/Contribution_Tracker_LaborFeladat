using ContributionTracker.Models;

namespace ContributionTracker.Interfaces
{
    public interface ITransactionWrite
    {
        void Write(TransactionDto transaction);
    }
}
