using ContributionTracker.Models;

namespace ContributionTracker.Interfaces
{
    public interface ITransactionRepository 
    {
        List<Transaction> Read();
        void Write(TransactionDto transaction);
        void Update(TransactionDto transaction);
        void Delete(Guid transactionId);
	}
}
