using ContributionTracker.Interfaces;
using ContributionTracker.Models;

namespace ContributionTracker.InterfaceImplementations
{
    public class TransactionService : ITransactionService
    {
        private ITransactionRepository _transactionRepository;
        public TransactionService(IEnumerable<ITransactionRepository> repositories) 
        {
            foreach (var repository in repositories) 
            {
                if(repository is JsonRepository) // here we can change, which data access we want to use
                {
                    _transactionRepository = repository;
                    break;
                }
            }
        }
        public void AddTransaction(TransactionDto transaction)
        {
            _transactionRepository.Write(transaction);
        }

        public void DeleteTransaction(Guid transactionId)
        {
            _transactionRepository.Delete(transactionId);
        }

        public List<Transaction> GetAllTransactions()
        {
            return _transactionRepository.Read();
        }

        public void UpdateTransaction(TransactionDto transaction)
        {
            _transactionRepository.Update(transaction);
        }
    }
}
