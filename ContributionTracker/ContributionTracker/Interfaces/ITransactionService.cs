using ContributionTracker.Models;

namespace ContributionTracker.Interfaces
{
    public interface ITransactionService
    {
        List<Transaction> GetAllTransactions();
        void AddTransaction(TransactionDto transaction);
        void DeleteTransaction(Guid transactionId);
        void UpdateTransaction(TransactionDto transaction);
        List<Tuple<string, List<int>>> ContributionsByMonthForCurrentYear();
        List<int> ContributionsForYear(DateTime dateTime);
        List<Tuple<string, List<int>>> ContributionsIncreaseDecreaseByYear();
        //List<Tuple<string, List<double>>> AverageContributionsByMonthAndYear();
    }
}
