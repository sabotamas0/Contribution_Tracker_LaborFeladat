namespace ContributionTracker
{
    public class TransactionDto
    {
        public Guid TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string PayeeName { get; set; }
        public string Amount { get; set; }
        public string Memo { get; set; }
    }
}
