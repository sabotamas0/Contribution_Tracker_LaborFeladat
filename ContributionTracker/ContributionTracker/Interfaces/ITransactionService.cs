using ContributionTracker.Models;

namespace ContributionTracker.Interfaces
{
    public interface ITransactionService
    {
        List<Transaction> GetAllTransactions();
        void AddTransaction(TransactionDto transaction);
        void DeleteTransaction(TransactionDto transaction);
        void UpdateTransaction(TransactionDto transaction);
    }
}
