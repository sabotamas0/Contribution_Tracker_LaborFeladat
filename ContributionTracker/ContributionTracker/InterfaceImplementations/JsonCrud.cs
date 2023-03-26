using ContributionTracker.Interfaces;
using ContributionTracker.Models;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace ContributionTracker.InterfaceImplementations
{
    public class JsonCrud : ITransactionCrud
    {
        private readonly string _filename;
        private List<Transaction> _transActions;
        private List<Transaction> Transactions
        {
            get
            {
                if(_transActions==null)
                {
                    return _transActions = Read();
                }
                return _transActions;
            }
        }

        public JsonCrud(string filename)
        {
            _filename = filename;
        }

        public void Delete(TransactionDto transaction)
        {
            _transActions.Clear(); //json implementation needs to be forced to re-read with each operation
            var transactionToBeDeleted=Transactions.Where(x => x.TransactionId == transaction.TransactionId).Single();

            Transactions.Remove(transactionToBeDeleted);

            string json = JsonConvert.SerializeObject(Transactions.ToArray());

            System.IO.File.WriteAllText(_filename, json);
        }

        public List<Transaction> Read()
        {
            return JsonConvert.DeserializeObject<List<Transaction>>(File.ReadAllText(_filename));
        }

        public void Update(TransactionDto transaction)
        {
            _transActions.Clear(); //json implementation needs to be forced to re-read with each operation

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

            System.IO.File.WriteAllText(_filename, json);
        }

        public void Write(TransactionDto transaction)
        {
            var amount = Double.Parse(transaction.Amount);

            var newTransaction = new Transaction(Guid.NewGuid(),transaction.TransactionDate,transaction.PayeeName, amount,transaction.Memo);

            Transactions.Add(newTransaction);

            string json = JsonConvert.SerializeObject(Transactions.ToArray());

            System.IO.File.WriteAllText(_filename, json);
        }
    }
}
