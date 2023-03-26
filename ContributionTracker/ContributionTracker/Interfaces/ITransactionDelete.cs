using ContributionTracker.Models;

namespace ContributionTracker.Interfaces
{
    public interface ITransactionDelete
    {
        void Delete(TransactionDto transaction);
    }
}
