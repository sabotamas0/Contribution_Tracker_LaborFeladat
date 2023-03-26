using ContributionTracker.Interfaces;
using ContributionTracker.Models;

namespace ContributionTracker.InterfaceImplementations
{
    public class TransactionService : ITransactionService
    {
        private ITransactionCrud _transactionCrud;
        public TransactionService(IEnumerable<ITransactionCrud> Cruds) 
        {
            foreach (var Crud in Cruds) 
            {
                if(Crud is JsonCrud) // here we can change, which data access we want to use
                {
                    _transactionCrud = Crud;
                    break;
                }
            }
        }
        public void AddTransaction(TransactionDto transaction)
        {
            //TODO: Add validation
        }

        public void DeleteTransaction(TransactionDto transaction)
        {
            //TODO: Add validation
        }

        public List<Transaction> GetAllTransactions()
        {
            //TODO: Add validation
        }

        public void UpdateTransaction(TransactionDto transaction)
        {
            //TODO: Add validation
        }
    }
}
