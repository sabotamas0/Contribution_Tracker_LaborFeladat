using ContributionTracker.Models;

namespace ContributionTracker.Interfaces
{
    public interface ITransactionService
    {
        List<Transaction> GetAllTransactions();
        void AddTransaction(TransactionDto transaction);
        void DeleteTransaction(Guid transactionId);
        void UpdateTransaction(TransactionDto transaction);
    }
}
