using ContributionTracker.Models;

namespace ContributionTracker.Interfaces
{
    public interface ITransactionRepository // necessary functionality grouped in a single interface https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design#:%7E:text=of%20Work%20patterns.-,The%20Repository%20pattern,from%20the%20domain%20model%20layer. other parts of this isn't needed in my opinion as this is a simple use case. It was all to avoid overcomplication, now it should be aligned with SOLID principles
    {
        List<Transaction> Read();
        void Write(TransactionDto transaction);
        void Update(TransactionDto transaction);
        void Delete(Guid transactionId);
    }
}
