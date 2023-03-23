namespace ContributionTracker.Models
{
    public class Transaction
    {
        public DateTime TransactionDate { get; set; }
        public string PayeeName { get; set; }
        public double Amount { get; set; }
        public string Memo { get; set; }
    }
}
