using ContributionTracker.Models;

namespace ContributionTracker.Interfaces
{
    public interface ITransactionUpdate
    {
        void Update(TransactionDto transaction);
    }
}
