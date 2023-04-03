using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;
using System;
using System.Transactions;

namespace ContributionTracker.Models
{
    public class Transaction
    {

        public Guid TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string PayeeName { get; set; }
        public double Amount { get; set; }
        public string Memo { get; set; }
        public Transaction(Guid Id, DateTime time, string name, double amount, string memo)
        {
            TransactionId = Id;
            TransactionDate = time;
            PayeeName = name;
            Amount = amount; 
            Memo = memo;
        }
    }
}
