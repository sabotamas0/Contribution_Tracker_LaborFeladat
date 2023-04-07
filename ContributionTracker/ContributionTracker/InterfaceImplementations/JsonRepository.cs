using ContributionTracker.Interfaces;
using ContributionTracker.Models;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Transactions;
using System.Xml.Linq;
using Transaction = ContributionTracker.Models.Transaction;

namespace ContributionTracker.InterfaceImplementations
{
    public class JsonRepository: ITransactionRepository
    {
        private List<Transaction> _Transactions;
        private List<Transaction> Transactions
        {
            get
            {
                if(_Transactions == null)
                {
                    return _Transactions = Read();
                }
                return _Transactions;
            }
        }

        public void Delete(Guid transactionId)
        {
            var transactionToBeDeleted=Transactions.Where(x => x.TransactionId == transactionId).Single();

            Transactions.Remove(transactionToBeDeleted);

            string json = JsonConvert.SerializeObject(Transactions.ToArray());

            System.IO.File.WriteAllText("Transactions.json", json);
        }

        public List<Transaction> Read()
        {
            return JsonConvert.DeserializeObject<List<Transaction>>(File.ReadAllText("Transactions.json"));
        }

        public void Update(TransactionDto transaction)
        {

            foreach (var currtransaction in Transactions)
            {
                if (currtransaction.TransactionId == transaction.TransactionId)
                {
                    if(!string.IsNullOrWhiteSpace(transaction.PayeeName))
                    {
                        currtransaction.PayeeName = transaction.PayeeName;
                    }
                    if (!string.IsNullOrWhiteSpace(transaction.Memo))
                    {
                        currtransaction.Memo = transaction.Memo;
                    }
                    if (!string.IsNullOrWhiteSpace(transaction.Amount))
                    {
                        currtransaction.Amount = Double.Parse(transaction.Amount);
                    }
                    break;
                }
            }

            string json = JsonConvert.SerializeObject(Transactions.ToArray());

            System.IO.File.WriteAllText("Transactions.json", json);
        }

        public void Write(TransactionDto transaction)
        {
            var amount = Double.Parse(transaction.Amount);

            var newTransaction = new Transaction(Guid.NewGuid(),transaction.TransactionDate,transaction.PayeeName, amount,transaction.Memo);

            Transactions.Add(newTransaction);

            string json = JsonConvert.SerializeObject(Transactions.ToArray());

            System.IO.File.WriteAllText("Transactions.json", json);
        }
    }
}
